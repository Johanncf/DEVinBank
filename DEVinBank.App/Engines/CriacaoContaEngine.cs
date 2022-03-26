using DEVinBank.App.Entidades;
using DEVinBank.App.Exceptions;
using DEVinBank.App.Interfaces;
using static DEVinBank.App.Enums.Enumerators;
using DEVinBank.App.Helpers;

namespace DEVinBank.App.Engines
{
    public class CriacaoContaEngine : ICriacaoContaEngine
    {
        private readonly IContasRepository _gerenciadorDeContas;
        private string Nome { get; set; } = "";
        private string CPF { get; set; } = "";
        private string Endereco { get; set; } = "";
        private double RendaMensal { get; set; }
        private AgenciaEnum Agencia { get; set; }
        private double Saldo { get; set; }

        public CriacaoContaEngine(IContasRepository contasRepository)
        {
            _gerenciadorDeContas = contasRepository;
        }
        #region Verificando o tipo de conta a ser criado
        public void GetTipoConta()
        {
            bool respondeu;
            do
            {
                Console.WriteLine(
                    "Qual tipo de conta você deseja criar?\n\n" +
                    "[1] - Conta corrente.\n" +
                    "[2] - Conta poupança.\n" +
                    "[3] - Conta investimento.\n" +
                    "[0] - Cancelar.\n");

                var resposta = Aplicacao.RecebeResposta();
                switch (resposta)
                {
                    case "1":
                        NovaConta(TipoContaEnum.Corrente);
                        respondeu = true;   
                        break;
                    case "2":
                        NovaConta(TipoContaEnum.Poupanca);
                        respondeu = true;
                        break;
                    case "3":
                        NovaConta(TipoContaEnum.Investimento);
                        respondeu = true;
                        break;
                    case "0":
                        respondeu = true;
                        break;
                    default:
                        respondeu = false;
                        break;
                }
            }
            while(!respondeu);
        }
        #endregion

        #region Criação de atributos padrão para Contas
        private void ContaPadrao()
        {
            string resposta;
            bool respondeu;
            do
            {
                Console.WriteLine("[DEVinBank]: Ok. Vou precisar de alguns dados pessoais, tudo bem?\n\n" +
                            "[1] - Prosseguir.\n" +
                            "[0] - Cancelar.\n");

                resposta = Aplicacao.RecebeResposta();

                switch (resposta)
                {
                    case "1":
                        respondeu = true;
                        break;
                    case "0":
                        respondeu = true;   
                        throw new FimCadastroException();
                    default:
                        respondeu = false;   
                        break;
                }
            }
            while (!respondeu);

            #region Coletando Nome
            Console.Write("Seu nome como você gostaria de ver no cartão: ");
            resposta = Console.ReadLine();
            if (string.IsNullOrEmpty(resposta)) throw new CadastroException("Nome não informado.");
            Nome = resposta;
            Console.WriteLine();
            #endregion

            #region Coletando CPF
            respondeu = false;
            do
            {
                Console.Write("Seu CPF: ");
                resposta = Console.ReadLine();
                Console.WriteLine();
                try
                {
                    if (string.IsNullOrEmpty(resposta)) throw new CadastroException("CPF não informado.");
                    if (!CPFehValido(resposta)) throw new CadastroException("CPF inválido.");
                    respondeu = true;
                }
                catch (Exception ex)
                {
                    Mensagens.Erro(ex.Message);
                    respondeu = false;
                }
            }
            while (!respondeu);

            CPF = resposta.Replace(".", "").Replace("-", "");
            #endregion

            #region Coletando Endereço
            Console.Write("Seu endereço: ");
            resposta = Console.ReadLine();
            if (string.IsNullOrEmpty(resposta)) throw new CadastroException("Endereço não informado.");
            Endereco = resposta;
            Console.WriteLine();
            #endregion

            #region Coletando renda mensal
            Console.Write("Sua renda mensal: ");
            try
            {
                RendaMensal = Convert.ToDouble(Console.ReadLine());
            }
            catch
            {
                throw new CadastroException("Renda mensal não informada ou com formato inconsistente");
            }
            Console.WriteLine();
            #endregion

            #region Coletando agência
            respondeu = false;
            do
            {
                Console.WriteLine("Escolha uma das seguintes agências: \n" +
                    "[1] - Florianópolis\n" +
                    "[2] - São José\n" +
                    "[3] - Biguaçu\n");
                resposta = Aplicacao.RecebeResposta();

                switch (resposta)
                {
                    case "1":
                        Agencia = AgenciaEnum.Florianopolis;
                        respondeu = true;
                        break;
                    case "2":
                        Agencia = AgenciaEnum.SaoJose;
                        respondeu = true;  
                        break;
                    case "3":
                        Agencia = AgenciaEnum.Biguacu;
                        respondeu = true;
                        break;
                    default:
                        respondeu = false;
                        Mensagens.Erro("Agência não informada.");
                        break;
                }
            }
            while (!respondeu);
            #endregion

            #region Coletando saldo inicial da conta
            respondeu = false;
            try
            {
                do
                {
                    Console.WriteLine("Deseja depositar algum valor inicial na conta?\n" +
                        "[1] - Sim\n" +
                        "[2] - Não\n");
                    resposta = Aplicacao.RecebeResposta();
                    switch (resposta)
                    {
                        case "1":
                            Console.Write("Digite o valor do depósito: ");
                            Saldo = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine();
                            if (Saldo < 0) throw new CadastroException("Saldo com formato inconsistente");
                            respondeu = true;
                            break;
                        case "2":
                            respondeu = true;
                            break;
                        default:
                            respondeu = false;
                            break;
                    }
                }
                while (!respondeu);
            }
            catch
            {
                throw new CadastroException("Saldo com formato inconsistente");
            }
        }
        #endregion

        #endregion

        #region Criação do tipo de conta específico
        private void NovaConta(TipoContaEnum tipoConta)
        {
            ContaPadrao();
            int totalContas = _gerenciadorDeContas.Elementos.Count() + 1;
            string novoNumeroConta;
            if (totalContas > 9999) novoNumeroConta = totalContas.ToString();
            else novoNumeroConta = totalContas.ToString("X4");
            Conta conta;
            switch (tipoConta)
            {
                case TipoContaEnum.Corrente:
                    conta = new ContaCorrente(Nome, CPF, Endereco, RendaMensal, novoNumeroConta, Agencia, Saldo);
                    Mensagens.ContaCriada(conta, "Conta Corrente");
                    break;
                case TipoContaEnum.Poupanca:
                    conta = new ContaPoupanca(Nome, CPF, Endereco, RendaMensal, novoNumeroConta, Agencia, Saldo);
                    Mensagens.ContaCriada(conta, "Conta Poupança");
                    break;
                case TipoContaEnum.Investimento:
                    conta = new ContaInvestimento(Nome, CPF, Endereco, RendaMensal, novoNumeroConta, Agencia, Saldo);
                    Mensagens.ContaCriada(conta, "Conta Investimento");
                    break;
                default:
                    throw new OpcaoInvalidaException();
            }
            _gerenciadorDeContas.Adicionar(conta);
        }
        #endregion
        private bool CPFehValido(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
    }
    
}
