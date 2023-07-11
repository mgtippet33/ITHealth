using FluentValidation.Results;

namespace ITHealth.Web.API.Models.Company
{
    public class CompanyResultModel : BaseOperationResultModel<CompanyResponseModel>
    {
        public CompanyResultModel(CompanyResponseModel data, ValidationResult validationResult) : base(data, validationResult) 
        { 
        }
    }
}
