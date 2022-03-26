using DEVinBank.App.Engines;
using DEVinBank.App.Repositorios;
using DEVinBank.Sistema;

ContasRepository gerenciadorDeContas = new ContasRepository();
TransacoesRepository gerenciadorDeTransacoes = new TransacoesRepository();  

CriacaoContaEngine criadorDeConta = new CriacaoContaEngine(gerenciadorDeContas);
OperacoesEngine operacoes = new OperacoesEngine(gerenciadorDeContas, gerenciadorDeTransacoes);
MinhaContaEngine minhaConta = new MinhaContaEngine(gerenciadorDeContas, operacoes);        

SistemaFinanceiro sistema = new SistemaFinanceiro(criadorDeConta, minhaConta, gerenciadorDeContas, operacoes);

#region Iniciando atendimento e interagindo com o usuário

bool finalizado = sistema.BoasVindas();

#endregion

while (!finalizado)
{
    finalizado = sistema.ProximaOperacao();
}


DateOnly data1 = new DateOnly();



