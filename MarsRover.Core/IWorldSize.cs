namespace MarsRover.Core
{
    public interface IWorldSize
    {
        int MaxY { get; }
        int YPositionNorthOfBoundary { get; }

        int MaxX { get; }
        int XPositionEastOfBoundary { get; }

        int MinY { get; }
        int YPositionSouthOfBoundary { get; }

        int MinX { get; }
        int XPositionWestOfBoundary { get; }
    }
}