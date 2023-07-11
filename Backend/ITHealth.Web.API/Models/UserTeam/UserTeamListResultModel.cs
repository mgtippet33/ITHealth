using FluentValidation.Results;
using ITHealth.Web.API.Models.Account;

namespace ITHealth.Web.API.Models.UserTeam
{
    public class UserTeamListResultModel : BaseOperationResultModel<List<UserResponseModel>>
    {
        public UserTeamListResultModel(List<UserResponseModel> data, ValidationResult validationResult) : base(data, validationResult) 
        {
        }
    }
}
