using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.UserTeam
{
    public class UserTeamCommandModelResult : BaseCommandModelResult<UserTeamCommandModel>
    {
        public UserTeamCommandModelResult(UserTeamCommandModel data = null, ValidationResult validationResult = null) : base(data, validationResult) 
        {
        }
    }
}
