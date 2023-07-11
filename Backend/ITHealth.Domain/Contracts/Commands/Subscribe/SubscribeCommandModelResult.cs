using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.Subscribe
{
    public class SubscribeCommandModelResult : BaseCommandModelResult<SubscribeResponseCommandModel>
    {
        public SubscribeCommandModelResult(SubscribeResponseCommandModel data = null, ValidationResult validationResult = null) : base(data, validationResult)
        {
        }
    }

    public class SubscribeResponseCommandModel
    {
        public double Price { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsPaid { get; set; }
    }
}
