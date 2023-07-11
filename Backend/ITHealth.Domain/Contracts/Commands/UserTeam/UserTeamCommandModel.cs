namespace ITHealth.Domain.Contracts.Commands.UserTeam
{
    public class UserTeamCommandModel : BaseUserTeamCommandModel
    {
        public string UserEmail { get; set; } = null!;
    }
}
