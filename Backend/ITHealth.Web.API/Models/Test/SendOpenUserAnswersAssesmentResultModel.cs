using FluentValidation.Results;

namespace ITHealth.Web.API.Models.Test
{
    public class SendOpenUserAnswersAssesmentResultModel : BaseOperationResultModel<SendOpenUserAnswersAssesmentResponseModel>
    {
        public SendOpenUserAnswersAssesmentResultModel(SendOpenUserAnswersAssesmentResponseModel data, ValidationResult validationResult) : base(data, validationResult)
        {
        }
    }

    public class SendOpenUserAnswersAssesmentResponseModel
    {
        public bool IsSuccessful { get; set; }
    }
}
