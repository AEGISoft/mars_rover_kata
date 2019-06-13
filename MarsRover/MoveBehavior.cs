using MarsRover.Core;
using Xunit;

namespace MarsRover.Specs
{
    public class MoveBehavior
    {
        private static void Verify(IWorldSize world, string dropLocation, string command, string endLocation)
        {
            //Arrange
            var initialLocation = new Location(dropLocation);
            var expectedLocation = new Location(endLocation);
            var rover = new Rover(world, initialLocation);

            //Act
            rover.Move(command);

            //Assert
            Assert.Equal(expectedLocation, rover.Location);
        }

        [InlineData(5, "0.0.N", "M", "0.1.N")]
        [InlineData(5, "1.1.S", "M", "1.0.S")]
        [InlineData(5, "0.0.E", "M", "1.0.E")]
        [InlineData(5, "1.1.W", "M", "0.1.W")]

        [InlineData(5, "0.0.N", "R", "0.0.E")]
        [InlineData(5, "0.0.E", "L", "0.0.N")]

        [InlineData(5, "0.0.N", "MM", "0.2.N")]
        [InlineData(5, "0.0.N", "LLLL", "0.0.N")]
        [InlineData(5, "0.0.N", "RRRR", "0.0.N")]

        [InlineData(5, "2.3.N", "LM LM LM LM M", "2.4.N")]
        [InlineData(5, "3.3.E", "MM RM MR MR RM", "5.1.E")]

        [Theory]
        public void VerifyMovingInsideTheWorld(int squareWorldSize, string dropLocation, string command, string endLocation)
        {
            Verify(new SquareWalledWorld(squareWorldSize), dropLocation, command, endLocation);
        }

        [InlineData(5, "5.5.N", "MMM", "5.5.N")]
        [InlineData(5, "5.5.E", "MMM", "5.5.E")]
        [InlineData(5, "0.0.S", "MMM", "0.0.S")]
        [InlineData(5, "0.0.W", "MMM", "0.0.W")]
        [Theory]
        public void VerifyBumpingAgainstTheEdgesOfAWalledWorld(int squareWorldSize, string dropLocation, string command, string endLocation)
        {
            Verify(new SquareWalledWorld(squareWorldSize), dropLocation, command, endLocation);
        }

        [InlineData(5, "5.5.N", "M", "5.0.N")]
        [InlineData(5, "5.5.E", "M", "0.5.E")]
        [InlineData(5, "0.0.S", "M", "0.5.S")]
        [InlineData(5, "0.0.W", "M", "5.0.W")]
        [Theory]
        public void VerifyCrossingTheBoundariesOfARoundWorld(int squareWorldSize, string dropLocation, string command, string endLocation)
        {
            Verify(new SquareRoundWorld(squareWorldSize), dropLocation, command, endLocation);
        }

    }
}
