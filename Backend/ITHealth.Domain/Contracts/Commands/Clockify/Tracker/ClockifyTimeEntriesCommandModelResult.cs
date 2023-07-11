using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.Clockify.Tracker
{
    public class ClockifyTimeEntriesCommandModelResult : BaseCommandModelResult<ClockifyTimeEntriesCommandModel>
    {
        public ClockifyTimeEntriesCommandModelResult(ClockifyTimeEntriesCommandModel data, ValidationResult validationResult) : base(data, validationResult)
        {
        }
    }
}
