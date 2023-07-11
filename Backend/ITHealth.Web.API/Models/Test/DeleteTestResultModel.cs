using FluentValidation.Results;

namespace ITHealth.Web.API.Models.Test;

public class DeleteTestResponseModel
{
    public bool IsSuccessful { get; set; }
}

public class DeleteTestResultModel : BaseOperationResultModel<DeleteTestResponseModel> 
{
    public DeleteTestResultModel(DeleteTestResponseModel data, ValidationResult validationResult) : base(data, validationResult) 
    {
    }
}