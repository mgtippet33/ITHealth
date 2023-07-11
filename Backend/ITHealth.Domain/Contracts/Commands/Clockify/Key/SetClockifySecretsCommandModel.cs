namespace ITHealth.Domain.Contracts.Commands.Clockify.Key
{
    public class SetClockifySecretsCommandModel : BaseCommandModel
    {
        public string Token { get; set; } = null!;

        public string WorkspaceName { get; set; } = null!;

        public string ProjectName { get; set; } = null!;

        public int TeamId { get; set; }
    }
}
