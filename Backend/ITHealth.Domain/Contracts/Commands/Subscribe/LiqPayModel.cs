using System.Text.Json.Serialization;

namespace ITHealth.Domain.Contracts.Commands.Subscribe
{
    public class LiqPayModel
    {
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("amount")]
        public double Amount { get; set; }

        [JsonPropertyName("sender_first_name")]
        public string CompanyId { get; set; } = string.Empty;
    }
}
