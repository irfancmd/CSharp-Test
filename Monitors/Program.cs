namespace Monitors;

class SharedResource
{
    public int Sum { get; set; }
    public readonly object sumLockObject = new();
}

class NumberUpCounter
{
    public SharedResource Resource { get; set; }
    
    public void CountUp()
    {
        try
        {
            Console.WriteLine("Count up started");
            Thread.Sleep(300);

            for (int i = 1; i <= 100; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Monitor.Enter(Resource.sumLockObject); // Critical section start
                Resource.Sum += i;
                Console.Write($"i = {i}, ");
                Monitor.Exit(Resource.sumLockObject); // Critical section end
                Thread.Sleep(100); // Artificial latency
            }

            Console.WriteLine();

            Thread.Sleep(300);
            Console.WriteLine("Count down completed");
        }
        catch (ThreadInterruptedException)
        {
            Console.WriteLine("Count-Down thread interrupted.");
        }
    }
}

class NumberDownCounter
{
    public SharedResource Resource { get; set; }
    
    public void CountDown()
    {
        try
        {
            Console.WriteLine("Count down started");
            Thread.Sleep(300);

            for (int i = 100; i >= 1; i--)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                lock (Resource.sumLockObject) // Using "lock" statement instead of Monitor
                {
                    Resource.Sum -= i;
                    Console.Write($"i = {i}, ");
                }
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
    }
}

class Program
{
    static void Main(string[] args)
    {
        Thread mainThread = Thread.CurrentThread;
        mainThread.Name = "Main Thread";
        Console.WriteLine($"{mainThread.Name} is {mainThread.ThreadState.ToString()}");

        SharedResource resource = new();
        
        // Count up thread

        NumberUpCounter numberUpCounter = new()
        {
            Resource = resource
        };
        
        ThreadStart threadStart1 = numberUpCounter.CountUp;

        Thread thread1 = new Thread(threadStart1)
        {
            Name = "Count-Up Thread",
        };
        
        Console.WriteLine($"{thread1.Name} is {thread1.ThreadState.ToString()}");
        
        thread1.Start();
        
        Console.WriteLine($"{thread1.Name} - {thread1.ManagedThreadId} is {thread1.ThreadState.ToString()}");
       
        // Count down thread
        
        NumberDownCounter numberDownCounter = new()
        {
            Resource = resource
        };
        
        ThreadStart threadStart2 = numberDownCounter.CountDown;

        Thread thread2 = new Thread(threadStart2)
        {
            Name = "Count-Down Thread",
        };
        
        Console.WriteLine($"{thread2.Name} is {thread2.ThreadState.ToString()}");
        
        thread2.Start();

        Console.WriteLine($"{thread2.Name} - {thread2.ManagedThreadId} is {thread2.ThreadState.ToString()}");

        thread1.Join();
        thread2.Join();
        
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"Sum = {resource.Sum}."); // Expected sum: 0
        Console.WriteLine($"{mainThread.Name}'s task is done.");
    }
}