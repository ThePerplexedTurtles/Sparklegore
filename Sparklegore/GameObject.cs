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
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sparklegore
{
    abstract class GameObject
    {
        //Attributes
        protected Vector2 v2_Position;
        protected Texture2D t2d_SpriteSheet;
        protected Vector2 v2_SpriteSheetPos;
        protected Vector2 v2_SpriteSize;
        protected List<Hitbox> listHitboxes = new List<Hitbox>();

        //[Constructor]
        protected GameObject(int posX, int posY, Texture2D spriteSheet, int spriteWidth, int spriteHeight)
        {
            //Defining the position
            v2_Position = new Vector2(posX, posY);

            //Defining the sprite sheet and standard sprite size
            t2d_SpriteSheet = spriteSheet;
            v2_SpriteSize = new Vector2(spriteWidth, spriteHeight);

            //Assuming the first sprite to draw is in the upper-left corner
            v2_SpriteSheetPos = new Vector2(0, 0);
        }

        //Update()
        abstract public void Update(GameTime gameTime);

        //Draw()
        abstract public void Draw(SpriteBatch spriteBatch);

        //ProcessCollision()
        abstract public void ProcessCollision(Rectangle collisionArea, CollisionType collisionType);

        //UpdateHitboxes()
        public void UpdateHitboxes()
        {
            //FOREACH hitbox in the hitbox list...
            foreach(Hitbox hitbox in listHitboxes)
            {
                //Update the hitbox
                hitbox.UpdateHitbox(v2_Position);
            }
        }

        //Accessors & Modifiers
        //::: ::: ::: ::: :::

        //POSITION
        public Vector2 Position
        {
            get { return v2_Position; }
        }

        //HITBOXES
        public List<Hitbox> Hitboxes
        {
            get { return listHitboxes; }
        }

        //::: ::: ::: ::: :::
    }
}
