using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP.Basics
{
    public class Shape : IRotatable
    {
        public string Name { get; set; }
        public string Color { get; set; }

        public Shape(string name, string color) 
        {
            Name = name;
            Color = color;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"Shape Name: {Name}");
            Console.WriteLine($"Shape Color: {Color}");
        }

        // The "viritual" keyword indicates that the child classes of this class can have their own implementatons
        // of the following function
        public virtual void Greet()
        {
            Console.WriteLine($"Hello from Generic Shape: {Name}!");
        }

        public void Rotate()
        {
            Console.WriteLine("The shape has been rotated");
        }
    }
}
