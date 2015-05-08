#region Contributors
/* Authors:
 * - Michael Berger
 * - Lucas Hedrick
 * - 
 */
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace Sparklegore
{
    //Enum "Screens" starts
    public enum Scenes
    {
        MainMenu,
        Game,
        GameOver
    }
    //Enum "Screens" ends

    //Enum "Menus" starts
    public enum Menus
    {
        Options,
        Settings,
        Controls,
        Abilities,
        Default
    }
    //Enum "Menus" ends


    //Class "Sparklegore" starts
    public class Sparklegore : Game
    {
        //Attributes
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static Scenes screenCurrent = Scenes.MainMenu;
        private static KeyboardState stateKeyboardCurrent;
        private static KeyboardState stateKeyboardPrevious;
        private static MouseState stateMouseCurrent;
        private static MouseState stateMousePrevious;
        private Scene_MainMenu sceneMainMenu;
        private Scene_Game sceneGame;
        private Scene_GameOver sceneGameOver;
        
        //Luke's added mess
        public static bool isPaused = true;
        bool someBool = true;
        Texture2D mainMenu;
        Texture2D pauseMenu;
        Scene_MainMenu mMenu;
        SpriteFont font;
        Texture2D options;
        Texture2D settings;
        Texture2D abilities;
        Texture2D controls;
        Texture2D gameOver;
        Rectangle visibleScreen = new Rectangle(0, 0, 800, 600);
        public static bool onMain = true;
        public static bool isDead = false;
        public static Menus menuCurrent = Menus.Default;
        int hoveringSprite = 0;
        Texture2D bg;
        Vector2 position = new Vector2(0, 0);
        Point currentFrame;
        Point size = new Point(800, 600);
        Texture2D menuSprites;
        

        //[Constructor]
        public Sparklegore()
            : base()
        {
            //[Generic Auto-Generated MonoGame Code]
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        //Initialize()
        protected override void Initialize()
        {
            //Setting the various states to their first instances
            stateKeyboardCurrent = Keyboard.GetState();
            stateKeyboardPrevious = Keyboard.GetState();
            stateMouseCurrent = Mouse.GetState();
            stateMousePrevious = Mouse.GetState();
            
            //set beginning of background looping
            currentFrame.X = 0;
            currentFrame.Y = 0;

            //Making the mouse visible
            this.IsMouseVisible = true;

            //Setting the window width and height
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;

            //Defining the various scenes
            sceneGame = new Scene_Game(this.Content);
            mMenu = new Scene_MainMenu(this.Content);
            


            base.Initialize();
        }

        //LoadContent()
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Luke's added mess
            mainMenu = this.Content.Load<Texture2D>("TitleScreen1");
            pauseMenu = this.Content.Load<Texture2D>("Possible menu style1");
            font = this.Content.Load<SpriteFont>("font1");
            controls = this.Content.Load<Texture2D>("Controls");
            abilities = this.Content.Load<Texture2D>("Abilities");
            settings = this.Content.Load<Texture2D>("Settings");
            options = this.Content.Load<Texture2D>("Options");
            gameOver = this.Content.Load<Texture2D>("GameOver");
            bg = this.Content.Load<Texture2D>("background");
            menuSprites = this.Content.Load<Texture2D>("menuSpriteSheet");
        }

        //UnloadContent()
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        //Update()
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Updating the current input states
            stateKeyboardCurrent = Keyboard.GetState();
            stateMouseCurrent = Mouse.GetState();

            //LUKES REALLY MESSY STUFF
            if(sceneGame.IsDead == true)
            {
                screenCurrent = Scenes.GameOver;
            }

            //Luke's added mess
            mMenu.Update(gameTime);

            currentFrame.Y -= 1;
            if(currentFrame.Y == -1)
            {
                currentFrame.Y = 3600;
            }

            //Main menu navigation
            if (screenCurrent == Scenes.MainMenu)
            {
                //Play button
                if (stateMouseCurrent.Y >= 164 && stateMouseCurrent.Y <= 225 && stateMouseCurrent.X >= 317 && stateMouseCurrent.X <= 500)
                {
                    hoveringSprite = 1;
                    if (stateMouseCurrent.LeftButton == ButtonState.Pressed && stateMousePrevious.LeftButton == ButtonState.Released)
                    {
                        if (menuCurrent == Menus.Default)
                        {
                            isPaused = false;
                            IsMouseVisible = false;
                            onMain = true;
                            screenCurrent = Scenes.Game;
                        }
                    }
                }

                //Options button
                else if (stateMouseCurrent.Y >= 269 && stateMouseCurrent.Y <= 311 && stateMouseCurrent.X >= 318 && stateMouseCurrent.X <= 498 && onMain == true)
                {
                    hoveringSprite = 2;
                    if (stateMouseCurrent.LeftButton == ButtonState.Pressed && stateMousePrevious.LeftButton == ButtonState.Released)
                    {
                        onMain = false;
                        menuCurrent = Menus.Options;
                    }
                }

                //Settings button
                else if (stateMouseCurrent.Y >= 314 && stateMouseCurrent.Y <= 357 && stateMouseCurrent.X >= 307 && stateMouseCurrent.X <= 509 && onMain == true)
                {
                    hoveringSprite = 3;
                    if (stateMouseCurrent.LeftButton == ButtonState.Pressed && stateMousePrevious.LeftButton == ButtonState.Released)
                    {
                        onMain = false;
                        menuCurrent = Menus.Settings;
                    }
                }

                //Controls button
                else if (stateMouseCurrent.Y >= 361 && stateMouseCurrent.Y <= 400 && stateMouseCurrent.X >= 307 && stateMouseCurrent.X <= 509 && onMain == true)
                {
                    hoveringSprite = 4;
                    if (stateMouseCurrent.LeftButton == ButtonState.Pressed && stateMousePrevious.LeftButton == ButtonState.Released)
                    {
                        onMain = false;
                        menuCurrent = Menus.Controls;
                    }
                }

                //Abilities button
                else if (stateMouseCurrent.Y >= 404 && stateMouseCurrent.Y <= 444 && stateMouseCurrent.X >= 310 && stateMouseCurrent.X <= 506 && onMain == true)
                {
                    hoveringSprite = 5;
                    if (stateMouseCurrent.LeftButton == ButtonState.Pressed && stateMousePrevious.LeftButton == ButtonState.Released)
                    {
                        onMain = false;
                        menuCurrent = Menus.Abilities;
                    }
                }

                //Resume
                else if (stateMouseCurrent.Y >= 394 && stateMouseCurrent.Y <= 429 && stateMouseCurrent.X >= 334 && stateMouseCurrent.X <= 446 && onMain == false)
                {
                    hoveringSprite = 11;
                    if (stateMouseCurrent.LeftButton == ButtonState.Pressed && stateMousePrevious.LeftButton == ButtonState.Released)
                    {
                        onMain = true;
                        menuCurrent = Menus.Default;
                    }
                }

                //turn off hover
                else
                {
                    hoveringSprite = 0;
                }

                //Quick Play
                if (stateKeyboardCurrent.IsKeyDown(Keys.Enter))
                {
                    onMain = false;
                    isPaused = false;
                    screenCurrent = Scenes.Game;
                }
            }

            if (screenCurrent == Scenes.Game)
            {
                //EXAMPLE CODE ::: EXAMPLE CODE ::: EXAMPLE CODE
                if (isPaused == false)
                {
                    sceneGame.Update(gameTime);
                    sceneGame.DetectCollisions();
                }
                //EXAMPLE CODE ::: EXAMPLE CODE ::: EXAMPLE CODE


                //pause the game
                if (isPaused == false && stateKeyboardCurrent.IsKeyDown(Keys.P) && !stateKeyboardPrevious.IsKeyDown(Keys.P))
                {
                    someBool = false;
                }

                //unpause the game: keyboard
                if (isPaused == true && stateKeyboardCurrent.IsKeyDown(Keys.P) && !stateKeyboardPrevious.IsKeyDown(Keys.P))
                {
                    someBool = true;
                }

                //actually pauses the game
                if (someBool == false && isPaused == false)
                {
                    isPaused = true;
                    IsMouseVisible = true;
                }

                //actually unpauses the game
                if (someBool == true && isPaused == true)
                {
                    isPaused = false;
                    IsMouseVisible = false;
                }

                //mouse menu navigation
                if (isPaused == true)
                {
                    //resumes game
                    if (stateMouseCurrent.Y >= 394 && stateMouseCurrent.Y <= 429 && stateMouseCurrent.X >= 334 && stateMouseCurrent.X <= 446)
                    {
                        hoveringSprite = 10;
                        if (stateMouseCurrent.LeftButton == ButtonState.Pressed && !(stateMousePrevious.LeftButton == ButtonState.Pressed))
                        {
                            if (menuCurrent == Menus.Default)
                            {
                                isPaused = false;
                                IsMouseVisible = false;
                                someBool = true;
                            }
                            else
                            {
                                menuCurrent = Menus.Default;
                            }
                        }
                    }

                    //Options submenu
                    else if (stateMouseCurrent.Y >= 145 && stateMouseCurrent.Y <= 175 && stateMouseCurrent.X >= 350 && stateMouseCurrent.X <= 445)
                    {
                        hoveringSprite = 6;
                        if (stateMouseCurrent.LeftButton == ButtonState.Pressed && !(stateMousePrevious.LeftButton == ButtonState.Pressed))
                        {
                            menuCurrent = Menus.Options;
                        }
                    }

                    //Settings submenu
                    else if (stateMouseCurrent.Y >= 180 && stateMouseCurrent.Y <= 212 && stateMouseCurrent.X >= 350 && stateMouseCurrent.X <= 445)
                    {
                        hoveringSprite = 7;
                        if (stateMouseCurrent.LeftButton == ButtonState.Pressed && !(stateMousePrevious.LeftButton == ButtonState.Pressed))
                        {
                            menuCurrent = Menus.Settings;
                        }
                    }
                    //Controls submenu
                    else if (stateMouseCurrent.Y >= 215 && stateMouseCurrent.Y <= 245 && stateMouseCurrent.X >= 350 && stateMouseCurrent.X <= 445)
                    {
                        hoveringSprite = 8;
                        if (stateMouseCurrent.LeftButton == ButtonState.Pressed && !(stateMousePrevious.LeftButton == ButtonState.Pressed))
                        {
                            menuCurrent = Menus.Controls;
                        }
                    }
                    //Abilities submenu
                    else if (stateMouseCurrent.Y >= 250 && stateMouseCurrent.Y <= 280 && stateMouseCurrent.X >= 350 && stateMouseCurrent.X <= 445)
                    {
                        hoveringSprite = 9;
                        if (stateMouseCurrent.LeftButton == ButtonState.Pressed && !(stateMousePrevious.LeftButton == ButtonState.Pressed))
                        {
                            menuCurrent = Menus.Abilities;
                        }
                    }

                    //turn off hover
                    else
                    {
                        hoveringSprite = 0;
                    }
                }
            }

            //game over navigation
            if(screenCurrent == Scenes.GameOver)
            {
                this.IsMouseVisible = true;
                
                if(stateMouseCurrent.Y >= 132 && stateMouseCurrent.Y <= 186 && stateMouseCurrent.X >= 266 && stateMouseCurrent.X <= 563 && stateMouseCurrent.LeftButton == ButtonState.Pressed)
                {
                    sceneGame.Startup();
                    onMain = true;
                    screenCurrent = Scenes.MainMenu;
                    menuCurrent = Menus.Default;
                }
                else if (stateMouseCurrent.Y >= 245 && stateMouseCurrent.Y <= 299 && stateMouseCurrent.X >= 350 && stateMouseCurrent.X <= 470 && stateMouseCurrent.LeftButton == ButtonState.Pressed)
                {
                    Environment.Exit(0);
                }
            }
            //LUKES REALLY MESSY STUFF

            //Setting the previous input states to what were the current states
            stateKeyboardPrevious = stateKeyboardCurrent;
            stateMousePrevious = stateMouseCurrent;

            //Calling the base "Update()"
            base.Update(gameTime);
        }

        //Draw()
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            //Beginning the sprite batch
            spriteBatch.Begin();

            spriteBatch.Draw(bg, position, new Rectangle(currentFrame.X, currentFrame.Y, size.X, size.Y), Color.White);

            //Main Menu
            if (screenCurrent == Scenes.MainMenu)
            {
                switch (menuCurrent)
                {
                    case Menus.Abilities:
                        spriteBatch.Draw(abilities, visibleScreen, Color.White);
                        break;
                    case Menus.Controls:
                        spriteBatch.Draw(controls, visibleScreen, Color.White);
                        break;
                    case Menus.Default:
                        spriteBatch.Draw(mainMenu, visibleScreen, Color.White);
                        switch(hoveringSprite)
                        {
                            case 0:
                                break;
                            case 1:
                                spriteBatch.Draw(menuSprites, new Rectangle(280, 67, 256, 256), new Rectangle(0, 0, 256,256), Color.White);
                                break;
                            case 2:
                                spriteBatch.Draw(menuSprites, new Rectangle(280, 161, 256, 256), new Rectangle(0, 256, 256, 256), Color.White);
                                break;
                            case 3:
                                spriteBatch.Draw(menuSprites, new Rectangle(281, 208, 256, 256), new Rectangle(0, 512, 256, 256), Color.White);
                                break;
                            case 4:
                                spriteBatch.Draw(menuSprites, new Rectangle(280, 243, 256, 256), new Rectangle(0, 758, 256, 256), Color.White);
                                break;
                            case 5:
                                spriteBatch.Draw(menuSprites, new Rectangle(280, 296, 256, 256), new Rectangle(256, 0, 256, 256), Color.White);
                                break;
                        }
                        break;
                    case Menus.Options:
                        spriteBatch.Draw(options, visibleScreen, Color.White);
                        break;
                    case Menus.Settings:
                        spriteBatch.Draw(settings, visibleScreen, Color.White);
                        break;
                }
            }
            
            //Game Screen
            if (screenCurrent == Scenes.Game)
            {
                //check if paused
                if (isPaused == true)
                {
                    switch (menuCurrent)
                    {
                        case Menus.Abilities:
                            spriteBatch.Draw(abilities, visibleScreen, Color.White);
                            break;
                        case Menus.Controls:
                            spriteBatch.Draw(controls, visibleScreen, Color.White);
                            break;
                        case Menus.Default:
                            //draw paused screen
                            spriteBatch.Draw(pauseMenu, visibleScreen, Color.White);
                            spriteBatch.DrawString(font, "Can Currently Only", new Vector2(50, 50), Color.White);
                            spriteBatch.DrawString(font, "Look At All Menus", new Vector2(50, 70), Color.White);
                            switch (hoveringSprite)
                            {
                                case 0:
                                    break;
                                case 6:
                                    spriteBatch.Draw(menuSprites, new Rectangle(268, 28, 256, 256), new Rectangle(256, 256, 256, 256), Color.White);
                                    break;
                                case 7:
                                    spriteBatch.Draw(menuSprites, new Rectangle(277, 69, 256, 256), new Rectangle(256, 512, 256, 256), Color.White);
                                    break;
                                case 8:
                                    spriteBatch.Draw(menuSprites, new Rectangle(274, 89, 256, 256), new Rectangle(256, 758, 256, 256), Color.White);
                                    break;
                                case 9:
                                    spriteBatch.Draw(menuSprites, new Rectangle(276, 134, 256, 256), new Rectangle(512, 0, 256, 256), Color.White);
                                    break;
                                case 10:
                                    spriteBatch.Draw(menuSprites, new Rectangle(270, 295, 256, 256), new Rectangle(512, 256, 256, 256), Color.White);
                                    break;
                            }
                            break;
                        case Menus.Options:
                            spriteBatch.Draw(options, visibleScreen, Color.White);
                            break;
                        case Menus.Settings:
                            spriteBatch.Draw(settings, visibleScreen, Color.White);
                            break;
                    }

                }
                //if not...
                else
                {
                    //draw regular game screen
                    sceneGame.Draw(spriteBatch);
                    spriteBatch.DrawString(font, "Press P to Pause", new Vector2(300, 280), Color.White);
                }
            }

            //Game Over Screen
            if (screenCurrent == Scenes.GameOver)
            {
                //Game Over screen drawn here
                spriteBatch.Draw(gameOver, visibleScreen, Color.White);
                
            }

            //Ending the sprite batch
            spriteBatch.End();
            

            base.Draw(gameTime);
        }

        //Accessors & Modifiers
        //::: ::: ::: ::: :::

        //CURRENT KEYBOARD STATE
        public static KeyboardState CurrentKeyboardState
        {
            get { return stateKeyboardCurrent; }
        }

        //PREVIOUS KEYBOARD STATE
        public static KeyboardState PreviousKeyboardState
        {
            get { return stateKeyboardPrevious; }
        }

        //CURRENT MOUSE STATE
        public static MouseState CurrentMouseState
        {
            get { return stateMouseCurrent; }
        }

        //PREVIOUS MOUSE STATE
        public static MouseState PreviousMouseState
        {
            get { return stateMousePrevious; }
        }

        //::: ::: ::: ::: :::
    }
    //Class "Sparklegore" ends
}
