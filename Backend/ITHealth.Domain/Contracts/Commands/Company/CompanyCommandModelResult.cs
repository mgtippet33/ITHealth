using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.Company
{
    public class CompanyCommandModelResult : BaseCommandModelResult<CompanyCommandModel>
    {
        public CompanyCommandModelResult(CompanyCommandModel data = null, ValidationResult validationResult = null) : base(data, validationResult)
        {
        }
    }
}
