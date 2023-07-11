using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.Test
{
    public class TeamTestDeadlineCommandModelResult : BaseCommandModelResult<CreateTeamTestDeadlineCommandModel>
    {
        public TeamTestDeadlineCommandModelResult(CreateTeamTestDeadlineCommandModel data = null, ValidationResult validationResult = null) : base(data, validationResult)
        {
        }
    }

    public class UsersTestDeadlineCommandModelResult : BaseCommandModelResult<CreateUsersTestDeadlineCommandModel>
    {
        public UsersTestDeadlineCommandModelResult(CreateUsersTestDeadlineCommandModel data = null, ValidationResult validationResult = null) : base(data, validationResult)
        {
        }
    }

    public class CreateUsersTestDeadlineCommandModel : TestDeadlineCommandModel
    {
        public List<int> UserIds { get; set; } = new();
    }

    public class CreateTeamTestDeadlineCommandModel : TestDeadlineCommandModel
    {
        public int TeamId { get; set; }
    }

    public class UserTestDeadlineCommandModel : TestDeadlineCommandModel
    {
        public int UserId { get; set; }
    }

    public class TestDeadlineCommandModel : BaseCommandModel
    {
        public int TestId { get; set; }
        public DateTime Deadline { get; set; }
    }
}
