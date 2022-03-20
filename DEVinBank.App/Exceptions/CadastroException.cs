using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEVinBank.App.Exceptions
{
    public class CadastroException : Exception
    {
        public CadastroException(string mensagem) : base(mensagem)
        {

        }
    }
}
