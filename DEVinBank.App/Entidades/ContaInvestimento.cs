using DEVinBank.App.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DEVinBank.App.Enums.Enumerators;

namespace DEVinBank.App.Entidades
{
    public class ContaInvestimento : Conta
    {
        public ContaInvestimento(
            string nome, 
            string cpf, 
            string endereco, 
            double rendaMensal, 
            string numeroConta,
            Enumerators.AgenciaEnum agencia, 
            double saldo = 0) 
            : base(nome, cpf, endereco, rendaMensal, numeroConta, agencia, saldo) { }

        public double SimularInvestimento(double meses, double valor)
        {
            double rentAnual;
            switch (meses)
            {
                case >= 6 and < 12:
                    rentAnual = 8;
                    break;
                case >= 12 and < 18:
                    rentAnual = 9;
                    break;
                case >= 18:
                    rentAnual = 10;
                    break;
                default:
                    throw new ArgumentException("O tempo mínimo de aplicação é de 6 (seis) meses.");
            }
            return valor * Math.Pow((rentAnual / 1200) + 1, meses);
        }
    }
}
