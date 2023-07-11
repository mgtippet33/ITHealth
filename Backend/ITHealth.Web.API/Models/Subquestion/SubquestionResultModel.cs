using FluentValidation.Results;

namespace ITHealth.Web.API.Models.Subquestion;

public class SubquestionResultModel : BaseOperationResultModel<SubquestionResponseModel>
{
    public SubquestionResultModel(SubquestionResponseModel data, ValidationResult validationResult) : base(data, validationResult)
    {
    }
}