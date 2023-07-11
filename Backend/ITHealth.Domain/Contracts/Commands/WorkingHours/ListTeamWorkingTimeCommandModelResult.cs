using FluentValidation.Results;
using ITHealth.Domain.Contracts.Commands;

public class ListTeamWorkingTimeCommandModelResult : BaseCommandModelResult<ListTeamWorkingTimeCommandModel>
{
    public ListTeamWorkingTimeCommandModelResult(ListTeamWorkingTimeCommandModel data, ValidationResult validationResult) : base(data, validationResult)
    {
    }
}

public class ListTeamWorkingTimeCommandModel
{
    public DateTime StartDate { get; set; } 

    public DateTime EndDate { get; set; }

    public List<TeamWorkingTimeCommandModel> Hours { get; set; } = new List<TeamWorkingTimeCommandModel>();
}

public class TeamWorkingTimeCommandModel
{
    public int Month { get; set; }

    public double Hours { get; set; }
}