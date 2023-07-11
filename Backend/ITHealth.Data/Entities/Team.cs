using ITHealth.Data.Enums;

namespace ITHealth.Data.Entities
{
    public class Team : BaseEntity
    {
        public int CompanyId { get; set; }
        public string Name { get; set; } = null!;
        public bool IsActive { get; set; }
        public WorkPlatform WorkPlatform { get; set; }

        public Company Company { get; set; } = new Company();
        public HashSet<User> Users { get; set; } = new HashSet<User>();
        public ClockifyWorkspaceSecrets ClockifyWorkspaceSecrets { get; set; }
        public JiraWorkspaceSecrets JiraWorkspaceSecrets { get; set; }
        public TrelloWorkspaceSecrets TrelloWorkspaceSecrets { get; set; }
    }
}
