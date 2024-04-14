namespace ThreadPoolExample;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Main thread is started.");

        // We cannot do use Join() in thread pool threads. However, we can do the work of join by
        // using reset events
        ManualResetEvent resetEvent = new ManualResetEvent(false);

        // Any parameter that we pass to the Task, will be available in the "state" object
        ThreadPool.QueueUserWorkItem((object? state) =>
        {
            long sum = 0;

            int limit = (int)state;

            for (int i = 0; i < limit; i++)
            {
                sum += i;
            }
            
            Console.WriteLine($"Result from worker thread: {sum}.");

            resetEvent.Set();
        }, 1000);
        
        resetEvent.WaitOne();
        
        Console.WriteLine("Main thread's job is complete.");
    }
}