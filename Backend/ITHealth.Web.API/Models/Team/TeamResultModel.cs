using FluentValidation.Results;

namespace ITHealth.Web.API.Models.Team
{
    public class TeamResultModel : BaseOperationResultModel<TeamResponseModel>
    {
        public TeamResultModel(TeamResponseModel data, ValidationResult validationResult) : base(data, validationResult) 
        {
        }
    }
}
