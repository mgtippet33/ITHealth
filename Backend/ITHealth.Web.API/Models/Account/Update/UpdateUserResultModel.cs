using FluentValidation.Results;

namespace ITHealth.Web.API.Models.Account.Update
{
    public class UpdateUserResultModel : BaseOperationResultModel<UserResponseModel>
    {
        public UpdateUserResultModel(UserResponseModel data, ValidationResult validationResut) : base(data, validationResut)
        {
        }
    }
}
