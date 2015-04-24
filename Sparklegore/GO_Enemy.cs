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
        private int walkSpeed;
        private int knockbackDistance;
        private bool isAlive;
        private List<GO_Platform> listPlatforms;
        private int patrolDistance;
        private enum enemyState { patrolLeft, patrolRight, chargeLeft, chargeRight, dead };
        enemyState enemyMove;

        //Constructor
        public GO_Enemy(int x, int y, Texture2D sheet, int width, int height, List<GO_Platform> platformList)
            : base(x, y, sheet, width, height)
        {
            //Setting the spritesheet location
            v2_SpriteSheetPos = new Vector2(1, 1);

            listPlatforms = platformList;

            //Creating the hitbox
            listHitboxes.Add(new Hitbox(v2_Position, 6, 6, 24, 24, CollisionType.Enemy1));
        }

        //AI loop
        public void enemy_AI()
        {
            while (isAlive == true)
            {

            }
        }



        public override void Update(GameTime gameTime)
        {
            enemy_AI();
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
