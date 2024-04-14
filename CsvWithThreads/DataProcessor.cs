namespace CsvWithThreads;

public class DataProcessor
{
    public string ChunkName { get; set; }
    public List<string> Chunk { get; set; }
    public Dictionary<string, long> genderWiseCounts = new();

    public void ProcessChunk()
    {
        foreach (string line in Chunk)
        {
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }

            string[] values = line.Split(',');

            if (values.Length >= 5)
            {
                string gender = values[4].Trim().ToLower();

                if (genderWiseCounts.ContainsKey(gender))
                {
                    genderWiseCounts[gender]++;
                }
                else
                {
                    genderWiseCounts.Add(gender, 1);
                }
            }
            
            // Randomized Artificial Latency
            Random random = new();
            Thread.Sleep(100 * random.Next(1, 3));
        } 
    }
}