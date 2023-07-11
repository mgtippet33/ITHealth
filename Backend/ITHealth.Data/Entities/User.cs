using ITHealth.Data.Enums;
using Microsoft.AspNetCore.Identity;

namespace ITHealth.Data.Entities
{
    public class User : IdentityUser<int>
    {
        public string FullName { get; set; } = null!;
        public Gender Gender { get; set; }
        public BloodType? BloodType { get; set; }
        public double AveragePressure { get; set; }
        public int WorkHoursCount { get; set; }
        public bool IsActive { get; set; }
        public int? CompanyId { get; set; }

        public Company Company { get; set; }
        public HashSet<Health> Healths { get; set; } = new HashSet<Health>();
        public HashSet<Team> Teams { get; set; } = new HashSet<Team>();
        public HashSet<TestResult> TestResults { get; set; } = new HashSet<TestResult>();
        public HashSet<TestDeadline> TestDeadlines { get; set; } = new HashSet<TestDeadline>();
    }
}
