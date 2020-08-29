using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PriceQuotationApp.Services
{
    public class NotificacaoServico : INotificacaoServico
    {

        private INotificacaoServico notificacaoServico;
        public NotificacaoServico()
        {

        }

        public NotificacaoServico(INotificacaoServico paramNotificacaoService)
        {
            notificacaoServico = paramNotificacaoService;
        }


        private static async Task EnviarEmailAPIAsync()
        {
            var config = System.Configuration.ConfigurationManager.AppSettings;
            var apiKey = config.Get("apikey");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(config.Get("from"));
            var to = new EmailAddress(config.Get("to"));
            var subject = "O Preço do {Ativo}";
            var plainTextContent = "O Preço do {Ativo.Symbol} Atingiu {Ativo.Max}";
            var htmlContent = "O Preço do {Ativo.Symbol} Atingiu {Ativo.Max}";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            await client.SendEmailAsync(msg).ConfigureAwait(false);
            //pode pegar response também;
            
        }

        public void NotificarPreco(string ativo, string condicao, float precoDesejado)
        {
            string mensagem = string.Format("Preço do ativo: {0} no alvo ou {1} ao desejado: {2}", ativo, condicao, precoDesejado);
            EnviarEmailSMTP(mensagem);
        }

        private static SmtpClient CreateSMTPCliente()
        {
            var config = System.Configuration.ConfigurationManager.AppSettings;
            var apiKey = config.Get("apikey");
            var smtp = config.Get("smtp");
            var smtpMethod = config.Get("smtpmethod");
            //only for test, remove after
            var to = config.Get("to");
            var from = config.Get("from");
            //only for test, remove after

            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = smtp;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("apikey", apiKey);

            return client;
        }

        private static void EnviarEmailSMTP(string mensagem)
        {

            var client = CreateSMTPCliente();
            var mail = CriarEstruturaEmail(mensagem);

            client.Send(mail);
            
        }

        private static MailMessage CriarEstruturaEmail(string mensagem)
        {
            var config = System.Configuration.ConfigurationManager.AppSettings;
            var to = config.Get("to");
            var from = config.Get("from");

            MailMessage mail = new MailMessage();
            mail.To.Add(new MailAddress(to));
            mail.From = new MailAddress(from);
            mail.Subject = "Serviço de vigia de preço!";
            mail.Body = mensagem;

            return mail;
        }
    }
}
