using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.Clockify.Key
{
    public class SetClockifySecretsCommandModelResult : BaseCommandModelResult<SetClockifySecretsCommandModel>
    {
        public SetClockifySecretsCommandModelResult(SetClockifySecretsCommandModel data, ValidationResult validationResult) : base(data, validationResult) 
        {
        }
    }
}
