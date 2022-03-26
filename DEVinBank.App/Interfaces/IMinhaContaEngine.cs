using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEVinBank.App.Interfaces
{
    public interface IMinhaContaEngine 
    {
        public bool Acessar();
        public bool ProximaOperacao();
    }
}
