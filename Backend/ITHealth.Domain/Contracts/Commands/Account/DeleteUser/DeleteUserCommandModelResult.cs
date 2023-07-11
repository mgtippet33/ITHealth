using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.Account.DeleteUser
{
    public class DeleteUserCommandModelResult : BaseCommandModelResult<DeleteUserResponseCommandModel>
    {
        public DeleteUserCommandModelResult(DeleteUserResponseCommandModel data, ValidationResult validationResult) : base(data, validationResult)
        {
        }
    }

    public class DeleteUserResponseCommandModel
    {
        public bool IsSuccessful { get; set; }
    }
}
