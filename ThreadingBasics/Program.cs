using Exception = System.Exception;

namespace ThreadingBasics;

class NumbersCounter
{
    public void CountUp()
    {
        try
        {
            Console.WriteLine("Count up started");
            Thread.Sleep(300);
            
            for (int i = 1; i < 100; i++)
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
    
    public void CountDown(int start)
    {
        Console.WriteLine("Count down started");
        Thread.Sleep(300);
        
        for (int j = start; j >= 1; j--)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"j = {j}, ");
            Thread.Sleep(100);// Artificial latency
        }
        
        Console.WriteLine();
        
        Thread.Sleep(300);
        Console.WriteLine("Count down completed");
    }
    
    // ParameterizedThreadStart requires a nullable object type
    public void CountDownParamTheadStart(object? start)
    {
        Console.WriteLine("Count down started");
        Thread.Sleep(300);

        int startInt = (int)start;
        
        for (int j = startInt; j >= 1; j--)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write($"j = {j}, ");
            Thread.Sleep(100);// Artificial latency
        }
        
        Console.WriteLine();
        
        Thread.Sleep(300);
        Console.WriteLine("Count down completed");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Thread mainThread = Thread.CurrentThread;
        mainThread.Name = "Main Thread";
        Console.WriteLine($"{mainThread.Name} is {mainThread.ThreadState.ToString()}");

        NumbersCounter numbersCounter = new NumbersCounter();
        
        // Because of context-switching, we'll see some some of the method
        // outputs printed in the wrong color. Because the CPU may just execute
        // the color changing code of one method, and immediately context switch
        // to the other method.
        
        // Creating first thread using parameter-less ThreadStart.
        // This is because our methods don't take parameters.
        ThreadStart threadStart1 = numbersCounter.CountUp;

        Thread thread1 = new Thread(threadStart1)
        {
           Name = "Count-Up Thread",
           Priority = ThreadPriority.AboveNormal // We're giving more priority to this thread
        };
        
        Console.WriteLine($"{thread1.Name} is {thread1.ThreadState.ToString()}");
        
        thread1.Start();
        
        Console.WriteLine($"{thread1.Name} - {thread1.ManagedThreadId} is {thread1.ThreadState.ToString()}");
        
        // Create second thread
        // Use parameterized function using lambda expression
        ThreadStart threadStart2 = () => numbersCounter.CountDown(200);

        Thread thread2 = new Thread(threadStart2)
        {
            Name = "Count-Down Thread"
        };
        
        Console.WriteLine($"{thread2.Name} - {thread2.ManagedThreadId} is {thread2.ThreadState.ToString()}");
        
        thread2.Start();
        
        Console.WriteLine($"{thread2.Name} is {thread2.ThreadState.ToString()}");
        
        // Creating third thread using ParameterizedThreadStart.
        ParameterizedThreadStart parameterizedThreadStart1 = numbersCounter.CountDownParamTheadStart;

        Thread thread3 = new Thread(parameterizedThreadStart1)
        {
           Name = "Count-Down ParameterizedThreadStart Thread",
        };
        
        Console.WriteLine($"{thread3.Name} is {thread3.ThreadState.ToString()}");
        
        thread3.Start(200); // If we're using ParameterizedThreadStart, we have to pass the parameter here
        
        Console.WriteLine($"{thread3.Name} - {thread3.ManagedThreadId} is {thread3.ThreadState.ToString()}");

        // thread1.Join(); // Main thread should wait until the completion of thread1
        // thread2.Join(); // Main thread should wait until the completion of thread2

        Thread.Sleep(2000);
        // Interrupting thread1. We typically interrupt threads when we decide
        // we don't need that thread's task to be done anymore.
        thread1.Interrupt();
        
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"{mainThread.Name}'s task is done.");
    }
}