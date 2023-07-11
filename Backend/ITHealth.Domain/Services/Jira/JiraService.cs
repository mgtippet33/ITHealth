using System.Globalization;
using AutoMapper;
using FluentValidation.Results;
using ITHealth.Data;
using ITHealth.Data.Entities;
using ITHealth.Domain.Contracts.Commands.Jira;
using ITHealth.Domain.Contracts.Interfaces;
using ITHealth.Domain.Http.Jira;
using ITHealth.Domain.Services.Jira.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using JiraIssueResponse = ITHealth.Domain.Contracts.Commands.Jira.JiraIssueResponse;

namespace ITHealth.Domain.Services.Jira;

public class JiraService : BaseApplicationService, IJiraService
{
    private readonly IJiraHttpClient _jiraHttpClient;

    public JiraService(IJiraHttpClient jiraHttpClient, AppDbContext appDbContext, IMapper mapper,
        UserManager<User> userManager, IServiceProvider serviceProvider) : base(userManager, appDbContext,
        serviceProvider, mapper)
    {
        _jiraHttpClient = jiraHttpClient;
    }

    public async Task<JiraCurrentUserTasksInProgressResultCommandModel> OpenedUserTaskAsync(string email, DateTime? date = null)
    {
        var jiraIssues = new List<JiraIssueResponse>();

        foreach (var secrets in await GetSecretsAsync(email))
        {
            var boards = (await _jiraHttpClient.GetCurrentBoardAsync(email, secrets.Token, secrets.Url)).Values;

            foreach (var board in boards)
            {
                var issues = (await _jiraHttpClient.GetCurrentUserIssuesByBoardAsync(board.Id, email, secrets.Token, secrets.Url)).Issues;

                foreach (var issue in issues)
                {
                    issue.Fields.UpdatedDate = DateTime.Parse(issue.Fields.Updated, null, DateTimeStyles.RoundtripKind);

                    if (date != null)
                    {
                        if (issue.Fields.UpdatedDate.Date == date)
                        {
                            jiraIssues.Add(new JiraIssueResponse()
                            {
                                Created = issue.Fields.UpdatedDate,
                                Self = issue.Self,
                                Description = issue.Fields.Description,
                                TimeTracking = issue.Fields.Timetracking,
                                Board = new JiraBoard()
                                {
                                    Self = board.Self,
                                    Id = board.Id,
                                    Name = board.Name
                                }
                            });
                        }

                        continue;
                    }
                    
                    if (issue.Fields.UpdatedDate.Month == DateTime.Now.Month &&
                        issue.Fields.UpdatedDate.Year == DateTime.Now.Year &&
                        issue.Fields.Assignee?.EmailAddress == email)
                    {
                        jiraIssues.Add(new JiraIssueResponse()
                        {
                            Created = issue.Fields.UpdatedDate,
                            Self = issue.Self,
                            Description = issue.Fields.Description,
                            TimeTracking = issue.Fields.Timetracking,
                            Board = new JiraBoard()
                            {
                                Self = board.Self,
                                Id = board.Id,
                                Name = board.Name
                            }
                        });
                    }
                }
            }
        }
        

        return new JiraCurrentUserTasksInProgressResultCommandModel(
            new JiraCurrentUserTasksInProgressCommandModel()
            {
                JiraIssues = jiraIssues
            }, new ValidationResult());
    }
    
    public async Task<JiraCurrentUserTasksInProgressResultCommandModel> OpenedUserTaskAsync(string email, int month)
    {
        var jiraIssues = new List<JiraIssueResponse>();

        foreach (var secrets in await GetSecretsAsync(email))
        {
            var boards = (await _jiraHttpClient.GetCurrentBoardAsync(email, secrets.Token, secrets.Url)).Values;

            foreach (var board in boards)
            {
                var issues = (await _jiraHttpClient.GetCurrentUserIssuesByBoardAsync(board.Id, email, secrets.Token, secrets.Url)).Issues;

                foreach (var issue in issues)
                {
                    issue.Fields.UpdatedDate = DateTime.Parse(issue.Fields.Updated, null, DateTimeStyles.RoundtripKind);

                    if (issue.Fields.UpdatedDate.Month == month &&
                        issue.Fields.UpdatedDate.Year == DateTime.Now.Year &&
                        issue.Fields.Assignee?.EmailAddress == email)
                    {
                        jiraIssues.Add(new JiraIssueResponse()
                        {
                            Created = issue.Fields.UpdatedDate,
                            Self = issue.Self,
                            TimeTracking = issue.Fields.Timetracking,
                            Board = new JiraBoard()
                            {
                                Self = board.Self,
                                Id = board.Id,
                                Name = board.Name
                            }
                        });
                    }
                }
            }
        }
        

        return new JiraCurrentUserTasksInProgressResultCommandModel(
            new JiraCurrentUserTasksInProgressCommandModel()
            {
                JiraIssues = jiraIssues
            }, new ValidationResult());
    }
    
    public async Task<int> UserTimeInSecondsAsync(string email)
    {
        var userTasks = await OpenedUserTaskAsync(email);

        return userTasks.Data.JiraIssues.Select(x => x.TimeTracking).Where(x => x != null).Sum(x => x.TimeSpentSeconds);
    }

    public async Task<SetJiraSecretsCommandModelResult> SetJiraSecretsAsync(SetJiraSecretsCommandModel command)
    {
        var validationResult = await ValidateCommandAsync(command);
        if (validationResult != null && validationResult.IsValid)
        {
            var jiraSecrets = _mapper.Map<JiraWorkspaceSecrets>(command);
            jiraSecrets.Team = null;
            jiraSecrets.Url += "/rest/agile/1.0/";
            
            var doesEntityExist = await _appDbContext.JiraWorkspaceSecrets.AnyAsync(x => x.TeamId == jiraSecrets.TeamId);
            if (doesEntityExist)
            {
                _appDbContext.Update(jiraSecrets);
            }
            else
            {
                await _appDbContext.JiraWorkspaceSecrets.AddAsync(jiraSecrets);
            }
            await _appDbContext.SaveChangesAsync();
        }

        return new SetJiraSecretsCommandModelResult(command, validationResult);
    }

    public async Task<ListEfficiencyCommandModelResult> GetEfficiencyStatisticsAsync(GetUserEfficiencyCommandModel command)
    {
        var validationResult = await ValidateCommandAsync(command);
        var responseCommand = _mapper.Map<ListEfficiencyCommandModel>(command);

        if (validationResult != null && validationResult.IsValid)
        {
            var user = await _appDbContext.Users.FirstAsync(u => u.Id == command.UserId);

            for (var date = command.StartDate; date <= command.EndDate; date = date.AddDays(1))
            {
                var efficiency = await CalculateEfficiencyAsync(user.Email, date);

                responseCommand.Efficiencies.Add(new EfficiencyCommandModel
                {
                    Date = date,
                    Efficiency = efficiency,
                });
            }
        }

        return new ListEfficiencyCommandModelResult(responseCommand, validationResult);
    }

    public async Task<bool> HasUserLowEfficiencyAsync(string email)
    {
        const double MinimalNormalEfficiencyPercent = 80.0;
        const double MaximumNormalEfficiencyPercent = 120.0;

        var datesOfWeek = GetCurrentWorkWeekDates();
        var efficiencyPercentList = new List<double>();

        foreach (var date in datesOfWeek)
        {
            var efficiency = await CalculateEfficiencyAsync(email, date);
            efficiencyPercentList.Add(efficiency);
        }

        var avarageEfficiency = efficiencyPercentList.Average();

        return avarageEfficiency >= MinimalNormalEfficiencyPercent && avarageEfficiency <= MaximumNormalEfficiencyPercent;
    }

    public async Task<List<JiraWorkspaceSecrets>> GetSecretsAsync(string email)
    {
        var userTeams = _appDbContext.Teams.Include(x => x.Users).Where(x => x.Users.Select(x => x.Email).Contains(email)).Select(x => x.Id).ToList();
        return await _appDbContext.JiraWorkspaceSecrets.Where(x => userTeams.Contains(x.TeamId)).ToListAsync();
    }
    
    private async Task<double> CalculateEfficiencyAsync(string email, DateTime date)
    {
        var userTasks = await OpenedUserTaskAsync(email, date);

        var currentJiraIssues = userTasks.Data.JiraIssues;

        if(currentJiraIssues.Sum(x => x.TimeTracking.OriginalEstimateSeconds) == 0)
            return 0;
        
        return (double)currentJiraIssues.Sum(x => x.TimeTracking.TimeSpentSeconds) /
               currentJiraIssues.Sum(x => x.TimeTracking.OriginalEstimateSeconds) * 100;
    }
}