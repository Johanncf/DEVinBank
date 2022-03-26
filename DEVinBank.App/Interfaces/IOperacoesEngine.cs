using DEVinBank.App.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEVinBank.App.Interfaces
{
    public interface IOperacoesEngine
    {
        public void Saque(Conta conta);
        public void Deposito(Conta conta);
        public void Transferencia(Conta origem);
        public void Extrato(Conta conta);   
        public void Saldo(Conta conta);
        public void AlterarDados(Conta conta);
        public void SimularRendimento(ContaPoupanca conta);
        public void SimularInvestimento(ContaInvestimento conta);
        public void SimularResgate(ContaInvestimento conta);
        public double TotalInvestimentosDEVinBank();
        public void RealizarDeposito();
    }
}
