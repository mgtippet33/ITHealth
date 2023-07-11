using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.Test
{
    public class TestPassedListCommandModelResult : BaseCommandModelResult<List<TestCommandModel>>
    {
        public TestPassedListCommandModelResult(List<TestCommandModel> data = null, ValidationResult validationResult = null) : base(data, validationResult)
        {
        }
    }
}
