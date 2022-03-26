using DEVinBank.App.Entidades;
using DEVinBank.App.Exceptions;
using DEVinBank.App.Helpers;
using DEVinBank.App.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DEVinBank.App.Enums.Enumerators;

namespace DEVinBank.App.Engines
{
    public class OperacoesEngine : IOperacoesEngine
    {
        private IContasRepository _gerenciadorDeContas;
        private ITransacoesRepository _gerenciadorDeTransacoes;
        public OperacoesEngine(IContasRepository gerenciadorDeContas,
            ITransacoesRepository gerenciadorDeTransacoes)
        {
            _gerenciadorDeContas = gerenciadorDeContas;
            _gerenciadorDeTransacoes = gerenciadorDeTransacoes;
        }
        public void Saque(Conta conta)
        {
            string novoIdTransacao = _gerenciadorDeTransacoes.NovoId();
            Console.Write("Valor: ");
            double valor = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine();

            Saque saque = new (novoIdTransacao, conta, valor);

            try
            {
                conta.Sacar(valor);
            }
            catch (Exception ex)
            {
                Mensagens.Erro(ex.Message);
                return;
            }

            _gerenciadorDeContas.RetornarElemento(conta.NumeroConta)
                .Transacoes.Add(saque);
            _gerenciadorDeTransacoes.Adicionar(saque);  

            Mensagens.OperacaoRealizadaComSucesso(
                $"Saque de {valor:C2} autorizado.\nSeu saldo atual é de:\n\n" +
                $"{conta.Saldo:C2}\n");
        }
        public void Deposito(Conta conta)
        {
            string novoIdTransacao = _gerenciadorDeTransacoes.NovoId();
            Console.Write("Valor: ");
            double valor = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine();

            Deposito deposito = new (novoIdTransacao, conta, valor);

            conta.Depositar(valor);

            _gerenciadorDeContas.RetornarElemento(conta.NumeroConta)
                .Transacoes.Add(deposito);
            _gerenciadorDeTransacoes.Adicionar(deposito);

            Mensagens.OperacaoRealizadaComSucesso(
                $"Depósito de {valor} realizado.\nSeu saldo atual é de:\n\n" +
                $"{conta.Saldo:C2}\n");
        }
        public void Transferencia(Conta origem)
        {
            DateOnly dateNow = DateOnly.FromDateTime(DateTime.Now);
            if (dateNow.DayOfWeek == DayOfWeek.Saturday ||
                dateNow.DayOfWeek == DayOfWeek.Sunday)
                throw new Exception("Não é possível realizar transferências aos " +
                    "finais de semama.");

            string novoIdTransacao = _gerenciadorDeTransacoes.NovoId();

            Console.Write("Valor: ");
            double valor = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine();

            Console.WriteLine("Número da conta de destino: ");
            var resposta = Console.ReadLine();
            Console.WriteLine();

            Conta destino = _gerenciadorDeContas.RetornarElemento(resposta);
            if (destino == origem) throw new Exception("As contas de origem e destino " +
                "de uma transferência não podem ser iguais.");

            origem.Sacar(valor);
            destino.Depositar(valor);
            Transferencia transferencia = new Transferencia(novoIdTransacao, origem, destino, valor);

            _gerenciadorDeContas.RetornarElemento(origem.NumeroConta)
                .Transacoes.Add(transferencia);
            _gerenciadorDeContas.RetornarElemento(destino.NumeroConta)
                .Transacoes.Add(transferencia);

            _gerenciadorDeTransacoes.Adicionar(transferencia);

            Mensagens.OperacaoRealizadaComSucesso(
                $"Transferência de {valor} realizada com sucesso para {destino.Nome}.\n" +
                $"Seu saldo atual é de:\n\n" +
                $"{origem.Saldo:C2}\n");
        }
        public void Saldo(Conta conta) => Mensagens.OperacaoRealizadaComSucesso(
                            $"Seu saldo atual é de:\n\n" +
                            $"{conta.Saldo:C2}\n");
        public void Extrato(Conta conta) => 
            conta.GetExtrato();
        public void AlterarDados(Conta conta)
        {
            #region Coletando Nome
            Console.Write("Seu nome como você gostaria de ver no cartão: ");
            string resposta = Console.ReadLine();
            if (string.IsNullOrEmpty(resposta)) throw new CadastroException("Nome não informado.");
            string nome = resposta;
            Console.WriteLine();
            #endregion

            #region Coletando Endereço
            Console.Write("Seu endereço: ");
            resposta = Console.ReadLine();
            if (string.IsNullOrEmpty(resposta)) throw new CadastroException("Endereço não informado.");
            string endereco = resposta;
            Console.WriteLine();
            #endregion

            #region Coletando renda mensal
            double renda;
            Console.Write("Sua renda mensal: ");
            try
            {
                renda = Convert.ToDouble(Console.ReadLine());
            }
            catch
            {
                throw new CadastroException("Renda mensal não informada ou com formato inconsistente");
            }
            Console.WriteLine();
            #endregion

            #region Coletando agência
            AgenciaEnum agencia = conta.Agencia;
            bool respondeu = false;
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
                        agencia = AgenciaEnum.Florianopolis;
                        respondeu = true;
                        break;
                    case "2":
                        agencia = AgenciaEnum.SaoJose;
                        respondeu = true;
                        break;
                    case "3":
                        agencia = AgenciaEnum.Biguacu;
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

            conta.AlterarDados(nome, endereco, renda, agencia);

            Mensagens.OperacaoRealizadaComSucesso(
                "Dados alterados com sucesso!\n" +
                "\n" +
                "\n" +
                "Cliente: " + conta.Nome + "\n" +
                "CPF: " + conta.CPF + "\n" +
                "Endereço: " + conta.Endereco + "\n" +  
                "Agência: " + $"{(int)conta.Agencia:X3} ({conta.Agencia})\n" +
                "N° da Conta: " + conta.NumeroConta + "\n" +
                "Tipo da Conta: " + _gerenciadorDeContas.RetornaTipoDeConta(conta) + "\n");
        }
        public void SimularRendimento(ContaPoupanca conta)
        {
            Console.Write("Tempo (em meses) para o rendimento: ");
            double tempo = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine();

            Console.Write("Rentabilidade anual (%) da poupança: ");
            double rentabilidade = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine();

            double montante = conta.SimularRendimento(tempo, rentabilidade);

            Mensagens.OperacaoRealizadaComSucesso(
                $"Total ao final dos {tempo} meses:\n" +
                $"{montante:C2}\n\n" +
                $"Rendimento total:\n" +
                $"{(montante - conta.Saldo):C2}\n\n");
        }
        public void SimularInvestimento(ContaInvestimento conta)
        {
            #region Mostra tipos de investimento e pega reposta
            double meses;
            double valor;
            bool respondeu;
            string resposta;
            do
            {
                Console.WriteLine("\nTipos de investimento:\n\n" +
                "LCI: 8% ao ano\n" +
                "\tTempo mínimo de aplicação: 6 meses\n" +
                "LCA: 9% ao ano\n" +
                "\tTempo mínimo de aplicação: 12 meses\n" +
                "CDB: 10% ao ano\n" +
                "\tTempo mínimo de aplicação: 18 meses\n\n");

                Console.WriteLine("Informe o tempo (meses) de investimento " +
                    "para realizar a simulação:\n");
                
                resposta = Aplicacao.RecebeResposta();
                
                try
                {
                    meses = Convert.ToDouble(resposta);
                    respondeu = true;
                }
                catch
                {
                    meses = 0;
                    break;
                }
            }
            while (!respondeu);
            #endregion

            #region Recebe o valor a ser investido
            respondeu = false;
            do
            {
                Console.WriteLine("\nValor do investimento simulado:\n");

                resposta = Aplicacao.RecebeResposta();
                try
                {
                    valor = Convert.ToDouble(resposta);
                    respondeu = true;
                }
                catch
                {
                    valor = 0;
                    respondeu = false;
                }
            }
            while (!respondeu);
            #endregion

            #region Simula o investimento e printa o resultado
            double montante;
            try
            {
                montante = conta.SimularInvestimento(meses, valor);
                Console.WriteLine($"O montante ao final dos {meses} meses será de:\n" +
                $"{montante:C2}\n");
            }
            catch (Exception ex)
            {
                Mensagens.Erro(ex.Message);
                return;
            }
            #endregion

            #region Realiza (ou não) o investimento
            respondeu = false;
            do
            {
                Console.WriteLine("Deseja realizar o investimento?\n\n" +
                "[1] - Sim\n" +
                "[2] - Não\n");

                resposta = Aplicacao.RecebeResposta();

                switch (resposta)
                {
                    case "1":
                        conta.Sacar(valor);
                        string idTransacao = _gerenciadorDeTransacoes.NovoId();
                        Investimento transacao = 
                            new (idTransacao, conta, valor, montante, Aplicacao.DataSistema.AddMonths((int)meses));

                        _gerenciadorDeContas.RetornarElemento(conta.NumeroConta)
                            .Transacoes.Add(transacao);
                        _gerenciadorDeTransacoes.Adicionar(transacao);
                        Mensagens.OperacaoRealizadaComSucesso($"" +
                            $"Investimento no valor de {valor:C2} realizado com sucesso!\n" +
                            $"Dentro de {meses} meses você pode resgatar o valor total acumulado.\n");
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
            
            #endregion
        }
        public void SimularResgate(ContaInvestimento conta)
        {
            Console.WriteLine(
                "Digite a quantidade de meses que deseja adiantar para a simulação: \n\n");
            string resposta = Aplicacao.RecebeResposta();

            int meses;
            try
            {
                meses = Convert.ToInt32(resposta);  
            }
            catch
            {
                Mensagens.Erro("Tempo não informado ou com formato incompatível. " +
                    "Informe um número inteiro referente aos meses que deseja adiantar" +
                    "no calendário.");
                return;
            }
            Console.WriteLine($"Data Sistema antes simulação: {Aplicacao.DataSistema}\n");
            Aplicacao.DataSistema = Aplicacao.DataSistema.AddMonths(meses);
            Console.WriteLine($"Data Sistema após simulação: {Aplicacao.DataSistema}\n");

            IEnumerable<Investimento> retornos =
                from elemento in _gerenciadorDeTransacoes.Elementos 
                where elemento is Investimento
                select (Investimento)elemento;        
            
            foreach(Investimento investimento in retornos)
            {
                if (investimento.DataResgate <= Aplicacao.DataSistema)
                {
                    Console.WriteLine($"Valor Investimento: {investimento.Valor:C2}\n");
                    conta.Depositar(investimento.ValorResgate);
                    Mensagens.OperacaoRealizadaComSucesso(
                    $"Investimento resgatado no valor de {investimento.ValorResgate:C2}.\n");
                }
            }

            Aplicacao.DataSistema = DateOnly.FromDateTime(DateTime.Now);
        }
        public double TotalInvestimentosDEVinBank() => _gerenciadorDeTransacoes.Elementos
                .Where(e => e is Investimento).Sum(e => e.Valor);
        public void RealizarDeposito()
        {
            Console.Write("Número da conta: ");
            string numeroConta = Console.ReadLine();
            Conta conta = _gerenciadorDeContas.RetornarElemento(numeroConta);

            Console.Write("Valor: ");
            double valor = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine();

            string novoIdTransacao = _gerenciadorDeTransacoes.NovoId();
            Deposito deposito = new(novoIdTransacao, conta, valor);

            conta.Depositar(valor);

            _gerenciadorDeContas.RetornarElemento(conta.NumeroConta)
                .Transacoes.Add(deposito);
            _gerenciadorDeTransacoes.Adicionar(deposito);

            Mensagens.OperacaoRealizadaComSucesso(
                $"Depósito de {valor} realizado.\n");
        }
    }
}
