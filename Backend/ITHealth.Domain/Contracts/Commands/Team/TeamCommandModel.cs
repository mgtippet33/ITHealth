using ITHealth.Data.Enums;

namespace ITHealth.Domain.Contracts.Commands.Team
{
    public class TeamCommandModel : BaseTeamCommandModel
    {
        public string Name { get; set; } = null!;

        public bool IsActive { get; set; }

        public WorkPlatform WorkPlatform { get; set; }
    }
}
