﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace DXGame.Core.Messaging
{
    public enum CollisionDirection
    {
        North,
        East,
        South,
        West
    }

    public class CollisionMessage : Message
    {
        public List<CollisionDirection> CollisionDirections { get; set; }

        public CollisionMessage()
        {
            CollisionDirections = new List<CollisionDirection>();
        }

        public CollisionMessage WithDirection(CollisionDirection direction)
        {
            CollisionDirections.Add(direction);
            return this;
        }

        public CollisionMessage(Vector2 collisionVector)
            : this()
        {
            if (collisionVector.X > 0)
            {
                WithDirection(CollisionDirection.East);
            }
            if (collisionVector.X < 0)
            {
                WithDirection(CollisionDirection.West);
            }
            if (collisionVector.Y > 0)
            {
                WithDirection(CollisionDirection.South);
            }
            if (collisionVector.Y < 0)
            {
                WithDirection(CollisionDirection.North);
            }
        }
    }
}