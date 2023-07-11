using FluentValidation.Results;

namespace ITHealth.Web.API.Models.Health
{
    public class HealthResultModel : BaseOperationResultModel<HealthResponseModel>
    {
        public HealthResultModel(HealthResponseModel data, ValidationResult validationResult) : base(data, validationResult) 
        {
        }
    }
}
