using System;
using System.Text.Json;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Menu de Opções:");
            Console.WriteLine("1 - Calcular Soma");
            Console.WriteLine("2 - Verificar Fibonacci");
            Console.WriteLine("3 - Analisar Faturamento");
            Console.WriteLine("4 - Calcular Percentual Faturamento");
            Console.WriteLine("5 - Inverter String");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");

            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    CalcularSoma();
                    break;
                case "2":
                    VerificarFibonacci();
                    break;
                case "3":
                    AnalisarFaturamento();
                    break;
                case "4":
                    CalcularPercentualFaturamento();
                    break;
                case "5":
                    InverterString();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }

            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }

    static void CalcularSoma()
    {
        int INDICE = 13, SOMA = 0, K = 0;

        while (K < INDICE)
        {
            K = K + 1;
            SOMA = SOMA + K;
        }

        Console.WriteLine("O valor final de SOMA é: " + SOMA);
    }

    static void VerificarFibonacci()
    {
        Console.Write("Informe um número para verificar se pertence à sequência de Fibonacci: ");
        if (int.TryParse(Console.ReadLine(), out int numero))
        {
            int a = 0, b = 1, soma = 0;
            while (soma < numero)
            {
                soma = a + b;
                a = b;
                b = soma;
            }
            if (soma == numero || numero == 0)
                Console.WriteLine($"O número {numero} pertence à sequência de Fibonacci.");
            else
                Console.WriteLine($"O número {numero} NÃO pertence à sequência de Fibonacci.");
        }
        else
        {
            Console.WriteLine("Entrada inválida. Digite um número válido.");
        }
    }

    static void AnalisarFaturamento()
    {
        try
        {
            string json = File.ReadAllText("faturamento.json");
            var faturamento = JsonSerializer.Deserialize<decimal[]>(json);

            if (faturamento == null || faturamento.Length == 0)
            {
                Console.WriteLine("Nenhum dado de faturamento encontrado.");
                return;
            }

            var diasComFaturamento = faturamento.Where(v => v > 0).ToList();
            if (diasComFaturamento.Count == 0)
            {
                Console.WriteLine("Não há faturamento válido para análise.");
                return;
            }

            decimal menorFaturamento = diasComFaturamento.Min();
            decimal maiorFaturamento = diasComFaturamento.Max();
            decimal mediaMensal = diasComFaturamento.Average();
            int diasAcimaDaMedia = diasComFaturamento.Count(v => v > mediaMensal);

            Console.WriteLine($"Menor faturamento: {menorFaturamento:C}");
            Console.WriteLine($"Maior faturamento: {maiorFaturamento:C}");
            Console.WriteLine($"Dias com faturamento acima da média: {diasAcimaDaMedia}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao processar os dados: {ex.Message}");
        }
    }

    static void CalcularPercentualFaturamento()
    {
        var faturamentoEstados = new (string Estado, decimal Valor)[]
        {
            ("SP", 67836.43m),
            ("RJ", 36678.66m),
            ("MG", 29229.88m),
            ("ES", 27165.48m),
            ("Outros", 19849.53m)
        };

        decimal faturamentoTotal = faturamentoEstados.Sum(e => e.Valor);

        Console.WriteLine("Percentual de representação por estado:");
        foreach (var (Estado, Valor) in faturamentoEstados)
        {
            decimal percentual = (Valor / faturamentoTotal) * 100;
            Console.WriteLine($"{Estado}: {percentual:F2}%");
        }
    }

    static void InverterString()
    {
        Console.Write("Digite uma string para inverter: ");
        string input = Console.ReadLine();
        char[] inverted = new char[input.Length];

        for (int i = 0; i < input.Length; i++)
        {
            inverted[i] = input[input.Length - 1 - i];
        }

        Console.WriteLine("String invertida: " + new string(inverted));
    }
}
