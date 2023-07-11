using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.Account.InviteUser
{
    public class InviteUserCommandModelResult : BaseCommandModelResult<InviteUserResponseCommandModel>
    {
        public InviteUserCommandModelResult(InviteUserResponseCommandModel data = null, ValidationResult validationResult = null) : base(data, validationResult)
        {
        }
    }

    public class InviteUserCommandModel : BaseCommandModel
    {
        public string CurrentUserEmail { get; set; } = null!;

        public string InvitedUserEmail { get; set; } = null!;
    }
}
