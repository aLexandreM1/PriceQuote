using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PriceQuotationApp;
using PriceQuotationApp.Domain;
using PriceQuotationApp.Services;
using System;
using System.Collections.Generic;

namespace PriceQuoteTests.Integration
{
    [TestClass]
    public class CotacaoServicoTest
    {

        [TestMethod]
        public void IniciarServicoDeVigiaDePrecoTest()
        {
            var mock = new Mock<IAvapiCotacaoServico>();
            mock.Setup(mock => mock.CotacaoPrecoAtivo("PETR4"));
            mock.Object.CotacaoPrecoAtivo("PETR4");

            mock.Verify(mock => mock.CotacaoPrecoAtivo("PETR4"));

        }

        [TestMethod]
        public void IniciarServicoDeVigiaDePrecoTest_AtivoComCotacaoAcimaDoDesejado_DeveRetornarTrue()
        {
            var mockCotacaoServico = new Mock<IAvapiCotacaoServico>();
            mockCotacaoServico.Setup(mock => mock.CotacaoPrecoAtivo("PETR4")).Returns(new Ativo()
            {
                Simbolo = "PETR4",
                Cotacoes = new List<Cotacao>() { new Cotacao() 
                    { 
                        Volume = 12687516,
                        Abertura = 24.25f,
                        DateTime = DateTime.Now,
                        Fechamento = 24.72f,
                        Maxima = 25.42f,
                        Minima = 22.88f
                    }
                },
                Informacao = "Info",
                TimeZone = "GMT -3",
                TipoOutput = "Compact",
                UltimaAtualizacao = DateTime.Now
            });


            var mockNotificacaoServico = new Mock<INotificacaoServico>();
            mockNotificacaoServico.Setup(mock => mock.NotificarPreco("PETR4", "superior", 25.30f));

            var cotacaoServico = new CotacaoServico(mockCotacaoServico.Object, mockNotificacaoServico.Object);

            var result = cotacaoServico.IniciarServicoDeVigiaDePreco("PETR4", "25.30","22.00");

            mockCotacaoServico.Verify(x => x.CotacaoPrecoAtivo("PETR4"), Times.Once());
            mockNotificacaoServico.Verify(x => x.NotificarPreco("PETR4", "superior", 25.30f), Times.Once());

            Assert.IsTrue(result, "result deve ser true");
        }

        [TestMethod]
        public void IniciarServicoDeVigiaDePrecoTest_AtivoComCotacaoAbaixoDoDesejado_DeveRetornarTrue()
        {
            var mockCotacaoServico = new Mock<IAvapiCotacaoServico>();
            mockCotacaoServico.Setup(mock => mock.CotacaoPrecoAtivo("PETR4")).Returns(new Ativo()
            {
                Simbolo = "PETR4",
                Cotacoes = new List<Cotacao>() { new Cotacao()
                    {
                        Volume = 12687516,
                        Abertura = 24.25f,
                        DateTime = DateTime.Now,
                        Fechamento = 24.72f,
                        Maxima = 25.42f,
                        Minima = 21.88f
                    }
                },
                Informacao = "Info",
                TimeZone = "GMT -3",
                TipoOutput = "Compact",
                UltimaAtualizacao = DateTime.Now
            });


            var mockNotificacaoServico = new Mock<INotificacaoServico>();
            mockNotificacaoServico.Setup(mock => mock.NotificarPreco("PETR4", "inferior", 22.00f));

            var cotacaoServico = new CotacaoServico(mockCotacaoServico.Object, mockNotificacaoServico.Object);

            var result = cotacaoServico.IniciarServicoDeVigiaDePreco("PETR4", "26.30", "22.00");

            mockCotacaoServico.Verify(x => x.CotacaoPrecoAtivo("PETR4"), Times.Once());
            mockNotificacaoServico.Verify(x => x.NotificarPreco("PETR4", "inferior", 22.00f), Times.Once());

            Assert.IsTrue(result, "result deve ser true");
        }

        [TestMethod]
        public void IniciarServicoDeVigiaDePrecoTest_AtivoComCotacaoEntreOsValoresDesejados_DeveRetornarFalse()
        {
            var mockCotacaoServico = new Mock<IAvapiCotacaoServico>();
            mockCotacaoServico.Setup(mock => mock.CotacaoPrecoAtivo("PETR4")).Returns(new Ativo()
            {
                Simbolo = "PETR4",
                Cotacoes = new List<Cotacao>() { new Cotacao()
                    {
                        Volume = 12687516,
                        Abertura = 24.25f,
                        DateTime = DateTime.Now,
                        Fechamento = 24.72f,
                        Maxima = 25.42f,
                        Minima = 22.88f
                    }
                },
                Informacao = "Info",
                TimeZone = "GMT -3",
                TipoOutput = "Compact",
                UltimaAtualizacao = DateTime.Now
            });


            var mockNotificacaoServico = new Mock<INotificacaoServico>();
            mockNotificacaoServico.Setup(mock => mock.NotificarPreco("PETR4", "inferior", 22.00f));

            var cotacaoServico = new CotacaoServico(mockCotacaoServico.Object, mockNotificacaoServico.Object);
            

            var result = cotacaoServico.IniciarServicoDeVigiaDePreco("PETR4", "26.30", "22.00");

            mockCotacaoServico.Verify(x => x.CotacaoPrecoAtivo("PETR4"), Times.Once());
            mockNotificacaoServico.Verify(x => x.NotificarPreco("PETR4", "inferior", 22.00f), Times.Never());
            

            Assert.IsFalse(result, "result deve ser false");
        }

    }
}