namespace ITHealth.Domain.Contracts.Commands.Subscribe
{
    public class SaveSubscribeCommandModel : BaseCommandModel
    {
        public string Data { get; set; } = string.Empty;

        public string Signature { get; set; } = string.Empty;
    }
}
