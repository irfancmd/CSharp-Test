namespace OOP.Basics
{
    internal class Program
    {
        static void Main(string[] args)
        {

            /* Note: 
             * 4 Principle of Object Orientation (OO): Encapsulation, Abstraction, Inheritance and Polymorphism
             * Encapsulation: Only necessary parts of the code is exposed to the outside world. Rest of the implementation and data is hidden.
             * Abstractoin: Hide the complex implementations and expose an easier interface 
             * Inheritance: Use functionality from parent classes. (Reusing code)
             * Polymorphism: Child classes can be used like thier parent classes but differnet child can have different behaviour
            */

            createSection("C# Inheritance Tree in Action");

            // Just like most other OOP lanugages, all classes are children of Object class in C#
            // Let's see this inheritance tree in action
            int[] numbers = { 1, 2, 3, 4, 5, };


            Type type = numbers.GetType();

            do
            {
                Console.WriteLine(type.FullName);
                type = type.BaseType;
            } while (type != null);

            Console.WriteLine();
            createSection("Encapsulaiton Demonstration");

            EncapsulationExample encapsulation = new EncapsulationExample();

            encapsulation.setName("Bob");
            Console.WriteLine(encapsulation.getName());
            // encapsulation.Country = "United Kingdom" // This won't work as have have made the set method private


            Console.WriteLine();
            createSection("Inheritance Demonstration");

            Rectangle rectangle1 = new Rectangle(10, 20, "Teal");
            rectangle1.PrintInfo();
            Console.WriteLine(rectangle1.CalculateArea());

            Console.WriteLine();
            createSection("Polymorshism Demonstration");

            // Rectangle Is-A shape. So depending on object created on the right side, different shapes can have
            // different behaviour
            Shape shape1 = new Rectangle(20, 48, "Indigo");
            shape1.PrintInfo();

            // However, this will not work as despite the object is a Rectangle, it's stored in a Shape reference
            // and Shape doesn't have any method called CalculateArea()
            // shape1.CalculateArea();

            // Polymorphism: virtual and override keyword

            // Rectangle and Circle have differnet implementations of Greet()
            Circle circle1 = new Circle(10, "Light Blue");

            rectangle1.Greet();
            circle1.Greet();

            Console.WriteLine();
            createSection("Interface Demonstration");

            rectangle1.Rotate();

        }

        static void createSection(string sectionTitle)
        {
            Console.WriteLine("**************************************************");
            Console.WriteLine(sectionTitle);
            Console.WriteLine("**************************************************");
        }
    }
}