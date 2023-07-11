using FluentValidation.Results;

namespace ITHealth.Web.API.Models.Account
{
    public class UserResultModel : BaseOperationResultModel<UserResponseModel>
    {
        public UserResultModel(UserResponseModel data, ValidationResult validationResult) : base(data, validationResult) 
        { 
        }
    }
}
