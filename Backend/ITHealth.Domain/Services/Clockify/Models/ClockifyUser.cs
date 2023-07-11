namespace ITHealth.Domain.Services.Clockify.Models
{
    public class ClockifyUser
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;
    }

    public class ClockifyMembership
    {
        public string UserId { get; set; } = null!;
    }
}
