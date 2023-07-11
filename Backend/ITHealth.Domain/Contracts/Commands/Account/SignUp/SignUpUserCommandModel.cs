using ITHealth.Data.Enums;

namespace ITHealth.Domain.Contracts.Commands.Account.SignUp
{
    public class SignUpUserCommandModel : UserCommandModel
    {
        public string Password { get; set; } = null!;
    }
}
