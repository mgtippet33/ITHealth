using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.Subscribe
{
    public class SaveSubscribeCommandModelResult : BaseCommandModelResult<SaveSubscribeResponseCommandModel>
    {
        public SaveSubscribeCommandModelResult(SaveSubscribeResponseCommandModel data = null, ValidationResult validationResult = null) : base(data, validationResult)
        {
        }
    }

    public class SaveSubscribeResponseCommandModel
    {
        public bool IsSuccessful { get; set; }
    }
}
