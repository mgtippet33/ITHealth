using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.Account.ResetPassword
{
    public class ResetPasswordCommandModelResult : BaseCommandModelResult<ResetPasswordCommandResponseModel>
    {
        public ResetPasswordCommandModelResult(ResetPasswordCommandResponseModel data = null, ValidationResult validationResult = null) : base(data, validationResult)
        {
        }
    }
}
