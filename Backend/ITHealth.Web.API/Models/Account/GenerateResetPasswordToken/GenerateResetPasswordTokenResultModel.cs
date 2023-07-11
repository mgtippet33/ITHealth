using FluentValidation.Results;

namespace ITHealth.Web.API.Models.Account.GenerateResetPasswordToken
{
    public class GenerateResetPasswordTokenResultModel : BaseOperationResultModel<GenerateResetPasswordTokenResponseModel>
    {
        public GenerateResetPasswordTokenResultModel(GenerateResetPasswordTokenResponseModel data, ValidationResult validationResult) : base(data, validationResult)
        {
        }
    }
}
