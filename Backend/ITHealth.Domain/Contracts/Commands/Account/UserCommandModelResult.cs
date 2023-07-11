using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.Account
{
    public class UserCommandModelResult : BaseCommandModelResult<UserCommandModel>
    {
        public UserCommandModelResult(UserCommandModel data, ValidationResult validationResult) : base(data, validationResult) 
        {
        }
    }
}
