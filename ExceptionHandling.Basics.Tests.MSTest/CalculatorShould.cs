namespace ExceptionHandling.Basics.Tests.MSTest
{
    [TestClass]
    public class CalculatorShould
    {
        [TestMethod]
        public void ThrowWhenUnsupportedOperation()
        {
            var sut = new Calculator();
        
            // Unlike NUnit and XUnit, MSTest doesn't have any ThrowsAny or InstanceOf functionality. So it doesn't allow
            // derieved exceptions
            Assert.ThrowsException<CalculationOperationNotSupportedException>(() => sut.Calculate(1, 1, "-"));
        }
    }
}