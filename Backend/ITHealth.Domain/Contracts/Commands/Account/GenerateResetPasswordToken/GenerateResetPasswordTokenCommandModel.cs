namespace ITHealth.Domain.Contracts.Commands.Account.GenerateResetPasswordToken
{
    public class GenerateResetPasswordTokenCommandModel : BaseCommandModel
    {
        public string Email { get; set; } = null!;
    }
}
