using FluentValidation.Results;

namespace ITHealth.Web.API.Models.Test
{
    public class TestPassedListResultModel : BaseOperationResultModel<List<TestPassedInfoResponseModel>>
    {
        public TestPassedListResultModel(List<TestPassedInfoResponseModel> data, ValidationResult validationResult) : base(data, validationResult)
        {
        }
    }

    public class TestPassedInfoResponseModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
