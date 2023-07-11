using FluentValidation.Results;

namespace ITHealth.Web.API.Models.Clockify.Secrets
{
    public class ClockifySetSecretsResultModel : BaseOperationResultModel<ClockifySetSecretsResponseModel>
    {
        public ClockifySetSecretsResultModel(ClockifySetSecretsResponseModel data = null, ValidationResult validationResult = null) : base(data, validationResult)
        {
        }
    }

    public class ClockifySetSecretsResponseModel
    {
        public bool IsSuccessful { get; set; }
    }
}
