namespace DEVinBank.App.Helpers
{
    public class Aplicacao
    {
        public static string RecebeComando()
        {
            Console.Write("> ");
            var resposta = Console.ReadLine();
            Console.WriteLine();
            return resposta;    
        }
    }
}
