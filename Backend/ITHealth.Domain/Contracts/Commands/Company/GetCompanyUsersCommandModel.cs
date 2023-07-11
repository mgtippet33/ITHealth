namespace ITHealth.Domain.Contracts.Commands.Company
{
    public class GetCompanyUsersCommandModel : BaseCommandModel
    {
        public string CurrentUserEmail { get; set; } = string.Empty;

        public int ExcludingTeamId { get; set; }
    }
}
