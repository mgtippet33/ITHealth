namespace ITHealth.Domain.Contracts.Commands.Team
{
    public class GetCompanyTeamsCommandModel : BaseCommandModel
    {
        public string CurrentUserEmail { get; set; } = null!;
    }
}
