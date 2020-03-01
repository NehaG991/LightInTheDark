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
    class GameObject
    {
        //Fields
        protected Rectangle position;
        protected Texture2D texture;

        //Properties
        public Rectangle Position { get { return position; } }
        public int X
        {
            get
            {
                return position.X;
            }
            set
            {
                position.X = value;
            }
        }
        public int Y
        {
            get
            {
                return position.Y;
            }
            set
            {
                position.Y = value;
            }
        }

        //Constructor
        /*public GameObject(int x, int y, int width, int height, Texture2D image)
        {
            position = new Rectangle(x, y, width, height);
            this.texture = image;
        }*/

        public GameObject(Rectangle pos)
        {
            position = pos;
        }


        //Methods

        //Draws the object
        /*public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, Color.White);
        }*/
    }
}
