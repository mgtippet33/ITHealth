using FluentValidation.Results;

namespace ITHealth.Web.API.Models.Answer;

public class AnswerResultModel : BaseOperationResultModel<AnswerResponseModel>
{
    public AnswerResultModel(AnswerResponseModel data, ValidationResult validationResult) : base(data, validationResult)
    {
    }
}