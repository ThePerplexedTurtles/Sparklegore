using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Project2_FinalFramework
{
    abstract class Scene
    {
        //Attributes
        protected ContentManager Content;
        protected bool boolResetRequested = false;
        
        //[Constructor]
        public Scene(ContentManager cont)
        {
            //Set the content manager attribute
            Content = cont;
        }

        //Startup() -- Used to (re)define the game objects back to their original states & positions
        abstract protected void Startup();

        //RequestReset() -- Used to request a reset of the scene by changing the 'ResetRequested' boolean to 'true'
        public void RequestReset()
        {
            //Changing the 'ResetRequested' boolean to 'true'
            boolResetRequested = true;
        }

        //Update() -- Used to call the individual update methods of the game objects
        abstract public void Update(GameTime gameTime);

        //Draw() -- Used to call the individual draw methods of the game objects
        abstract public void Draw(SpriteBatch spriteBatch);
        
        //DetectCollisions() -- Used to detect whether or not game objects have collided with each-other 
        abstract public void DetectCollisions();
    }
}
