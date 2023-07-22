namespace ExceptionHandling.Basics.Tests.NUnit
{
    public class CalculatorShould
    {
        [Test]
        public void ThrowWhenUnsupportedOperation()
        {
            // "SUT" stands for System Under Test
            var sut = new Calculator();

            // Note: TypeOf checks for exact match of the exception class. So, if we used CalculatorExceptoin in TypeOf, the
            // test would fail even through CalculationOperationNotSupportedException is a child of CalculatorException. If
            // we want to allow exception inheritance, we can use InstanceOf instead of TypeOf.
            Assert.That(() => sut.Calculate(1, 2, "-"), Throws.TypeOf<CalculationOperationNotSupportedException>()
                                                               .With
                                                               .Property("Operation").EqualTo("-"));
        }

        [Test]
        public void ThrowWhenArgumentOutOfRange()
        {
            var sut = new Calculator();

            Assert.That(() => sut.Calculate(1, 2, "+"), Throws.TypeOf<ArgumentOutOfRangeException>()
                                                                .With
                                                                .Property("Data")
                                                                .ContainKey("Operator Symbol"));
        }
    }
}