using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using WorkoutTracker.Domain.Services.Interfaces;
using System.Reflection;

namespace WorkoutTracker.Domain.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogService _logService;
        public EmailService(IConfiguration configuration, ILogService logService)
        {
            _configuration = configuration;
            _logService = logService;
        }


        public void SendVerifyAccountEmail(string userEmail, string confirmationLink)
        {
            _logService.Create("Entered SendVerifyAccountEmail");
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);

            var filePath = Path.Combine(outPutDirectory, _configuration.GetSection("VerifyAccountEmailTemplatePath").Value);

            string relativePath = new Uri(filePath).LocalPath;

            string messageBody = ReadEmailTemplate(relativePath);
            var updatedMessageBody = messageBody.Replace("#name#", userEmail.Substring(0, userEmail.IndexOf("@"))).Replace("#confirmationLink", confirmationLink);

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(userEmail));
            email.Subject = "Verify your account";
            email.Body = new TextPart(TextFormat.Html) { Text = updatedMessageBody };

            using (var smtp = new SmtpClient())
            {
                smtp.Connect(_configuration.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(_configuration.GetSection("EmailUsername").Value, _configuration.GetSection("EmailPassword").Value);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
            _logService.Create("Sending complete");
        }

        public void SendForgotPasswordEmail(string userEmail, string resetPasswordLink)
        {
            string messageBody = ReadEmailTemplate(_configuration.GetSection("ForgotPasswordEmailTemplatePath").Value);
            var updatedMessageBody = messageBody.Replace("#name#", userEmail.Substring(0, userEmail.IndexOf("@"))).Replace("#resetPasswordLink", resetPasswordLink);

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(userEmail));
            email.Subject = "Reset Password";
            email.Body = new TextPart(TextFormat.Html) { Text = updatedMessageBody };

            using (var smtp = new SmtpClient())
            {
                smtp.Connect(_configuration.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(_configuration.GetSection("EmailUsername").Value, _configuration.GetSection("EmailPassword").Value);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }

        private string ReadEmailTemplate(string templatePath)
        {
            StreamReader reader = new StreamReader(templatePath);
            string result = reader.ReadToEnd();
            reader.Close();
            return result;
        }
    }
}
