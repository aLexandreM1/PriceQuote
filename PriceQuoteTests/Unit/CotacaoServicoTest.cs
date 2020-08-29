using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PriceQuotationApp.Domain;
using PriceQuotationApp.Services;
using System;
using System.Collections.Generic;

namespace PriceQuoteTests.Unit
{
    [TestClass]
    public class CotacaoServicoTest
    {
        [TestMethod]
        public void EncontrarCotacaoMaisRecente_AtivoComCotacaoDoDia_DeveRetornarCotacaoDoDia()
        {
            var ativo = new Ativo()
            {
                Informacao = "A",
                Cotacoes = new List<Cotacao>(),
                Simbolo = "PETR4",
                TimeZone = "GMT-3",
                TipoOutput = "Compact",
                UltimaAtualizacao = DateTime.Now
            };

            Cotacao cotacao = new Cotacao()
            {
                Abertura = 10.10f,
                DateTime = DateTime.Now,
                Fechamento = 10.10f,
                Maxima = 12.12f,
                Minima = 9.9f,
                Volume = 12637678
            };

            ativo.Cotacoes.Add(cotacao);

            var result = CotacaoServico.EncontrarCotacaoMaisRecente(ativo);

            Assert.AreEqual(result, cotacao);
        }


        [TestMethod]
        public void EncontrarCotacaoMaisRecente_AtivoComCotacaoDoDiaAnterior_DeveRetornarCotacaoDoDiaAnteior()
        {
            var ativo = new Ativo()
            {
                Informacao = "A",
                Cotacoes = new List<Cotacao>(),
                Simbolo = "PETR4",
                TimeZone = "GMT-3",
                TipoOutput = "Compact",
                UltimaAtualizacao = DateTime.Now
            };

            Cotacao cotacao = new Cotacao()
            {
                Abertura = 10.10f,
                DateTime = DateTime.Now.AddDays(-1),
                Fechamento = 10.10f,
                Maxima = 12.12f,
                Minima = 9.9f,
                Volume = 12637678
            };

            ativo.Cotacoes.Add(cotacao);

            var result = CotacaoServico.EncontrarCotacaoMaisRecente(ativo);

            Assert.AreEqual(result, cotacao);
        }

        [TestMethod]
        public void EncontrarCotacaoMaisRecente_AtivoSemCotacao_DeveRetornarNull()
        {
            var ativo = new Ativo()
            {
                Informacao = "A",
                Cotacoes = new List<Cotacao>(),
                Simbolo = "PETR4",
                TimeZone = "GMT-3",
                TipoOutput = "Compact",
                UltimaAtualizacao = DateTime.Now
            };

            var result = CotacaoServico.EncontrarCotacaoMaisRecente(ativo);

            Assert.IsNull(result);
        }
    }
}
