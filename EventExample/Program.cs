namespace EventExample;

class Shared
{
    public static int[] Data { get; set; }
    public static int DataCount { get; set; }
    
    public static ManualResetEvent Event { get; set; }

    static Shared()
    {
        Data = new int[15];
        DataCount = Data.Length;
        Event = new(false); // We want it to be unsignaled by default
    }
}

class Producer
{
    public void Produce()
    {
        Console.WriteLine($"{Thread.CurrentThread.Name} started.");

        for (int i = 0; i < Shared.Data.Length; i++)
        {
            Shared.Data[i] = i + 1;
            Thread.Sleep(200); // Artificial latency
        }

        Shared.Event.Set(); // Tell the consumer that data is ready, so it can resume.
        
        Console.WriteLine($"{Thread.CurrentThread.Name} completed.");
    }
}

class Consumer
{
    public void Consume()
    {
        Console.WriteLine($"{Thread.CurrentThread.Name} started.");
        
        // Wait for the producer to populate data
        Console.WriteLine($"{Thread.CurrentThread.Name} is waiting for producer.");
        Shared.Event.WaitOne();
        Console.WriteLine($"{Thread.CurrentThread.Name} is resuming.");

        for (int i = 0; i < Shared.Data.Length; i++)
        {
            Console.Write($"{Shared.Data[i]} ");
        } 
        Console.WriteLine();
        
        Console.WriteLine($"{Thread.CurrentThread.Name} completed.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Producer producer = new();
        Consumer consumer = new();

        ThreadStart threadStart1 = producer.Produce;
        ThreadStart threadStart2 = consumer.Consume;

        Thread producerThread = new(threadStart1)
        {
            Name = "Producer Thread"
        };
        Thread consumerThread = new(threadStart2)
        {
            Name = "Consumer Thread"
        };
        
        producerThread.Start();
        consumerThread.Start();

        producerThread.Join();
        consumerThread.Join();
    }
}