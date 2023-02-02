using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using WorkoutMasters.Domain.Services.Interfaces;
using System.Reflection;

namespace WorkoutMasters.Domain.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogService _logService;
        public EmailService(IConfiguration configuration, ILogService logService)
        {
            _logService = logService;
        }
        public void VerifyAccountEmail(string userEmail, string confirmationLink)
        {
            string messageBody = Properties.Resources.VerifyAccountEmailTemplate;
            var updatedMessageBody = messageBody.Replace("#name#", userEmail.Substring(0, userEmail.IndexOf("@"))).Replace("#confirmationLink", confirmationLink);

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(Properties.Resources.EmailUsername));
            email.To.Add(MailboxAddress.Parse(userEmail));
            email.Subject = "Verify your account";
            email.Body = new TextPart(TextFormat.Html) { Text = updatedMessageBody };

            using (var smtp = new SmtpClient())
            {
                SendEmail(email);
            }
        }
        public void ForgotPasswordEmail(string userEmail, string resetPasswordLink)
        {
            string messageBody = Properties.Resources.ForgotPasswordEmailTemplate;
            var updatedMessageBody = messageBody.Replace("#name#", userEmail.Substring(0, userEmail.IndexOf("@"))).Replace("#resetPasswordLink", resetPasswordLink);

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(Properties.Resources.EmailUsername));
            email.To.Add(MailboxAddress.Parse(userEmail));
            email.Subject = "Reset Password";
            email.Body = new TextPart(TextFormat.Html) { Text = updatedMessageBody };

            using (var smtp = new SmtpClient())
            {
                SendEmail(email);
            }
        }
        private void SendEmail(MimeMessage email)
        {
            using (var smtp = new SmtpClient())
            {
                smtp.Connect(Properties.Resources.EmailHost, 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(Properties.Resources.EmailUsername, Properties.Resources.EmailPassword);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }
    }
}
