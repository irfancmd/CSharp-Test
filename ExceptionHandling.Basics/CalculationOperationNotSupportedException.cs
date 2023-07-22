using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandling.Basics
{
    public class CalculationOperationNotSupportedException : CalculatorException
    {

        private const string DefaultMessage = "The requested operation isn't supported.";

        public string? Operation { get; set; }

        // We're overriding the Message property of base exception class
        public override string Message
        {
            get
            {
                if (Operation is null)
                {
                    return base.Message;
                }
                else
                {
                    return base.Message + Environment.NewLine + $"Unsupported Operation: '{Operation}'";
                }
            }
        }


        public CalculationOperationNotSupportedException() : base(DefaultMessage) { }

        public CalculationOperationNotSupportedException(string operation) : this(operation, DefaultMessage) { }

        // Expression body syntax can also be used for constructors
        public CalculationOperationNotSupportedException(string operation, string message) : base(message) => Operation = operation;

        public CalculationOperationNotSupportedException(Exception innerException) : base(DefaultMessage, innerException) { }

        public CalculationOperationNotSupportedException(string message, Exception innerException) : base(message, innerException) { }

    }
}
