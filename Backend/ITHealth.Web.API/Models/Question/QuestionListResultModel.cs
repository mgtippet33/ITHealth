using FluentValidation.Results;

namespace ITHealth.Web.API.Models.Question;

public class QuestionListResultModel : BaseOperationResultModel<List<QuestionResponseModel>>
{
    public QuestionListResultModel(List<QuestionResponseModel> data, ValidationResult validationResult) : base(data, validationResult) 
    {
    }
}