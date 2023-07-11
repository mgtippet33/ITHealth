using FluentValidation.Results;

namespace ITHealth.Web.API.Models.UserTeam
{
    public class UserTeamResultModel : BaseOperationResultModel<UserTeamResponseModel>
    {
        public UserTeamResultModel(UserTeamResponseModel data, ValidationResult validationResult) : base(data, validationResult) 
        {
        }
    }
}
