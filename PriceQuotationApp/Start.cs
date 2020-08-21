using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;
using System;

namespace PriceQuotationApp
{
    class Start
    {
        public static void Main(string[] args)
        {
            var config = System.Configuration.ConfigurationManager.AppSettings;
            string simbolo = "PETR4";
            simbolo += ".SA"; //Suffix needed for AlphaVantage
            bool naoNotificou = true;
            string precoMaximo = "28.31";
            string precoMinimo = "21.24";

            while (naoNotificou)
            {
                float precoMaximoDesejado = float.Parse(precoMaximo);
                float precoMinimoDesejado = float.Parse(precoMinimo);
                var ativo = AvapiQuotacaoServico.CotacaoPrecoAtivo(simbolo);
                var cotacaoDoDia = ativo.Cotacoes.Find(cotacao => cotacao.DateTime == DateTime.Today);
                if (cotacaoDoDia.Maxima >= precoMaximoDesejado)
                {
                    NotificacaoServico.NotificarPrecoSMTP(
                        String.Format(
                        "Preço do ativo: {0} no alvo ou superior ao desejado: {1}", ativo.Simbolo ,precoMaximoDesejado)
                        );
                    naoNotificou = false;
                }
                if (cotacaoDoDia.Minima <= precoMinimoDesejado)
                {
                    NotificacaoServico.NotificarPrecoSMTP(
                        String.Format(
                        "Preço do ativo: {0} no alvo ou inferior ao desejado: {1}", ativo.Simbolo, precoMinimoDesejado)
                        );
                    naoNotificou = false;
                }
            }
        }
    }
}