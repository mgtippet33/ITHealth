using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.Answer
{
    public class AnswerCommandModelResult : BaseCommandModelResult<AnswerCommandModel>
    {
        public AnswerCommandModelResult(AnswerCommandModel data = null, ValidationResult validationResult = null) : base(data, validationResult)
        {
        }
    }
}
