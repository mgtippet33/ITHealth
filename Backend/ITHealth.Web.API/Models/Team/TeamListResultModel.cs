using FluentValidation.Results;

namespace ITHealth.Web.API.Models.Team
{
    public class TeamListResultModel : BaseOperationResultModel<List<TeamResponseModel>>
    {
        public TeamListResultModel(List<TeamResponseModel> data, ValidationResult validationResult) : base(data, validationResult) 
        {
        }
    }
}
