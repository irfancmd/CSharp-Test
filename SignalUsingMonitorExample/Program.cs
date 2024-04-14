﻿namespace SignalUsingMonitorExample;

static class Shared
{
    public static object lockObject = new object();
    public static Queue<int> buffer = new Queue<int>();
    public const int bufferCapacity = 5;
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
                    Monitor.Wait(Shared.lockObject);
                }
                
                Console.WriteLine("Producer generating data");
                Thread.Sleep(300); // Artificial latency
                
                Shared.buffer.Enqueue(i);
                Console.WriteLine($"Producer has produced {i}.");

                Monitor.Pulse(Shared.lockObject); // Tell the consumer that a value is available
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
                    Monitor.Wait(Shared.lockObject);
                }

                Thread.Sleep(1000); // Artificial latency

                int num = Shared.buffer.Dequeue();
                Console.WriteLine($"Consumer read {num}.");
                
                Monitor.Pulse(Shared.lockObject); // Signal the producer that there is a space in the buffer.
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