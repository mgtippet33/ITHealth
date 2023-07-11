using FluentValidation.Results;

namespace ITHealth.Web.API.Models.Test
{
    public class TestPassedUserResultModel : BaseOperationResultModel<List<TestPassedUserResponseModel>>
    {
        public TestPassedUserResultModel(List<TestPassedUserResponseModel> data, ValidationResult validationResult) : base(data, validationResult)
        {
        }
    }

    public class TestPassedUserResponseModel
    {
        public int UserId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public DateTime PassingDate { get; set; }
    }
}
