using DEVinBank.App.Entidades;
using DEVinBank.App.Helpers;
using DEVinBank.App.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEVinBank.App.Repositorios
{
    public class ContasRepository : BaseRepository<Conta>, IContasRepository
    {
        public string RetornaTipoDeConta(Conta conta)
        {
            if (conta is ContaCorrente) return "Conta Corrente";
            if (conta is ContaPoupanca) return "Conta Poupança";
            if (conta is ContaInvestimento) return "Conta Investimento";
            throw new Exception("Conta inválida.");
        }
    }
}
