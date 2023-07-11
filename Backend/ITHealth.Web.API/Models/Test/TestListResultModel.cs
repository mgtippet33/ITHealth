using FluentValidation.Results;

namespace ITHealth.Web.API.Models.Test;

public class TestListResultModel : BaseOperationResultModel<TestPaginationResponseModel<TestResponseModel>>
{
    public TestListResultModel(TestPaginationResponseModel<TestResponseModel> data, ValidationResult validationResult) : base(data, validationResult) 
    {
    }
}