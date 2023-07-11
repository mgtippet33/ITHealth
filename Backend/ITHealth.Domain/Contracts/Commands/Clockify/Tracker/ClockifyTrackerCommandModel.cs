namespace ITHealth.Domain.Contracts.Commands.Clockify.Tracker
{
    public class ClockifyTrackerCommandModel : BaseCommandModel
    {
        public string CurrentUserEmail { get; set; } = null!;
    }
}
