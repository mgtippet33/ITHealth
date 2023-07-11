using ITHealth.Domain.Contracts.Commands.Health;

namespace ITHealth.Domain.Contracts.Interfaces
{
    public interface IHealthService
    {
        Task<ListStressLevelCommandModelResult> GetUserStressLevelsAsync(GetUserStressLevelsCommandModel command);

        Task<HealthCommandModelResult> CreateHealthRecordAsync(CreateHealthCommandModel command);

        Task<double> GetAvarageSleepTimeByDateTimeRange(int userId, DateTime start, DateTime end);

        Task<BurnoutCommandModelResult> GetBurnoutInformationAsync(string email);
    }
}
