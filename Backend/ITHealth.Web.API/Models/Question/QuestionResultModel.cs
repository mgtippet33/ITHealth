using FluentValidation.Results;

namespace ITHealth.Web.API.Models.Question;

public class QuestionResultModel : BaseOperationResultModel<QuestionResponseModel>
{
    public QuestionResultModel(QuestionResponseModel data, ValidationResult validationResult) : base(data, validationResult) 
    {
    }
}