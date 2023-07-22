using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandling.Basics
{
    public class Calculator
    {
        public int Calculate(int num1, int num2, string op)
        {
            /* Instead of handling the exception in switch, we could also do this:
                
                stirng nonNullOperator = op ?? throw new ArgumentOutOfRangeException(nameof(op), "The mathematical operator is not supported.");

            */

            switch (op)
            {
                case "/":
                    try
                    {
                        int result =  Divide(num1, num2);
                        return result;
                    }
                    catch(DivideByZeroException)
                    {
                        // Note that in order to bubble up an exception we don't throw the specific exception object. If we thew that,
                        // C# would incorrectly show the Calculate() method as the exception origin whereas that exception actually
                        // originated in the Divide() method. So for bubbling just use the "throw" keyword.
                        throw;


                        /* We could also wrap the exception inside another higher level exception like this:
    
                            throw new ArithmeticException("An exception occurred during calculation", ex);

                        */
                    }
                case "-":
                    throw new CalculationOperationNotSupportedException(op);
                default:
                    // Rather than creating new exceptions, we can also use exception's data property for adding our own data. Beware that this
                    // property isn't secured as cache blocks will be able to modify it
                    ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException(nameof(op), "The mathematical operator is not supported.");
                    ex.Data.Add("Operator Symbol", op);

                    throw ex;
            }
        }

        // Expression body syntax
        public int Divide(int num1, int num2) => num1 / num2;
    }
}
