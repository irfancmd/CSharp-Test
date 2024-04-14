List<int> temperatures = [ 1, 2, 3, 4 ];

IEnumerable<string> GetTemperature(char unit)
{
    foreach (int temperature in temperatures)
    {
        yield return $"{temperature} {unit}";
    }

    yield return "Done";
}

foreach (var temperatureString in GetTemperature('F'))
{
    Console.WriteLine(temperatureString);
}

Console.WriteLine();

foreach (var temperatureString in GetTemperature('C'))
{
    Console.WriteLine(temperatureString);
}
