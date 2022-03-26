using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEVinBank.App.Entidades
{
    public class Deposito : Transacao
    {
        public Deposito(string id, Conta conta, double valor) : base(id, conta, valor)
        {

        }
    }
}
