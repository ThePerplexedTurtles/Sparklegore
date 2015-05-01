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

namespace Sparklegore
{
    class Scene_MainMenu : Scene
    {
        //Attributes
        Texture2D mainMenu;
        Rectangle visibleScreen = new Rectangle(0, 0, 800, 600);
        

        //[Constructor]
        public Scene_MainMenu(ContentManager cont) : base(cont)
        {

        }

        //Startup()
        public override void Startup()
        {
            mainMenu = this.Content.Load<Texture2D>("TitleScreen");
        }

        //Update()
        public override void Update(GameTime gameTime)
        {
            
        }

        //Draw()
        public override void Draw(SpriteBatch spriteBatch)
        {
            //
            spriteBatch.Draw(mainMenu, visibleScreen, Color.White);
        }

        //DetectCollisions()
        public override void DetectCollisions()
        {
            
        }
    }
}
