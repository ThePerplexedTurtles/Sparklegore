#region Contributors
/* Authors:
 * - Felipe Yoon
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
    class GO_Enemy : GameObject
    {
        //attributes
        private int walkSpeed = 10;
        private int chargeSpeed = 20;
        private int knockbackDistance;
        private enum enemyState { patrolLeft, patrolRight, chargeLeft, chargeRight, dead };
        enemyState enemyS = enemyState.patrolRight;

        //Constructor
        public GO_Enemy(int x, int y, Texture2D sheet, int width, int height)
            : base(x, y, sheet, width, height)
        {
            //Setting the spritesheet location
            v2_SpriteSheetPos = new Vector2(1, 1);
            //Creating the hitbox
            listHitboxes.Add(new Hitbox(v2_Position, 6, 6, 24, 24, CollisionType.Enemy1));

        }




        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(t2d_SpriteSheet,
                               v2_Position,
                               new Rectangle((int)((v2_SpriteSheetPos.X - 1) * v2_SpriteSize.X),
                                               (int)((v2_SpriteSheetPos.Y - 1) * v2_SpriteSize.Y),
                                               (int)v2_SpriteSize.X, (int)v2_SpriteSize.Y),
                               Color.White);
        }

        public override void ProcessCollision(Rectangle collisionArea, CollisionType collisionType)
        {
            throw new NotImplementedException();
        }
    }
}
