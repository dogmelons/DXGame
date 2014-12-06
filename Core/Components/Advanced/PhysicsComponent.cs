﻿using System.Diagnostics;
using DXGame.Core.Components.Basic;
using DXGame.Core.Utils;
using Microsoft.Xna.Framework;

namespace DXGame.Core.Components.Advanced
{
    /**
    <summary>

    </summary>
    */

    public class PhysicsComponent : UpdateableComponent
    {
        protected Vector2 acceleration_;
        protected Vector2 maxVelocity_;
        protected Vector2 maxAcceleration_;
        protected PositionalComponent position_;
        protected Vector2 velocity_;

        public PhysicsComponent(float maxVelocityX = 5.0f, float maxVelocityY = 10.0f, float maxAccelerationX = 1.0f,
            float maxAccelerationY = 2.5f, PositionalComponent position = null, GameObject parent = null)
            : base(parent)
        {
            maxVelocity_ = new Vector2(maxVelocityX, maxVelocityY);
            maxAcceleration_ = new Vector2(maxAccelerationX, maxAccelerationY);
            position_ = position;
            priority_ = UpdatePriority.NORMAL;
        }

        public virtual Vector2 Velocity
        {
            get { return velocity_; }
            set { velocity_ = value; }
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
            Debug.Assert(maxAcceleration != null, "PhysicsComponent cannot be initialized with a null maximum acceleration ");
            maxAcceleration_ = maxAcceleration;
            return this;
        }

        public override bool Update(GameTime gameTime)
        {
            Velocity = VectorUtils.ConstrainVector(Velocity + acceleration_, maxVelocity_);
            position_.Position += Velocity;
            return true;
        }
    }
}