using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEVinBank.App.Exceptions
{
    public class OpcaoInvalidaException : Exception
    {
        public OpcaoInvalidaException(string mensagem = "Opção inválida. Escolha uma das opções disponíveis.") 
            : base(mensagem)
        {

        }
    }
}
