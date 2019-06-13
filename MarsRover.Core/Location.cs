using System;
using System.Collections.Generic;
using System.Drawing;

namespace MarsRover.Core
{
    public class Location : IEquatable<Location>
    {
        #region construction
        private readonly Point point;
        private readonly Facing facing;

        public Location(string dropLocation): this(ExtractPointFrom(dropLocation), ExtractFacingFrom(dropLocation)) { }

        public Location(Point point, Facing facing)
        {
            this.point = point;
            this.facing = facing;
        }
        #endregion

        #region relative locations
        internal Location MoveForward(IWorldSize worldSize)
        {
            if (facing == Facing.North) return new Location(PositionNorthOfMe(worldSize), facing);
            if (facing == Facing.South) return new Location(PositionSouthOfMe(worldSize), facing);
            if (facing == Facing.East)  return new Location(PositionEastOfMe(worldSize), facing);
            if (facing == Facing.West)  return new Location(PositionWestOfMe(worldSize), facing);

            throw new InvalidOperationException(facing + " is not supported");
        }

        internal Location PivotLeft() { return new Location(new Point(point.X, point.Y), PivotFacingLeft()); }
        internal Location PivotRight() { return new Location(new Point(point.X, point.Y), PivotFacingRight()); }

        private Facing PivotFacingRight()
        {
            if (facing == Facing.West) return Facing.North;
            return facing + 1;
        }
        private Facing PivotFacingLeft()
        {
            if (facing == Facing.North) return Facing.West;
            return facing - 1;
        }
        private Point PositionNorthOfMe(IWorldSize worldSize)
        {
            if (point.Y == worldSize.MaxY) return new Point(point.X, worldSize.YPositionNorthOfBoundary);
            return new Point(point.X, point.Y + 1);
        }
        private Point PositionEastOfMe(IWorldSize worldSize)
        {
            if (point.X == worldSize.MaxX) return new Point(worldSize.XPositionEastOfBoundary, point.Y);
            return new Point(point.X + 1, point.Y);
        }
        private Point PositionSouthOfMe(IWorldSize worldSize)
        {
            if (point.Y == worldSize.MinY) return new Point(point.X, worldSize.YPositionSouthOfBoundary);
            return new Point(point.X, point.Y - 1);
        }
        private Point PositionWestOfMe(IWorldSize worldSize)
        {
            if (point.X == worldSize.MinX) return new Point(worldSize.XPositionWestOfBoundary, point.Y);
            return new Point(point.X - 1, point.Y);
        }
        #endregion

        #region Location string conversion
        public override string ToString() { return point.X + "." + point.Y + "." + facing.ToString().Remove(1); }

        private static Point ExtractPointFrom(string location)
        {
            var locationParts = Parse(location);
            if (!int.TryParse(locationParts[0], out int x)) ThrowInvalidLocationCommandException(location);
            if (!int.TryParse(locationParts[1], out int y)) ThrowInvalidLocationCommandException(location);

            return new Point(x, y);
        }

        private static Facing ExtractFacingFrom(string location)
        {
            var locationParts = Parse(location);

            switch (locationParts[2])
            {
                case "N": return Facing.North;
                case "E": return Facing.East;
                case "S": return Facing.South;
                case "W": return Facing.West;

                default: return ThrowInvalidLocationCommandException(location);
            }
        }

        private static string[] Parse(string location)
        {
            var locationParts = location.Split('.');
            if (locationParts.Length != 3) ThrowInvalidLocationCommandException(location);

            return locationParts;
        }

        private static Facing ThrowInvalidLocationCommandException(string location)
        {
            throw new ArgumentOutOfRangeException(location + " should have format: 'int.int.N|E|S|W' ");
        }
        #endregion

        #region equality
        public override bool Equals(object obj)
        {
            return Equals(obj as Location);
        }

        public bool Equals(Location other)
        {
            return other != null &&
                   point.Equals(other.point) &&
                   facing == other.facing;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(point, facing);
        }

        public static bool operator ==(Location location1, Location location2)
        {
            return EqualityComparer<Location>.Default.Equals(location1, location2);
        }

        public static bool operator !=(Location location1, Location location2)
        {
            return !(location1 == location2);
        }
        #endregion
    }

    public enum Facing
    {
        North,
        East,
        South,
        West
    }

}