using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP.Basics
{
    internal class Circle: Shape
    {
        public double Radius { get; set; }

        public Circle(double radius, string color) : base("Circle", color)
        { 
            Radius = radius;
        }

        public override void Greet()
        {
            Console.WriteLine($"Hello from Circle: {Name}!");
        }
    }
}
