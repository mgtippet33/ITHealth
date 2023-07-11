using FluentValidation.Results;

namespace ITHealth.Web.API.Models.Answer;

public class DeleteAnswerResponseModel
{
    public bool IsSuccessful { get; set; }
}

public class DeleteAnswerResultModel : BaseOperationResultModel<DeleteAnswerResponseModel> 
{
    public DeleteAnswerResultModel(DeleteAnswerResponseModel data, ValidationResult validationResult) : base(data, validationResult) 
    {
    }
}