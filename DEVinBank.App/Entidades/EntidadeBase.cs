using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEVinBank.App.Entidades
{
    public class EntidadeBase
    {
        public string Id { get; protected set; }
        public EntidadeBase(string id)
        {
            Id = id;
        }
    }
}
