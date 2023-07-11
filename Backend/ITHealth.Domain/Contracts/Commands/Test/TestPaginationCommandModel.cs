namespace ITHealth.Domain.Contracts.Commands.Test
{
    public class TestPaginationRequestCommandModel : BaseCommandModel
    {
        public int CurrentPageNumber { get; set; }

        public int TestCount { get; set; }
    }

    public class TestPaginationResponseCommandModel<TCommandModel>
    {
        public int CurrentPageNumber { get; set; }

        public int LastPageNumber { get; set; }

        public List<TCommandModel> Tests { get; set; } = new();
    }
}
