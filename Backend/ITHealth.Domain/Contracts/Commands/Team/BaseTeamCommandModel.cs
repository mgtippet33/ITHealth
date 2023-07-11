namespace ITHealth.Domain.Contracts.Commands.Team
{
    public class BaseTeamCommandModel : BaseCommandModel
    {
        public int Id { get; set; }

        public string? CurrentUserEmail { get; set; }
    }
}
