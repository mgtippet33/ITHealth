using ITHealth.Domain.Contracts.Interfaces;
using ITHealth.Domain.Settings;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;

namespace ITHealth.Domain.Services
{
    public class MailService : IMailService
    {
        private readonly SmtpSettings _smtpSettings;

        public MailService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public bool SendResetPasswordTokenMail(string email, string token)
        {
            var message = new MailMessage(_smtpSettings.Email, email);
            message.Subject = Resources.EmailResource.ResetPassword_Subject;
            message.Body = string.Format(Resources.EmailResource.ResetPassword_Body, email, _smtpSettings.WebUrl, token);

            return SendMail(message);
        }

        public bool SendUserInviteMail(string email, string token)
        {
            var message = new MailMessage("test@gmail.com", email);
            message.Subject = Resources.EmailResource.InviteUser_Subject;
            message.Body = string.Format(Resources.EmailResource.InviteUser_Body, _smtpSettings.WebUrl, email, token);

            return SendMail(message);
        }

        private bool SendMail(MailMessage message)
        {
            try
            {
                var smtpClient = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port);
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential(_smtpSettings.Email, _smtpSettings.Password);

                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
