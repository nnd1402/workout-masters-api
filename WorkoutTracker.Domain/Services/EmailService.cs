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
        //private readonly IConfiguration _configuration;
        private readonly ILogService _logService;
        public EmailService(IConfiguration configuration, ILogService logService)
        {
            // _configuration = configuration;
            _logService = logService;
        }

        public void SendVerifyAccountEmail(string userEmail, string confirmationLink)
        {
            _logService.Create("Entered SendVerifyAccountEmail");

            string messageBody = Properties.Resources.VerifyAccountEmailTemplate;
            Console.WriteLine(messageBody);
            var updatedMessageBody = messageBody.Replace("#name#", userEmail.Substring(0, userEmail.IndexOf("@"))).Replace("#confirmationLink", confirmationLink);

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(Properties.Resources.EmailUsername));
            email.To.Add(MailboxAddress.Parse(userEmail));
            email.Subject = "Verify your account";
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = updatedMessageBody;
            bodyBuilder.TextBody = updatedMessageBody;
            email.Body = bodyBuilder.ToMessageBody();
            //email.Body = new TextPart(TextFormat.Html) { Text = updatedMessageBody };
            _logService.Create("Email composed");

            using (var smtp = new SmtpClient())
            {
                smtp.Connect(Properties.Resources.EmailHost, 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(Properties.Resources.EmailUsername, Properties.Resources.EmailPassword);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
            _logService.Create("Sending complete");
        }

        public void SendForgotPasswordEmail(string userEmail, string resetPasswordLink)
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
                smtp.Connect(Properties.Resources.EmailHost, 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(Properties.Resources.EmailUsername, Properties.Resources.EmailPassword);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }

        //private string ReadEmailTemplate(string templatePath)
        //{
        //    StreamReader reader = new StreamReader(templatePath);
        //    string result = reader.ReadToEnd();
        //    reader.Close();
        //    return result;
        //}
    }
}
