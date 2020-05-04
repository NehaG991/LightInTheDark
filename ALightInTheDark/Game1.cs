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
        bool leverPressable;

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
        TempButton reset;

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
            easy = new TempButton(Content.Load<Texture2D>("easyIndicator"), Content.Load<Texture2D>("godMode"), new Rectangle(GraphicsDevice.Viewport.Width / 2 - GraphicsDevice.Viewport.Width / 8, GraphicsDevice.Viewport.Height / 2 - 100, GraphicsDevice.Viewport.Width / 4, GraphicsDevice.Viewport.Height / 8));

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
            Texture2D selectHover = Content.Load<Texture2D>("selectionBoxHover");
            jump = new TempButton(select, selectHover, new Rectangle(250, 175, select.Width, select.Height));
            left = new TempButton(select, selectHover, new Rectangle(250, 275, select.Width, select.Height));
            right = new TempButton(select, selectHover, new Rectangle(250, 375, select.Width, select.Height));
            jumpTitle = Content.Load<Texture2D>("jumpButton");
            leftTitle = Content.Load<Texture2D>("leftButton");
            rightTitle = Content.Load<Texture2D>("rightButton");
            godModeTitle = Content.Load<Texture2D>("godMode");
            godModeSelect = new TempButton(select, select, new Rectangle(350, 515, select.Width, select.Height));
            godModeChecked = new TempButton(check, check, new Rectangle(450, 515, select.Width, select.Height));
            reset = new TempButton(select, selectHover, new Rectangle(850, 415, select.Width, select.Height));
            

            // loading levels
            // files must be in the debug folder to work
            test = new LevelReader(platform, player, openDoor, closedDoor, leverBefore, leverAfter, buttonBefore, buttonAfter, @"test.level");
            test.ReadFile();
            walls = test.Interactable;
            inputObjects = test.InputObjects;
            clDoor = test.DoorClose;
            oDoor = test.DoorOpen;

            doorOpen = false;
            leverPressable = false;

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
                        // code to reset everything back after completing the level and going back to the main menu
                        win = false;
                        deaths = 0;
                        // reset player to base location
                        test.Player.X = test.Player.StartRectangle.X;
                        test.Player.Y = test.Player.StartRectangle.Y;
                        stopwatch.Reset();
                        stopwatch.Start();
                        // reset intereactable objects
                        doorOpen = false;
                        leverPressable = false;
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
                        if (easy.Click() && godMode == false)
                        {
                            godMode = true;
                        } 
                        else if(easy.Click() && godMode == true)
                        {
                            godMode = false;
                        }

                        break;
                    }
                case State.Controls:
                    {
                        // Various buttons for controls and their functions
                        // If a button is clicked change the control through another method
                        if (jump.Click())
                        {
                            jump.ControlEdit(MovementKeys.jumpKey, test.Player);
                        }
                        if (left.Click())
                        {
                            left.ControlEdit(MovementKeys.leftKey, test.Player);
                        }
                        if (right.Click())
                        {
                            right.ControlEdit(MovementKeys.rightKey, test.Player);
                        }
                        // Toggle god mode on and off
                        if (godModeSelect.Click())
                        {
                            godMode = true;
                            Console.WriteLine(godMode);
                        }
                        if (godModeChecked.Click())
                        {
                            godMode = false;
                            Console.WriteLine(godMode);
                        }
                        // Reset button
                        if (reset.Click())
                        {
                            test.Player.ChangeKeys(MovementKeys.jumpKey, Keys.W);
                            test.Player.ChangeKeys(MovementKeys.leftKey, Keys.A);
                            test.Player.ChangeKeys(MovementKeys.rightKey, Keys.D);
                            godMode = false;
                        }
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
                                stopwatch.Start();
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
                            deaths = 0;
                            // reset player to base location
                            test.Player.X = test.Player.StartRectangle.X;
                            test.Player.Y = test.Player.StartRectangle.Y;
                            stopwatch.Reset();
                            stopwatch.Start();
                            // reset intereactable objects
                            doorOpen = false;
                            leverPressable = false;
                            
                            
                        }
                        if (quit.Click())
                        {
                            gameState = State.MainMenu;
                        }
                        break;
                    }
                case State.Game:
                    {
                        if (godMode)
                        {
                            gameState = State.EasyMode;
                        }
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
                            godMode = true;
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
                            Exit();
                        }
                        if (back.Click())
                        {
                            gameState = State.MainMenu;
                        }
                        break;
                    }
                case State.EasyMode:
                    {
                        if (!godMode)
                        {
                            gameState = State.Game;
                        }
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
                            godMode = false;
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
                        // Draw the control buttons
                        spriteBatch.DrawString(font, "To change the controls, hold down a button \nthen press the key you want to change it to.", new Vector2(225, 50), Color.White);
                        spriteBatch.Draw(jumpTitle, new Rectangle(25, 175, jumpTitle.Width, jumpTitle.Height), Color.White);
                        spriteBatch.Draw(leftTitle, new Rectangle(25, 275, leftTitle.Width, leftTitle.Height), Color.White);
                        spriteBatch.Draw(rightTitle, new Rectangle(25, 375, rightTitle.Width, rightTitle.Height), Color.White);
                        spriteBatch.Draw(godModeTitle, new Rectangle(25, 475, godModeTitle.Width, godModeTitle.Height), Color.White);
                        jump.DrawButton(spriteBatch);
                        left.DrawButton(spriteBatch);
                        right.DrawButton(spriteBatch);
                        reset.DrawButton(spriteBatch);
                        spriteBatch.DrawString(font, test.Player.GetMovementKey(MovementKeys.jumpKey).ToString(), new Vector2(280, 180), Color.White);
                        spriteBatch.DrawString(font, test.Player.GetMovementKey(MovementKeys.leftKey).ToString(), new Vector2(280, 280), Color.White);
                        spriteBatch.DrawString(font, test.Player.GetMovementKey(MovementKeys.rightKey).ToString(), new Vector2(280, 380), Color.White);
                        if (godMode)
                        {
                            godModeChecked.DrawButton(spriteBatch);
                        }
                        if (!godMode)
                        {
                            godModeSelect.DrawButton(spriteBatch);
                        }
                        spriteBatch.DrawString(font, "Reset", new Vector2(850, 375), Color.White);
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
                            // changes lever back to default image when clicking 'e'
                            if (test.InputObjects[i].Texture == leverAfter && (doorOpen == false && leverPressable == false))
                            {
                                test.InputObjects[i].Texture = leverBefore;
                            }
                            else if (test.InputObjects[i].Texture == leverBefore && doorOpen == true)
                            {
                                test.InputObjects[i].Texture = leverAfter;
                            }

                            // changing button sprite
                            if (test.InputObjects[i].Texture == buttonBefore && leverPressable == true)
                            {
                                test.InputObjects[i].Texture = buttonAfter;
                            }
                            else if (test.InputObjects[i].Texture == buttonAfter && leverPressable == false)
                            {
                                test.InputObjects[i].Texture = buttonBefore;
                            }
                            
                            test.InputObjects[i].Draw(spriteBatch);
                        }

                        // check if player is dead
                        if (test.Player.IsPlayerDead(GraphicsDevice.Viewport.Height))
                        {
                            deaths++;
                            // reset player to base location
                            test.Player.X = test.Player.StartRectangle.X;
                            test.Player.Y = test.Player.StartRectangle.Y;
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

                        if (!godMode)
                        {
                            // vector puts clear circle at top white corner of game dimensions
                            spriteBatch.Draw(flashlight, new Vector2(-1275 + test.Player.X, -665 + test.Player.Y), Color.White);
                        }
                        
                        
                        // Draw the UI
                        spriteBatch.DrawString(font, "Level: " + level, new Vector2(0, 50), Color.White);
                        spriteBatch.DrawString(font, "Time: " + time + " seconds", new Vector2(GraphicsDevice.Viewport.Width / 2 - 250, 50), Color.White);
                        spriteBatch.DrawString(font, "Deaths: " + deaths + " deaths", new Vector2(GraphicsDevice.Viewport.Width / 2 + 75, 50), Color.White);

                        break;
                    }
                case State.Victory:
                    {
                        // Draw the victory stuff
                        spriteBatch.DrawString(font, "Level passed!", new Vector2(50, 50), Color.White);
                        quit.DrawButton(spriteBatch);
                        back.DrawButton(spriteBatch);
                        win = false;
                        break;
                    }
                case State.EasyMode:
                    {
                        // Draw all the easy mode game stuff
                        // test level
                        // drawing objects
                        for (int i = 0; i < test.Interactable.Count; i++)
                        {
                            test.Interactable[i].Draw(spriteBatch);
                        }

                        // drawing input objects
                        for (int i = 0; i < test.InputObjects.Count; i++)
                        {
                            // changes lever back to default image when clicking 'e'
                            if (test.InputObjects[i].Texture == leverAfter && (doorOpen == false && leverPressable == false))
                            {
                                test.InputObjects[i].Texture = leverBefore;
                            }
                            else if (test.InputObjects[i].Texture == leverBefore && doorOpen == true)
                            {
                                test.InputObjects[i].Texture = leverAfter;
                            }

                            // changing button sprite
                            if (test.InputObjects[i].Texture == buttonBefore && leverPressable == true)
                            {
                                test.InputObjects[i].Texture = buttonAfter;
                            }
                            else if (test.InputObjects[i].Texture == buttonAfter && leverPressable == false)
                            {
                                test.InputObjects[i].Texture = buttonBefore;
                            }

                            test.InputObjects[i].Draw(spriteBatch);
                        }

                        // drawing door when clicking 'e'
                        if (doorOpen == false)
                        {
                            clDoor.Draw(spriteBatch);

                        }
                        else
                        {
                            oDoor.Draw(spriteBatch);
                        }

                        // check if player is dead
                        if (test.Player.IsPlayerDead(GraphicsDevice.Viewport.Height))
                        {
                            deaths++;
                            // reset player to base location
                            test.Player.X = test.Player.StartRectangle.X;
                            test.Player.Y = test.Player.StartRectangle.Y;
                        }
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

                
                //This is a temporary boolean to determine if the lever was toggled
                bool leverPressed = false;

                //THis cycles through all the buttons/levers and checks if any are colliding with the player.
                for (int i = 0; i < inputObjects.Count && !leverPressed; i++) //checks to see what objects the player would hit on its current path
                {

                    // makes lever pressable if button is pressable
                    if (inputObjects[i].Position.Intersects(player.Position) && (inputObjects[i].Texture == leverBefore || inputObjects[i].Texture == leverAfter) && leverPressable == true)
                    {
                        //If the player is colliding, then the player has pressed it
                        leverPressed = true;
                    }

                    // changes button bool 
                    if (inputObjects[i].Position.Intersects(player.Position) && inputObjects[i].Texture == buttonBefore)
                    {
                        leverPressable = true;
                    }
                    else if (inputObjects[i].Position.Intersects(player.Position) && inputObjects[i].Texture == buttonAfter)
                    {
                        leverPressable = false;
                        doorOpen = false;
                    }
                }

                if (leverPressed) //if the door was already open, it closes it
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
