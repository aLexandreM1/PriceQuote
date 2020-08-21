using PriceQuotationApp.Services;
using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace PriceQuotationApp
{
    class Start
    {
        public static void Main(string[] args)
        {
            if(!ValidacaoServico.InputValido(args))
            {
                Console.WriteLine("Erro dectectado nos inputs");
                System.Environment.Exit(1);
            }
            if (!ValidacaoServico.InputAtivoValido(args[0]))
            {
                Console.WriteLine("Erro dectectado no input do ativo");
                System.Environment.Exit(2);
            }
            if (!ValidacaoServico.InputPriceValido(args[1], args[2]))
            {
                Console.WriteLine("Erro dectectado no input do preço");
                System.Environment.Exit(3);
            }
            Console.WriteLine(args);

            //TESTES <-


            //Timer timer = null;
            //timer = new Timer((e) =>
            //    {
            //        if(IniciarServicoDeProcuraDePreco())
            //        {
            //            timer.Dispose();
            //        }
            //    }, null, TimeSpan.Zero, TimeSpan.FromMinutes(0.30));
            //Console.ReadLine(); //Hack to not stop console app
        }
        public static bool IniciarServicoDeProcuraDePreco()
        {
            var config = System.Configuration.ConfigurationManager.AppSettings;
            string simbolo = "ITUB5";
            simbolo += ".SA"; //Suffix needed for AlphaVantage
            string precoMaximo = "28.31";
            string precoMinimo = "22.24";

            float precoMaximoDesejado = float.Parse(precoMaximo);
            float precoMinimoDesejado = float.Parse(precoMinimo);
            var ativo = AvapiQuotacaoServico.CotacaoPrecoAtivo(simbolo);
            var cotacaoDoDia = ativo.Cotacoes.Find(cotacao => cotacao.DateTime == DateTime.Today);
            if (cotacaoDoDia.Maxima >= precoMaximoDesejado || cotacaoDoDia.Minima <= precoMinimoDesejado)
            {
                string condicao = cotacaoDoDia.Maxima >= precoMaximoDesejado ? "superior" : "inferior";
                float precoDesejado = cotacaoDoDia.Maxima >= precoMaximoDesejado ? precoMaximoDesejado : precoMinimoDesejado;
                NotificacaoServico.NotificarPreco(ativo.Simbolo, condicao, precoDesejado);
                return true;
            }
            return false;
        }
    }
}