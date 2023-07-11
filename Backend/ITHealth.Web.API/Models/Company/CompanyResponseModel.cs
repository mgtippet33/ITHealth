namespace ITHealth.Web.API.Models.Company
{
    public class CompanyResponseModel : CompanyRequestModel
    {
        public int Id { get; set; }

        public string InviteCode { get; set; } = string.Empty;
    }
}
