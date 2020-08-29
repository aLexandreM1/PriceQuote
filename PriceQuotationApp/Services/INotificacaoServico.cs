using System.Text;

namespace PriceQuotationApp.Services
{
    public interface INotificacaoServico
    {
        public void NotificarPreco(string ativo, string condicao, float precoDesejado);
    }
}