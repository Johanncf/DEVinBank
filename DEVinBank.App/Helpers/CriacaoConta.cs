using DEVinBank.App.Entidades;
using DEVinBank.App.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DEVinBank.App.Enums.Enumerators;

namespace DEVinBank.App.Helpers
{
    public class CriacaoConta
    {
        private static string _tipoConta { get; set; } = "";
        private static string _nome { get; set; }
        private static string _cpf { get; set; }
        private static string _endereco { get; set; }
        private static double _rendaMensal { get; set; }
        private static AgenciaEnum _agencia { get; set; }
        private static double _saldo { get; set; }

        #region Construtor: Criação de atributos padrão para Contas
        private static void ContaPadrao()
        {
            Console.WriteLine("[DEVinBank]: Ok. Vou precisar de alguns dados pessoais, tudo bem?\n\n" +
                            "[1] - Prosseguir.\n" +
                            "[0] - Cancelar.\n");

            var resposta = Aplicacao.RecebeComando();

            if (resposta != "1")
            {
                throw new FimCadastroException();
            }

            Console.Write("Seu nome como você gostaria de ver no cartão: ");
            resposta = Console.ReadLine();
            if (string.IsNullOrEmpty(resposta)) throw new CadastroException("Nome não informado.");
            _nome = resposta;
            Console.WriteLine("\n");

            Console.Write("Seu CPF: ");
            resposta = Console.ReadLine();
            if (string.IsNullOrEmpty(resposta)) throw new CadastroException("CPF não informado.");
            _cpf = resposta;
            Console.WriteLine("\n");

            Console.Write("Seu endereço: ");
            resposta = Console.ReadLine();
            if (string.IsNullOrEmpty(resposta)) throw new CadastroException("Nome não informado.");
            _endereco = resposta;
            Console.WriteLine("\n");

            Console.Write("Sua renda mensal: ");
            try
            {
                _rendaMensal = Convert.ToDouble(Console.ReadLine());
            }
            catch
            {
                throw new CadastroException("Renda mensal não informada ou com formato inconsistente");
            }
            Console.WriteLine("\n");

            Console.WriteLine("Escolha uma das seguintes agências: \n" +
                "[1] - Florianópolis\n" +
                "[2] - São José\n" +
                "[3] - Biguaçu\n");
            resposta = Console.ReadKey().KeyChar.ToString();
            Console.WriteLine("\n");
            switch (resposta)
            {
                case "1":
                    _agencia = AgenciaEnum.Florianopolis;
                    break;
                case "2":
                    _agencia = AgenciaEnum.SaoJose;
                    break;
                case "3":
                    _agencia = AgenciaEnum.Biguacu;
                    break;
                default:
                    throw new CadastroException("Agência não informada.");
            }

            Console.WriteLine("Deseja depositar algum valor inicial na conta?\n" +
                "[1] - Sim\n" +
                "[2] - Não\n");
            resposta = Console.ReadKey().KeyChar.ToString();
            Console.WriteLine("\n");
            if(resposta == "1")
            {
                Console.Write("Digite o valor do depósito: ");
                try
                {
                    _saldo = Convert.ToDouble(Console.ReadLine());
                    if (_saldo < 0) throw new CadastroException("Saldo com formato inconsistente");
                }
                catch
                {
                    throw new CadastroException("Saldo com formato inconsistente");
                }
            }

            Console.WriteLine("\n");
        }
        #endregion

        #region Criação do tipo de conta específico
        public static Conta ContaCorrente()
        {
            ContaPadrao();
            var conta = new ContaCorrente(_nome, _cpf, _endereco, _rendaMensal, _agencia, _saldo);
            Mensagens.ContaCriada(conta, "Conta Corrente");
            return conta;
        }
        public static Conta ContaPoupanca() 
        {
            ContaPadrao();
            var conta = new ContaCorrente(_nome, _cpf, _endereco, _rendaMensal, _agencia, _saldo);
            Mensagens.ContaCriada(conta, "Conta Poupança");
            return conta;
        }
        public static Conta ContaInvestimento()
        {
            ContaPadrao();
            var conta = new ContaCorrente(_nome, _cpf, _endereco, _rendaMensal, _agencia, _saldo);
            Mensagens.ContaCriada(conta, "Conta Investimento");
            return conta;
        }
        #endregion

       
    }
}
