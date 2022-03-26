using DEVinBank.App.Entidades;
using DEVinBank.App.Helpers;
using DEVinBank.App.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEVinBank.App.Engines
{
    public class MinhaContaEngine : IMinhaContaEngine
    {
        private readonly IContasRepository _gerenciadorDeContas;
        private readonly IOperacoesEngine _operacoesEngine;
        private Conta _conta;
        public MinhaContaEngine(IContasRepository gerenciadorDeConta,
            IOperacoesEngine operacoesEngine)
        {
            _gerenciadorDeContas = gerenciadorDeConta;
            _operacoesEngine = operacoesEngine; 
        }
        public bool Acessar()
        {
            Console.WriteLine("Digite o número da conta:\n");
            string resposta = Aplicacao.RecebeResposta();

            _conta = _gerenciadorDeContas.RetornarElemento(resposta);

            if (_conta is ContaCorrente) return MostrarOpcoesContaCorrente();
            if (_conta is ContaPoupanca) return MostrarOpcoesContaPoupanca();
            if (_conta is ContaInvestimento) return MostrarOpcoesContaInvestimento();
            return false;
        }
        public bool ProximaOperacao()
        {
            do
            {
                Console.WriteLine(
                "Deseja realizar outra operação nesta conta?\n" +
                "\n" +
                "[1] - Sim\n" +
                "[2] - Não\n");
                string resposta = Aplicacao.RecebeResposta();
                if (resposta == "1") break;
                if (resposta == "2") return true;
            }
            while (true);

            if (_conta is ContaCorrente) return MostrarOpcoesContaCorrente();
            if (_conta is ContaPoupanca) return MostrarOpcoesContaPoupanca();
            if (_conta is ContaInvestimento) return MostrarOpcoesContaInvestimento();
            return false;
        }

        private bool MostrarOpcoesContaCorrente()
        {
            Console.WriteLine(
            $"Olá, {_conta.Nome}! Informe o que deseja:\n\n" +
            "[1] - Saque\n" +
            "[2] - Depósito\n" +
            "[3] - Transferência\n" +
            "[4] - Extrato\n" +
            "[5] - Saldo\n" +
            "[6] - Alterar Dados\n" +
            "[0] - Sair\n");

            string resposta = Aplicacao.RecebeResposta();

            try
            {
                switch (resposta)
                {
                    case "1":
                        _operacoesEngine.Saque(_conta);
                        break;
                    case "2":
                        _operacoesEngine.Deposito(_conta);
                        break;
                    case "3":
                        _operacoesEngine.Transferencia(_conta); 
                        break;
                    case "4":
                        _operacoesEngine.Extrato(_conta);
                        break;
                    case "5":
                        _operacoesEngine.Saldo(_conta);
                        break;
                    case "6":
                        _operacoesEngine.AlterarDados(_conta);
                        break;
                    case "0":
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensagens.Erro(ex.Message);  
            }
            return false;
        }
        private bool MostrarOpcoesContaPoupanca()
        {
            Console.WriteLine(
            $"Olá, {_conta.Nome}! Informe o que deseja:\n\n" +
            "[1] - Saque\n" +
            "[2] - Depósito\n" +
            "[3] - Transferência\n" +
            "[4] - Extrato\n" +
            "[5] - Saldo\n" +
            "[6] - Simular rendimento\n" +
            "[7] - Alterar Dados\n" +
            "[0] - Sair\n");

            string resposta = Aplicacao.RecebeResposta();

            try
            {
                switch (resposta)
                {
                    case "1":
                        _operacoesEngine.Saque(_conta);
                        break;
                    case "2":
                        _operacoesEngine.Deposito(_conta);
                        break;
                    case "3":
                        _operacoesEngine.Transferencia(_conta);
                        break;
                    case "4":
                        _operacoesEngine.Extrato(_conta);
                        break;
                    case "5":
                        _operacoesEngine.Saldo(_conta);
                        break;
                    case "6":
                        _operacoesEngine.SimularRendimento((ContaPoupanca)_conta);
                        break;
                    case "7":
                        _operacoesEngine.AlterarDados(_conta);
                        break;
                    case "0":
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensagens.Erro(ex.Message);
            }
            return false;
        }
        private bool MostrarOpcoesContaInvestimento()
        {
            Console.WriteLine(
            $"Olá, {_conta.Nome}! Informe o que deseja:\n\n" +
            "[1] - Saque\n" +
            "[2] - Depósito\n" +
            "[3] - Transferência\n" +
            "[4] - Extrato\n" +
            "[5] - Saldo\n" +
            "[6] - Simular investimento\n" +
            "[7] - Simular resgate do investimento\n" +
            "[8] - Alterar Dados\n" +
            "[0] - Sair\n");

            string resposta = Aplicacao.RecebeResposta();

            try
            {
                switch (resposta)
                {
                    case "1":
                        _operacoesEngine.Saque(_conta);
                        break;
                    case "2":
                        _operacoesEngine.Deposito(_conta);
                        break;
                    case "3":
                        _operacoesEngine.Transferencia(_conta);
                        break;
                    case "4":
                        _operacoesEngine.Extrato(_conta);
                        break;
                    case "5":
                        _operacoesEngine.Saldo(_conta);
                        break;
                    case "6":
                        _operacoesEngine.SimularInvestimento((ContaInvestimento)_conta);
                        break;
                    case "7":
                        _operacoesEngine.SimularResgate((ContaInvestimento)_conta);
                        break;
                    case "8":
                        _operacoesEngine.AlterarDados(_conta);
                        break;
                    case "0":
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensagens.Erro(ex.Message);
            }
            return false;
        }
    }
}
