using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PriceQuotationApp.Services;

namespace PriceQuoteTests.Integration
{
    [TestClass]
    public class NotificacaoServicoTest
    {

        [TestMethod]
        public void ChamarMetodoNotificarPreco()
        {
            var mock = new Mock<INotificacaoServico>();
            mock.Setup(mock => mock.NotificarPreco("AA", "BB", 22.12f));
            mock.Object.NotificarPreco("AA", "BB", 22.12f);

            mock.Verify(x => x.NotificarPreco("AA", "BB", 22.12f), Times.Once());
        }
    }
}