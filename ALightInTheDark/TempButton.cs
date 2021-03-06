﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ALightInTheDark
{
    /// <summary>
    /// This is just a temporary button class in order give an example
    /// of what the finite state machine will look like when we implement
    /// the actual button class
    /// </summary>
    class TempButton
    {
        private Texture2D defaultTexture, hoverTexture;
        private Rectangle rectangle;
        private bool hover;
        private bool clicked;

        public bool Clicked { set { clicked = value; } }

        // Temporary constructor to hold a texture and rectangle
        public TempButton(Texture2D defaultTexture, Rectangle rectangle)
        {
            this.rectangle = rectangle;
            this.defaultTexture = defaultTexture;
        }

        // Temporary constructor with two textures and rectangle
        public TempButton(Texture2D defaultTexture, Texture2D hoverTexture, Rectangle rectangle)
        {
            this.rectangle = rectangle;
            this.defaultTexture = defaultTexture;
            this.hoverTexture = hoverTexture;
            hover = false;
        }

        /// <summary>
        /// Check whether a button is clicked on or hovered over
        /// </summary>
        /// <returns></returns>
        public bool Click()
        {
            MouseState mouse = Mouse.GetState();

            if (rectangle.Contains(mouse.X, mouse.Y))
            {
                hover = true;

                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    return true;
                }
            }
            else
            {
                hover = false;
            }

            return false;
        }

        /// <summary>
        /// Change a player movement key
        /// </summary>
        /// <param name="mKey">The movement key to change</param>
        /// <param name="player">The player whos movement will change</param>
        public void ControlEdit(MovementKeys mKey, Player player)
        {
            // Gets all keys currently pressed and stores them
            KeyboardState kbState = Keyboard.GetState();
            Keys[] keyArray = kbState.GetPressedKeys();

            // If there are keys pressed, changes the player's movement key to the new one
            if (keyArray.Length > 0)
            {
                player.ChangeKeys(mKey, keyArray[0]);
            }
        }

        /// <summary>
        /// Draw the button with the default texture
        /// </summary>
        /// <param name="sb"></param>
        public void DrawButton(SpriteBatch sb)
        {
            if (hover)
            {
                sb.Draw(hoverTexture, rectangle, Color.White);
            }
            else
            {
                sb.Draw(defaultTexture, rectangle, Color.White);
            }
        }
    }
}
