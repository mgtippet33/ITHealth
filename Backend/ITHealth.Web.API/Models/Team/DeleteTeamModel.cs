using FluentValidation.Results;

namespace ITHealth.Web.API.Models.Team
{
    public class DeleteTeamResponseModel
    {
        public bool IsSuccessful { get; set; }
    }

    public class DeleteTeamResultModel : BaseOperationResultModel<DeleteTeamResponseModel> 
    {
        public DeleteTeamResultModel(DeleteTeamResponseModel data, ValidationResult validationResult) : base(data, validationResult) 
        {
        }
    }
}
