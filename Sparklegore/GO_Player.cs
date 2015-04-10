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
    class GO_Player: GameObject
    {
        //Attributes
        

        //[Constructor]
        public GO_Player(int x, int y, Texture2D sheet, int width, int height): base(x, y, sheet, width, height)
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
