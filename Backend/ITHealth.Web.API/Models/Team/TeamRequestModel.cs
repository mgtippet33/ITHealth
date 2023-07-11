using ITHealth.Data.Enums;

namespace ITHealth.Web.API.Models.Team
{
    public class TeamRequestModel
    {
        public string Name { get; set; } = null!;

        public bool IsActive { get; set; }

        public WorkPlatform WorkPlatform { get; set; }
    }
}
