using DEVinBank.App.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEVinBank.App.Interfaces
{
    public interface IBaseRepository<T> where T : EntidadeBase
    {
        public IList<T> Elementos { get; set; }  
        public void Adicionar(T elemento);
        public void RemoverPeloId(string id);
        public T RetornarElemento(string id);
        public string NovoId();
    }
}
