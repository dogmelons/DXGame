﻿using System.Runtime.InteropServices;
using Microsoft.Xna.Framework.Input;

namespace DXGame.Core.Simple
{
    public class SimplePlayer : SimpleSprite
    {
        private const string playerName_ = "Player";
        private SimpleMap map_;

        private const char PLAYER_CHARACTER = 'Z';

        public SimplePlayer(SimpleMap map)
           : base(playerName_)
        {
            map_ = map;
            position_.X = map.GetPlayerPosition().X * BLOCK_WIDTH;
            position_.Y = map.GetPlayerPosition().Y * BLOCK_WIDTH;
        }

        private void Move(int x, int y)
        {
            float updatedX = position_.X + x;
            float updatedY = position_.Y + y;

            if ((updatedX > 0) && (updatedX < map_.GetMapSize().X))
            {
                position_.X += x;
            }
            if ((updatedY > 0) && ((updatedY + space_.Height) < map_.GetMapSize().Y))
            {
                position_.Y += y;
            }
        }

        private void HandleInput()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            Keys [] pressedKeys = keyboardState.GetPressedKeys();
            foreach (Keys key in pressedKeys)
            {
                switch (key)
                {
                case Keys.Left:
                    Move(-5, 0);
                    break;
                case Keys.Right:
                    Move(5, 0);
                    break;
                case Keys.Up:
                    Move(0, -5);
                    break;
                case Keys.Down:
                    Move(0, 5);
                    break;
                }
            }
        }

        public static bool CanCreateFrom(char character)
        {
            return character == PLAYER_CHARACTER;
        }

        public bool Update()
        {
            HandleInput();
            return true;
        }
    }
}
