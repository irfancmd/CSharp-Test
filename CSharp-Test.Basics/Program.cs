using System.Linq.Expressions;
using System.Text;

namespace CSharp_Test.Basics
{
    internal class Program
    {
        static void Main(string[] args)
        {
            createSection("Doubles and Decimals");

            // The decimal is a primitive data type provided by C# that can store
            // lesser numbers compared to double but has higher precision than doubles 
            Console.WriteLine($"For Double, Min = {double.MinValue} and Max = {double.MaxValue}");
            Console.WriteLine($"For Decimal, Min = {decimal.MinValue} and Max = {decimal.MaxValue}");

            // As we saw, decimal can't hold numbers as large as double. But it has greater precision
            double a = 1.0 / 3;
            Console.WriteLine($"Double Result: {a}");

            // Note that all floating point literals in C# are typed as double by default. So,
            // if we want to do calculations with decimal, we have to create variables of
            // decimal type or we can use the 'M' suffix.
            decimal b1 = 1M;
            decimal b2 = 3M;
            decimal b = b1 / b2;
            Console.WriteLine($"Decimal Result: {b}");

            Console.WriteLine();
            createSection("Working with DateTime and TimeSpan");

            // Working with DateTime and TimeSpan
            DateTime date1 = new DateTime(2022, 10, 14); // yyyy/MM/dd

            Console.WriteLine("The date is: ");
            Console.WriteLine(date1);

            date1 = date1.AddDays(2);

            Console.WriteLine("After adding 2 days: ");
            Console.WriteLine(date1);

            TimeSpan timeSpan1 = new TimeSpan(8, 25, 27); // hh/mm/ss 
            date1 = date1.Add(timeSpan1);

            Console.WriteLine("After adding TimeSpan: ");
            Console.WriteLine(date1);

            Console.WriteLine();
            createSection("Implicit Typing in C#");

            // Implicit typing (C# infers the type based on the value on right hand side)
            var v1 = 123;
            var v2 = 24.85;

            Console.WriteLine(v1.GetType());
            Console.WriteLine(v2.GetType());

            // Note: Switch statement works for most but not all data types. For instance, it doesn't work for
            // float and double

            // Note: The structure of a for loop is "for(initialization; condition; iterator)"

            Console.WriteLine();
            createSection("Named Parameters for Methods");

            // Named parameters of methods
            Console.WriteLine("7 + 10 =");
            Console.WriteLine(Add(b: 10, a: 7));

            Console.WriteLine("7 - 2 =");
            Console.WriteLine(Subtract(7, 2));

            Console.WriteLine();
            createSection("Working with Strings");

            // Working with strings
            string firstName = "Bob";
            string lastName = "Doe";

            // This is how we can format a string
            Console.WriteLine(string.Format("{0} {1}", firstName, lastName));

            // But in modern days, string interpolation is preferred
            Console.WriteLine($"{firstName} {lastName}");

            // Escape characters
            Console.WriteLine("Hello \n World \t from C#!");

            // If we need to use '\' in a string, we have to write '\\' to prevent C# from reading the back slash
            // as an escape character. This can be hard to read so instead, we can simply use verbatim strings
            // that tell C# to ignore any escape character.
            Console.WriteLine(@"C:\User\Documents\Document.txt");

            // Parsing from other data types to string
            string intStr = "123";
            Console.WriteLine(int.Parse(intStr));

            // Using TryParse
            int tryParseOutput;
            int.TryParse(intStr, out tryParseOutput);
            Console.WriteLine(tryParseOutput);

            Console.WriteLine();
            createSection("Constant Values");

            // We can declare constant values like this
            const int A1 = 481;
            Console.WriteLine(A1);

            Console.WriteLine();
            createSection("Passing Parameters by Reference by the 'ref' Keyword");

            // Note: In C#, types can be categorized into "Value Types" and "Reference Types".
            // Class and Interface are example of reference types whereas Enum, Struct are example
            // of value types. Value types are stored in the stack whereas reference types are
            // stored in the heap.

            // Passing a parameter by reference type

            // If we use ref, the ref parameter must be initialized before calling the method
            int b3 = 40;

            fooRef(20, ref b3);

            Console.WriteLine(b3);

            Console.WriteLine();
            createSection("Passing reference using the 'out' Keyword");

            // The "out" keyword works like the "ref" keyword except it doesn't require us to 
            // initialize the out parameter before calling the method. However, we have to initialize it
            // inside the method before it exits.
            // We can use the "out" parameter to return multiple values from a single method

            int b4;

            fooOut(80, out b4);

            Console.WriteLine(b4);

            Console.WriteLine();
            createSection("String are reference types, but behave a bit differently!");

            // Although strings are reference types, they behave differently from regular objects
            // Let's look at the following example
            string foo1 = "Hello";

            string bar1 = foo1;
            bar1 += " World";

            // As reference type, we expect both strings to change but they didn't!
            Console.WriteLine(foo1);
            Console.WriteLine(bar1);

            // Strings are stored in a "String Pool". Two strings share the same memory address as
            // long as they contain the same value. But when one of them changes, a new memory address
            // is allocated for storing the changed string. So, everytime we do a concatenation operation,
            // the content of the small strings are copied everytime which sometimes can lead to
            // performance issues. To solve this, C# provides a class called "StringBuilder" to efficiently
            // construct and contatenate strings.

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("Hello");
            stringBuilder.Append(" Optimized ");
            stringBuilder.AppendLine("World");

            Console.WriteLine(stringBuilder.ToString());

            Console.WriteLine();
            createSection("Using Enum");

            // Enums allows us to use numerical values in a more meaningful way.
            // C# will automaticaly assign numbers to Enumerated types starting from index 0. We can define our own
            // values as well.
            Console.WriteLine(StarType.BLUE_GIANT);
            Console.WriteLine((int)StarType.BLUE_GIANT);

        
            // Visual allows us to create regions for folding codes
            #region Using Struct

            Console.WriteLine();
            createSection("Using Struct");

            // Structs can be considered as simple classes. Just like classes, they can contain properties and methods.
            // However, instread of being stored in the heap, they are stored in the stack. They are appropriate for
            // simple use cases.

            // We can instantiate structs with the "new" keyword but it's not necessary
            // Note note1 = new Note("Meeting at 3:15 PM", "Meeting about product");
            Note note1;
            note1.title = "Meeting at 3:15 PM";
            note1.description = "Meeting about product";

            note1.printNote();

            #endregion


            Console.WriteLine();
            createSection("Null values in C#");

            // C# allows both value and reference type objects to be null. To speficy a value type as nullable, we hve to
            // add '?' at the end of the type name
            int? a5  = null;

            if(a5.HasValue)
            {
                // This code won't be executed
                Console.WriteLine(a5.Value);
            }

            // We can use null coalesence operator '??' to use a default value if a variable is null
            Console.WriteLine(a5 ?? 10);

            // Now, lets assign a value to a5
            a5 = 19;

            Console.WriteLine(a5.Value);

            /* Note: Garbage collection in C#
             * In debug mode, the garbage collection works a bit differently and so it may not trigger when we expect it
             * to be. We can manually trigger the garbage collector by calling GC.Collect() method.
            */

            Console.WriteLine();
            createSection("Using Arrays");

            // Note: Arrays are reference types

            // Initializing arrays
            int[] arr1 = new int[4] { 1, 2, 3, 4}; // If we're initializing like this, the mumber of elements HAS TO match the array length 
            int[] arr2 = new int[] { 1, 2, 3, 4, 5}; // We can also omit the array lenghth if we're directly initializing like this 

            int[] arr3 = new int[arr2.Length];

            // Copy elements of arr2 in arr3 starting from index 0
            arr2.CopyTo(arr3, 0);

            printArray(arr2);
            printArray(arr3);

            Array.Reverse(arr3);

            printArray(arr3);

        }

        static void createSection(string sectionTitle)
        {
            Console.WriteLine("**************************************************");
            Console.WriteLine(sectionTitle);
            Console.WriteLine("**************************************************");
        }

        static void printArray(int[] arr)
        {
            Console.Write("[");

            foreach(var element in arr)
            {
                Console.Write($"{element} ");
            }

            Console.WriteLine("]");
        }

        // b has a default vaue
        static int Add(int a, int b = 5)
        {
            return a + b;
        }

        // Expression bodied syntax
        static int Subtract(int a, int b) => a - b;

        // This method takes parameter 'b' as a reference
        static void fooRef(int a, ref int b) {
            b += a;
        }

        // This method also takes parameter 'b' as a reference
        static void fooOut(int a, out int b) {
            b = 10;

            b += a;
        }
    }
}