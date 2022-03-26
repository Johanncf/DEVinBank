using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEVinBank.App.Enums
{
    public class Enumerators
    {
        public enum AgenciaEnum
        {
            Florianopolis = 1, SaoJose, Biguacu
        }

        public enum TransacaoEnum
        {
            Saque,
            Deposito,
            Investimento
        }
        public enum TipoContaEnum
        {
            Corrente,
            Poupanca,
            Investimento
        }

        public enum TipoInvestimentoEnum
        {
            LCI, 
            LCA,
            CDB
        }
    }
}
