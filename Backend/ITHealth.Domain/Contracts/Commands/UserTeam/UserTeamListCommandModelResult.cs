using FluentValidation.Results;
using ITHealth.Domain.Contracts.Commands.Account;

namespace ITHealth.Domain.Contracts.Commands.UserTeam
{
    public class UserTeamListCommandModelResult : BaseCommandModelResult<List<UserCommandModel>>
    {
        public UserTeamListCommandModelResult(List<UserCommandModel> data = null, ValidationResult validationResult = null) : base(data, validationResult) 
        {
        }
    }
}
