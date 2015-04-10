using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project2_FinalFramework
{
    class EXAMPLE_Wall: GameObject
    {
        //Attributes

        
        //[Constructor]
        public EXAMPLE_Wall(int x, int y, Texture2D sheet, int width, int height): base(x, y, sheet, width, height)
        {
            //Setting the position of the sprite sheet
            v2_SpriteSheetPos = new Vector2(2, 5);

            //Creating the hitbox
            listHitboxes.Add(new Hitbox(v2_Position, 2, 2, 28, 28, CollisionType.Platform));
        }

        //Update()
        public override void Update(GameTime gameTime)
        {
            
        }

        //Draw()
        public override void Draw(SpriteBatch spriteBatch)
        {
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
