using DEVinBank.App.Entidades;
using DEVinBank.App.Helpers;
using DEVinBank.App.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEVinBank.App.Repositorios
{
    public class ContasRepository : BaseRepository<Conta>, IContasRepository
    {
        public void Listar()
        {
            if (Elementos.Count == 0)
            {
                Mensagens.DEVinBank("Não existem contas ativas no DEVinBank no momento.\n");
                return;
            }
            Console.WriteLine("Contas ativas do DEVinBank:\n");
            foreach (Conta conta in Elementos)
            {
                Console.WriteLine(
                    new string('*', 35) + "\n" +
                    "\n" +
                    "Cliente: " + conta.Nome + "\n" +
                    "CPF: " + conta.CPF + "\n" +
                    "Agência: " + $"{((int)conta.Agencia):X3} ({conta.Agencia})\n" +
                    "N° da Conta: " + conta.NumeroConta + "\n" +
                    "Tipo da Conta: " + RetornaTipoDeConta(conta) + "\n"
                    );
            }
        }
        public void ListarContasNegativadas()
        {
            if (!Elementos.Any(conta => conta.Saldo < 0))
                Console.WriteLine("Não existem contas negativadas no DEVinBank no momento.\n");
            
            foreach(ContaCorrente conta in Elementos.Where(conta => conta.Saldo < 0))
            {
                Console.WriteLine(
                    new string('*', 35) + "\n" +
                    "\n" +
                    "Cliente: " + conta.Nome + "\n" +
                    "CPF: " + conta.CPF + "\n" +
                    "Agência: " + $"{(int)conta.Agencia:X3} ({conta.Agencia})\n" +
                    "N° da Conta: " + conta.NumeroConta + "\n" +
                    "Tipo da Conta: " + RetornaTipoDeConta(conta) + "\n"
                    );
            }
        }
        public string RetornaTipoDeConta(Conta conta)
        {
            if (conta is ContaCorrente) return "Conta Corrente";
            if (conta is ContaPoupanca) return "Conta Poupança";
            if (conta is ContaInvestimento) return "Conta Investimento";
            throw new Exception("Conta inválida.");
        }
    }
}
