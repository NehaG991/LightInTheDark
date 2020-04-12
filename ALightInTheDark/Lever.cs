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
    class Lever : GameObject
    {

        //Fields
        //The ID is for determining what the lever will interact with
        private int id;
        //This is storing the lever's position
        private Rectangle pos;


        //Properties
        public int Id { get { return id; } }


        //Constructor
        public Lever(Rectangle pos, Texture2D texture, int id)
            : base(pos, texture)
        {
            this.id = id;
            this.pos = pos;
        }

        //This method returns the id of the lever, which can be used to toggle any gate connected to it.
        //This method should only be called if the player has pressed the activation button on the current frame, but not the previous frame.
        //With the way this is set up, the id for any gate or lever should only ever be 0 or greater.
        public int ToggleConnectedDoors(Rectangle playerPos)
        {
            if (pos.Intersects(playerPos))
            {
                return id;
            }
            return -1;
        }

    }
}
