using EllipticCurve.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriceQuotationApp.Services
{
    public class ValidacaoServico
    {
        public static bool InputValido(string[] args)
        {
            if (args == null || args.Length != 3)
                return false;
            return true;
        }

        public static bool InputAtivoValido(string ativo)
        {
            bool primeirasQuatroLetras = char.IsLetter(ativo[0]) && char.IsLetter(ativo[1]) && char.IsLetter(ativo[2]) && char.IsLetter(ativo[3]);

            if (ativo == null || ativo.Length != 5 || !char.IsNumber(ativo[4]) || !primeirasQuatroLetras)
                return false;
            return true;
        }

        public static bool InputPrecoValido(string precoMin, string precoMax)
        {
            if (!(float.TryParse(precoMin, out _) || float.TryParse(precoMax, out _)))
                return false;
            return true;
        }
    }
}
