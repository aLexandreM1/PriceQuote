using System;
using System.Collections.Generic;

namespace PriceQuotationApp
{
    class Ativo
    {
        public string Informacao { get; set; }
        public string Simbolo { get; set; }
        public DateTime UltimaAtualizacao { get; set; }
        public string TipoOutput { get; set; }
        public string TimeZone { get; set; }
        public virtual List<Cotacao> Cotacoes { get; set; }
    }
}
