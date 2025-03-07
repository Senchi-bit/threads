using System;
using System.Diagnostics;

public class Program
{
    static void Main(string[] args)
    {
        int number = 100000000;
        int multiThreadResult = 0;
        var countProcesses = Environment.ProcessorCount;
        Console.WriteLine($"N число для суммирования: {number}");
        Console.WriteLine($"Количество логических процессоров: {countProcesses.ToString()}");
        
        var time = Stopwatch.StartNew();
        multiThreadResult = SumFirstNNumber(number, countProcesses);
        time.Stop();
        
        Console.WriteLine($"Сумма в {countProcesses} потоках: {multiThreadResult}");
        Console.WriteLine($"Время затраченное на вычисления {time.Elapsed.TotalSeconds} мс");
        
        
        var time2 = Stopwatch.StartNew();
        int oneThreadResult = 0;
        for (int i = 0; i < number; i++)
        {
            oneThreadResult += i;
        }
        time2.Stop();
        
        Console.WriteLine($"Сумма в одном потоке: {oneThreadResult}");
        Console.WriteLine($"Время затраченное на вычисления {time2.Elapsed.TotalSeconds} мс");
    }
    
    public static int SumFirstNNumber(int countNumbers, int countProcesses)
    {
        int oldPart = (countNumbers / countProcesses);
        int result = 0;
        List<int> sums = new List<int>();
        List<Thread> threads = new List<Thread>();
        
        for (int i = 0; i < countProcesses; i++)
        {
            int startPart = i * oldPart + 1;
            int endPart = (i == countProcesses - 1) ? countNumbers : (i + 1) * oldPart + 1;

            Thread thread = new Thread(() =>
            {
                int partialSum = SumNumbers(startPart, endPart);
                sums.Add(partialSum);
            });
            
            threads.Add(thread);
            thread.Start();
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }

        foreach (var sum in sums)
        {
            result += sum;
        }

        return result;
    }

    public static int SumNumbers(int min, int max)
    {
        int result = 0;
        for (int i = min; i < max; i++)
        {
            result += i;
        }
        return result;
    }
}