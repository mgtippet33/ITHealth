namespace ITHealth.Domain.Services.Clockify.Models
{
    public class ClockifyProject
    {
        public string Id { get; set; } = null!;

        public string WorkspaceId { get; set; } = null!;

        public string Name { get; set; } = null!;

        public List<ClockifyMembership> Memberships { get; set; } = new List<ClockifyMembership>();
    }
}
