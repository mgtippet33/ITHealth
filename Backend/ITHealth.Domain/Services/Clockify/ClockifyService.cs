using AutoMapper;
using ITHealth.Data;
using ITHealth.Data.Entities;
using ITHealth.Domain.Contracts.Commands.Clockify.Key;
using ITHealth.Domain.Contracts.Commands.Clockify.Tracker;
using ITHealth.Domain.Contracts.Interfaces;
using ITHealth.Domain.Http.Clockify;
using ITHealth.Domain.Resources.Validator;
using ITHealth.Domain.Services.Clockify.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace ITHealth.Domain.Services.Clockify
{
    public class ClockifyService : BaseApplicationService, IClockifyService
    {
        private readonly IClockifyHttpClient _clockifyHttpClient;

        public ClockifyService(IClockifyHttpClient clockifyHttpClient, AppDbContext appDbContext, IMapper mapper,
            UserManager<User> userManager, IServiceProvider serviceProvider) : base(userManager, appDbContext, serviceProvider, mapper)
        {
            _clockifyHttpClient = clockifyHttpClient;
        }

        public async Task<SetClockifySecretsCommandModelResult> SetClockifySecretsAsync(SetClockifySecretsCommandModel command)
        {
            var validationResult = await ValidateCommandAsync(command);

            if (validationResult != null && validationResult.IsValid)
            {
                var clockifyWorkspace = await GetClockifyWorkspaceByNameAsync(command.Token, command.WorkspaceName);
                validationResult = ValidateNullData(clockifyWorkspace, "WorkspaceName_NotFound", ClockifyResource.WorkspaceName_NotFound);

                if (validationResult.IsValid) 
                {
                    var clockifyProject = await GetClockifyProjectByNameAsync(command.Token, clockifyWorkspace.Id, command.ProjectName);
                    validationResult = ValidateNullData(clockifyProject, "ProjectName_NotFound", ClockifyResource.ProjectName_NotFound);

                    if (validationResult.IsValid)
                    {
                        var clockifyWorkspaceSecrets =
                            await _appDbContext.ClockifyWorkspaceSecrets.FirstOrDefaultAsync(x => x.TeamId == command.TeamId)
                            ?? _mapper.Map<ClockifyWorkspaceSecrets>(command);
                        clockifyWorkspaceSecrets.WorkspaceId = clockifyWorkspace.Id;
                        clockifyWorkspaceSecrets.ProjectId = clockifyProject.Id;
                        clockifyWorkspaceSecrets.Team = await _appDbContext.Teams.FirstAsync(x => x.Id == command.TeamId);

                        await SaveClockifyWorkspaceSecretsChangesAsync(clockifyWorkspaceSecrets);
                    }
                }
            }

            return new SetClockifySecretsCommandModelResult(command, validationResult);
        }

        public async Task<ClockifyTimeEntriesCommandModelResult> GetUserTimeEntriesAsync(ClockifyTrackerCommandModel command)
        {
            var timeEntriesCommand = new ClockifyTimeEntriesCommandModel();
            var validationResult = await ValidateCommandAsync(command);

            if (validationResult != null && validationResult.IsValid)
            {
                long timeWorked = 0;
                var teams = (await _appDbContext.Users
                    .Include(u => u.Teams)
                    .ThenInclude(t => t.ClockifyWorkspaceSecrets)
                    .FirstAsync(x => x.Email == command.CurrentUserEmail))
                    .Teams
                    .Where(team => team.ClockifyWorkspaceSecrets != null);

                foreach (var team in teams)
                {
                    timeWorked += await CalculateUserTimeEntriesTicksByMonthAsync(DateTime.Today.Month, team.Id, command.CurrentUserEmail);
                }

                timeEntriesCommand.TimeWorked = TimeSpan.FromTicks(timeWorked);
            }

            return new ClockifyTimeEntriesCommandModelResult(timeEntriesCommand, validationResult);
        }

        public async Task<long> CalculateUserTimeEntriesTicksByMonthAsync(int month, int teamId, string email)
        {
            var timeEntriesList = await GetClockifyClockifyTimeIntervalsAsync(teamId, email);

            var totalDurationTicks = timeEntriesList
                .Where(x => IsMonthInInterval(x, month))
                .Sum(x => XmlConvert.ToTimeSpan(x.Duration).Ticks);

            return totalDurationTicks;
        }
        
        public async Task<long> CalculateUserTimeEntriesTicksByDayAsync(DateTime date, int teamId, string email)
        {
            var timeEntriesList = await GetClockifyClockifyTimeIntervalsAsync(teamId, email);

            var totalDurationTicks = timeEntriesList
                .Where(x => DateTime.Parse(x.Start) <= date.Date && date.Date <= DateTime.Parse(x.End))
                .Sum(x => XmlConvert.ToTimeSpan(x.Duration).Ticks);

            return totalDurationTicks;
        }

        private bool IsMonthInInterval(ClockifyTimeInterval timeInterval, int month)
        {
            var start = DateTime.Parse(timeInterval.Start);
            var end = DateTime.Parse(timeInterval.End);

            return start.Month >= month && end.Month <= month;
        }

        private async Task<ClockifyWorkspace?> GetClockifyWorkspaceByNameAsync(string token, string name)
        {
            var workspace = (await _clockifyHttpClient.ListWorkspacesAsync(token)).FirstOrDefault(x => x.Name == name);
            return workspace;
        }

        private async Task<ClockifyProject?> GetClockifyProjectByNameAsync(string token, string workspaceId, string name)
        {
            var project = (await _clockifyHttpClient.ListProjectsOnWorkplaceAsync(token, workspaceId)).FirstOrDefault(x => x.Name == name);
            return project;
        }

        private async Task<ClockifyUser?> GetClockifyUserByEmailAsync(string token, string workspaceId, string email)
        {
            var user = (await _clockifyHttpClient.ListUsersOnWorkspaceAsync(token, workspaceId)).FirstOrDefault(x => x.Email == email);
            return user;
        }

        private async Task<List<ClockifyTimeInterval>> GetClockifyClockifyTimeIntervalsAsync(int teamId, string email)
        {
            var clockifySecrets = await _appDbContext.ClockifyWorkspaceSecrets.FirstOrDefaultAsync(x => x.TeamId == teamId);

            var timeIntervalList = new List<ClockifyTimeInterval>();
            if (clockifySecrets != null)
            {
                var clockifyUserId = (await GetClockifyUserByEmailAsync(clockifySecrets.Token, clockifySecrets.WorkspaceId, email))?.Id;

                if (clockifyUserId != null)
                {
                    var timeEntriesList = await _clockifyHttpClient.ListUserTimeEntriesAsync(clockifySecrets.Token, clockifySecrets.WorkspaceId, clockifyUserId);
                    timeIntervalList = timeEntriesList.Where(x => x.ProjectId == clockifySecrets.ProjectId).Select(x => x.TimeInterval).ToList();
                }
            }

            return timeIntervalList;
        }

        private async Task SaveClockifyWorkspaceSecretsChangesAsync(ClockifyWorkspaceSecrets clockifyWorkspaceSecrets)
        {
            var doesEntityExist = await _appDbContext.ClockifyWorkspaceSecrets.AnyAsync(x => x.TeamId == clockifyWorkspaceSecrets.TeamId);

            if (doesEntityExist)
            {
                _appDbContext.Update(clockifyWorkspaceSecrets);
            }
            else
            {
                await _appDbContext.ClockifyWorkspaceSecrets.AddAsync(clockifyWorkspaceSecrets);
            }

            await _appDbContext.SaveChangesAsync();
        }
    }
}
