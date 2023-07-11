using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.Account.ChangeUserRole
{
    public class ChangeUserRoleCommandModelResult : BaseCommandModelResult<ChangeUserRoleResponseCommandModel>
    {
        public ChangeUserRoleCommandModelResult(ChangeUserRoleResponseCommandModel data, ValidationResult validationResult) : base(data, validationResult)
        {
        }
    }

    public class ChangeUserRoleResponseCommandModel
    {
        public bool IsSuccessful { get; set; }
    }
}
