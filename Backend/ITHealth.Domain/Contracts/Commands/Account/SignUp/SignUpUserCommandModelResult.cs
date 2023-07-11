using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.Account.SignUp
{
    public class SignUpUserCommandModelResult : BaseCommandModelResult<SignUpUserCommandModel>
    {
        public SignUpUserCommandModelResult(ValidationResult validationResult = null) : base(null, validationResult) 
        { 
        }
    }
}
