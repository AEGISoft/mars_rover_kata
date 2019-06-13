
namespace MarsRover.Core
{
    public class SquareRoundWorld : IWorldSize
    {
        #region construction
        private readonly int maxWorldSize;

        public SquareRoundWorld(int squareWorldSize)
        {
            maxWorldSize = squareWorldSize;
        }
        #endregion

        public int MaxY => maxWorldSize;
        public int YPositionNorthOfBoundary => 0;

        public int MaxX => maxWorldSize;
        public int XPositionEastOfBoundary => 0;

        public int MinY => 0;
        public int YPositionSouthOfBoundary => maxWorldSize;

        public int MinX => 0;
        public int XPositionWestOfBoundary => maxWorldSize;
    }
}