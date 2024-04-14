namespace EventExampleBatch;

class Shared
{
    public static int[] Data { get; set; }
    public static int DataCount { get; set; }
    public static int BatchCount { get; set; }
    public static int BatchSize { get; set; }
    
    // public static ManualResetEvent Event { get; set; }
    public static AutoResetEvent Event { get; set; }

    static Shared()
    {
        Data = new int[15];
        DataCount = Data.Length;
        BatchCount = 5;
        BatchSize = 3;
        Event = new(false); // We want it to be unsignaled by default
    }
}

class Producer
{
    public void Produce()
    {
        Console.WriteLine($"{Thread.CurrentThread.Name} started.");

        for (int i = 0; i < Shared.BatchCount; i++)
        {
            for (int j = 0; j < Shared.BatchSize; j++)
            {
                Shared.Data[(i * Shared.BatchSize) + j] = (i * Shared.BatchSize) + j + 1;
                Thread.Sleep(200); // Artificial latency
            }
            
            Shared.Event.Set(); // Notify the consumer that writing a batch is complete and it can read that batch.

            // We don't have to call Reset() for AutoResetEvent
            // Shared.Event.Reset(); // We need to write further batches, so we're telling consumer to wait for next batches.
        }
        
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

        for (int i = 0; i < Shared.BatchCount; i++)
        {
            Shared.Event.WaitOne();
            
            Console.WriteLine($"{Thread.CurrentThread.Name} is resuming.");
            
            for (int j = 0; j < Shared.BatchSize; j++)
            {
                Console.Write($"{Shared.Data[(i * Shared.BatchSize) + j]} ");
            }
            
            Console.WriteLine();
        }

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