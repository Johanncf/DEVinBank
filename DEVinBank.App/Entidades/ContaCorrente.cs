using DEVinBank.App.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEVinBank.App.Entidades
{
    public class ContaCorrente : Conta
    {
        public ContaCorrente(
            string nome,
            string cpf,
            string endereco,
            double rendaMensal,
            string numeroConta,
            Enumerators.AgenciaEnum agencia,
            double saldo = 0)
            : base(nome, cpf, endereco, rendaMensal, numeroConta, agencia, saldo) { }
        public override void Sacar(double valor)
        {
            if (valor <= Saldo + RendaMensal * 0.1) _saldo -= valor;
            else throw new Exception("Saldo insuficiente.");
        }
    }
}
