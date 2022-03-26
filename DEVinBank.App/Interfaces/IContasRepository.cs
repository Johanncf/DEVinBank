using DEVinBank.App.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEVinBank.App.Interfaces
{
    public interface IContasRepository : IBaseRepository<Conta>
    {
        public void Listar();
        public void ListarContasNegativadas();
        public string RetornaTipoDeConta(Conta conta);
    }
}
