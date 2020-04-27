using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
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
        SpriteFont font;

        KeyboardState kbState = new KeyboardState();
        KeyboardState kbOld = new KeyboardState();
        State gameState = State.MainMenu;
        State prevState = State.MainMenu; // Used to access the previous game state (like with a back button)

        TempButton start;
        TempButton options;
        TempButton controls;
        TempButton quit;
        TempButton back;
        TempButton restart;
        TempButton resume;
        TempButton easy;
        bool win = false;
        int deaths = 0;
        int level = 1;
        Stopwatch stopwatch = new Stopwatch();
        float time = 0f;
        bool godMode;

        //This is half of the win condition the other half is making contact with the door
        bool doorOpen;

        // Control button textures & variables
        Texture2D jumpTitle;
        TempButton jump;
        Texture2D leftTitle;
        TempButton left;
        Texture2D rightTitle;
        TempButton right;
        Texture2D godModeTitle;
        TempButton godModeSelect;
        TempButton godModeChecked;

        List<GameObject> walls;
        List<GameObject> inputObjects;
        GameObject oDoor;
        GameObject clDoor;

        // sprite textures
        Texture2D platform;
        Texture2D player;
        Texture2D easyIndicator;
        Texture2D flashlight;
        Texture2D closedDoor;
        Texture2D openDoor;
        Texture2D leverBefore;
        Texture2D leverAfter;
        Texture2D buttonBefore;
        Texture2D buttonAfter;


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
            easy = new TempButton(Content.Load<Texture2D>("startButton"), Content.Load<Texture2D>("startButtonHover"), new Rectangle(GraphicsDevice.Viewport.Width / 2 - GraphicsDevice.Viewport.Width / 8, GraphicsDevice.Viewport.Height / 2 - 100, GraphicsDevice.Viewport.Width / 4, GraphicsDevice.Viewport.Height / 8));

            // Load the font
            font = Content.Load<SpriteFont>("font");


            // sprite loading
            platform = Content.Load<Texture2D>("platform");
            player = Content.Load<Texture2D>("Player");
            easyIndicator = Content.Load<Texture2D>("easyIndicator");
            flashlight = Content.Load<Texture2D>("flashlight");
            closedDoor = Content.Load<Texture2D>("closeDoor");
            openDoor = Content.Load<Texture2D>("openDoor");
            leverBefore = Content.Load<Texture2D>("leverBefore");
            leverAfter = Content.Load<Texture2D>("leverAfter");
            buttonBefore = Content.Load<Texture2D>("buttonBefore");
            buttonAfter = Content.Load<Texture2D>("buttonAfter");
            godMode = false;
            
            // Control loading
            Texture2D select = Content.Load<Texture2D>("selectionBox");
            Texture2D check = Content.Load<Texture2D>("selectionBoxChecked");
            jump = new TempButton(select, select, new Rectangle(250, 60, select.Width, select.Height));
            left = new TempButton(select, select, new Rectangle(250, 150, select.Width, select.Height));
            right = new TempButton(select, select, new Rectangle(250, 250, select.Width, select.Height));
            jumpTitle = Content.Load<Texture2D>("jumpButton");
            leftTitle = Content.Load<Texture2D>("leftButton");
            rightTitle = Content.Load<Texture2D>("rightButton");
            godModeTitle = Content.Load<Texture2D>("godMode");
            godModeSelect = new TempButton(select, select, new Rectangle(350, 390, select.Width, select.Height));
            godModeChecked = new TempButton(check, check, new Rectangle(350, 390, select.Width, select.Height));

            // loading levels
            // files must be in the debug folder to work
            test = new LevelReader(platform, player, openDoor, closedDoor, leverBefore, leverAfter, buttonBefore, buttonAfter, @"test.level");
            test.ReadFile();
            walls = test.Interactable;
            inputObjects = test.InputObjects;
            clDoor = test.DoorClose;
            oDoor = test.DoorOpen;

            doorOpen = false;
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
                            stopwatch.Start();
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
                        // enable god mode
                        if (easy.Click())
                        {
                            godMode = true;
                        }

                        break;
                    }
                case State.Controls:
                    {
                        if (jump.Click())
                        {
                            jump.ControlClick(MovementKeys.jumpKey, test.Player);
                        }
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
                            if (godMode)
                            {
                                gameState = State.EasyMode;
                            }
                            else
                            {
                                gameState = State.Game;
                                stopwatch.Start();
                            }
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
                        kbOld = kbState;
                        kbState = Keyboard.GetState();
                        MovementManager(test.Player);
                        time = stopwatch.ElapsedMilliseconds / 1000;
                        if (win)
                        {
                            gameState = State.Victory;
                        }
                        if (SingleKeyPress(Keys.P))
                        {
                            prevState = State.Game;
                            gameState = State.Pause;
                            stopwatch.Stop();
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
                        kbOld = kbState;
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
                        easy.DrawButton(spriteBatch);
                        break;
                    }
                case State.Controls:
                    {
                        spriteBatch.Draw(jumpTitle, new Rectangle(25, 50, jumpTitle.Width, jumpTitle.Height), Color.White);
                        spriteBatch.Draw(leftTitle, new Rectangle(25, 150, leftTitle.Width, leftTitle.Height), Color.White);
                        spriteBatch.Draw(rightTitle, new Rectangle(25, 250, rightTitle.Width, rightTitle.Height), Color.White);
                        spriteBatch.Draw(godModeTitle, new Rectangle(25, 350, godModeTitle.Width, godModeTitle.Height), Color.White);
                        jump.DrawButton(spriteBatch);
                        left.DrawButton(spriteBatch);
                        right.DrawButton(spriteBatch);
                        if (godMode)
                        {
                            godModeChecked.DrawButton(spriteBatch);
                        }
                        if(!godMode)
                        {
                            godModeSelect.DrawButton(spriteBatch);
                        }
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
                        if (!godMode)
                        {
                            GraphicsDevice.Clear(Color.White);
                        }
                        else
                        {
                            GraphicsDevice.Clear(Color.Black);
                        }



                        // test level
                        // drawing objects
                        for (int i = 0; i < test.Interactable.Count; i++)
                        {
                            test.Interactable[i].Draw(spriteBatch);
                        }

                        // drawing input objects
                        for (int i = 0; i < test.InputObjects.Count; i++)
                        {
                            // changes lever image when clicking 'e'
                            if (test.InputObjects[i].Texture == leverAfter && doorOpen == false)
                            {
                                test.InputObjects[i].Texture = leverBefore;
                            }
                            else if (test.InputObjects[i].Texture == leverBefore && doorOpen == true)
                            {
                                test.InputObjects[i].Texture = leverAfter;
                            }
                            test.InputObjects[i].Draw(spriteBatch);
                        }

                        

                        //drawing player
                        test.Player.Draw(spriteBatch);

                        // drawing door when clicking 'e'
                        if (doorOpen == false)
                        {
                            clDoor.Draw(spriteBatch);

                        }
                        else
                        {
                            oDoor.Draw(spriteBatch);
                        }
                        

                        //drawing flashlight

                        /*if (!godMode)
                        {
                            // vector puts clear circle at top white corner of game dimensions
                            spriteBatch.Draw(flashlight, new Vector2(-1275 + test.Player.X, -665 + test.Player.Y), Color.White);
                        }*/
                        
                       
                        
                        // Draw the UI
                        spriteBatch.DrawString(font, "Level: " + level, new Vector2(0, 50), Color.White);
                        spriteBatch.DrawString(font, "Time: " + time + " seconds", new Vector2(GraphicsDevice.Viewport.Width / 2 - 250, 50), Color.White);
                        spriteBatch.DrawString(font, "Deaths: " + deaths + " deaths", new Vector2(GraphicsDevice.Viewport.Width / 2 + 75, 50), Color.White);

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

            player.Movement(kbState);

            //This list will store all wall tiles that the player would hit on its current path
            List<GameObject> wallsHit = new List<GameObject>();
            //THis makes a temporary rectangle where the player would end up if unimpeded
            Rectangle theoreticalPosition = new Rectangle(player.X + player.VelocityX, player.Y + player.VelocityY, player.Position.Width, player.Position.Height);

            for (int i = 0; i < walls.Count; i++) //checks to see what objects the player would hit on its current path
            {
                if (walls[i].Position.Intersects(theoreticalPosition))
                {
                    wallsHit.Add(walls[i]);
                }
            }
            if(wallsHit.Count == 0) //if it hits nothing, we good
            {
                player.X += player.VelocityX;
                player.Y += player.VelocityY;
            }
            else
            {
                int xFrac;
                int yFrac;
                if (player.VelocityX > 0)
                {
                    xFrac = 1;
                }
                else if (player.VelocityX < 0)
                {
                    xFrac = -1;
                }
                else
                {
                    xFrac = 0;
                }
                if (player.VelocityY > 0)
                {
                    yFrac = 1;
                }
                else if (player.VelocityY < 0)
                {
                    yFrac = -1;
                }
                else
                {
                    yFrac = 0;
                }

                int xTemp = 0;
                int yTemp = 0;

                while(xTemp < Math.Abs(player.VelocityX) && yTemp < Math.Abs(player.VelocityY))
                {
                    if(xTemp < Math.Abs(player.VelocityX))
                    {
                        player.X += xFrac;
                        for (int i = 0; i < wallsHit.Count; i++) //checks to see what objects the player would hit on its current path
                        {
                            if (wallsHit[i].Position.Intersects(player.Position))
                            {
                                player.X -= xFrac;
                                i = wallsHit.Count;
                                player.VelocityX = 0;
                            }
                        }
                        xTemp++;
                    }
                    if(yTemp < Math.Abs(player.VelocityY))
                    {
                        player.Y += yFrac;
                        for (int i = 0; i < wallsHit.Count; i++) //checks to see what objects the player would hit on its current path
                        {
                            if (wallsHit[i].Position.Intersects(player.Position))
                            {
                                player.Y -= yFrac;
                                i = wallsHit.Count;
                                player.VelocityY = 0;
                                player.Grounded = true;
                            }
                        }
                        yTemp++;
                    }
                }

            }

            
            //This part only trigers if the player has just pressed the interact key (Which is E) as of this frame
            if(kbState.IsKeyDown(Keys.E) && !kbOld.IsKeyDown(Keys.E))
            {

                //This is a temporary boolean to determine if the button/lever was toggled
                bool itemPressed = false;

                //THis cycles through all the buttons/levers and checks if any are colliding with the player.
                for (int i = 0; i < inputObjects.Count && !itemPressed; i++) //checks to see what objects the player would hit on its current path
                {
                    if (inputObjects[i].Position.Intersects(player.Position))
                    {
                        //If the player is colliding, then the player has pressed it
                        itemPressed = true;
                    }
                }

                if (itemPressed) //if the door was already open, it closes it
                    if (doorOpen)
                    {
                        doorOpen = false;
                    }
                    else
                    {
                        //if it was closed, it opens
                        doorOpen = true;
                        
                    }

            }
            if (doorOpen) //If the door is open
            {
                if (player.Position.Intersects(oDoor.Position)) //and the player touches the door
                {
                    gameState = State.Victory; //Victory!
                }
            }
        }

        /// <summary>
        /// Checks to see if a single key has been pressed
        /// </summary>
        /// <param name="key">The key to check</param>
        /// <returns>True if a single key has been pressed, false if it hasn't</returns>
        public bool SingleKeyPress(Keys key)
        {
            if (kbState.IsKeyDown(key) && !kbOld.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }

    }
}
