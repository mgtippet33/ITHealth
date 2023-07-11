namespace ITHealth.Domain.Contracts.Commands.Jira;

public class JiraTimeEntriesCommandModel : BaseCommandModel
{
    public TimeSpan TimeWorked { get; set; }
}