using FluentValidation.Results;

namespace ITHealth.Web.API.Models.Account.InviteUser
{
    public class InviteUserResultModel : BaseOperationResultModel<InviteUserResponseModel>
    {
        public InviteUserResultModel(InviteUserResponseModel data, ValidationResult validationResult) : base(data, validationResult)
        {
        }
    }

    public class InviteUserResponseModel
    {
        public bool IsEmailSent { get; set; }
    }
}
