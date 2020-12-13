using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020.Grid
{
    public static class MovementDirectionHelper
    {
        public static bool TryParse(
            string directionString, 
            bool invertY,
            out MovementDirection direction)
        {
            direction = MovementDirection.Down;
            bool result = false;
            switch (directionString.ToLower())
            {
                case "east":
                    direction = MovementDirection.Right;
                    result = true;
                    break;
                case "west":
                    direction = MovementDirection.Left;
                    result = true;
                    break;
                case "north":
                    direction = MovementDirection.Up;
                    if (invertY)
                        direction = MovementDirection.Down;
                    result = true;
                    break;
                case "south":
                    direction = MovementDirection.Down;
                    if (invertY)
                        direction = MovementDirection.Up;
                    result = true;
                    break;
            }
            return result;
        }
        public static MovementDirection GetOppositeDirection(MovementDirection direction)
        {
            return direction switch
            {
                MovementDirection.Down => MovementDirection.Up,
                MovementDirection.Left => MovementDirection.Right,
                MovementDirection.Right => MovementDirection.Left,
                MovementDirection.Up => MovementDirection.Down,
                MovementDirection.In => MovementDirection.Out,
                MovementDirection.Out => MovementDirection.In,
                _ => throw new Exception($"Invalid movement direction {direction}"),
            };
        }

        public static MovementDirection Rotate(MovementDirection direction, int degrees, bool rotateLeft)
        {
            int currentAngle = 0;
            if (MovementDirection.Right.Equals(direction))
            {
                currentAngle = 0;
            }
            else if (MovementDirection.Up.Equals(direction))
            {
                currentAngle = 90;
            }
            else if (MovementDirection.Left.Equals(direction))
            {
                currentAngle = 180;
            }
            else if (MovementDirection.Down.Equals(direction))
            {
                currentAngle = 270;
            }

            int newAngle = rotateLeft ? currentAngle + degrees : currentAngle - degrees;
            newAngle %= 360;
            if (newAngle < 0)
            {
                newAngle += 360;
            }
            var nextDirection = MovementDirection.Right;
            if (newAngle == 0)
            {
                nextDirection = MovementDirection.Right;
            }
            else if (newAngle == 90)
            {
                nextDirection = MovementDirection.Up;
            }
            else if (newAngle == 180)
            {
                nextDirection = MovementDirection.Left;
            }
            else if (newAngle == 270)
            {
                nextDirection = MovementDirection.Down;
            }
            return nextDirection;
        }
    }
}
