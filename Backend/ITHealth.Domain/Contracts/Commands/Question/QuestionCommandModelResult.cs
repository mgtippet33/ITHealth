using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.Question;

public class QuestionCommandModelResult : BaseCommandModelResult<QuestionCommandModel>
{
    public QuestionCommandModelResult(QuestionCommandModel data = null, ValidationResult validationResult = null) :
        base(data, validationResult)
    {
    }
}