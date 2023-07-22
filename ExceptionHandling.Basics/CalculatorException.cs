using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandling.Basics
{
    public class CalculatorException : Exception
    {
        private const string DefaultMessage = "An error occurred during calculation.";

        public CalculatorException() : base(DefaultMessage) { }

        public CalculatorException(string message) : base(message) { }

        public CalculatorException(Exception innerException) : base(DefaultMessage, innerException) { }

        public CalculatorException(string message, Exception innerException) : base(message, innerException) { }
    }
}
