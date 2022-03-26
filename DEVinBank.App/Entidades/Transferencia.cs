using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEVinBank.App.Entidades
{
    public class Transferencia : Transacao
    {
        public Conta ContaDestino { get; private set; }
        public Transferencia(string id, Conta origem, Conta destino, double valor)
            : base(id, origem, valor)    
        {
            Id = id;
            ContaDestino = destino;  
            Valor = valor;
        }
    }
}
