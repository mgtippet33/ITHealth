namespace ITHealth.Domain.Contracts.Commands.Account.Profile
{
    public class GetUserProfileCommandModel : BaseCommandModel
    {
        public string CurrentUserEmail { get; set; } = string.Empty;

        public int UserId { get; set; }
    }
}
