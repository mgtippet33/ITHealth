using FluentValidation.Results;

namespace ITHealth.Web.API.Models.Test;

public class TestResultModel : BaseOperationResultModel<TestResponseModel>
{
    public TestResultModel(TestResponseModel data, ValidationResult validationResult) : base(data, validationResult) 
    {
    }
}