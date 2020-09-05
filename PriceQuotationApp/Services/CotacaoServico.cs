using PriceQuotationApp.Domain;
using System;
using System.Linq;

namespace PriceQuotationApp.Services
{
    public class CotacaoServico
    {
        IAvapiCotacaoServico avapiService;
        INotificacaoServico notificacaoServico;

        public CotacaoServico(IAvapiCotacaoServico paramAvapiService, INotificacaoServico paramNotificacaoServico) 
        {
            avapiService = paramAvapiService;
            notificacaoServico = paramNotificacaoServico;
        }


        public bool IniciarServicoDeVigiaDePreco(string simbolo, string precoMaximo, string precoMinimo)
        {

            float precoMaximoDesejado = float.Parse(precoMaximo);
            float precoMinimoDesejado = float.Parse(precoMinimo);
            Ativo ativo = avapiService.CotarPrecoAtivo(simbolo);
            Cotacao cotacaoDoDia = EncontrarCotacaoMaisRecente(ativo);

            if (cotacaoDoDia.Maxima >= precoMaximoDesejado || cotacaoDoDia.Minima <= precoMinimoDesejado)
            {
                string condicao = cotacaoDoDia.Maxima >= precoMaximoDesejado ? "superior" : "inferior";
                float precoDesejado = cotacaoDoDia.Maxima >= precoMaximoDesejado ? precoMaximoDesejado : precoMinimoDesejado;
                notificacaoServico.NotificarPreco(ativo.Simbolo, condicao, precoDesejado);
                return true;
            }
            return false;
        }

        public static Cotacao EncontrarCotacaoMaisRecente(Ativo ativo)
        {
            if (ativo.Cotacoes.Count == 0)
                return null;
            var cotacaoDoDia = ativo.Cotacoes.Find(cotacao => cotacao.DateTime == DateTime.Today);
            if (cotacaoDoDia == null)
            {
                cotacaoDoDia = ativo.Cotacoes.First();
                Console.WriteLine("Cotação do dia não encontrada, exibindo ultima cotação disponível: {0}", cotacaoDoDia.DateTime);
            }

            return cotacaoDoDia;
        }
    }
}
