namespace ITHealth.Domain.Contracts.Commands.Company
{
    public class BaseCompanyCommandModel : BaseCommandModel
    {
        public int Id { get; set; }

        public string? CurrentUserEmail { get; set; }
    }
}
