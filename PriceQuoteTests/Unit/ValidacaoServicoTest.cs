
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PriceQuotationApp.Services;

namespace PriceQuoteTests.Unit
{
    [TestClass]
    public class ValidacaoServicoTest
    {

        [TestMethod]
        public void InputValido_ComArrayVazio_DeveRetornarFalse()
        {
            var result = ValidacaoServico.InputValido(new string[] { });

            Assert.IsFalse(result, "result deve ser false");
        }

        [TestMethod]
        public void InputValido_ComArrayPelaMetade_DeveRetornarFalse()
        {
            var result = ValidacaoServico.InputValido(new string[] { "PETR4", });

            Assert.IsFalse(result, "result deve ser false");
        }

        [TestMethod]
        public void InputValido_ComArrayCompleto_DeveRetornarTrue()
        {
            var result = ValidacaoServico.InputValido(new string[] { "PETR4","22.31", "23.34" });

            Assert.IsTrue(result, "result deve ser true");
        }

        [TestMethod]
        public void InputAtivoValido_AtivoCerto_DeveRetornarTrue()
        {
            var result = ValidacaoServico.InputAtivoValido("PETR4");

            Assert.IsTrue(result, "result deve ser true");
        }

        [TestMethod]
        public void InputAtivoValido_AtivoSemNumeroNoFinal_DeveRetornarFalse()
        {
            var result = ValidacaoServico.InputAtivoValido("PETRA");

            Assert.IsFalse(result, "result deve ser false");
        }

        [TestMethod]
        public void InputAtivoValido_AtivoComMaisDeUmNumero_DeveRetornarFalse()
        {
            var result = ValidacaoServico.InputAtivoValido("PET54");

            Assert.IsFalse(result, "result deve ser false");
        }

        [TestMethod]
        public void InputAtivoValido_AtivoCertoPoremGrande_DeveRetornarFalse()
        {
            var result = ValidacaoServico.InputAtivoValido("PETR4A");

            Assert.IsFalse(result, "result deve ser false");
        }

        [TestMethod]
        public void InputPrecoValido_PrecoCerto_DeveRetornarTrue()
        {
            var result = ValidacaoServico.InputPrecoValido("21.23", "1.40");

            Assert.IsTrue(result, "result deve ser true");
        }
        
        [TestMethod]
        public void InputPrecoValido_PrecoCertoGrande_DeveRetornarTrue()
        {
            var result = ValidacaoServico.InputPrecoValido("21.2323132", "5.425121");

            Assert.IsTrue(result, "result deve ser true");
        }


        [TestMethod]
        public void InputPrecoValido_PrecoErrado_DeveRetornarFalse()
        {
            var result = ValidacaoServico.InputPrecoValido("2AB1.2323132", "5.4DDD");

            Assert.IsFalse(result, "result deve ser false");
        }

    }
}
