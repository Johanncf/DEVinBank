using DEVinBank.App.Entidades;
using DEVinBank.App.Helpers;
using DEVinBank.Sistema;

#region Iniciando atendimento e interagindo com o usuário

bool finalizado = SistemaEngine.BoasVindas();

#endregion

while (!finalizado)
{
    finalizado = SistemaEngine.ProximaOperacao();
}



