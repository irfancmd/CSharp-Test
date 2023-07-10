using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReadWrite
{
    public class Note
    {
        public static int noteCount = 0;

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public Note(string title, string content, int id = -1)
        {
            if(id == -1)
            {
                Id = ++noteCount;
            }
            else
            {
                Id = id;
            }

            Title = title;
            Content = content;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("**************************************************");
            sb.AppendLine($"Note ID: {Id}");
            sb.AppendLine($"Title: {Title}");
            sb.AppendLine($"Content: {Content}");
            sb.AppendLine("**************************************************");

            return sb.ToString();
        }
    }
}
