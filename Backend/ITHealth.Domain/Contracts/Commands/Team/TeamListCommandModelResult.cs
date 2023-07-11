using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.Team
{
    public class TeamListCommandModelResult : BaseCommandModelResult<List<TeamCommandModel>>
    {
        public TeamListCommandModelResult(List<TeamCommandModel> data = null, ValidationResult validationResult = null) : base(data, validationResult) 
        {
        }
    }
}
