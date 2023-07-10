namespace FileReadWrite
{
    internal class Program
    {
        private static string DIRECTORY_NAME = @"./NoteData/";
        private static string FILE_NAME = "data.txt";
        private static string FILE_PATH = $"{DIRECTORY_NAME}/{FILE_NAME}";

        static void Main(string[] args)
        {
            if(!File.Exists(FILE_PATH))
            {
                if(!Directory.Exists(DIRECTORY_NAME))
                {
                    Directory.CreateDirectory(DIRECTORY_NAME);
                }

                File.Create(FILE_PATH);
            }

            int input = 0;

            do
            {
                Console.ForegroundColor = ConsoleColor.Magenta;

                Console.WriteLine("**************************************************");
                Console.WriteLine("Command Line Note Taking Application");
                Console.WriteLine("**************************************************");

                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.WriteLine("Please choose one of the following options:");
                Console.WriteLine("(1) View Notes");
                Console.WriteLine("(2) Create New Note");
                Console.WriteLine("(9) Exit");

                Console.ResetColor();

                string? inputStr = Console.ReadLine();

                if (!string.IsNullOrEmpty(inputStr) && int.TryParse(inputStr, out input))
                {
                    switch (input)
                    {
                        case 1:
                            PrintAllNotes();
                            break;
                        case 2:
                            CreateNote();
                            break;
                        case 9:
                            break;
                        default:
                            PrintError("Invalid input. Please try again.");
                            break;
                    }
                }
                else
                {
                    PrintError("Invalid input. Please try again.");
                }
            } while (input != 9) ;
        }

        private static void CreateNote()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("Enter Note Title: ");

            Console.ResetColor();

            string? title = Console.ReadLine();

            if(string.IsNullOrEmpty(title))
            {
                PrintError("Note title cannot be empty.");
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("Enter Note Content: ");

            Console.ResetColor();

            string? content = Console.ReadLine();

            if(string.IsNullOrEmpty(content))
            {
                PrintError("Note title cannot be empty.");
                return;
            }

            Note note = new Note(title, content);

            WriteNoteToFile(note);
        }

        private static void WriteNoteToFile(Note note)
        {
            string[] lines = new string[] { $"id:{note.Id};title:{note.Title};content:{note.Content};" };

            File.AppendAllLines(FILE_PATH, lines);

            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("The note has been saved successfully.");

            Console.ResetColor();
        }

        private static ICollection<Note> ReadAllNotesFromFile()
        {
            ICollection<Note> notes = new List<Note>();

            string[] lines = File.ReadAllLines(FILE_PATH);

            foreach(var line in lines)
            {
                string[] splittedNoteData = line.Split(";");

                string idStr = splittedNoteData[0].Substring(splittedNoteData[0].IndexOf(':') + 1);

                // Writing try/catch instead of TryParse for demonstration
                try {
                    int id = int.Parse(idStr);
                }
                catch (FormatException e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);

                    Console.ResetColor();
                }
                catch (Exception e) 
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);

                    Console.ResetColor();
                }

                string title = splittedNoteData[1].Substring(splittedNoteData[1].IndexOf(':') + 1);
                string content = splittedNoteData[2].Substring(splittedNoteData[2].IndexOf(':') + 1);

                notes.Add(new Note(id: id, title: title, content: content));
            }

            Console.ForegroundColor= ConsoleColor.Green;

            Console.WriteLine($"Successfully loaded {notes.Count} note(s)");

            Console.ResetColor();

            return notes;
        }

        private static void PrintAllNotes()
        {
            ICollection <Note> notes = ReadAllNotesFromFile();

            foreach (Note note in notes) {
                Console.WriteLine(note); 
            }
        }

        private static void PrintError(string errorString)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"ERROR: {errorString}");
            Console.ResetColor();
        }
    }
}