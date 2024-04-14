namespace MonitorWithManualResetEvent;

static class Shared
{
    public static object lockObject = new object();
    public static Queue<int> buffer = new Queue<int>();
    public const int bufferCapacity = 5;
    // By default, the producerThread will proceed and consumerThread will wait.
    public static ManualResetEvent producerEvent = new(true);
    public static ManualResetEvent consumerEvent = new(false);
}

class Producer
{
    public void Produce()
    {
        Console.WriteLine("Producer: Generating Data.");

        for (int i = 1; i <= 10; i++)
        {
            lock (Shared.lockObject)
            {
                if (Shared.buffer.Count == Shared.bufferCapacity)
                {
                    Console.WriteLine("Buffer is full. Waiting for signal from consumer.");
                    Shared.producerEvent.Reset();
                }
            }

            Shared.producerEvent.WaitOne(); // This is an extra step we have to do for using reset events. We have
                                            // to tell it to continue execution only after the event is set to signaled.
                
            lock (Shared.lockObject)
            {
                Console.WriteLine("Producer generating data");
                Thread.Sleep(300); // Artificial latency
                
                Shared.buffer.Enqueue(i);
                Console.WriteLine($"Producer has produced {i}.");

                Shared.consumerEvent.Set(); // Tell the consumer that a value is available by setting the consumer event
                                            // to signaled.
            }
        }
        
        Console.WriteLine("Producer: Data Generation Complete.");
    }
}

class Consumer
{
    public void Consume()
    {
        Console.WriteLine("Consumer: Consumer Started.");

        for (int i = 0; i < 10; i++)
        {
            lock (Shared.lockObject)
            {
                if (Shared.buffer.Count == 0)
                {
                    Console.WriteLine("Buffer is empty. Waiting for signal from producer.");
                    Shared.consumerEvent.Reset(); // Consumer event will become unsignaled.
                }
            }

            // WaitOne should stay outside "lock". Otherwise deadlock will occur.
            Shared.consumerEvent.WaitOne(); // This is an extra step we have to do for using reset events. We have
                                            // to tell it to continue execution only after the event is set to signaled.

            lock (Shared.lockObject)
            {
                Thread.Sleep(1000); // Artificial latency

                int num = Shared.buffer.Dequeue();
                Console.WriteLine($"Consumer read {num}.");

                Shared.producerEvent.Set(); // Signal the producer that there is a space in the buffer. So, the consumer
                                            // thread can continue.
            }
        }

        Console.WriteLine("Consumer: Consumption Complete.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Producer producer = new();
        Consumer consumer = new();

        Thread producerThread = new(producer.Produce);
        Thread consumerThread = new(consumer.Consume);
        
        producerThread.Start();
        consumerThread.Start();

        producerThread.Join();
        consumerThread.Join();
    }
}