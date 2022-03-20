using DEVinBank.App.Entidades;
using DEVinBank.App.Exceptions;
using DEVinBank.App.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEVinBank.Sistema
{
    public class SistemaEngine
    {
        public static bool BoasVindas()
        {
            Console.WriteLine("Olá! Bem-vindo(a) ao DEVinBank.");
            return MostrarMenuOpcoes();
        }

        public static bool ProximaOperacao()
        {
            Console.WriteLine("Deseja realizar outra operação?\n" +
                "[1] - Sim\n" +
                "[2] - Não\n");
            var resposta = Console.ReadLine();
            switch (resposta)
            {
                case "1":
                    return MostrarMenuOpcoes();
                case "2":
                    Mensagens.ProcessoFinalizado();
                    return true;
                default:
                    return ProximaOperacao();  
            }
            
            //if (resposta == "1") return MostrarMenuOpcoes();
            //else return true;
        }

        #region Menu de Opções
        private static bool MostrarMenuOpcoes()
        {
            Console.WriteLine(
            "Informe o que deseja:\n\n" +
            "[1] - Abrir uma conta corrente.\n" +
            "[2] - Abrir uma conta poupança.\n" +
            "[3] - Abrir uma conta investimento.\n" +
            "[0] - Sair.\n");

            var resposta = Aplicacao.RecebeComando();
            try
            {
                switch (resposta)
                {
                    case "1":
                        var contaCorrente = CriacaoConta.ContaCorrente();
                        Console.WriteLine(contaCorrente.NumeroConta);
                        break;
                    case "2":
                        var contaPoupanca = CriacaoConta.ContaPoupanca();
                        Console.WriteLine(contaPoupanca.NumeroConta);
                        break;
                    case "3":
                        var contaInvestimento = CriacaoConta.ContaInvestimento();
                        Console.WriteLine(contaInvestimento.NumeroConta);
                        break;
                    case "0":
                        Mensagens.ProcessoFinalizado();
                        return true;
                    default:
                        return MostrarMenuOpcoes();
                }
                return false;
            }
            catch (Exception ex)
            {
                if(ex is CadastroException)
                {
                    Mensagens.Erro(ex);
                    return false;
                }
                if(ex is FimCadastroException)
                {
                    Mensagens.ProcessoFinalizado();
                    return true;
                }
                return true;    
            }
        }
        #endregion
    }
}
