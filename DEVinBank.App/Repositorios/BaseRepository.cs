using DEVinBank.App.Entidades;
using DEVinBank.App.Exceptions;
using DEVinBank.App.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEVinBank.App.Repositorios
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : EntidadeBase
    {
        public IList<T> Elementos { get; set; }
        public BaseRepository() => Elementos = new List<T>(); 
        public void Adicionar(T elemento)
        {
            Elementos.Add(elemento);    
        }
        public void RemoverPeloId(string id) => Elementos.Remove(RetornarElemento(id));
        public T RetornarElemento(string id) => Elementos.FirstOrDefault(el => el.Id == id) ??
            throw new CadastroException($"Identificador inválido.");
        public string NovoId() => (Elementos.Count + 1).ToString(); 
    }
}
