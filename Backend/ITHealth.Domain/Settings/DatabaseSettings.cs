namespace ITHealth.Domain.Settings
{
    public class DatabaseSettings
    {
        public const string SectionName = "Database";

        public string SqlConnection { get; set; } = null!;
    }
}
