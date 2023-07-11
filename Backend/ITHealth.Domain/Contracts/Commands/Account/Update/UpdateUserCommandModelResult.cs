using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.Account.Update
{
    public class UpdateUserCommandModelResult : BaseCommandModelResult<UpdateUserCommandModel>
    {
        public UpdateUserCommandModelResult(UpdateUserCommandModel data = null, ValidationResult validationResult = null) : base(data, validationResult)
        {
        }
    }
}
