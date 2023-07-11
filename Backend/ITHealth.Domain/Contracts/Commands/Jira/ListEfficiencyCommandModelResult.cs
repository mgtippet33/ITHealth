using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.Jira;

public class ListEfficiencyCommandModelResult : BaseCommandModelResult<ListEfficiencyCommandModel>
{
    public ListEfficiencyCommandModelResult(ListEfficiencyCommandModel data, ValidationResult validationResult) : base(data, validationResult)
    {
    }
}

public class ListEfficiencyCommandModel
{
    public DateTime StartDate { get; set; } 

    public DateTime EndDate { get; set; }

    public List<EfficiencyCommandModel> Efficiencies { get; set; } = new List<EfficiencyCommandModel>();
}


public class EfficiencyCommandModel
{
    public DateTime Date { get; set; }

    public double Efficiency { get; set; }
}