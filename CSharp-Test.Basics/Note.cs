using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Test.Basics
{
    internal struct Note
    {
        public string title;
        public string description;

        public Note(string title, string description)
        {
            this.title = title;
            this.description = description;
        }

        public void printNote()
        {
            Console.WriteLine("--------------------");
            Console.WriteLine(title);
            Console.WriteLine(description);
            Console.WriteLine("--------------------");
        }
    }
}
