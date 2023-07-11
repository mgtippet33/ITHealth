namespace ITHealth.Domain.Contracts.Commands.Company
{
    public class CompanyCommandModel : BaseCompanyCommandModel
    {
        public string Name { get; set; } = null!;

        public string InviteCode { get; set; } = string.Empty;
    }
}
