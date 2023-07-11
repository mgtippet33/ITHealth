namespace ITHealth.Domain.Contracts.Commands.UserTeam
{
    public class BaseUserTeamCommandModel : BaseCommandModel
    {
        public string? CurrentUserEmail { get; set; }

        public int TeamId { get; set; }
    }
}
