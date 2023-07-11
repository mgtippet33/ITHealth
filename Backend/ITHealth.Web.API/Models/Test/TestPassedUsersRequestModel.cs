namespace ITHealth.Web.API.Models.Test
{
    public class TestPassedUsersRequestModel
    {
        public int TeamId { get; set; }

        public int TestId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
