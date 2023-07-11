﻿namespace ITHealth.Domain.Settings
{
    public class SmtpSettings
    {
        public const string SectionName = "SMTP";

        public string Host { get; set; } = null!;

        public int Port { get; set; }

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string WebUrl { get; set; } = null!;
    }
}
