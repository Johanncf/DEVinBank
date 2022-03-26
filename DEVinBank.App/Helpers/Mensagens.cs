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
        public static void DEVinBank(string mensagem)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"[DEVinBank]: {mensagem}\n");
            Console.ResetColor();
        }
        
        public static void Erro(string mensagem)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ERRO]: " + mensagem + "\n");
            Console.ResetColor();
        }

        public static void ProcessoFinalizado()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("[FIM]: O DEVinBank agradece pelo interesse. Até mais!\n");
            Console.ResetColor();
        }
        public static void ContaCriada(Conta conta, string tipoConta)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Parabéns {conta.Nome.Split(" ").ToList()[0]}! Você acaba de criar uma {tipoConta} no DEVinBank.\n\n" +
                                $"Dados da sua conta:\n" +
                                $"\tAgência - {(int)conta.Agencia:X3} ({conta.Agencia})\n" +
                                $"\tConta - {conta.NumeroConta}\n\n");
            Console.ResetColor();
        }
        public static void OperacaoRealizadaComSucesso(string mensagem)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(mensagem);    
            Console.ResetColor();
        }
        
    }
}
