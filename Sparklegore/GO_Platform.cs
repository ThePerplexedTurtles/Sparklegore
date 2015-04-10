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

namespace Project2_FinalFramework
{
    class GO_Platform: GameObject
    {
        //Attributes


        //[Constructor]
        public GO_Platform(int posX, int posY, Texture2D spriteSheet, int spriteWidth, int spriteHeight)
                        : base(posX, posY, spriteSheet, spriteWidth, spriteHeight)
        {
            
        }

        //Update()
        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        //Draw()
        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        //ProcessCollision()
        public override void ProcessCollision(Rectangle collisionArea, CollisionType collisionType)
        {
            throw new NotImplementedException();
        }
    }
}
