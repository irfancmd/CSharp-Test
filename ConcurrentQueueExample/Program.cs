using System.Collections.Concurrent;

namespace ConcurrentQueueExample;

static class Shared
{
    // We don't need lockObject since we're using concurrent queue
    public static ConcurrentQueue<int> buffer = new();
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
            // We don't need to put this inside lock statement, since we're using concurrent queue
            if (Shared.buffer.Count == Shared.bufferCapacity)
            {
                Console.WriteLine("Buffer is full. Waiting for signal from consumer.");
                Shared.producerEvent.Reset();
            }

            Shared.producerEvent.WaitOne(); // This is an extra step we have to do for using reset events. We have
                                            // to tell it to continue execution only after the event is set to signaled.
                
            // We don't need to put this inside lock statement, since we're using concurrent queue
            Console.WriteLine("Producer generating data");
            Thread.Sleep(300); // Artificial latency
            
            Shared.buffer.Enqueue(i);
            Console.WriteLine($"Producer has produced {i}.");

            Shared.consumerEvent.Set(); // Tell the consumer that a value is available by setting the consumer event
                                        // to signaled.
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
            // We don't need to put this inside lock statement, since we're using concurrent queue
            if (Shared.buffer.Count == 0)
            {
                Console.WriteLine("Buffer is empty. Waiting for signal from producer.");
                Shared.consumerEvent.Reset(); // Consumer event will become unsignaled.
            }

            // WaitOne should stay outside "lock". Otherwise deadlock will occur.
            Shared.consumerEvent.WaitOne(); // This is an extra step we have to do for using reset events. We have
                                            // to tell it to continue execution only after the event is set to signaled.

            // We don't need to put this inside lock statement, since we're using concurrent queue
            Thread.Sleep(1000); // Artificial latency

            // ConcurrentQueue doesn't have Dequeue() method. Instead, we have TryDequeue()
            bool isSuccess = Shared.buffer.TryDequeue(out int num);

            if (isSuccess)
            {
                Console.WriteLine($"Consumer read {num}.");
            }
            else
            {
                Console.WriteLine("Failed to read data from queue.");
            }

            Shared.producerEvent.Set(); // Signal the producer that there is a space in the buffer. So, the consumer
                                        // thread can continue.
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