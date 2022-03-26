using DEVinBank.App.Entidades;
using DEVinBank.App.Exceptions;
using DEVinBank.App.Helpers;
using DEVinBank.App.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEVinBank.Sistema
{
    public class SistemaFinanceiro
    {
        private readonly ICriacaoContaEngine _criadorDeConta;
        private readonly IMinhaContaEngine _minhaConta;   
        private readonly IContasRepository _gerenciadorDeContas;
        private readonly IOperacoesEngine _operacoes;   

        public DateOnly DataSistema { get; private set; }  

        public SistemaFinanceiro(ICriacaoContaEngine criadorDeConta, IMinhaContaEngine minhaConta,
             IContasRepository gerenciadorDeContas, IOperacoesEngine operacoes)
        {
            _criadorDeConta = criadorDeConta;
            _gerenciadorDeContas = gerenciadorDeContas;
            _minhaConta = minhaConta;
            _operacoes = operacoes;
            DataSistema = DateOnly.FromDateTime(DateTime.Now); 
        }

        public bool BoasVindas()
        {
            Console.WriteLine("\n\n\nOlá! Bem-vindo(a) ao DEVinBank.\n");
            return MostrarMenuOpcoes();
        }

        public bool ProximaOperacao()
        {
            Console.WriteLine("Deseja realizar outra operação no DEVinBank?\n" +
                "\n" +
                "[1] - Sim\n" +
                "[2] - Não\n");
            var resposta = Aplicacao.RecebeResposta();
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
        }

        #region Menu de Opções
        private bool MostrarMenuOpcoes()
        {
            Console.WriteLine(
            "Informe o que deseja:\n\n" +
            "[1] - Abrir uma conta\n" +
            "[2] - Listar contas\n" +
            "[3] - Listar contas negativadas\n" +
            "[4] - Total de investimentos no DEVinBank\n" +
            "[5] - Minha conta\n" +
            "[6] - Realizar depósito\n" +
            "[0] - Sair\n");

            string resposta = Aplicacao.RecebeResposta();
            try
            {
                switch (resposta)
                {
                    case "1":
                        _criadorDeConta.GetTipoConta();
                        break;
                    case "2":
                        _gerenciadorDeContas.Listar();
                        break;
                    case "3":
                        _gerenciadorDeContas.ListarContasNegativadas();
                        break;
                    case "4":
                        Console.WriteLine($"" +
                            $"O valor total investido no DEVinHouse é de:\n" +
                            $"\n" +
                            $"{_operacoes.TotalInvestimentosDEVinBank():C2}\n");
                        break;
                    case "5":
                        bool finalizado = _minhaConta.Acessar();
                        while (!finalizado)
                        {
                            finalizado = _minhaConta.ProximaOperacao();
                        }
                        break;
                    case "6":
                        _operacoes.RealizarDeposito();
                        break;
                    case "0":
                        Mensagens.ProcessoFinalizado();
                        return true;
                    default:
                        throw new OpcaoInvalidaException();
                }
                return false;
            }
            catch (Exception ex)
            {
                if(ex is CadastroException)
                {
                    Mensagens.Erro(ex.Message);
                    return false;
                }
                else if(ex is OpcaoInvalidaException)
                {
                    Mensagens.Erro(ex.Message);
                    return MostrarMenuOpcoes();
                }
                else if(ex is FimCadastroException) Mensagens.ProcessoFinalizado();
                else Mensagens.Erro(ex.Message);  
                return false;    
            }
        }
        #endregion
    }
}
