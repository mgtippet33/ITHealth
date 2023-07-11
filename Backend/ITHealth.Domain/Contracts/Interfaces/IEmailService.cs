namespace ITHealth.Domain.Contracts.Interfaces
{
    public interface IMailService
    {
        public bool SendResetPasswordTokenMail(string email, string token);

        public bool SendUserInviteMail(string email, string token);
    }
}
