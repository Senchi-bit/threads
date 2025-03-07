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
        Thread thread1 = new(WorkWithXThenY);
        Thread thread2 = new(WorkWithY);

        thread1.Start();
        thread2.Start();

        thread1.Join();
        thread2.Join();
    }

    private static void WorkWithXThenY()
    {
        mutexX.WaitOne(); 
        Console.WriteLine("Поток 1 захватил x");

        x = 1;
        Console.WriteLine("Поток 1: x = 1");

        //Thread.Sleep(100);
        Console.WriteLine("Поток 1 пытается захватить y");
        mutexY.WaitOne();
        
        y = 1;
        Console.WriteLine("Поток 1: захватил y, y = 1");

        mutexY.ReleaseMutex();
        mutexX.ReleaseMutex();
    }

    private static void WorkWithY()
    {
        mutexY.WaitOne();
        Console.WriteLine("Поток 2 захватил y");


        y = 2;
        Console.WriteLine("Поток 2: y = 2");
        mutexY.ReleaseMutex();
        
        mutexX.WaitOne();
        x = 1;
        Console.WriteLine("Поток 2: захватил x");
        
        
        mutexX.ReleaseMutex();

    }
}