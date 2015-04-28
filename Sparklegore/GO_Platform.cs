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

namespace Sparklegore
{
    class GO_Platform: GameObject
    {
        //Attributes
        public static readonly int MOVEMENT_SPEED = 3;

        //[Constructor]
        public GO_Platform(int posX, int posY, Texture2D spriteSheet, int spriteWidth, int spriteHeight)
                        : base(posX, posY, spriteSheet, spriteWidth, spriteHeight)
        {
            //Setting the spritesheet position vector
            v2_SpriteSheetPos = new Vector2(2, 5);

            //Creating the hitbox for the platform
            listHitboxes.Add(new Hitbox(v2_Position, 0, 0, spriteWidth, spriteHeight, CollisionType.Platform));
        }

        //Update()
        public override void Update(GameTime gameTime)
        {
            //IF the platform is not off-screen...
            if (v2_Position.Y > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height + 32)
            {
                //v2_Position.Y += MOVEMENT_SPEED;
            }

            //Updating the hitboxes
            UpdateHitboxes();
        }

        //Draw()
        public override void Draw(SpriteBatch spriteBatch)
        {
            //Drawing the platform
            spriteBatch.Draw(t2d_SpriteSheet,
                                v2_Position,
                                new Rectangle((int)((v2_SpriteSheetPos.X - 1) * v2_SpriteSize.X),
                                                (int)((v2_SpriteSheetPos.Y - 1) * v2_SpriteSize.Y),
                                                (int)v2_SpriteSize.X, (int)v2_SpriteSize.Y),
                                Color.White);
        }

        //ProcessCollision()
        public override void ProcessCollision(Rectangle collisionArea, CollisionType collisionType)
        {
            
        }
    }
}
