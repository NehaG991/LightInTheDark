using System;
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

        // Empty constructor to be replaced
        public TempButton()
        {

        }

        // Empty method to give an example
        // Like when this button is clicked the FSM
        // does this etc.
        public bool Click()
        {
            return true;
        }

        // Temporary constructor to hold a texture and rectangle
        public TempButton(Texture2D defaultTexture, Rectangle rectangle)
        {
            this.rectangle = rectangle;
            this.defaultTexture = defaultTexture;
        }

        /// <summary>
        /// Draw the button with the default texture
        /// </summary>
        /// <param name="sb"></param>
        public void DrawButton(SpriteBatch sb)
        {
            sb.Draw(defaultTexture, rectangle, Color.White);
        }
    }
}
