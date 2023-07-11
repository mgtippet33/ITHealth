namespace ITHealth.Domain.Contracts.Commands.Account.Login
{
    public class LoginUserCommandModel : BaseCommandModel
    {
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
