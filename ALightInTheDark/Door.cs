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
    class Door : GameObject
    {

        //Fields
        //The ID is for determining what the lever will interact with
        private int id;

        //This is storing the lever's position
        private Rectangle pos;

        //determines if the door is open or not
        private bool open;


        //Properties
        public int Id { get { return id; } }


        //Constructor: pos is the lever's position, texture is its texture, id is the value for opening, and open is whether it starts open or closed.
        public Door(Rectangle pos, Texture2D texture, int id, bool open)
            : base(pos, texture)
        {
            this.id = id;
            this.pos = pos;
            this.open = open;
        }

        //This will change whether the door is opened or closed, assuming the criteria are met.
        //Any time the method "ToggleConnectedDoors" from the lever class is called, it should call ToggleDoor for every door, with the ID being input.
        public void ToggleDoor(int inputId)
        {
            if(id == inputId) //if the proper id for this door is input
            {
                if (open) //if open, it changes to closed.
                    open = false;
                else //if closed, it changes to open.
                    open = true;
            }
        }

    }
}
