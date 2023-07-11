namespace ITHealth.Web.API.Models.Account.ChangeUserRole
{
    public class ChangeUserRoleRequestModel
    {
        public int UserId { get; set; }

        public string Role { get; set; } = string.Empty;
    }
}
