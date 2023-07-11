using FluentValidation.Results;

namespace ITHealth.Web.API.Models.Account.DeleteUser
{
    public class DeleteUserResultModel : BaseOperationResultModel<DeleteUserResponseModel>
    {
        public DeleteUserResultModel(DeleteUserResponseModel data, ValidationResult validationResult) : base(data, validationResult)
        {
        }
    }

    public class DeleteUserResponseModel
    {
        public bool IsSuccessful { get; set; }
    }
}
