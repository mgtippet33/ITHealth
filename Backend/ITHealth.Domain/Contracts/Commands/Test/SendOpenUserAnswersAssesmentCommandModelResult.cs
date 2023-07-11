using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.Test
{
    public class SendOpenUserAnswersAssesmentCommandModelResult : BaseCommandModelResult<SendOpenUserAnswersAssesmentCommandModel>
    {
        public SendOpenUserAnswersAssesmentCommandModelResult(ValidationResult validationResult = null) : base(null, validationResult)
        {
        }
    }
}
