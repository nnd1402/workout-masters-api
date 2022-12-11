using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using WorkoutTracker.Domain.Services.Interfaces;

namespace WorkoutTracker.Domain.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public void SendEmail(string userEmail, string confirmationLink)
        {
            string messageBody = ReadEmailTemplate(_configuration.GetSection("EmailTemplatePath").Value);
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
