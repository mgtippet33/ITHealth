using ITHealth.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Data
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Health> Healths { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<SubscribeHistory> SubscribeHistories { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<Subquestion> Subquestions { get; set; }
        public virtual DbSet<TestResult> TestResults { get; set; }
        public virtual DbSet<TestDeadline> TestDeadlines { get; set; }
        public virtual DbSet<TrelloWorkspaceSecrets> TrelloWorkspaceSecrets { get; set; }
        public virtual DbSet<UserAnswer> UserAnswers { get; set; }
        public virtual DbSet<UserOpenAnswer> OpenUserAnswers { get; set; }
        public virtual DbSet<ClockifyWorkspaceSecrets> ClockifyWorkspaceSecrets { get; set; }
        public virtual DbSet<JiraWorkspaceSecrets> JiraWorkspaceSecrets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
