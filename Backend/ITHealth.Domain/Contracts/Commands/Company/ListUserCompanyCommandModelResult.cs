using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.Company
{
    public class ListUserCompanyCommandModelResult : BaseCommandModelResult<List<UserCompanyResponseCommandModel>>
    {
        public ListUserCompanyCommandModelResult(List<UserCompanyResponseCommandModel> data = null, ValidationResult validationResult = null) : base(data, validationResult)
        {
        }
    }

    public class UserCompanyResponseCommandModel
    {
        public int Id { get; set; }

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Role { get; set; } = null!;

        public bool IsActive { get; set; }
    }
}
