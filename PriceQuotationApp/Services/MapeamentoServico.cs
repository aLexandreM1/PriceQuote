using Avapi.AvapiTIME_SERIES_DAILY;
using System;
using System.Collections.Generic;

namespace PriceQuotationApp
{
    class MapeamentoServico
    {
        public static Ativo MapearParaAtivo(MetaData_Type_TIME_SERIES_DAILY MetaData)
        {
            Ativo ativo = new Ativo
            {
                Informacao = MetaData.Information,
                Simbolo = MetaData.Symbol,
                TimeZone = MetaData.TimeZone,
                TipoOutput = MetaData.OutputSize,
                UltimaAtualizacao = MetaData.LastRefreshed
            };

            return ativo;
        }

        public static List<Cotacao> MapearParaCotacoes(IList<TimeSeries_Type_TIME_SERIES_DAILY> timeSeries)
        {
            List<Cotacao> cotacoes = new List<Cotacao>();

            foreach (TimeSeries_Type_TIME_SERIES_DAILY timeSerie in timeSeries) 
            {
                Cotacao cotacao = new Cotacao
                {
                    DateTime = Convert.ToDateTime(timeSerie.DateTime),
                    Abertura = float.Parse(timeSerie.open),
                    Fechamento = float.Parse(timeSerie.close),
                    Maxima = float.Parse(timeSerie.high),
                    Minima = float.Parse(timeSerie.low),
                    Volume = Int32.Parse(timeSerie.volume)
                };
                cotacoes.Add(cotacao);
            }

            return cotacoes;
        }
    }
}