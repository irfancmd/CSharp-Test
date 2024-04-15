namespace TPL_Wait;

class UpCounter
{
    public void CountUp(int count)
    {
        long sum = 0;
        for (int i = 1; i <= count; ++i)
        {
            sum += i;
        }
        
        Console.WriteLine("Up Count Sum: {0}", sum);
    }
}

class DownCounter
{
    public void CountDown(int count)
    {
        long sum = 0;
        for (int j = count; j >= 1; --j)
        {
            sum += j;
        }
        
        Console.WriteLine("Down Count Sum: {0}", sum);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Main thread has been started.");
        
        UpCounter upCounter = new();
        DownCounter downCounter = new();
        
        // This is another way of creating and running tasks
        Task upCounterTask = Task.Factory.StartNew(() =>
        {
            upCounter.CountUp(100);
        });
        
        Task downCounterTask = Task.Factory.StartNew(() =>
        {
            downCounter.CountDown(100);
        });

        // We can wait for specific threads.
        // upCounterTask.Wait();
        // downCounterTask.Wait();
        
        // Or, we can pass several tasks to WaitAll() method to wait for multiple tasks.
        // One of the overloads of this method supports passing tasks through an array as well.
        Task.WaitAll(upCounterTask, downCounterTask);
        
        Console.WriteLine("Main thread's job is complete.");
    }
}