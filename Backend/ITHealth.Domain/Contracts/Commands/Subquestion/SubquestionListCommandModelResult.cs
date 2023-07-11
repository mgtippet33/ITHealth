using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.Subquestion;

public class SubquestionListCommandModelResult : BaseCommandModelResult<List<SubquestionCommandModel>>
{
    public SubquestionListCommandModelResult(List<SubquestionCommandModel> data = null,
        ValidationResult validationResult = null) : base(data, validationResult)
    {
    }
}