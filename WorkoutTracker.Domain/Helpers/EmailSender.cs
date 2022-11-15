using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace WorkoutTracker.Domain.Helpers
{
    public class EmailSender
    {
        public void SendEmail(string userEmail, string confirmationLink)
        {
            //sakriti url, relative putanja
            string messageBody = ReadEmailTemplate("C:\\Users\\nenad\\source\\repos\\WorkoutTracker.API\\WorkoutTracker.Domain\\Templates\\EmailTemplate.html");
            var updatedMessageBody = messageBody.Replace("#name#", userEmail.Substring(0, userEmail.IndexOf("@"))).Replace("#confirmationLink", confirmationLink);

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("workouttestapp01@gmail.com"));
            email.To.Add(MailboxAddress.Parse(userEmail));
            email.Subject = "Confirm you account";
            email.Body = new TextPart(TextFormat.Html) { Text = updatedMessageBody };

            //using var smtp = new SmtpClient();
            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("workoutapptest01@gmail.com", "nvfhmzvnqjfxyhan");
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
