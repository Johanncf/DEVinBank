using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DEVinBank.App.Enums.Enumerators;

namespace DEVinBank.App.Entidades
{
    public abstract class Conta : EntidadeBase
    {
        public string NumeroConta { get; protected set; }
        public string Nome  { get; protected set; }
        public string CPF { get; }
        public string Endereco { get; protected set; }
        public double RendaMensal { get; protected set; }
        public AgenciaEnum Agencia { get; protected set; }
        protected double _saldo { get; set; }
        public IList<Transacao> Transacoes { get; set; }

        public Conta(string nome, string cpf, string endereco, double rendaMensal, 
            string numeroConta, AgenciaEnum agencia, double saldo = 0) : base(numeroConta)
        {
            Nome = nome;
            CPF = cpf;
            Endereco = endereco;    
            RendaMensal = rendaMensal;
            Agencia = agencia;
            _saldo = saldo;
            NumeroConta = numeroConta;

            Transacoes = new List<Transacao>();  
        }

        public virtual void Sacar(double valor)
        {
            if (valor <= _saldo)
                _saldo -= valor;
            else throw new Exception("Saldo insuficiente.");
        }
        public void Depositar(double valor)
        {
            _saldo += valor;
        }
        public double Saldo => _saldo;
        public void GetExtrato()
        {
            foreach (var transacao in Transacoes)
            {
                Console.WriteLine(transacao);
            }
        }
        public void AlterarDados(string nome, string endereco, double renda, AgenciaEnum agencia)
        {
            Nome = nome;
            Endereco = endereco;
            RendaMensal = renda;
            Agencia = agencia;
        }
    }
}
