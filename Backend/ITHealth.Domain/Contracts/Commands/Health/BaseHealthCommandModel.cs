namespace ITHealth.Domain.Contracts.Commands.Health
{
    public class BaseHealthCommandModel : BaseCommandModel
    {
        public int Id { get; set; }

        public string? CurrentUserEmail { get; set; }
    }
}
