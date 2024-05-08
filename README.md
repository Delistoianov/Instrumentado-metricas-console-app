# Instrumentado-metricas-console-app

## Passos para a execução:

1. No Visual Studio, foi criado o projeto do tipo "Console App".
   
![tipo_projeto](/Assets/tipo_projeto.png)

2. O projeto criado vem por padrão com um arquivo Program.cs.

   * O código do Program foi subtituido pelo código:
  
  ```
    using System;
    using System.Diagnostics.Metrics;
    using System.Threading;

    class Program
    {
        static Meter s_meter = new Meter("HatCo.Store");
        static Counter<int> s_hatsSold = s_meter.CreateCounter<int>("hatco.store.hats_sold");

        static void Main(string[] args)
        {
            Console.WriteLine("Projeto do João Executando");
            while (!Console.KeyAvailable)
            {
                Thread.Sleep(1000);
                s_hatsSold.Add(4);
            }
        }
    }
  
  ```

   * Esse código cria um contador de métricas para vendas de chapéus usando a biblioteca System.Diagnostics.Metrics. Ele inicia um medidor chamado "HatCo.Store" e define um contador s_hatsSold para rastrear a quantidade de chapéus vendidos. No método Main, o programa entra em um loop que a cada segundo adiciona quatro unidades ao contador s_hatsSold, enquanto não houver interrupção pelo usuário. Esse procedimento simula um cenário contínuo de vendas e serve para testar e monitorar o fluxo de dados de métricas

3. Em seguida foi instalado o Nuget System.Diagnostics.DiagnosticSource:

  ![tipo_projeto](/Assets/nuget_DiagnosticSource.png)

4. Nessa etapa o projeto foi executado com objetivo de verificar se a execução está conforme esperada

  ![tipo_projeto](/Assets/projeto_executando.png)


5. O próximo passo foi adicionar uma classe "HatCoMetrics" ao projeto, com o seguinte código:

```
    using System.Diagnostics.Metrics;

    namespace ConsoleApp_metrics
    {
        public class HatCoMetrics
        {
            private readonly Counter<int> _hatsSold;

            public HatCoMetrics(IMeterFactory meterFactory)
            {
                var meter = meterFactory.Create("HatCo.Store");
                _hatsSold = meter.CreateCounter<int>("hatco.store.hats_sold");
            }

            public void HatsSold(int quantity)
            {
                _hatsSold.Add(quantity);
            }
        }
    }
```

  * Esse código é responsável por gerenciar métricas de vendas de chapéus para a empresa HatCo. Ele utiliza a biblioteca System.Diagnostics.Metrics, a classe se integra a uma fábrica de medidores (IMeterFactory) para criar e gerenciar um contador de vendas de chapéus (_hatsSold). Este contador é incrementado através do método HatsSold(int quantity), que é chamado para adicionar a quantidade especificada de chapéus vendidos ao contador. 


6. Após isso já é possivel monitorar as métricas. Para conseguir obter os resultados das métricas é necessário que o código esteja em execução.

   * Descobrir o id do processo:

    ![id_processo](/Assets/id_processo.png)

    * Com o id do processo, o comando abaixo foi executado
  
    
    ```
        dotnet-counters monitor -p 22416 --counters "HatCo.Store"
    ```

    * Metricas geradas:
  
      ![metrica_01](/Assets/metricas_geradas.png)
