namespace ITHealth.Domain.Contracts.Commands.Test
{
    public class GetUserTestListCommandModel : TestPaginationRequestCommandModel
    {
        public string UserEmail { get; set; } = null!;
    }
}
