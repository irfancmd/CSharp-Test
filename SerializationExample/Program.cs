using System.Text.Json;
using System.Xml.Serialization;

const string XML_FILE_PATH = "../../../serialized.xml";
const string JSON_FILE_PATH = "../../../serialized.json";

Shape s1 = new()
{
    Height = 2.5m,
    Width = 3.5m,
    Color = "#00ff00"
};

// ***** Begin XML Serialization: Write *****
FileStream fs = File.Open(XML_FILE_PATH, FileMode.Create, FileAccess.Write);

XmlSerializer xmlSerializer = new XmlSerializer(typeof(Shape));
xmlSerializer.Serialize(fs, s1);

fs.Close();
// ***** End XML Serialization: Write *****


// ***** Begin XML Serialization: Read *****
fs = File.Open(XML_FILE_PATH, FileMode.Open, FileAccess.Read);

Shape? s1FromXml = xmlSerializer.Deserialize(fs) as Shape;

if (s1FromXml is not null)
{
    Console.WriteLine("Read form XML file:");
    Console.WriteLine($"s1 height from xml: {s1FromXml.Height}");
    Console.WriteLine($"s1 width from xml: {s1FromXml.Width}");
    Console.WriteLine($"s1 color from xml: {s1FromXml.Color}");
    Console.WriteLine();
}

fs.Close();
// ***** End XML Serialization: Read *****


// ***** Begin JSON Serialization: Write *****
fs = File.Open(JSON_FILE_PATH, FileMode.Create, FileAccess.Write);

JsonSerializer.Serialize(fs, s1);

fs.Close();
// ***** End JSON Serialization: Write *****


// ***** Begin JSON Serialization: Read *****
fs = File.Open(JSON_FILE_PATH, FileMode.Open, FileAccess.Read);

Shape? s1FromJson = JsonSerializer.Deserialize(fs, typeof(Shape)) as Shape;

if (s1FromJson is not null)
{
    Console.WriteLine("Read form JSON file:");
    Console.WriteLine($"s1 height from json: {s1FromJson.Height}");
    Console.WriteLine($"s1 width from json: {s1FromJson.Width}");
    Console.WriteLine($"s1 color from json: {s1FromJson.Color}");
    Console.WriteLine();
}

fs.Close();
// ***** End JSON Serialization: Read *****

public class Shape
{
    public decimal Height { get; set; }
    public decimal Width { get; set; }
    public string Color { get; set; } = "#ffffff";
}



