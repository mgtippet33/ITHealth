namespace ITHealth.Web.API.Models.Clockify.Secrets
{
    public class ClockifySetSecretsRequestModel
    {
        public string Token { get; set; } = null!;

        public string WorkspaceName { get; set; } = null!;

        public string ProjectName { get; set; } = null!;

        public int TeamId { get; set; }
    }
}
