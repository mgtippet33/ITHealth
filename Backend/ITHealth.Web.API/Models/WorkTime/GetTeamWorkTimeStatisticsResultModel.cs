using FluentValidation.Results;

namespace ITHealth.Web.API.Models.WorkTime;

public class GetTeamWorkTimeStatisticsResultModel : BaseOperationResultModel<GetTeamWorkTimeStatisticsResponseModel>
{
    public GetTeamWorkTimeStatisticsResultModel(GetTeamWorkTimeStatisticsResponseModel data = null,
        ValidationResult validationResult = null) : base(data, validationResult)
    {
    }
}

public class GetTeamWorkTimeStatisticsResponseModel
{
    public DateTime StartDate { get; set; } 

    public DateTime EndDate { get; set; }

    public List<TeamWorkingTimeResponse> Hours { get; set; } = new List<TeamWorkingTimeResponse>();
}

public class TeamWorkingTimeResponse
{
    public int Month { get; set; }

    public double Hours { get; set; }
}