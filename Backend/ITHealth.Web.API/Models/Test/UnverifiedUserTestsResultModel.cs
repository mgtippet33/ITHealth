using FluentValidation.Results;

namespace ITHealth.Web.API.Models.Test
{
    public class UnverifiedUserTestsResultModel : BaseOperationResultModel<TestPaginationResponseModel<UnverifiedUserTestResponseModel>>
    {
        public UnverifiedUserTestsResultModel(TestPaginationResponseModel<UnverifiedUserTestResponseModel> data, ValidationResult validationResult) : base(data, validationResult)
        {
        }
    }

    public class UnverifiedUserTestResponseModel
    {
        public int TestId { get; set; }

        public string TestName { get; set; } = string.Empty;

        public int UserId { get; set; }

        public string UserEmail { get; set; } = string.Empty;

        public bool IsNeededAssessment { get; set; }
    }
}
