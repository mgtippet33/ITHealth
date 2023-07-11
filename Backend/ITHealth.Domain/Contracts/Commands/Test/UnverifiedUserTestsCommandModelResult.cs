using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.Test
{
    public class UnverifiedUserTestsCommandModelResult : BaseCommandModelResult<TestPaginationResponseCommandModel<UnverifiedUserTestInfoCommandModel>>
    {
        public UnverifiedUserTestsCommandModelResult(TestPaginationResponseCommandModel<UnverifiedUserTestInfoCommandModel> data = null, ValidationResult validationResult = null) : base(data, validationResult)
        {
        }
    }

    public class UnverifiedUserTestInfoCommandModel
    {
        public int TestId { get; set; }

        public string TestName { get; set; } = string.Empty;

        public int UserId { get; set; }

        public string UserEmail { get; set; } = string.Empty;

        public bool IsNeededAssessment { get; set; }
    }
}
