﻿using System.Diagnostics;
using DXGame.Core.Components.Basic;
using DXGame.Core.Utils;
using DXGame.Main;
using Microsoft.Xna.Framework;

namespace DXGame.Core.Components.Advanced
{
    /**
    <summary>

    </summary>
    */

    public class PhysicsComponent : Component
    {
        protected Vector2 acceleration_;
        protected Vector2 maxVelocity_;
        protected Vector2 maxAcceleration_;
        protected PositionalComponent position_;
        protected Vector2 velocity_;

        public PhysicsComponent(DxGame game)
            : base(game)
        {
            maxVelocity_ = new Vector2(5.0f, 5.0f);
            maxAcceleration_ = new Vector2(5.0f, 5.0f);
            UpdatePriority = UpdatePriority.HIGHEST;
        }

        public virtual Vector2 Velocity
        {
            get { return velocity_; }
            set { velocity_ = VectorUtils.ConstrainVector(value, maxVelocity_); }
        }

        public virtual Vector2 Acceleration
        {
            get { return acceleration_; }
            set { acceleration_ = VectorUtils.ConstrainVector(value, maxAcceleration_); }
        }

        public PhysicsComponent WithVelocity(Vector2 velocity)
        {
            Debug.Assert(velocity != null, "PhysicsComponent cannot be initialized with null velocity");
            velocity_ = velocity;
            return this;
        }

        public PhysicsComponent WithAcceleration(Vector2 acceleration)
        {
            Debug.Assert(acceleration != null, "PhysicsComponent cannot be initialized with null acceleration");
            acceleration_ = acceleration;
            return this;
        }

        public PhysicsComponent WithPositionalComponent(PositionalComponent position)
        {
            Debug.Assert(position != null, "PhysicsComponent cannot be initialized with null position");
            position_ = position;
            return this;
        }

        public PhysicsComponent WithMaxVelocity(Vector2 maxVelocity)
        {
            Debug.Assert(maxVelocity != null, "PhysicsComponent cannot be initialized with a null maximum velocity ");
            maxVelocity_ = maxVelocity;
            return this;
        }

        public PhysicsComponent WithMaxAcceleration(Vector2 maxAcceleration)
        {
            Debug.Assert(maxAcceleration != null,
                "PhysicsComponent cannot be initialized with a null maximum acceleration ");
            maxAcceleration_ = maxAcceleration;
            return this;
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 acceleration = Acceleration;
            Vector2 velocity = VectorUtils.ConstrainVector(Velocity + acceleration_, maxVelocity_);
            Vector2 previousPosition = position_.Position;
            position_.Position += velocity;

            // These checks will cease an object's velocity and acceleration in a direction if it was unable to move
            // TODO: Remove shit code, replace with proper collision

            if (previousPosition.X == position_.Position.X)
            {
                velocity.X = 0;
                acceleration.X = 0;
            }
            if (previousPosition.Y == position_.Position.Y)
            {
                velocity.Y = 0;
                acceleration.Y = 0;
            }

            Velocity = velocity;
            Acceleration = acceleration;
        }
    }
}