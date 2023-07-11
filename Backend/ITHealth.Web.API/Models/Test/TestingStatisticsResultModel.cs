using FluentValidation.Results;

namespace ITHealth.Web.API.Models.Test
{
    public class TestingStatisticsResultModel : BaseOperationResultModel<List<UserTestingStatisticResponseModel>>
    {
        public TestingStatisticsResultModel(List<UserTestingStatisticResponseModel> data, ValidationResult validationResult) : base(data, validationResult)
        {
        }
    }

    public class UserTestingStatisticResponseModel : TestPassedUserResponseModel
    {
        public int AssessmentPercent { get; set; }
    }
}
