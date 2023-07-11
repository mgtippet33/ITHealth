using FluentValidation.Results;

namespace ITHealth.Web.API.Models.Company
{
    public class ListUserCompanyResultModel : BaseOperationResultModel<List<UserCompanyResponseModel>>
    {
        public ListUserCompanyResultModel(List<UserCompanyResponseModel> data = null, ValidationResult validationResult = null) : base(data, validationResult)
        {
        }
    }

    public class UserCompanyResponseModel
    {
        public int Id { get; set; }

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Role { get; set; } = null!;

        public bool IsActive { get; set; }
    }
}
