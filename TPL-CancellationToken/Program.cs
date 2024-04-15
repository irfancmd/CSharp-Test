using Random = System.Random;

namespace TPL_CancellationToken;

class Program
{
    static void Main(string[] args)
    {
        // For creating cancellation tokens, we need a CancellationTokenSource
        CancellationTokenSource cancellationTokenSource = new();

        // We have to pass the cancellation token to both the task and the anonymous method.
        Task task1 = Task.Run(() =>
        {
            Console.WriteLine("Task 1 has started.");
            
            PerformTask(cancellationTokenSource.Token);
            
            Console.WriteLine("Task 1 is complete.");
        }, cancellationTokenSource.Token).ContinueWith(task =>
        {
            // Continuing the task will prevent .NET from throwing the cancellation exception to the main thread.
            if (task.Status == TaskStatus.Canceled)
            {
                Console.WriteLine("Task 1 was cancelled");
            }
            else if (task.Status == TaskStatus.RanToCompletion)
            {
                Console.WriteLine("Task 1 is complete without obstacles.");
            }
    });

        // We'll randomly decide to wait for the task, or cancel it
        Random random = new Random();
        int flag = random.Next(0, 2);
        
        if (flag > 0)
        {
            task1.Wait();
        }
        else
        {
            cancellationTokenSource.Cancel(); // Cancelling the task with this token.
            task1.Wait();
        }
        
        Console.WriteLine("Main thread's job is complete.");
    }

    static void PerformTask(CancellationToken cancellationToken)
    {
        int sum = 0;
        
        for (int i = 0; i < 10000; i++)
        {
            // We'll check if a cancellation is requested before proceeding
            cancellationToken.ThrowIfCancellationRequested();

            sum += i;
        }
    }
}