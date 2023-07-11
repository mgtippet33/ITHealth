using FluentValidation.Results;

namespace ITHealth.Web.API.Models.Account.ResetPassword
{
    public class ResetPasswordResultModel : BaseOperationResultModel<ResetPasswordResponseModel>
    {
        public ResetPasswordResultModel(ResetPasswordResponseModel data, ValidationResult validationResult) : base(data, validationResult)
        {
        }
    }
}
