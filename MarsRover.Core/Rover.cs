using System;

namespace MarsRover.Core
{
    public class Rover
    {
        #region  construction
        private readonly IWorldSize worldSize;

        public Rover(IWorldSize worldSize, Location dropLocation)
        {
            this.worldSize = worldSize;
            Location = dropLocation;
        }
        #endregion

        public Location Location { get; internal set; }

        public void Move(string command)
        {
            var commandparts = command.Replace(" ", string.Empty).ToCharArray();

            foreach (var moveCommand in commandparts)
            {
                switch (moveCommand)
                {
                    case 'M': Location = Location.MoveForward(worldSize); break;
                    case 'R': Location = Location.PivotRight(); break;
                    case 'L': Location = Location.PivotLeft(); break;

                    default: throw new InvalidOperationException(moveCommand + " is not supported");
                }
            }
        }
    }
}