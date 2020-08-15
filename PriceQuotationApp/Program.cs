using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;
using System.Collections.Generic;
using System.Text;
using System;
using System.Net;

namespace PriceQuotationApp
{
    class Program
    {
        public static void Main(string[] args)
        {
            AvapiExample.Example();

            //await SendEmailAsync();

            SendEmailSMTP();
        }

        public static async Task SendEmailAsync()
        {
            var apiKey = "apikey";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("email", "your name");
            var subject = "TESTE2";
            var to = new EmailAddress("email", "receive name");
            var plainTextContent = "teste";
            var htmlContent = "<strong>teste</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }

        public static void SendEmailSMTP()
        {

            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.sendgrid.net";
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("apikey", "apikey");

            MailMessage mail = new MailMessage();
            mail.To.Add(new MailAddress("email@teste.com"));
            mail.From = new MailAddress("teste@email.com");
            mail.Subject = "Second Email";
            mail.Body = "Email via STMP client";

            client.Send(mail);
        }
    }
}