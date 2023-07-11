using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.Company
{
    public class AcceptUserCompanyCommandModelResult : BaseCommandModelResult<AcceptUserCompanyResponseCommandModel>
    {
        public AcceptUserCompanyCommandModelResult(AcceptUserCompanyResponseCommandModel data = null, ValidationResult validationResult = null) : base(data, validationResult)
        {
        }
    }

    public class AcceptUserCompanyResponseCommandModel
    {
        public bool IsSuccessful { get; set; }
    }
}
