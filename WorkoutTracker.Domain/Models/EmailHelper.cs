using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Domain.Models
{
    public class EmailHelper
    {
        public void SendEmail(string userEmail, string confirmationLink)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("workouttestapp01@gmail.com"));
            email.To.Add(MailboxAddress.Parse(userEmail));
            email.Subject = "Confirm you email";
            email.Body = new TextPart(TextFormat.Html) { Text = confirmationLink };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("workoutapptest01@gmail.com", "nvfhmzvnqjfxyhan");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
