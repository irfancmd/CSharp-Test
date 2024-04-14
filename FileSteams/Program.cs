const string filePath = "../../../File.txt";

// Writing file
FileStream fs = File.Open(filePath, FileMode.Open, FileAccess.Write);

string content = "Hello world!\nThis is a \nregular text file.";

byte[] bytes = System.Text.Encoding.ASCII.GetBytes(content);

fs.Write(bytes, 0, bytes.Length);

string content2 = " Here is some\nadditional text.";

byte[] bytes2 = System.Text.Encoding.ASCII.GetBytes(content2);

fs.Write(bytes2, 0, bytes2.Length);

// Closing the fileStream
fs.Close();

// Reading file
FileStream fs2 = File.Open(filePath, FileMode.Open, FileAccess.Read); 

byte[] bytes3 = new byte[fs2.Length];

fs2.Read(bytes3, 0, (int)fs2.Length);

string contentRead = System.Text.Encoding.ASCII.GetString(bytes3);

Console.Write(contentRead);

// Closing the fileStream
fs2.Close();
