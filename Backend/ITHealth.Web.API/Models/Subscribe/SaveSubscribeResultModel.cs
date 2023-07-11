using FluentValidation.Results;

namespace ITHealth.Web.API.Models.Subscribe
{
    public class SaveSubscribeResultModel : BaseOperationResultModel<SaveSubscribeResponseModel>
    {
        public SaveSubscribeResultModel(SaveSubscribeResponseModel data, ValidationResult validationResult) : base(data, validationResult)
        {
        }
    }

    public class SaveSubscribeResponseModel
    {
        public bool IsSuccessful { get; set; }
    }
}
