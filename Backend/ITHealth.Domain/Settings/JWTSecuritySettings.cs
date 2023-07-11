namespace ITHealth.Domain.Settings
{
    public class JWTSecuritySettings
    {
        public const string SectionName = "JWT";

        public string Key { get; set; } = null!;

        public string Issuer { get; set; } = null!;

        public string Audience { get; set; } = null!;

        public string Subject { get; set; } = null!;
    }
}
