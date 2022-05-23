using System.Net;
using System.Net.Mail;

namespace PizzaAPI.Services
{
    public class EmailSenderService
    {
        private static string _email = "pizzaAPIofficial@gmail.com";
        private static SmtpClient _smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential(_email, "pizzaAPI123"),
            EnableSsl = true
        };

        public static void SendEmail(string recipient, string subject, string body)
        {
            _smtpClient.Send(_email, recipient, subject, body);
        }

        public static async Task SendSignupConfirmationEmail(string recipient, string confirmationCode, string confirmationLink)
        {

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_email),
                Subject = "Thank you for registering with our services",
                Body = "<h2>Hello from PizzaAPI Team,</h2>" +
                "<p>Thank you for creating an account on our website. To confirm your newly" +
                "created account, please follow the steps presented below</p> " +
                "<h3>Please enter this code: <b>" + confirmationCode + "<b></h3>" +
                "<h3>In this link <a href=\"" + confirmationLink + "\">Click here</a></h3>" +
                "<br><br><p>Kind regards,</p><p>PizzaAPI Team</p>",
                IsBodyHtml = true
            };

            mailMessage.To.Add(new MailAddress(recipient));

            await _smtpClient.SendMailAsync(mailMessage);
        }
    }
}
