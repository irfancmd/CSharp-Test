using System.Diagnostics;

class UpCounter
{
    public void CountUp(int count)
    {
        // Console.WriteLine("\nCount-Up Starts");

        long sum = 0;
        for (int i = 1; i <= count; ++i)
        {
            // Console.Write($"i = {i} ");
            sum += i;
        }
        // Console.WriteLine("\nCount-Up ends");
    }
}

class DownCounter
{
    public void CountDown(int count)
    {
        // Console.WriteLine("\nCount-Down Starts");

        long sum = 0;
        for (int j = count; j >= 1; --j)
        {
            // Console.Write($"j = {j} ");
            sum += j;
        }
        // Console.WriteLine("\nCount-Down ends");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Apart from showing how to use tasks, we'll also compare the
        // performance difference of Tasks and raw threads in this example.
        Stopwatch sw = new();
        
        // Tasks
        sw.Start();
        WithTasks();
        sw.Stop();
        
        long timeTakenForTasks = sw.ElapsedMilliseconds;
        Console.WriteLine($"\n Tasks - Time taken: {timeTakenForTasks} ms.");
        
        sw.Restart();
        WithThreads();
        sw.Stop();
        
        long timeTakenForThreads = sw.ElapsedMilliseconds;
        Console.WriteLine($"\n Threads - Time taken: {timeTakenForThreads} ms.");
    }

    static void WithTasks()
    {
        UpCounter upCounter = new();
        DownCounter downCounter = new();
        
        // We'll use CountDownEvent to keep track of the status of both task.
        CountdownEvent countdownEvent = new(2);
        
        // Instead of creating two threads, we'll create two tasks
        // We aren't supposed to crete Task manually using new(). This is because if we do tht
        // it'll be our responsibility to configure and manage that task. However, we want
        // the TaskScheduler to do that for us. So, we'll create tasks using the Task.Run static method.
        Task upCounterTask = Task.Run(() =>
        {
            upCounter.CountUp(100);
            
            countdownEvent.Signal();
        });
        
        Task downCounterTask = Task.Run(() =>
        {
            downCounter.CountDown(100);
            
            countdownEvent.Signal();
        });

        countdownEvent.Wait();
    }
    
    static void WithThreads()
    {
        UpCounter upCounter = new();
        DownCounter downCounter = new();
        
        // We could use join, but still using CountDownEvent to maintain fairness.
        CountdownEvent countdownEvent = new(2);
        
        Thread upCounterThread = new(() =>
        {
            upCounter.CountUp(100);
            
            countdownEvent.Signal();
        });
        
        Thread downCounterThread = new(() =>
        {
            downCounter.CountDown(100);
            
            countdownEvent.Signal();
        });
        
        upCounterThread.Start();
        downCounterThread.Start();

        countdownEvent.Wait();
    }
    
    
}
