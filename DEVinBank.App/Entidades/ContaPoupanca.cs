using DEVinBank.App.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEVinBank.App.Entidades
{
    public class ContaPoupanca : Conta
    {
        public ContaPoupanca(
            string nome,
            string cpf,
            string endereco,
            double rendaMensal,
            string numeroConta,
            Enumerators.AgenciaEnum agencia,
            double saldo = 0)
            : base(nome, cpf, endereco, rendaMensal, numeroConta, agencia, saldo) { }

        public double SimularRendimento(double meses, double rentAnual)
        {
            double montante = Saldo * Math.Pow((rentAnual / 1200) + 1, meses);
            return montante;
        }
    }
}
