using PriceQuotationApp.Services;
using System;
using System.Threading;

namespace PriceQuotationApp
{
    public class Start
    {
        public static void Main(string[] args)
        {
            CotacaoServico cotacaoServico = new CotacaoServico(new AvapiCotacaoServico(), new NotificacaoServico());

            if (!ValidacaoServico.InputValido(args))
            {
                Console.WriteLine("Erro dectectado nos inputs");
                System.Environment.Exit(1);
            }
            if (!ValidacaoServico.InputAtivoValido(args[0]))
            {
                Console.WriteLine("Erro dectectado no input do ativo");
                System.Environment.Exit(2);
            }
            if (!ValidacaoServico.InputPrecoValido(args[1], args[2]))
            {
                Console.WriteLine("Erro dectectado no input do preço");
                System.Environment.Exit(3);
            }
            Console.WriteLine(args);

            Timer timer = null;
            timer = new Timer((e) =>
                {
                    if (cotacaoServico.IniciarServicoDeVigiaDePreco(args[0], args[1], args[2]))
                    {
                        timer.Dispose();
                    }
                }, null, TimeSpan.Zero, TimeSpan.FromMinutes(0.30));
            Console.ReadLine(); //Hack to not stop console app
        }
    }
}