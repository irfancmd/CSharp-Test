namespace CsvWithThreads;

public class Shared
{
    // public static object LockObject { get; set; }
    public static Mutex MyMutex { get; set; } // If we use mutex instead of monitor, we don't have to use lock objects
    public static string FilePath { get; set; }
    public static int ChunkSize { get; set; }
    public static int MaxConcurrency { get; set; }
    public static CountdownEvent Count { get; set; }

    static Shared()
    {
        // LockObject = new object();
        MyMutex = new();
        FilePath = "../../../data.csv";
        ChunkSize = 100;
        MaxConcurrency = 3;
    }
}