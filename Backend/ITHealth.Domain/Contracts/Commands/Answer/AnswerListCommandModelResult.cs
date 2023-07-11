using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.Answer
{
    public class AnswerListCommandModelResult : BaseCommandModelResult<List<AnswerCommandModel>>
    {
        public AnswerListCommandModelResult(List<AnswerCommandModel> data = null, ValidationResult validationResult = null) : base(data, validationResult)
        {
        }
    }
}
