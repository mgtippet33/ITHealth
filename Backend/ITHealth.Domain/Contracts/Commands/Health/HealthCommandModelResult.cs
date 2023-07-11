using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.Health
{
    public class HealthCommandModelResult : BaseCommandModelResult<HealthCommandModel>
    {
        public HealthCommandModelResult(HealthCommandModel data = null, ValidationResult validationResult = null) : base(data, validationResult) 
        {
        }
    }
}
