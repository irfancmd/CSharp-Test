namespace ReturningFromTasks;

class UpCounter
{
    public long CountUp(int count)
    {
        long sum = 0;
        for (int i = 1; i <= count; ++i)
        {
            sum += i;
        }
        
        return sum;
    }
}

class DownCounter
{
    public long CountDown(int count)
    {
        long sum = 0;
        for (int j = count; j >= 1; --j)
        {
            sum += j;
        }

        return sum;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Main thread has been started.");
        
        UpCounter upCounter = new();
        DownCounter downCounter = new();
        
        Task<long> upCounterTask = Task<long>.Factory.StartNew(() =>
        {
            return upCounter.CountUp(100);
        });
        
        Task<long> downCounterTask = Task<long>.Factory.StartNew(() =>
        {
            return downCounter.CountDown(100);
        });

        Task.WaitAll(upCounterTask, downCounterTask);
        
        Console.WriteLine($"Up counter result: {upCounterTask.Result}");
        Console.WriteLine($"Down counter result: {downCounterTask.Result}");
        
        Console.WriteLine("Main thread's job is complete.");
    }
}