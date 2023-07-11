using ITHealth.Domain.Contracts.Commands.Clockify.Key;
using ITHealth.Domain.Contracts.Commands.Clockify.Tracker;

namespace ITHealth.Domain.Contracts.Interfaces
{
    public interface IClockifyService
    {
        Task<SetClockifySecretsCommandModelResult> SetClockifySecretsAsync(SetClockifySecretsCommandModel command);

        Task<ClockifyTimeEntriesCommandModelResult> GetUserTimeEntriesAsync(ClockifyTrackerCommandModel command);

        Task<long> CalculateUserTimeEntriesTicksByMonthAsync(int month, int teamId, string email);

        Task<long> CalculateUserTimeEntriesTicksByDayAsync(DateTime date, int teamId, string email);
    }
}
