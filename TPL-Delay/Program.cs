namespace TPL_Delay;

class Program
{
    static void Main(string[] args)
    {
        // Delay is like "logical sleep". It delays a task, but it doesn't put the entire
        // thread it's in to sleep. This is important because TaskScheduler may run multiple
        // tasks on a single thread by switching between them. So it's not a good idea to put
        // that entire thread to sleep.
        Task task1 = Task.Factory.StartNew(() =>
        {
            Console.WriteLine("Task 1 has started.");
            // The Delay() method returns a Task object. So we have to call Wait() for it to make the actual delay.
            Task.Delay(2000).Wait();
            Console.WriteLine("Task 1 has been completed..");
        });

        task1.Wait();
        
        Console.WriteLine("Main thread's job is complete.");
    }
}