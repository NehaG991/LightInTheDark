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
        KeyboardState previousKbState = new KeyboardState();
        State gameState = State.MainMenu;
        State prevState = State.MainMenu; // Used to access the previous game state (like with a back button)

        TempButton start;
        TempButton options;
        TempButton controls;
        TempButton quit;
        TempButton back;
        TempButton restart;
        TempButton resume;
        bool win = false;

        List<GameObject> walls;

        // sprite textures
        Texture2D platform;
        Texture2D player;
        Texture2D easyIndicator;

        // levels
        LevelReader test;

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

            // show the mouse
            this.IsMouseVisible = true;

            // changing window size
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 725;
            graphics.ApplyChanges();

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

            // create the buttons
            start = new TempButton(Content.Load<Texture2D>("startButton"), Content.Load<Texture2D>("startButtonHover"), new Rectangle(GraphicsDevice.Viewport.Width / 2 - GraphicsDevice.Viewport.Width / 8, GraphicsDevice.Viewport.Height / 2 - 100, GraphicsDevice.Viewport.Width / 4, GraphicsDevice.Viewport.Height / 8));
            options = new TempButton(Content.Load<Texture2D>("optionsButton"), Content.Load<Texture2D>("optionsButtonHover"), new Rectangle(GraphicsDevice.Viewport.Width / 2 - GraphicsDevice.Viewport.Width / 8, GraphicsDevice.Viewport.Height / 2, GraphicsDevice.Viewport.Width / 4, GraphicsDevice.Viewport.Height / 8));
            controls = new TempButton(Content.Load<Texture2D>("controlsButton"), Content.Load<Texture2D>("controlsButtonHover"), new Rectangle(GraphicsDevice.Viewport.Width / 2 - GraphicsDevice.Viewport.Width / 8, GraphicsDevice.Viewport.Height / 2 + 100, GraphicsDevice.Viewport.Width / 4, GraphicsDevice.Viewport.Height / 8));
            quit = new TempButton(Content.Load<Texture2D>("quitButton"), Content.Load<Texture2D>("quitButtonHover"), new Rectangle(GraphicsDevice.Viewport.Width / 2 - GraphicsDevice.Viewport.Width / 8, GraphicsDevice.Viewport.Height / 2 + 200, GraphicsDevice.Viewport.Width / 4, GraphicsDevice.Viewport.Height / 8));
            back = new TempButton(Content.Load<Texture2D>("backButton"), Content.Load<Texture2D>("backButtonHover"), new Rectangle(GraphicsDevice.Viewport.Width / 2 + 200, GraphicsDevice.Viewport.Height / 2 + 150, GraphicsDevice.Viewport.Width / 4, GraphicsDevice.Viewport.Height / 8));
            resume = new TempButton(Content.Load<Texture2D>("resumeButton"), Content.Load<Texture2D>("resumeButtonHover"), new Rectangle(GraphicsDevice.Viewport.Width / 2 - GraphicsDevice.Viewport.Width / 8, GraphicsDevice.Viewport.Height / 2 - 100, GraphicsDevice.Viewport.Width / 4, GraphicsDevice.Viewport.Height / 8));
            restart = new TempButton(Content.Load<Texture2D>("restartButton"), Content.Load<Texture2D>("restartButtonHover"), new Rectangle(GraphicsDevice.Viewport.Width / 2 - GraphicsDevice.Viewport.Width / 8, GraphicsDevice.Viewport.Height / 2, GraphicsDevice.Viewport.Width / 4, GraphicsDevice.Viewport.Height / 8));



            // sprite loading
            platform = Content.Load<Texture2D>("platform");
            player = Content.Load<Texture2D>("Player");
            easyIndicator = Content.Load<Texture2D>("easyIndicator");


            // loading levels
            // files must be in the debug folder to work
            test = new LevelReader(platform, player, @"test.level");
            test.ReadFile();
            walls = test.Interactable;
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
                    {
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
                            Exit();
                        }
                        break;
                    }
                case State.Options:
                    {
                        // Various buttons for options and their functions
                        if (back.Click())
                        {
                            gameState = prevState;
                        }
                        break;
                    }
                case State.Controls:
                    {
                        // Various buttons for controls and their functions
                        if (back.Click())
                        {
                            gameState = prevState;
                        }
                        break;
                    }
                case State.Pause:
                    {
                        if (resume.Click())
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
                    }
                case State.Game:
                    {
                        previousKbState = kbState;
                        kbState = Keyboard.GetState();
                        MovementManager(test.Player);
                        if (win)
                        {
                            gameState = State.Victory;
                        }
                        if (SingleKeyPress(Keys.P))
                        {
                            prevState = State.Game;
                            gameState = State.Pause;
                        }
                        if (SingleKeyPress(Keys.G))
                        {
                            gameState = State.EasyMode;
                        }
                        // Other game update code
                        break;
                    }
                case State.Victory:
                    {
                        if (start.Click())
                        {
                            // Move on to the next level
                        }
                        if (quit.Click())
                        {
                            gameState = State.MainMenu;
                        }
                        break;
                    }
                case State.EasyMode:
                    {
                        previousKbState = kbState;
                        kbState = Keyboard.GetState();
                        MovementManager(test.Player);
                        if (win)
                        {
                            gameState = State.Victory;
                        }
                        if (SingleKeyPress(Keys.P))
                        {
                            prevState = State.EasyMode;
                            gameState = State.Pause;
                        }
                        if (SingleKeyPress(Keys.G))
                        {
                            gameState = State.Game;
                        }
                        // Easy game update code
                        break;
                    }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Removing this temporarily to prevent flashing
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            switch (gameState)
            {
                case State.MainMenu:
                    {
                        // Draw the main menu buttons
                        start.DrawButton(spriteBatch);
                        options.DrawButton(spriteBatch);
                        controls.DrawButton(spriteBatch);
                        quit.DrawButton(spriteBatch);
                        break;
                    }
                case State.Options:
                    {
                        // Draw any options we have
                        back.DrawButton(spriteBatch);
                        break;
                    }
                case State.Controls:
                    {
                        // Draw the control buttons
                        back.DrawButton(spriteBatch);
                        break;
                    }
                case State.Pause:
                    {
                        // Draw the pause buttons
                        resume.DrawButton(spriteBatch);
                        restart.DrawButton(spriteBatch);
                        controls.DrawButton(spriteBatch);
                        quit.DrawButton(spriteBatch);
                        break;
                    }
                case State.Game:
                    {
                        // Draw all the game stuff


                        


                        // test level
                        // drawing platforms
                        for (int i = 0; i < test.Interactable.Count; i++)
                        {
                            test.Interactable[i].Draw(spriteBatch);
                        }

                        //drawing player
                        test.Player.Draw(spriteBatch);

                        break;
                    }
                case State.Victory:
                    {
                        // Draw the victory stuff
                        break;
                    }
                case State.EasyMode:
                    {
                        // Draw all the easy mode game stuff
                        
                        
                        // draw easy mode indicator
                        spriteBatch.Draw(easyIndicator, new Vector2(50, 50), Color.White);
                        
                        // drawing platforms
                        for (int i = 0; i < test.Interactable.Count; i++)
                        {
                            test.Interactable[i].Draw(spriteBatch);
                        }

                        //drawing player
                        test.Player.Draw(spriteBatch);

                        break;
                    }
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
                
            }
        }

        /// <summary>
        /// Checks to see if a single key has been pressed
        /// </summary>
        /// <param name="key">The key to check</param>
        /// <returns>True if a single key has been pressed, false if it hasn't</returns>
        public bool SingleKeyPress(Keys key)
        {
            if (kbState.IsKeyDown(key) && !previousKbState.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }

    }
}
