using Avapi;
using Avapi.AvapiTIME_SERIES_DAILY;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json;

namespace PriceQuotationApp
{
    class AvapiQuotacaoServico
    {
        public static Ativo CotacaoPrecoAtivo(string ativoString)
        {
            var config = System.Configuration.ConfigurationManager.AppSettings;
            string apiKey = config.Get("apikeyAV");
            // Creating the connection object
            IAvapiConnection connection = AvapiConnection.Instance;

            // Set up the connection and pass the API_KEY provided by alphavantage.co
            connection.Connect(apiKey);

            // Get the TIME_SERIES_DAILY query object
            Int_TIME_SERIES_DAILY time_series_daily =
                connection.GetQueryObject_TIME_SERIES_DAILY();

            // Perform the TIME_SERIES_DAILY request and get the result
            IAvapiResponse_TIME_SERIES_DAILY time_series_dailyResponse =
            time_series_daily.Query(
                 ativoString, //Work SA prices
                 Const_TIME_SERIES_DAILY.TIME_SERIES_DAILY_outputsize.compact);
            Console.WriteLine(time_series_dailyResponse);

            //var x = JsonSerializer.Serialize(time_series_dailyResponse.Data.MetaData);
            //var w = JsonConvert.DeserializeObject<Ativo>(time_series_dailyResponse.RawData);
            //var y = JsonSerializer.Serialize(time_series_dailyResponse.Data.TimeSeries);
            //List<TimeSeries> timeSeries = JsonSerializer.Deserialize<List<TimeSeries>>(y);
            var ativo = MapeamentoServico.MapearParaAtivo(time_series_dailyResponse.Data.MetaData);
            var cotacoes = MapeamentoServico.MapearParaCotacoes(time_series_dailyResponse.Data.TimeSeries);
            //Ativo ativo = JsonSerializer.Deserialize<Ativo>(x);
            ativo.Cotacoes = cotacoes;

            return ativo;
        }
    }
}
