namespace ITHealth.Domain.Contracts.Commands.Account.ResetPassword
{
    public class ResetPasswordCommandModel : BaseCommandModel
    {
        public string ResetToken { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
