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

namespace Project2_FinalFramework
{
    //Enum "Screens" starts
    public enum Scenes
    {
        MainMenu,
        Game,
        GameOver
    }
    //Enum "Screens" ends


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

        //EXAMPLE CODE ::: EXAMPLE CODE ::: EXAMPLE CODE
        EXAMPLE_Scene1 testScene;
        //EXAMPLE CODE ::: EXAMPLE CODE ::: EXAMPLE CODE

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

            //Making the mouse visible
            this.IsMouseVisible = true;

            //EXAMPLE CODE ::: EXAMPLE CODE ::: EXAMPLE CODE
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;

            testScene = new EXAMPLE_Scene1(this.Content);
            //EXAMPLE CODE ::: EXAMPLE CODE ::: EXAMPLE CODE


            base.Initialize();
        }

        //LoadContent()
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

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

            //EXAMPLE CODE ::: EXAMPLE CODE ::: EXAMPLE CODE
            testScene.Update(gameTime);
            testScene.DetectCollisions();
            //EXAMPLE CODE ::: EXAMPLE CODE ::: EXAMPLE CODE

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

            //EXAMPLE CODE ::: EXAMPLE CODE ::: EXAMPLE CODE
            spriteBatch.Begin();
            testScene.Draw(spriteBatch);
            spriteBatch.End();
            //EXAMPLE CODE ::: EXAMPLE CODE ::: EXAMPLE CODE

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
