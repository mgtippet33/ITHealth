using System.ComponentModel.DataAnnotations;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace ITHealth.Web.API.Models.Answer;

public class AnswerListResultModel : BaseOperationResultModel<List<AnswerResponseModel>>
{
    public AnswerListResultModel(List<AnswerResponseModel> data, ValidationResult validationResult) : base(data, validationResult)
    {
    }
}