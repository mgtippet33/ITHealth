using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.Question;

public class QuestionListCommandModelResult : BaseCommandModelResult<List<QuestionCommandModel>>
{
    public QuestionListCommandModelResult(List<QuestionCommandModel> data = null,
        ValidationResult validationResult = null) : base(data, validationResult)
    {
    }
}