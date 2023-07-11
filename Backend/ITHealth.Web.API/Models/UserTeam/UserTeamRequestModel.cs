namespace ITHealth.Web.API.Models.UserTeam
{
    public class UserTeamRequestModel
    {
        public int TeamId { get; set; }

        public string UserEmail { get; set; } = null!;
    }
}
