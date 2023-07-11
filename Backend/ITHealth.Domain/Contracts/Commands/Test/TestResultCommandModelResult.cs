using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.Test;

public class TestResultCommandModelResult : BaseCommandModelResult<TestResultCommandModel>
{
    public TestResultCommandModelResult(TestResultCommandModel data = null, ValidationResult validationResult = null) : base(data, validationResult) 
    {
    }
}

public class TestResultCommandModel
{
    public int UserId { get; set; }
    public int TestId { get; set; }
    public int Result { get; set; }
    public int MaxPoints { get; set; }
}