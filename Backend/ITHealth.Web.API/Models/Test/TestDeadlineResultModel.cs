using FluentValidation.Results;

namespace ITHealth.Web.API.Models.Test
{
    public class TestDeadlineResultModel : BaseOperationResultModel<TestDeadlineResponseModel>
    {
        public TestDeadlineResultModel(TestDeadlineResponseModel data, ValidationResult validationResult) : base(data, validationResult)
        {
        }
    }

    public class TestDeadlineResponseModel
    {
        public bool IsSuccessful { get; set; }
    }
}
