<h1 align="center"> DEVinBank - Projeto 1 - Módulo 2 - DEVinHouse</h1>

## Descrição do Projeto
<p align="left">Durante as últimas semanas aprendemos muito sobre a linguagem C#, e em muitas ocasiões nossas práticas e exercícios tinham características do que a indústria de software se refere como back-end, aprendendo sobre tipos de dados, variáveis, Arrays, Listas, e Conceitos aplicados à Programação Orientada a Objetos.
Que tal construir um sisteminha 100% funcional, sem depender de nenhuma outra tecnologia para exibição?! Vamos construir uma pequena fintech.
</p>

## Requisitos da Aplicação
<p align="left">A aplicação deve contemplar os seguintes requisitos:</p>
<ul>
    <li>O sistema deverá ser desenvolvido em C#;</li>
    <li>O sistema deve seguir o Roteiro da Aplicação;</li>
    <li>O desenvolvimento das mensagens de saída da fintech, esperando pela ação do usuário;</li>
    <li>Captura da interação do usuário via entrada padrão;</li>
    <li>O sistema deverá ser apresentado diretamente na linha de comando;</li>
</ul>

## Roteiro da Aplicação
<p align="left">A fintech DEVinBank deseja automatizar todo o seu sistema de armazenamento de informações
referentes aos seus clientes. O sistema deve conter os seguintes tipos de contas, cada uma com
suas características:</p>
<ul>
    <li>Conta corrente</li>
        <ul>
            <li>
                Na conta corrente o cliente tem direito ao cheque especial, ou seja, poderá ficar negativo durante um período de tempo. O sistema deve definir o total do cheque especial, conforme a renda mensal do correntista (10% da renda mensal).
            </li>
            <li>
                Extrato das transações.
            </li>
        </ul>
    <li>Conta poupança</li>
        <ul>
            <li>
                Na conta poupança o cliente não tem direito ao cheque especial.
            </li>
            <li>
                Na conta poupança, o cliente poderá simular quanto o seu valor renderá em um determinado tempo, para isso, o cliente deve informar a quantidade de tempo (em meses) e a rentabilidade anual da poupança.
            </li>
            <li>
                Extrato das transações.
            </li>
        </ul>
    <li>Conta investimento</li>
        <ul>
            <li>
                Neste tipo de conta, o cliente poderá escolher um tipo de investimento e o sistema deve apresentar o rendimento anual do investimento solicitado, conforme os valores abaixo:
            </li>
                <ul>
                    <li>
                        LCI: 8% ao ano.
                    </li>      
                        <ul>
                            <li>
                               Tempo mínimo de aplicação: 6 meses
                            </li>
                        </ul>
                    <li>
                        LCA: 9% ao ano.
                    </li>
                        <ul>
                            <li>
                               Tempo mínimo de aplicação: 12 meses
                            </li>
                        </ul>
                    <li>
                        CDB: 10% ao ano.
                    </li>
                        <ul>
                            <li>
                               Tempo mínimo de aplicação: 18 meses
                            </li>
                        </ul>
                </ul>
            <li>
                O cliente pode realizar uma simulação do valor aplicado, para isso, ele deve indicar:
            </li>
                <ul>
                    <li>
                        Valor que será aplicado.
                    </li>
                    <li>
                        Tempo que ficará aplicado o valor (em meses).
                    </li>
                    <li>
                        Ao final da simulação deve-se apresentar uma mensagem para o cliente perguntando se ele deseja efetuar o investimento.
                    </li>
                </ul>
            <li>
                Extrato das transações. Neste caso, sempre que o cliente efetuar uma transação deve-se armazenar:
            </li>
                <ul>
                    <li>
                        Valor aplicado.
                    </li>
                    <li>
                        Tipo da aplicação.
                    </li>
                    <li>
                        Data da aplicação
                    </li>
                    <li>
                        Data da retirada do valor.
                    </li>
                </ul>
        </ul>
</ul>

## Regras de Negócio
<ul>
    <li>
        A fintech também deseja manter um histórico das transferências, que deverá armazenar (utilizar conceitos de composição):
    </li>
        <ul>
            <li>
                Dados Conta Origem.
            </li>
            <li>
                Dados Conta Destino.
            </li>
            <li>
                Valor.
            </li>
            <li>
                Data (pegar a data e hora do sistema).
            </li>
        </ul>
    <li>
        O sistema também deverá apresentar os seguintes relatórios:
    </li>
        <ul>
            <li>
                Listar todas as contas.
            </li>
            <li>
                Contas com saldo negativo.
            </li>
            <li>
                Total do valor investido.
            </li>
            <li>
                Todas as transações de um determinado cliente.
            </li>
        </ul>
    <li>
        É importante que algumas transações não possam ser executadas em caso de problemas percebidos em suas operações:
    </li>
        <ul>
            <li>
                Transferência entre contas cujo montante supera o saldo acrescido do limite do cheque especial da conta de origem.
            </li>
            <li>
                Operações em momentos anteriores ao dia/hora da transação.
            </li>
            <li>
                Transferências durante o final de semana (sábado ou domingo).
            </li>
            <li>
                Não é possível fazer transferências para si próprio.
            </li>
        </ul>
</ul>


## Observações

<p align="left">O sistema deve iniciar no dia do seu sistema operacional e você deve pegar essa informação de forma automática. Para simular os valores de investimentos, faça uma função que adiante o tempo no seu algoritmo. Por exemplo: Adiantar em 1 ano o sistema bancário.</p>


## Detalhes da implementação

<ul>
  <li>
    A aplicação é iniciada através da classe <i>SistemaFinanceiro</i>. Esta classe centraliza o menu de interação com o usuário e possui dependências localizadas nas classes <i>Engines</i>. Tais dependências são injetadas no início da classe <i>Project</i> (código de entrada da aplicação).
  </li>
  <li>
    O projeto utiliza o padrão de design <i>Repositories</i>. Toda a manipulação de dados de contas e transações se apoiam nos repositórios das respectivas classes. Os       repositórios são classes de dependência paras as engines do sistema, e suas injeções também são realizadas no início da classe <i>Program</i>.
  </li>
  <li>
    As operações bancárias estão dividas entre:
  </li>
  <ul>
    <li>
      Operações realizadas pelo sistema, ou seja, não dependem necessariamente de uma conta de partida. Essas operações são executadas através do menu principal da aplicação. Exs.: "Listar contas negativadas", "Total de investimentos no DEVinBank", "Depósito".
    </li>
    <li>
      Operações realizadas por uma conta específica. São executadas a partir do menu "Minha Conta", que pede o número da conta para o acesso. Exs.: "Saque", "Transferência", "Depósito" (depósito é a única operação que pode ser realizada por ambas as rotas).
    </li>
  </ul>
  <li>
    As operações de investimento, das contas <i>Investimento</i>, estão separadas nas categorias de aplicação LCI, LCA e CDB, como descrito no roteiro. A escolha do modo de investimento está implícido no tempo de aplicação determinado pelo usuário. Ex.: Uma aplicação com duração de 15 meses, será obrigatoriamente da categoria LCA, uma vez que ultrapassa o mínimo exigido para LCA, porém não alcança a duração mínima de uma aplicação CDB.
  </li>
</ul>

## 🛠 Tecnologias

As seguintes ferramentas foram usadas na construção do projeto:

- [C#](https://docs.microsoft.com/pt-br/dotnet/csharp/)
- [DotNet 6.0](https://dotnet.microsoft.com/en-us/)
