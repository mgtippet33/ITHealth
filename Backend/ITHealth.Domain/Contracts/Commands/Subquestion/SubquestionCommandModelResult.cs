using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.Subquestion
{
    public class SubquestionCommandModelResult : BaseCommandModelResult<SubquestionCommandModel>
    {
        public SubquestionCommandModelResult(SubquestionCommandModel data = null,
            ValidationResult validationResult = null) : base(data, validationResult)
        {
        }
    }
}