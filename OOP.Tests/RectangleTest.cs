using OOP.Basics;

namespace OOP.Tests
{
    public class RectangleTest
    {
        /*
         * Note: An unit test has three parts:
         * 1. Arrange: This is where we set up necessary data or mock objects
         * 2. Act: We call the method we're testing
         * 3. Assert: We verify the result
         */
        [Fact]
        public void CalculateArea_CalculatesArea()
        {
            // Arrange
            double width = 10.0;
            double height = 20.0;
            Rectangle rectangle = new Rectangle(width, height, "Magenta");

            // Act
            double result = rectangle.CalculateArea();

            // Assert
            Assert.Equal(width * height, result);
        }
    }
}