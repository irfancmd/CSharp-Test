using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP.Basics
{
    public class Hexagon : Shape
    {
        public int SideLength { get; set; }

        public Hexagon(int sideLength, string name, string color) : base(name, color)
        {
            SideLength = sideLength;
        }

        // This is called property/method shadowing. If we don't want to associate a child classe's method/property
        // to it's parent class, we can use the "new" keyword. This will break the "Inheritance Chain" for this property/method.
        // Refer to the program class to see its use case.
        public new void Greet()
        {
            Console.WriteLine("Hexagon specific greet.");
        }
    }
}
