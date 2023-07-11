using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.Test;

public class TestCommandModelResult : BaseCommandModelResult<TestCommandModel>
{
    public TestCommandModelResult(TestCommandModel data = null, ValidationResult validationResult = null) : base(data, validationResult) 
    {
    }
}