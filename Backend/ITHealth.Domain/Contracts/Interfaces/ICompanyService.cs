using ITHealth.Domain.Contracts.Commands.Company;

namespace ITHealth.Domain.Contracts.Interfaces
{
    public interface ICompanyService
    {
        Task<CompanyCommandModelResult> GetCompanyAsync(string email);

        Task<ListUserCompanyCommandModelResult> GetCompanyAdministrators(string currentUserEmail);

        Task<ListUserCompanyCommandModelResult> GetCompanyUsersExcludingCurrentTeamUsersAsync(GetCompanyUsersCommandModel command);

        Task<CompanyCommandModelResult> CreateCompanyAsync(CompanyCommandModel command);

        Task<CompanyCommandModelResult> UpdateCompanyAsync(UpdateCompanyCommandModel command);

        Task<AcceptUserCompanyCommandModelResult> AcceptUserToCompany(AcceptUserCompanyCommandModel command);
    }
}
