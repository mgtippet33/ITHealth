namespace ITHealth.Data.Entities
{
    public class ClockifyWorkspaceSecrets : BaseWorkspaceSecrets
    {
        public string Token { get; set; }
        public string WorkspaceId { get; set; }
        public string ProjectId { get; set; }
    }
}
