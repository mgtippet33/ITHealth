using FluentValidation.Results;

namespace ITHealth.Web.API.Models.Test;

public class TestResultsResultModel : BaseOperationResultModel<TestResultResponseModel>
{
    public TestResultsResultModel(TestResultResponseModel data, ValidationResult validationResult) : base(data, validationResult) 
    {
    }
}

public class TestResultResponseModel
{
    public int TestId { get; set; }
    public int Result { get; set; }
    public int MaxPoints { get; set; }
}