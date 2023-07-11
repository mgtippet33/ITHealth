using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.Test;

public class TestListCommandModelResult : BaseCommandModelResult<TestPaginationResponseCommandModel<TestCommandModel>>
{
    public TestListCommandModelResult(TestPaginationResponseCommandModel<TestCommandModel> data = null, ValidationResult validationResult = null) : base(data, validationResult) 
    {
    }
}