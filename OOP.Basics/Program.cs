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

            CreateSection("C# Inheritance Tree in Action");

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
            CreateSection("Encapsulaiton Demonstration");

            EncapsulationExample encapsulation = new EncapsulationExample();

            encapsulation.setName("Bob");
            Console.WriteLine(encapsulation.getName());
            // encapsulation.Country = "United Kingdom" // This won't work as have have made the set method private


            Console.WriteLine();
            CreateSection("Inheritance Demonstration");

            Rectangle rectangle1 = new Rectangle(10, 20, "Teal");
            rectangle1.PrintInfo();
            Console.WriteLine(rectangle1.CalculateArea());

            Console.WriteLine();
            CreateSection("Polymorshism Demonstration");

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
            CreateSection("Interface Demonstration");

            rectangle1.Rotate();

            Console.WriteLine();
            CreateSection("The 'is' operator");

            // The 'is' keyword allows us to check instance type
            Shape shape2 = new Rectangle(15, 10, "Teal");

            if (shape2 is Rectangle)
            {
                Console.WriteLine("The shape is a rectangle");
            }
            else
            {
                Console.WriteLine("The shape is not a rectangle");
            }

            Console.WriteLine();
            CreateSection("The 'as' operator");

            // The as keyword allows us to do explicit typing. If casting isn't successful, it returns null
            var shape3 = new Rectangle(30, 25, "Skyblue") as Shape;

            // The above line is actually a shorthand for
            /* 
             * Rectangle rectangle = new Rectangle(30, 25, "Skyblue");
             * var shape3 = rectangle is Shape ? (Shape)rectangle : null
            */

            // If casting can't be done, the value of the variable will be set to null
            if (shape3 != null)
            {
                Console.WriteLine("The casting was successful.");
            }

            // Note: Even when we do Upcasting using polymorphism, if we check the type of an object using typeof(),
            // C# will return the most specific type/instance type of that object. That means, typeof(shape3) will be rectangle
            // even through it has been upcasted to Shape. In simple words, typeOf() refers to instance type, not reference type.
            Console.WriteLine(shape3);

            Console.WriteLine();
            CreateSection("Property/Method shadowing");

            // In regular cases of Upcasting, if we call any method that is overriden by a child class, like Greet(), the overridden
            // versoin of the method would be called despite the reference being of Shape type. This is because, C# calls the most specific
            // version of a method. However, if we want to avoid this, we can shadow the Greet() method in a child class of shape which will
            // Break the inheritance chain for that method. So, If reference type is shape, The Greet() method of Shape will be called.
            // This practice is known as property/method shadowing and C# allows it using the "new" keyword in a property/method declaration.
            Shape shape4 = new Hexagon(25, "Hexa 1", "Teal");
            shape4.Greet();

            Hexagon hexagon2 = new Hexagon(27, "Hexa 2", "Skyblue");
            hexagon2.Greet();

            // Note: What's the difference between virtual method and abstract method?
            // Ans: For virtual methods, override is optional. For abstract methods, override is mandatory.
            // abstract methods can only be used in abstract classes.
        }

        static void CreateSection(string sectionTitle)
        {
            Console.WriteLine("**************************************************");
            Console.WriteLine(sectionTitle);
            Console.WriteLine("**************************************************");
        }
    }
}