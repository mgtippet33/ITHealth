using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.Test
{
    public class TestPassedUsersCommandModelResult : BaseCommandModelResult<List<TestPassedUserResponseCommandModel>>
    {
        public TestPassedUsersCommandModelResult(List<TestPassedUserResponseCommandModel> data = null, ValidationResult validationResult = null) : base(data, validationResult)
        {
        }
    }

    public class TestPassedUserResponseCommandModel
    {
        public int UserId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public DateTime PassingDate { get; set; }
    }
}
