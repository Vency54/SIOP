using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;



class program
{
    // Lock para sincronizar o acesso ao console
    static object lockConsole = new object();

    // Banco de dados de funcionários
    static string[] nomes = {
      "Ana", "Carlos", "Mariana", "João", "Fernanda",
    "Pedro", "Lucas", "Juliana", "Rafael", "Beatriz",
    "Bruno", "Camila", "Diego", "Eduarda", "Felipe",
    "Gabriela", "Henrique", "Isabela", "José", "Karina"
        };

    // Variável para armazenar a média salarial calculada
    static double mediaCalculada = 0;

    // Banco de dados de salários
    static 
        double[] salarios = {
          3000, 4500, 5000, 2800, 5200,
    4100, 3900, 4700, 6000, 3500,
    3200, 4800, 5100, 2900, 5300,
    4200, 4000, 4600, 6100, 3600
        };

    // Banco de dados de cargos
    static string[] cargos = {
      "Gerente", "Analista", "Desenvolvedor", "Suporte", "RH",
    "Financeiro", "Tester", "Designer", "DevOps", "Diretor",
    "Analista", "Desenvolvedor", "Suporte", "RH", "Financeiro",
    "Tester", "Designer", "DevOps", "Gerente", "Diretor"
    };


    // Método para mostrar os funcionários
    static void exibirFuncionario(int inicio, int fim)
    {
        for(int i = inicio; i < fim; i++)
        {
            // Sincroniza o acesso ao console para evitar que as informações se misturem
            lock (lockConsole) { 
                Console.WriteLine($"Nome: {nomes[i]}");
            Console.WriteLine($"Salário: R$ {salarios[i]:F2}");
            Console.WriteLine($"Cargo: {cargos[i]}");
            Console.WriteLine("--------------------");
            }
            Thread.Sleep(800);
        }
    }

    // Método para calcular o resumo financeiro
    static void ResumoFinanceiro()
    {
        double total = 0;

        for (int i = 0; i < salarios.Length; i++)
        {
            total += salarios[i];
            Thread.Sleep(500); 
        }

        double mediaCalculada = total / salarios.Length;

        lock (lockConsole)
        {
            Console.WriteLine($"-- Resumo Financeiro --");
            Console.WriteLine($"Total de funcionários: {nomes.Length}");
            Console.WriteLine($"Total da folha salarial: R$ {total:F2}");
            Console.WriteLine($"Média salarial: R$ {mediaCalculada:F2}");
            Console.WriteLine($"-----------------------\n");

        }
    }


    static void Main()
    {
        // Inicia o contador de tempo
        var sw = new Stopwatch();
        sw.Start();

        // Divide o array de funcionários em duas partes para as threads
        int meio = nomes.Length / 2;

        // Inicia as threads para mostrar os funcionários
        Console.WriteLine("BANCO DE DADOS DE FUNCIONÁRIOS DA EMPRESA\n");
        Console.WriteLine("Gerando Funcionários...\n");
        Thread.Sleep(500);
        Console.WriteLine("-- Funcionários --\n");
        Thread t1 = new Thread(() => exibirFuncionario(0, meio));
        Thread t2 = new Thread(() => exibirFuncionario(meio, nomes.Length));
        Thread t3 = new Thread(ResumoFinanceiro);

        // Inicia as threads
        t1.Start();
        t2.Start();
        t3.Start();

        // Aguarda as threads de funcionários terminarem antes de mostrar o resumo financeiro
        t1.Join();
        t2.Join();
        Console.WriteLine("\nTodos os funcionários foram gerados!\n");
        t3.Join();


    
        //Encerrando o contador de tempo
        sw.Stop();

        Console.WriteLine($"Tempo decorrido: {sw.Elapsed.TotalSeconds:F2} s"); 
        Console.WriteLine("Programa encerrado!");
    }

}