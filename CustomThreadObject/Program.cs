namespace CustomThreadObject;

class NumberUpCounter
{
    public int Count { get; set; }
    
    public void CountUp()
    {
        try
        {
            Console.WriteLine("Count up started");
            Thread.Sleep(300);
            
            for (int i = 1; i < Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"i = {i}, ");
                Thread.Sleep(100);// Artificial latency
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

        NumberUpCounter numberUpCounter = new()
        {
            Count = 200
        };
        
        ThreadStart threadStart1 = numberUpCounter.CountUp;

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