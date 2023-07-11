using FluentValidation.Results;
using ITHealth.Web.API.Models.Account;

namespace ITHealth.Web.API.Models.Company
{
    public class AcceptUserCompanyResultModel : BaseOperationResultModel<UserResponseModel>
    {
        public AcceptUserCompanyResultModel(UserResponseModel data = null, ValidationResult validationResult = null) : base(data, validationResult)
        {
        }
    }
}
