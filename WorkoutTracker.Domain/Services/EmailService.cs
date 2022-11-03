//using MailKit.Net.Smtp;
//using MailKit.Security;
//using MimeKit;
//using MimeKit.Text;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using WorkoutTracker.Domain.DTO.EmailDTO;
//using WorkoutTracker.Domain.Services.Interfaces;

//namespace WorkoutTracker.Domain.Services
//{
//    public class EmailService : IEmailService
//    {
//        public void SendEmail(EmailDTO emailDTO)
//        {
//            var email = new MimeMessage();
//            email.From.Add(MailboxAddress.Parse("josiane.reinger44@ethereal.email"));
//            email.To.Add(MailboxAddress.Parse(emailDTO.To));
//            email.Subject = emailDTO.Subject;
//            email.Body = new TextPart(TextFormat.Html) { Text = emailDTO.Body };

//            using var smtp = new SmtpClient();
//            smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
//            smtp.Authenticate("josiane.reinger44@ethereal.email", "T7qaXHgKzjtF4BYcAA");
//            smtp.Send(email);
//            smtp.Disconnect(true);
//        }
//    }
//}
