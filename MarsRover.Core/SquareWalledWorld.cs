using System.Drawing; 

namespace MarsRover.Core 
{
    public class SquareWalledWorld: IWorldSize 
    {
        #region construction
        private Point maxWorldSize; 

        public SquareWalledWorld(int squareWorldSize) 
        { 
            maxWorldSize = new Point(squareWorldSize, squareWorldSize); 
        }
        #endregion

        public int MaxY => maxWorldSize.Y;  
        public int YPositionNorthOfBoundary => maxWorldSize.Y;

        public int MaxX => maxWorldSize.X;
        public int XPositionEastOfBoundary => maxWorldSize.X;

        public int MinY => 0;
        public int YPositionSouthOfBoundary => 0;

        public int MinX => 0;
        public int XPositionWestOfBoundary => 0;
    } 
}