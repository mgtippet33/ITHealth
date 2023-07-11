using FluentValidation.Results;

namespace ITHealth.Web.API.Models.Subscribe
{
    public class SubscribeResultModel : BaseOperationResultModel<SubscribeResponseModel>
    {
        public SubscribeResultModel(SubscribeResponseModel data, ValidationResult validationResult) : base(data, validationResult)
        {
        }
    }

    public class SubscribeResponseModel
    {
        public double Price { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsPaid { get; set; }
    }
}
