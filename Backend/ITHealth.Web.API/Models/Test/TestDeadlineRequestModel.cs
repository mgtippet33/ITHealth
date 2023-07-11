namespace ITHealth.Web.API.Models.Test
{
    public class TestDeadlineRequestModel
    {
        public int TestId { get; set; }
        public DateTime Deadline { get; set; }
    }

    public class UsersTestDeadlineRequestModel : TestDeadlineRequestModel
    {
        public List<int> UserIds { get; set; } = new();
    }

    public class TeamTestDeadlineRequestModel : TestDeadlineRequestModel
    {
        public int TeamId { get; set; }
    }
}
