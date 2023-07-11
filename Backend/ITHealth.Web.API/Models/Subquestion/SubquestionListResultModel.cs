using FluentValidation.Results;

namespace ITHealth.Web.API.Models.Subquestion;

public class SubquestionListResultModel : BaseOperationResultModel<List<SubquestionResponseModel>>
{
    public SubquestionListResultModel(List<SubquestionResponseModel> data, ValidationResult validationResult) : base(data,
        validationResult)
    {
    }
}