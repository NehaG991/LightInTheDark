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
    enum State
    {
        MainMenu, Options, Controls, Pause, Game, Victory, EasyMode
    }
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        KeyboardState kbState = new KeyboardState();
        State gameState = State.MainMenu;
        State prevState = State.MainMenu; // Used to access the previous game state (like with a back button)

        TempButton start = new TempButton();
        TempButton options = new TempButton();
        TempButton controls = new TempButton();
        TempButton quit = new TempButton();
        TempButton back = new TempButton();
        TempButton restart = new TempButton();
        bool win = false;

        List<GameObject> walls;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (gameState)
            {
                case State.MainMenu:
                    if (start.Click())
                    {
                        gameState = State.Game;
                    }
                    if (options.Click())
                    {
                        prevState = State.MainMenu;
                        gameState = State.Options;
                    }
                    if (controls.Click())
                    {
                        prevState = State.MainMenu;
                        gameState = State.Controls;
                    }
                    if (quit.Click())
                    {
                        // Code to quit the game
                    }
                    break;
                case State.Options:
                    // Various buttons for options and their functions
                    if (back.Click())
                    {
                        gameState = prevState;
                    }
                    break;
                case State.Controls:
                    // Various buttons for controls and their functions
                    if (back.Click())
                    {
                        gameState = prevState;
                    }
                    break;
                case State.Pause:
                    if (back.Click())
                    {
                        gameState = prevState;
                    }
                    if (controls.Click())
                    {
                        prevState = State.Pause;
                        gameState = State.Controls;
                    }
                    if (restart.Click())
                    {
                        gameState = State.Game;
                        // Code to restart the level
                    }
                    if (quit.Click())
                    {
                        gameState = State.MainMenu;
                    }
                    break;
                case State.Game:
                    if (win)
                    {
                        gameState = State.Victory;
                    }
                    if (kbState.IsKeyDown(Keys.Escape))
                    {
                        prevState = State.Game;
                        gameState = State.Pause;
                    }
                    // Other game update code
                    break;
                case State.Victory:
                    if (start.Click())
                    {
                        // Move on to the next level
                    }
                    if (quit.Click())
                    {
                        gameState = State.MainMenu;
                    }
                    break;
                case State.EasyMode:
                    if (win)
                    {
                        gameState = State.Victory;
                    }
                    if (kbState.IsKeyDown(Keys.Escape))
                    {
                        prevState = State.EasyMode;
                        gameState = State.Pause;
                    }
                    // Easy game update code
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            switch (gameState)
            {
                case State.MainMenu:
                    // Draw the main menu buttons
                    break;
                case State.Options:
                    // Draw any options we have
                    break;
                case State.Controls:
                    // Draw the control buttons
                    break;
                case State.Pause:
                    // Draw the pause buttons
                    break;
                case State.Game:
                    // Draw all the game stuff
                    break;
                case State.Victory:
                    // Draw the victory stuff
                    break;
                case State.EasyMode:
                    // Draw all the easy mode game stuff
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        //This makes the player move each frame, unless obstructed by a wall [This is probably not the greatest way of acomplishing this
        private void MovementManager(Player player)
        {
            //This list will store all wall tiles that the player would hit on its current path
            List<GameObject> wallsHit = new List<GameObject>();
            //THis makes a temporary rectangle where the player would end up if unimpeded
            Rectangle theoreticalPosition = new Rectangle(player.X + player.VelocityX, player.Y + player.VelocityY, player.Position.Width, player.Position.Height);

            for (int i = 0; i < walls.Count; i++)
            {
                if (walls[i].Position.Intersects(theoreticalPosition))
                {
                    wallsHit.Add(walls[i]);
                }
            }
            if(wallsHit.Count == 0)
            {
                player.X += player.VelocityX;
                player.Y += player.VelocityY;
            }
            else
            {
                int i = 0;
                int max = Math.Abs(player.VelocityX) + Math.Abs(player.VelocityY);
                while (i < max)
                {
                    //TO DO: SLOWLY INCREMENT THE POSITION UNTIL A COLLISION OCCURS, THEN STOP WHEN COLLIDE
                }
            }


        }

    }
}
