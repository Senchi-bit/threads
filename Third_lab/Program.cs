using System;
using System.Threading;

public static class Program
{
    private static int x;
    private static int y;
    private static readonly Mutex mutexX = new();
    private static readonly Mutex mutexY = new();

    public static void Main()
    {
        Thread myThread = new(Print1)
        {
            Name = "Поток 1"
        };
        myThread.Start();

        Thread myThread2 = new(Print2)
        {
            Name = "Поток 2"
        };
        myThread2.Start();
    }

    private static void Print1()
    {
        WorkWithVariable("x", ref x, mutexX);
        WorkWithVariable("y", ref y, mutexY);
    }

    private static void Print2()
    {
        WorkWithVariable("y", ref y, mutexY);
        WorkWithVariable("x", ref x, mutexX);
    }

    private static void WorkWithVariable(string variableName, ref int variable, Mutex mutex)
    {
        mutex.WaitOne();
        try
        {
            variable = 1;
            for (int i = 1; i < 3; i++)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name}: {variableName} = {variable}");
                variable++;
                Thread.Sleep(100);
            }
        }
        finally
        {
            mutex.ReleaseMutex();
        }
    }
}