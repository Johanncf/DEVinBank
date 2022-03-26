using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEVinBank.App.Entidades
{
    public class Investimento : Transacao
    {
        public double ValorResgate { get; private set; }    
        public DateOnly DataResgate { get; private set; }
        public Investimento(string id, Conta conta, double valorAplicado, double valorResgate, 
            DateOnly dataResgate) 
            : base(id, conta, valorAplicado)
        {
            DataResgate = dataResgate;
            ValorResgate = valorResgate;    
        }


    }
}
