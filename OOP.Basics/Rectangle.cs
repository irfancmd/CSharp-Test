using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP.Basics
{
    public class Rectangle : Shape
    {

        public double Width { get; set; }
        public double Height { get; set; }

        public Rectangle(double width, double height, string color) : base("Rectangle", color)
        {
            Width = width;
            Height = height;
        }

        public double CalculateArea()
        {
            return Width * Height;
        }

        public override void Greet() {
            Console.WriteLine($"Hello from Rectangle: {Name}!");
        }
    }
}
