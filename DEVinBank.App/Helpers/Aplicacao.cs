namespace DEVinBank.App.Helpers
{
    public static class Aplicacao
    {
        public static DateOnly DataSistema = DateOnly.FromDateTime(DateTime.Now);
        public static string RecebeResposta()
        {
            Console.Write("> ");
            var resposta = Console.ReadLine();
            Console.WriteLine();
            return resposta;    
        }
    }
}
