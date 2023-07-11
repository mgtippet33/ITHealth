using FluentValidation.Results;

namespace ITHealth.Web.API.Models.Health
{
    public class BurnoutResultModel : BaseOperationResultModel<BurnoutResponseModel>
    {
        public BurnoutResultModel(BurnoutResponseModel data, ValidationResult validationResult) : base(data, validationResult)
        {
        }
    }

    public class BurnoutResponseModel
    {
        public bool HasStress { get; set; }

        public bool HasBadSleep { get; set; }

        public bool HasLowEfficiency { get; set; }

        public bool HasBadTestResults { get; set; }

        public bool HasOvertime { get; set; }

        public string GeneralState { get; set; } = string.Empty;
    }
}
