namespace ITHealth.Domain.Services.Clockify.Models
{
    public class ClockifyTimeEntry
    {
        public string ProjectId { get; set; } = null!;
        
        public ClockifyTimeInterval TimeInterval { get; set; } = new ClockifyTimeInterval();
    }
}
