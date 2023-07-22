namespace ExceptionHandling.Basics
{
    internal class Program
    {

        /*
            Note don't thow exceptions in a program for normal logical flow like input validation. Exception are for
            "Exceptional" scenarions so they should only be thrown for such cases.
        */

        // If we want to catch any exception before it bubbles up to the operating system, we can use AppDomain's global
        // exception handler
        private static AppDomain currentAppDomain = AppDomain.CurrentDomain;

        static void Main(string[] args)
        {
            currentAppDomain.UnhandledException += new UnhandledExceptionEventHandler(HandleException);

            Calculator calculator = new Calculator();

            Console.WriteLine("Enter first operand:");
            int num1 = int.Parse(Console.ReadLine()!);
            Console.WriteLine("Enter second operand:");
            int num2 = int.Parse(Console.ReadLine()!);
            Console.WriteLine("Enter the operator:");
            string opr = Console.ReadLine()!.ToUpperInvariant();

            // Note: DivideByZero Exception is thrown for integers but not for floating point values. For floating point
            // values, C# will return positive infinity.
            try
            {
                // '!' is a null forgiving operator. We can use it to supress compiler warning while using null values.
                int result = calculator.Calculate(num1, num2, opr);
                Console.WriteLine(result);
            }
            // In C#, we can use exceptoin filtrs like this. Note what we're putting more specialized exception handlers above
            // more general ones.
            catch (ArgumentOutOfRangeException ex) when (ex.ParamName == "op")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);

                if(ex.Data.Contains("Operator Symbol"))
                {
                    Console.WriteLine(ex.Data["Operator Symbol"]);
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
            }
            catch (CalculationOperationNotSupportedException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
            }
            catch (DivideByZeroException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            catch
            {
                // This catch block will catch all exceptions in general. If we wanted to do something with the exception object,
                // we could write "catch(Exception ex)" instead of just "catch"
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An Unknown Exception occurred.");
            }
            finally
            {
                Console.ResetColor();
            }
        }

        private static void HandleException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"An error occured and the application needs to close. Details {e.ExceptionObject}");
            Console.ResetColor();
        }
    }
}