using FluentValidation.Results;

namespace ITHealth.Web.API.Models.Test
{
    public class UserTestListResultModel : BaseOperationResultModel<TestPaginationResponseModel<UserTestInfoResponseModel>>
    {
        public UserTestListResultModel(TestPaginationResponseModel<UserTestInfoResponseModel> data, ValidationResult validationResult) : base(data, validationResult)
        {
        }
    }

    public class UserTestInfoResponseModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime Deadline { get; set; }
    }
}
