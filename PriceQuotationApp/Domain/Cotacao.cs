using System;

namespace PriceQuotationApp.Domain
{
    public class Cotacao
    {
        public DateTime DateTime { get; set; }
        public float Abertura { get; set; }
        public float Maxima { get; set; }
        public float Minima { get; set; }
        public float Fechamento{ get; set; }
        public int Volume { get; set; }
    }
}
