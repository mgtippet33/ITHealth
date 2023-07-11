namespace ITHealth.Domain.Services.Clockify.Models
{
    public class ClockifyTask
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string ProjectId { get; set; } = null!;

        public List<string> AssigneeIds { get; set; } = new List<string>();
    }
}
