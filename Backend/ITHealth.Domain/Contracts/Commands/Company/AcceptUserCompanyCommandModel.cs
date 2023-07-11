namespace ITHealth.Domain.Contracts.Commands.Company
{
    public class AcceptUserCompanyCommandModel : BaseCommandModel
    {
        public string? CurrentUserEmail { get; set; }

        public string? InviteCode { get; set; }
    }
}
