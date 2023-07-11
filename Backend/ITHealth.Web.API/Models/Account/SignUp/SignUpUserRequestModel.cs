namespace ITHealth.Web.API.Models.Account.SignUp
{
    public class SignUpUserRequestModel : UserResponseModel
    {
        public string Password { get; set; } = null!;
    }
}
