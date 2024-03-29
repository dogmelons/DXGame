﻿using System.Collections.Generic;
using System.Diagnostics;
using DXGame.Core.Components.Advanced;
using DXGame.Main;
using Microsoft.Xna.Framework;

namespace DXGame.Core.Generators
{
    public class PlayerGenerator : Generator<GameObject>
    {
        // TODO: Make player sprites scalable
        private const string PLAYER_NONE = "PlayerNone";
        private const string PLAYER_WALKING = "PlayerWalking";
        private const string PLAYER_JUMPING = "PlayerJumping";
        private const string PLAYER_2 = "Player2";
        private static readonly Vector2 MAX_VELOCITY = new Vector2(5.0f, 20.0f);
        private readonly SimplePlayerInputComponent input_;
        private readonly PhysicsComponent physics_;
        private readonly SpatialComponent space_;
        private readonly StateComponent state_;
        private readonly AnimationComponent animation_;
        private readonly WeaponComponent weapon_;

        // Addendum to prior TODO: change isLocalPlayer to something that's not a bool
        public PlayerGenerator(DxGame game, Vector2 playerPosition, Rectangle bounds)
        {
            space_ =
                (BoundedSpatialComponent) new BoundedSpatialComponent(game).WithXMin(bounds.X)
                    .WithXMax(bounds.Width)
                    .WithXMin(bounds.Y)
                    .WithYMax(bounds.Height)
                    .WithDimensions(new Vector2(50, 100)) // TODO: un-hard code these
                    .WithPosition(playerPosition);
            physics_ = new MapCollideablePhysicsComponent(game).WithMaxVelocity(MAX_VELOCITY).WithPositionalComponent(space_);
            state_ = new StateComponent(game);
            AddPlayerStates();
            animation_ = new AnimationComponent(game).WithPosition(space_).WithState(state_);
            AddPlayerAnimations();
            weapon_ = new RangedWeaponComponent(game).WithPhysicsComponent(physics_).WithDamage(50);
            input_ = new SimplePlayerInputComponent(game).WithPhysics(physics_).WithPlayerState(state_).WithWeapon(weapon_);
        }

        public override List<GameObject> Generate()
        {
            var objects = new List<GameObject>();
            var player = new GameObject();
            player.WithComponents(space_, physics_, animation_, input_, state_, weapon_);
            objects.Add(player);
            return objects;
        }

        public SpatialComponent PlayerSpace
        {
            get { return space_; }
        }

        private void AddPlayerStates()
        {
            Debug.Assert(state_ != null, "StateComponent cannot be null during AddPlayerStates");
            state_.AddStates("None", "Walking", "Jumping");
        }

        private void AddPlayerAnimations()
        {
            Debug.Assert(animation_ != null, "AnimationComponent cannot be null during AddPlayerAnimations");
            animation_.AddAnimation("None", PLAYER_NONE);
            animation_.AddAnimation("Walking", PLAYER_WALKING);
            animation_.AddAnimation("Jumping", PLAYER_JUMPING);
        }
    }
}