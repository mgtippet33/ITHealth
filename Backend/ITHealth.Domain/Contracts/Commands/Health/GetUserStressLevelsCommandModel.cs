namespace ITHealth.Domain.Contracts.Commands.Health
{
    public class GetUserStressLevelsCommandModel : BaseCommandModel
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string CurrentUserEmail { get; set; } = null!;
    }
}
