namespace CsvWithThreads;

class Program
{
    static void Main(string[] args)
    {
        using (StreamReader sr = new StreamReader(Shared.FilePath))
        {
            string? line;
            long lineNumber = 0;

            List<string> chunk = new();

            List<Thread> threads = new();
            int threadCount = 0;

            // We're setting the same value for initial and max
            Semaphore semaphore = new(Shared.MaxConcurrency, Shared.MaxConcurrency);

            while ((line = sr.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                ++lineNumber;
                chunk.Add(line);

                if (lineNumber % Shared.ChunkSize == 0)
                {
                    // Reached to the end of a chunk
                    
                    // Create a duplicate copy of chunk for maintaining consistency
                    List<string> chunkCopy = chunk.Select(line => line).ToList();

                    int chunkNumber = threadCount + 1;
                    
                    Thread thread = new(() =>
                    {
                        // Wait for semaphore slot to become available
                        semaphore.WaitOne();

                        try
                        {
                            InvokeDataProcessor(chunkNumber, chunkCopy);
                        }
                        finally
                        {
                            semaphore.Release(); // Release the semaphore slot when the thread is done with its work.
                        }
                    })
                    {
                        Name = $"Chunk {chunkNumber} Thread"
                    };
                    
                    
                    threads.Add(thread);
                    threadCount++;
                    
                    thread.Start();
                    
                    chunk.Clear();
                }
            }
            
            // Process the last chunk (if any)
            if (chunk.Count > 0)
            {
                List<string> chunkCopy = chunk.Select(line => line).ToList();

                int chunkNumber = threadCount + 1;
                
                Thread thread = new(() =>
                {
                    semaphore.WaitOne();

                    try
                    {
                        InvokeDataProcessor(chunkNumber, chunkCopy);
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                })
                {
                    Name = $"Chunk {chunkNumber} Thread"
                };
                
                threads.Add(thread);
                threadCount++;
                
                thread.Start();
                
                chunk.Clear();
            }

            // foreach (Thread thread in threads)
            // {
            //     thread.Join();
            // }

            // CountDoenEvent Example: We can use them instead of Join() if we want.
            Shared.Count = new(threadCount);

            Shared.Count.Wait(); // Hold the current (Main) thread until the count reaches 0.
            
            Console.WriteLine("All rows of the CSV file has been processed.");
        }
    }

    static void InvokeDataProcessor(int chunkNumber, List<string> chunk)
    {
        DataProcessor dataProcessor = new()
        {
            ChunkName = $"Chunk {chunkNumber}",
            Chunk = chunk
        };
        
        Console.WriteLine($"Processing Chunk: {dataProcessor.ChunkName}.");

        dataProcessor.ProcessChunk();
        
        // We don't want the program to jump to another thread in the middle of the printing process of a thread.
        // That's why we'll put the printing logic inside lock statement.
        // lock (Shared.LockObject)
        // {
        Shared.MyMutex.WaitOne(); // Has same effect as starting a lock block.
            Console.WriteLine($"Done Processing Chunk: {dataProcessor.ChunkName}.");

            foreach (var genderKV in dataProcessor.genderWiseCounts)
            {
                Console.WriteLine($"{genderKV.Key} : {genderKV.Value}");
            }
            
            // After the job of a thead is done, signal the CountDownEvent for reducing its count.
            if (Shared.Count.CurrentCount > 0)
            {
                Shared.Count.Signal();
            }
        // }
        Shared.MyMutex.ReleaseMutex(); // Has same effect as releasing a lock block. It's recommended to put this
                                       // statement in finally block and keep the thread works in try block, so that
                                       // the mutex isn't kept occupied unnecessarily.
    }
}