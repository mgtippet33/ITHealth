using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.Team
{
    public class TeamCommandModelResult : BaseCommandModelResult<TeamCommandModel>
    {
        public TeamCommandModelResult(TeamCommandModel data = null, ValidationResult validationResult = null) : base(data, validationResult) 
        {
        }
    }
}
