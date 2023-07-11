using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.Account.GenerateResetPasswordToken
{
    public class GenerateResetPasswordTokenCommandModelResult : BaseCommandModelResult<GenerateResetPasswordTokenCommandResponseModel>
    {
        public GenerateResetPasswordTokenCommandModelResult(
            GenerateResetPasswordTokenCommandResponseModel data,
            ValidationResult validationResult) : base(data, validationResult)
        {
        }
    }
}
