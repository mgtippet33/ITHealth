using FluentValidation.Results;

namespace ITHealth.Web.API.Models.Health
{
    public class ListStressLevelResultModel : BaseOperationResultModel<ListStressLevelResponseModel>
    {
        public ListStressLevelResultModel(ListStressLevelResponseModel data, ValidationResult validationResult) : base(data, validationResult)
        {
        }
    }

    public class ListStressLevelResponseModel
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<StressLevelResponseModel> StressLevels { get; set; } = new();
    }

    public class StressLevelResponseModel
    {
        public DateTime Date { get; set; }

        public double StressLevel { get; set; }
    }
}
