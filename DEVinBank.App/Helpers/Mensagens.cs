using DEVinBank.App.Entidades;
using DEVinBank.App.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEVinBank.App.Helpers
{
    public class Mensagens
    {
        public static void Erro(Exception ex)
        {
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("[ERROR]: " + ex.Message + "\n");
            System.Console.ResetColor();
        }

        public static void ProcessoFinalizado()
        {
            System.Console.ForegroundColor = ConsoleColor.Blue;
            System.Console.WriteLine("[FIM]: O DEVinBank agradece pelo interesse. Até mais!\n");
            System.Console.ResetColor();
        }

        public static void ContaCriada(Conta conta, string tipoConta)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Parabéns {conta.Nome}. Você acaba de criar uma {tipoConta} no DEVinBank" +
                                $" na agencia {conta.Agencia}.\n");
            Console.ResetColor();
        }
        
    }
}
