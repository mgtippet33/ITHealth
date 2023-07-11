namespace ITHealth.Web.API.Models.Test
{
    public class TestPaginationRequestModel
    {
        public int CurrentPageNumber { get; set; }

        public int TestCount { get; set; }
    }

    public class TestPaginationResponseModel<TInfoResponseModel>
    {
        public int CurrentPageNumber { get; set; }

        public int LastPageNumber { get; set; }

        public List<TInfoResponseModel> Tests { get; set; } = new();
    }
}
