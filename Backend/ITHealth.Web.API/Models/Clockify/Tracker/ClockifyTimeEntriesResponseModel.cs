using FluentValidation.Results;

namespace ITHealth.Web.API.Models.Clockify.Tracker
{
    public class ClockifyTimeEntriesResponseModel
    {
        public TimeSpan TImeWorked { get; set; }
    }

    public class ClockifyTimeEntriesResultModel : BaseOperationResultModel<ClockifyTimeEntriesResponseModel>
    {
        public ClockifyTimeEntriesResultModel(ClockifyTimeEntriesResponseModel data, ValidationResult validationResult) : base(data, validationResult)
        {
        }
    }
}
