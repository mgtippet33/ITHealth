using FluentValidation.Results;

namespace ITHealth.Web.API.Models.Account.ChangeUserRole
{
    public class ChangeUserRoleResultModel : BaseOperationResultModel<ChangeUserRoleResponseModel>
    {
        public ChangeUserRoleResultModel(ChangeUserRoleResponseModel data, ValidationResult validationResult) : base(data, validationResult)
        {
        }
    }

    public class ChangeUserRoleResponseModel
    {
        public bool IsSuccessful { get; set; }
    }
}
