using AutoMapper;
using FluentValidation.Results;
using ITHealth.Data;
using ITHealth.Data.Entities;
using ITHealth.Domain.Contracts.Commands.WorkingHours;
using ITHealth.Domain.Contracts.Interfaces;
using ITHealth.Domain.Resources.Validator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Services;

public class HoursService : BaseApplicationService, IHoursService
{
    private readonly IClockifyService _clockifyService;
    private readonly IJiraService _jiraService;

    public HoursService(
        AppDbContext appDbContext,
        UserManager<User> userManager,
        IServiceProvider serviceProvider,
        IMapper mapper, IClockifyService clockifyService, IJiraService jiraService)
        : base(userManager, appDbContext, serviceProvider, mapper)
    {
        _clockifyService = clockifyService;
        _jiraService = jiraService;
    }

    public async Task<double?> GetLoggedHoursOnAWeekAsync(string email)
    {
        double hours = 0;
        var statistics = await GetEfficiencyStatisticsAsync(new GetWorkingTimeStatisticsCommandModel()
        {
            Email = email,
        });

        hours += statistics != null ? statistics.Data.Hours.Sum(x => x.Hours) : 0;

        return hours;
    }

    public async Task<ListTeamWorkingTimeCommandModelResult?> GetTeamEfficiencyStatisticsAsync(
        GetTeamWorkingTimeStatisticsCommandModel command)
    {
        var usersInTeam = _appDbContext.Teams
            .Include(x => x.Users)
            .First(x => x.Id == command.TeamId)
            .Users;

        var clockifySecrets = _appDbContext.ClockifyWorkspaceSecrets
            .FirstOrDefault(x => x.TeamId == command.TeamId);

        var currentMonth = DateTime.UtcNow.Month;
        if (clockifySecrets != null)
        {
            var now = DateTime.UtcNow;
            var workingTimeClockify = new ListTeamWorkingTimeCommandModel();
            for (int month = 1; month <= currentMonth; month++)
            {
                long totalTicks = 0;
                foreach (var user in usersInTeam)
                {
                    var workTimePerDay = await _clockifyService.CalculateUserTimeEntriesTicksByMonthAsync(month, command.TeamId, user.Email);
                    totalTicks += workTimePerDay;
                }

                workingTimeClockify.Hours.Add(new TeamWorkingTimeCommandModel()
                {
                    Month = month,
                    Hours = TimeSpan.FromTicks(totalTicks).TotalHours
                });
            }

            workingTimeClockify.StartDate = new DateTime(DateTime.Today.Year, 1, 1);
            workingTimeClockify.EndDate = new DateTime(DateTime.Today.Year, currentMonth, DateTime.Today.Day);

            return new ListTeamWorkingTimeCommandModelResult(workingTimeClockify, null);
        }

        var jiraSecrets = _appDbContext.JiraWorkspaceSecrets
            .FirstOrDefault(x => x.TeamId == command.TeamId);
        if (jiraSecrets != null)
        {
            var workingTimeJira = new ListTeamWorkingTimeCommandModel();

            for (int i = 1; i <= currentMonth; i++)
            {
                long seconds = 0;
                foreach (var user in usersInTeam)
                {
                    var tasksPerDay = await _jiraService.OpenedUserTaskAsync(user.Email, i);

                    seconds += tasksPerDay.Data.JiraIssues.Sum(x => x.TimeTracking.TimeSpentSeconds);
                }

                workingTimeJira.Hours.Add(new TeamWorkingTimeCommandModel
                {
                    Month = i,
                    Hours = TimeSpan.FromSeconds(seconds).TotalHours
                });
                workingTimeJira.StartDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
                workingTimeJira.EndDate = new DateTime(DateTime.Today.Year, currentMonth, DateTime.Today.Day);
            }

            return new ListTeamWorkingTimeCommandModelResult(workingTimeJira, null);
        }

        return new ListTeamWorkingTimeCommandModelResult(new ListTeamWorkingTimeCommandModel(),
            new ValidationResult
            {
                Errors = new List<ValidationFailure>
                {
                    new()
                    {
                        ErrorMessage = HealthCommandResource.CanNotGetWorkingTime,
                        PropertyName = "workTime"
                    }
                }
            }
        );
    }

    public async Task<ListWorkingTimeCommandModelResult?> GetEfficiencyStatisticsAsync(
        GetWorkingTimeStatisticsCommandModel command)
    {
        var userTeamIds = _appDbContext.Users
            .Include(x => x.Teams)
            .First(x => x.Email == command.Email)
            .Teams
            .Select(x => x.Id)
            .ToList();

        var clockifySecrets =
            await _appDbContext.ClockifyWorkspaceSecrets.Where(x => userTeamIds.Contains(x.TeamId))
                .ToListAsync();
        if (clockifySecrets.Any())
        {
            var workingTimeClockify = await GetClockifyTimeEntriesAsync(command.Email, userTeamIds);
            workingTimeClockify.StartDate = GetStartDateOfWeek();
            workingTimeClockify.EndDate = GetStartDateOfWeek().AddDays(5);
            return new ListWorkingTimeCommandModelResult(workingTimeClockify, null);
        }

        var jiraSecrets = await _jiraService.GetSecretsAsync(command.Email);
        if (jiraSecrets.Any())
        {
            var workingTimeJira = await GetJiraWorkLogAsync(command.Email);
            workingTimeJira.StartDate = GetStartDateOfWeek();
            workingTimeJira.EndDate = GetStartDateOfWeek().AddDays(5);
            return new ListWorkingTimeCommandModelResult(workingTimeJira, null);
        }

        return new ListWorkingTimeCommandModelResult(new ListWorkingTimeCommandModel(),
            new ValidationResult
            {
                Errors = new List<ValidationFailure>
                {
                    new()
                    {
                        ErrorMessage = "Can't find secrets for jira or clockify",
                        PropertyName = "workTime"
                    }
                }
            }
        );
    }

    private async Task<ListWorkingTimeCommandModel> GetJiraWorkLogAsync(string email)
    {
        var workingTimeClockify = new ListWorkingTimeCommandModel();

        foreach (var date in GetCurrentWorkWeekDates())
        {
            var tasksPerDay =
                await _jiraService.OpenedUserTaskAsync(email, date);

            var seconds = tasksPerDay.Data.JiraIssues.Sum(x => x.TimeTracking.TimeSpentSeconds);
            workingTimeClockify.Hours.Add(new WorkingTimeCommandModel
            {
                Date = date,
                Hours = TimeSpan.FromSeconds(seconds).TotalHours
            });
        }

        return workingTimeClockify;
    }

    private async Task<ListWorkingTimeCommandModel> GetClockifyTimeEntriesAsync(string email, List<int> userTeamIds)
    {
        var workingTimeClockify = new ListWorkingTimeCommandModel();

        foreach (var date in GetCurrentWorkWeekDates())
        {
            long totalHoursTicks = 0;
            foreach (var teamId in userTeamIds)
            {
                var workTimePerDay =
                    await _clockifyService.CalculateUserTimeEntriesTicksByDayAsync(date, teamId, email);

                totalHoursTicks += workTimePerDay;
            }

            workingTimeClockify.Hours.Add(new WorkingTimeCommandModel()
            {
                Date = date,
                Hours = TimeSpan.FromTicks(totalHoursTicks).TotalHours
            });
        }

        return workingTimeClockify;
    }
}