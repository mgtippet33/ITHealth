namespace ITHealth.Web.API.Models.Test
{
    public class UsersTestingStatisticsRequestModel
    {
        public int TestId { get; set; }

        public List<int> UserIds { get; set; } = new();

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
