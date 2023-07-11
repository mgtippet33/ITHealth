using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.WorkingHours;

public class ListWorkingTimeCommandModelResult : BaseCommandModelResult<ListWorkingTimeCommandModel>
{
    public ListWorkingTimeCommandModelResult(ListWorkingTimeCommandModel data, ValidationResult validationResult) : base(data, validationResult)
    {
    }
}

public class ListWorkingTimeCommandModel
{
    public DateTime StartDate { get; set; } 

    public DateTime EndDate { get; set; }

    public List<WorkingTimeCommandModel> Hours { get; set; } = new List<WorkingTimeCommandModel>();
}

public class WorkingTimeCommandModel
{
    public DateTime Date { get; set; }

    public double Hours { get; set; }
}