namespace ITHealth.Domain.Contracts.Commands.Clockify.Tracker
{
    public class ClockifyTimeEntriesCommandModel : BaseCommandModel
    {
        public TimeSpan TimeWorked { get; set; }
    }
}
