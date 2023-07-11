using FluentValidation.Results;

namespace ITHealth.Web.API.Models.WorkTime;

public class GetWorkTimeStatisticsResultModel : BaseOperationResultModel<GetWorkTimeStatisticsResponseModel>
{
    public GetWorkTimeStatisticsResultModel(GetWorkTimeStatisticsResponseModel data = null,
        ValidationResult validationResult = null) : base(data, validationResult)
    {
    }
}

public class GetWorkTimeStatisticsResponseModel
{
    public DateTime StartDate { get; set; } 

    public DateTime EndDate { get; set; }

    public List<WorkingTimeResponse> Hours { get; set; } = new List<WorkingTimeResponse>();
}

public class WorkingTimeResponse
{
    public DateTime Date { get; set; }

    public double Hours { get; set; }
}