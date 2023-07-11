using FluentValidation.Results;

namespace ITHealth.Web.API.Models.Account.Login
{
    public class LoginUserResultModel : BaseOperationResultModel<LoginUserResponseModel>
    {
        public LoginUserResultModel(LoginUserResponseModel data, ValidationResult validationResut) : base(data, validationResut) 
        { 
        }
    }
}
