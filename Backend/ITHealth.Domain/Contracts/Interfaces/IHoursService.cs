using ITHealth.Domain.Contracts.Commands.WorkingHours;

namespace ITHealth.Domain.Contracts.Interfaces;

public interface IHoursService
{
    Task<ListWorkingTimeCommandModelResult?> GetEfficiencyStatisticsAsync(GetWorkingTimeStatisticsCommandModel command);
    
    Task<ListTeamWorkingTimeCommandModelResult?> GetTeamEfficiencyStatisticsAsync(GetTeamWorkingTimeStatisticsCommandModel command);
    
    Task<double?> GetLoggedHoursOnAWeekAsync(string email);
}