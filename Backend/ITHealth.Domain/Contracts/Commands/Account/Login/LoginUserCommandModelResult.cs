using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.Account.Login
{
    public class LoginUserCommandModelResult : BaseCommandModelResult<LoginUserResponseCommandModel>
    {
        public LoginUserCommandModelResult(LoginUserResponseCommandModel data = null, ValidationResult validationResult = null) : base(data, validationResult) 
        {
        }
    }

    public class LoginUserResponseCommandModel
    {
        public string? Token { get; set; }

        public string? Role { get; set; }
    }
}
