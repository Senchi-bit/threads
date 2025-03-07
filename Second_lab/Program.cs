
public class Program
{
    public static void Main(string[] args)
    {
        List<Thread> threads = new List<Thread>();
        for (int i = 0; i <= 300; i++)
        {
            Thread thread = new Thread(() =>
            {
                for (int a = 0; a < 100; a++)
                {
                    Console.WriteLine($"{Thread.CurrentThread.Priority}");
                }
            });
            if (i <= 100)
            {
                thread.Priority = ThreadPriority.Lowest;
                threads.Add(thread);
            }
            else if (i <= 200)
            {
                thread.Priority = ThreadPriority.Normal;
                threads.Add(thread);
            }
            else
            {
                thread.Priority = ThreadPriority.Highest;
                threads.Add(thread);
            }
        }

        foreach (var thread in threads)
        {
            thread.Start();
        }
        foreach (var thread in threads)
        {
            thread.Join();
        }
        Console.WriteLine("Все потоки завершены!");
    }
}