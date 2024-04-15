namespace TPL_HandlingExceptions;

class Program
{
    static void Main(string[] args)
    {
        Task<long> task1 = Task<long>.Run(() =>
        {
            Console.WriteLine("Task 1 has started.");
            
            Random random = new Random();
            int flag = random.Next(0, 2);

            if (flag > 0)
            {
                Task.Delay(2000).Wait();
            }
            else
            {
                throw new Exception("Couldn't execute task.");
            }
            
            Console.WriteLine("Task 1 has ended.");
            
            return 0L;
        }).ContinueWith(task =>
        {
            // If we continue a task where exception has occurred, that exception won't be thrown to the main thread.
            // So, we check that task's status and put exception handling code within the method inside ContinueWith.
            // Thus, we don't have to put the task starting code within try/catch block.
            if (task.Status == TaskStatus.Faulted) // This is the status a task will have if it stops due to an exceptoinl.
            {
               Console.WriteLine($"Task 1 failed with error: {task.Exception.InnerException.Message}"); 
            }
            else
            {
                Console.WriteLine("Task 1 is complete without errors.");
            }

            return 0L;
        });

        task1.Wait();
        
            
        Console.WriteLine("Main thread's job is complete.");
    }
}