namespace ExceptionHandling.Basics.Tests.XUnit
{
    public class CalculatorShould
    {
        [Fact]
        public void ThrowUnsupportedException()
        {
            var sut  = new Calculator();

            // This will check for exact exception type
            //Assert.Throws<CalculationOperationNotSupportedException>(() => sut.Calculate(1, 2, "-"));

            // This will allow derived exception types
            Assert.ThrowsAny<CalculatorException>(() => sut.Calculate(1, 2, "-"));
        }
    }
}