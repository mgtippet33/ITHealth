namespace ITHealth.Domain.Contracts.Commands.Jira;

public class GetUserEfficiencyCommandModel : BaseCommandModel
{
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int UserId { get; set; }
}