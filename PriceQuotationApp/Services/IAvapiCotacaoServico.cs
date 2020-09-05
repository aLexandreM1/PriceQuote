using PriceQuotationApp.Domain;

namespace PriceQuotationApp.Services
{
    public interface IAvapiCotacaoServico
    {
        public Ativo CotarPrecoAtivo(string ativoString);
    }
}