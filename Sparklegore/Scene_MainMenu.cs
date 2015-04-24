#region Contributors
/* Authors:
 * - Michael Berger
 * - 
 */
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Project2_FinalFramework
{
    class Scene_MainMenu : Scene
    {
        //Attributes
        Texture2D mainMenu;
        Rectangle visibleScreen = new Rectangle(0, 0, 800, 600);
        KeyboardState kState = new KeyboardState();
        MouseState mState = new MouseState();

        //[Constructor]
        public Scene_MainMenu(ContentManager cont) : base(cont)
        {
            mainMenu = this.Content.Load<Texture2D>("TitleScreen");
        }

        //Startup()
        public override void Startup()
        {
            throw new NotImplementedException();
        }

        //Update()
        public override void Update(GameTime gameTime)
        {
            //get states of keyboard and mouse
            kState = Keyboard.GetState();
            mState = Mouse.GetState();
        }

        //Draw()
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mainMenu, visibleScreen, Color.White);
        }

        //DetectCollisions()
        public override void DetectCollisions()
        {
            throw new NotImplementedException();
        }
    }
}
