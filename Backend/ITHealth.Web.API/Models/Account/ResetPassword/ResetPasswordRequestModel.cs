namespace ITHealth.Web.API.Models.Account.ResetPassword
{
    public class ResetPasswordRequestModel
    {
        public string ResetToken { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
