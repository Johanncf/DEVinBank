using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DEVinBank.App.Enums.Enumerators;

namespace DEVinBank.App.Entidades
{
    public abstract class Conta
    {
        private static string _numeroConta = "0000";
        public string NumeroConta { get; private set; }
        public string Nome  { get; }
        public string CPF { get; }
        public string Endereco { get; }
        public double RendaMensal { get; }
        public AgenciaEnum Agencia { get; set; }
        public double Saldo { get; set; }

        public Conta(string nome, string cpf, string endereco, double rendaMensal, 
            AgenciaEnum agencia, double saldo = 0)
        {
            Nome = nome;
            CPF = cpf;
            Endereco = endereco;    
            RendaMensal = rendaMensal;
            Agencia = agencia;
            Saldo = saldo;

            _numeroConta = (Convert.ToInt32(_numeroConta) + 1).ToString("X4");
            NumeroConta = _numeroConta;
        }

    }
}
