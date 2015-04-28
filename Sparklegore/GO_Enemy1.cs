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
using Microsoft.Xna.Framework.Input;

namespace Sparklegore
{
    class GO_Enemy1: GameObject
    {
        //Attributes
        bool boolIsFacingLeft = true;
        int intPlatformSpeed = 0;


        //[Constructor]
        public GO_Enemy1(int platformSpeed, int x, int y, Texture2D sheet, int width, int height): base(x, y, sheet, width, height)
        {
            //Setting the spritesheet position
            v2_SpriteSheetPos = new Vector2(1, 1);

            //Creating the hiboxes
            //::: ::: ::: ::: :::

            //GENERIC HITBOX
            listHitboxes.Add(new Hitbox(v2_Position, 
                                        3,
                                        3,
                                        (int) v2_SpriteSize.X - 6,
                                        (int) v2_SpriteSize.Y - 6,
                                        CollisionType.Enemy1));

            //PLATFORM CHECK HITBOX
            listHitboxes.Add(new Hitbox(v2_Position,
                                        0,
                                        (int) v2_SpriteSize.Y,
                                        (int) v2_SpriteSize.X,
                                        1,
                                        CollisionType.Enemy1));

            //::: ::: ::: ::: :::

            //
            intPlatformSpeed = platformSpeed;
        }

        //Update()
        public override void Update(GameTime gameTime)
        {
            //
            v2_Position.Y += intPlatformSpeed;

            //if
            if (v2_Position.Y >= 600)
            {
                v2_Position.Y = -64;
            }

            //Updating the hitboxes
            UpdateHitboxes();
        }

        //Draw()
        public override void Draw(SpriteBatch spriteBatch)
        {
            //IF the enemy should be drawn normally... (facing the left)
            if (boolIsFacingLeft == true)
            {
                //Drawing the enemy
                spriteBatch.Draw(t2d_SpriteSheet,
                                v2_Position,
                                new Rectangle((int)((v2_SpriteSheetPos.X - 1) * v2_SpriteSize.X),
                                                (int)((v2_SpriteSheetPos.Y - 1) * v2_SpriteSize.Y),
                                                (int)v2_SpriteSize.X, (int)v2_SpriteSize.Y),
                                Color.White,
                                0.0f,
                                new Vector2(),
                                1.0f,
                                SpriteEffects.None,
                                0.0f);
            }
            //ELSE... (the enemy is facing the right)
            else
            {
                //Drawing the enemy (flipped horizontally)
                spriteBatch.Draw(t2d_SpriteSheet,
                                v2_Position,
                                new Rectangle((int)((v2_SpriteSheetPos.X - 1) * v2_SpriteSize.X),
                                                (int)((v2_SpriteSheetPos.Y - 1) * v2_SpriteSize.Y),
                                                (int)v2_SpriteSize.X, (int)v2_SpriteSize.Y),
                                Color.White,
                                0.0f,
                                new Vector2(),
                                1.0f,
                                SpriteEffects.FlipHorizontally,
                                0.0f);
            }
        }

        //ProcessCollisions()
        public override void ProcessCollision(Rectangle collisionArea, CollisionType collisionType)
        {
            
        }


    }
}
