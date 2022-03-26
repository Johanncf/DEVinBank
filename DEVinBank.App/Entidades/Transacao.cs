using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DEVinBank.App.Enums.Enumerators;

namespace DEVinBank.App.Entidades
{
    public class Transacao : EntidadeBase
    {
        public double Valor { get; protected set; }
        public Conta Conta { get; set; }
        public DateOnly DataTransacao { get; protected set; }
        public Transacao(string id, Conta conta, double valor)
            : base(id)
        {
            Id = id;
            Conta = conta;
            Valor = valor;
            DataTransacao = DateOnly.FromDateTime(DateTime.Now);
        }
    }
}
