namespace TPL_Continuation;

class Program
{
    static void Main(string[] args)
    {
        Task importantTask = Task.Factory.StartNew(() =>
        {
            Console.WriteLine("Important task started.");
            Task.Delay(1000).Wait();
            Console.WriteLine("Important task ended.");
        }).ContinueWith(t =>
        {
            // We can put any code here, including creating another task. 
            Task toBeDoneLater = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Less important task started.");
                Task.Delay(1000).Wait();
                Console.WriteLine("Less important task ended.");
            });

            toBeDoneLater.Wait();
        }).ContinueWith(t =>
        {
            Console.WriteLine("Both tasks are done.");
        });

        importantTask.Wait();
        
        Console.WriteLine("Main thread is done is with its work.");
    }
}