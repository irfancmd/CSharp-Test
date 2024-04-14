namespace Callback;

class NumberUpCounter
{
    public int Count { get; set; }
    
    public void CountUp(Action<long> callback)
    {
        long sum = 0;
        try
        {
            Console.WriteLine("Count up started");
            Thread.Sleep(300);

            for (int i = 1; i < Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                sum += i;
                Console.Write($"i = {i}, ");
                Thread.Sleep(100); // Artificial latency
            }

            Console.WriteLine();

            Thread.Sleep(300);
            Console.WriteLine("Count up completed");
        }
        catch (ThreadInterruptedException)
        {
            Console.WriteLine("Count-Up thread interrupted.");
        }
        finally
        {
            callback(sum);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Thread mainThread = Thread.CurrentThread;
        mainThread.Name = "Main Thread";
        Console.WriteLine($"{mainThread.Name} is {mainThread.ThreadState.ToString()}");

        NumberUpCounter numberUpCounter = new()
        {
            Count = 100
        };

        Action<long> callback = sum =>
        {
            Console.WriteLine($"Return value from the count up thread is {sum}.");
        };
        
        ThreadStart threadStart1 = () => numberUpCounter.CountUp(callback);

        Thread thread1 = new Thread(threadStart1)
        {
            Name = "Count-Up Thread",
        };
        
        Console.WriteLine($"{thread1.Name} is {thread1.ThreadState.ToString()}");
        
        thread1.Start();
        
        Console.WriteLine($"{thread1.Name} - {thread1.ManagedThreadId} is {thread1.ThreadState.ToString()}");
        
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"{mainThread.Name}'s task is done.");
    }
}