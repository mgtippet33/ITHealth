using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.Test
{
    public class UserTestListCommandModelResult : BaseCommandModelResult<TestPaginationResponseCommandModel<UserTestInfoCommandModel>>
    {
        public UserTestListCommandModelResult(TestPaginationResponseCommandModel<UserTestInfoCommandModel> data = null, ValidationResult validationResult = null) : base(data, validationResult)
        {
        }
    }

    public class UserTestInfoCommandModel : BaseTestCommandModel
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime Deadline { get; set; }
    }
}
