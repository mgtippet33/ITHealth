namespace ITHealth.Domain.Contracts.Commands.Test
{
    public class GetTestsCommandModel : TestPaginationRequestCommandModel
    {
        public string CurrentUserEmail { get; set; } = string.Empty;
    }
}
