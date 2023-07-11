namespace ITHealth.Domain.Contracts.Commands.Account.ChangeUserRole
{
    public class ChangeUserRoleCommandModel : BaseCommandModel
    {
        public int UserId { get; set; }

        public string Role { get; set; } = null!;
    }
}
