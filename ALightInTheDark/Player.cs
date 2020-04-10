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
    class Player : GameObject
    {

        //Fields

        //This determines if the player is on the ground or not
        private bool grounded;
        private bool drag;

        //This stores the previous keyboard input
        private KeyboardState kbOld;


        //Velocity
        private int velocityX; //the speed the player is moving left/right
        private int velocityY; //the speed the player is moving up/down
        private int velocityXMax; //the max speed the player can move left/right
        private int velocityXMin; //the min speed the player can move left/right
        private int velocityYMax; //the max speed the player can move up/down
        private int velocityYMin; //the min speed the player can move up/down

        //Acceleration
        private int accelX; //The rate at which the horizontal speed is changing
        private int accelY; //The rate at which the vertical speed is changing


        //Properties
        /// <summary>
        /// Get/set if the player is grounded or not
        /// </summary>
        public bool Grounded
        {
            get
            {
                return grounded;
            }
            set
            {
                grounded = value;
            }
        }

        /// <summary>
        /// Get/set the X velocity
        /// </summary>
        public int VelocityX
        {
            get
            {
                return velocityX;
            }
            set
            {
                velocityX = value;
            }
        }

        /// <summary>
        /// Get/set the Y velocity
        /// </summary>
        public int VelocityY
        {
            get
            {
                return velocityY;
            }
            set
            {
                velocityY = value;
            }
        }

        /// <summary>
        /// get/set the X acceleration
        /// </summary>
        public int AccelX
        {
            get
            {
                return accelX;
            }
            set
            {
                accelX = value;
            }
        }

        /// <summary>
        /// get/set the Y acceleration
        /// </summary>
        public int AccelY
        {
            get
            {
                return accelY;
            }
            set
            {
                accelY = value;
            }
        }
        
        //Constructor
        /// <summary>
        /// Creates a new instance of a player object
        /// </summary>
        /// <param name="pos">Positional rect.</param>
        /// <param name="texture">Texture to use</param>
        public Player(Rectangle pos, Texture2D texture)
            : base(pos, texture)
        {
            velocityX = 0;
            velocityY = 0;
            velocityXMax = 16;
            velocityXMin = -16;
            velocityYMax = 24;
            velocityYMin = -24;
            accelX = 0;
            accelY = 0;

        }


        //Method

        /// <summary>
        /// Method to manage the player movement, using the value of accel., & how fast the player can move
        /// </summary>
        /// <param name="kb">keyboard state</param>
        public void Movement(KeyboardState kb)
        {
            //Updates velocity
            if (drag && Math.Abs(accelX) > Math.Abs(velocityX))
            {
                velocityX = 0;
                velocityY += accelY;
            }
            else
            {
                velocityX += accelX;
                velocityY += accelY;
            }
            

            if (kbOld != null) //Prevents movement on the first frame, in order to have a value in kbOld
            {
                //Run and Jump update the x acceleration and y velocity
                Run(kb);

                //Activates jump method if the player presses a jump button
                if (kb.IsKeyDown(Keys.W) || kb.IsKeyDown(Keys.Space))
                    Jump();

                //The player will accelerate downwards if in the air
                if (!grounded)
                {
                    accelY = 1;
                }

                //Prevents the player from going too fast
                if (velocityX > velocityXMax)
                    velocityX = velocityXMax;
                if (velocityX < velocityXMin)
                    velocityX = velocityXMin;
                if (velocityY > velocityYMax)
                    velocityY = velocityYMax;
                if (velocityY < velocityYMin)
                    velocityY = velocityYMin;

            }

            kbOld = kb;
        }

        
        /// <summary>
        /// Reads user input, and determines whether the player should be accelerating. It is called by the movement manager
        /// </summary>
        /// <param name="kb">keyboard state</param>
        private void Run(KeyboardState kb)
        {
            //'flow' changes based on whether the player is in the air or not. If they are in the air, their movement is dulled.
            int flow = 1;
            if (grounded)
                flow = 2; //when on the ground, movement is normal

            //If one key is pressed, and not the other, the player will move. both keys being pressed results in no movement
            if (kb.IsKeyDown(Keys.A) && !kb.IsKeyDown(Keys.D))
            { //Accelerate left
                accelX = -flow;
                drag = false;
            }
            else if (kb.IsKeyDown(Keys.D) && !kb.IsKeyDown(Keys.A))
            { //Accelerate Right
                accelX = flow;
                drag = false;
            }
            else //If the user is not pressing a movement button, They will accelerate in the opposite direction from the one they're moving in
            {
                if (velocityX > 0)
                    accelX = -flow;
                if (velocityX < 0)
                    accelX = flow;
                drag = true;
            }
        }

        /// <summary>
        /// Makes player jump if this is the first frame the jump button is held & player is grounded
        /// </summary>
        private void Jump()
        {
            if (!kbOld.IsKeyDown(Keys.W) && !kbOld.IsKeyDown(Keys.Space) && grounded) //Makes sure that this is a fresh press, and that the player is on the ground before jumping
            {
                velocityY = velocityYMin; //This sets the player to the maximum upward velocity
                grounded = false;
            }
        }

    }
}
