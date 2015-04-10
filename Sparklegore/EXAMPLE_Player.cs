using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project2_FinalFramework
{
    class EXAMPLE_Player: GameObject
    {
        //Attributes
        private int intMoveSpeed = 4;
        private int intKnockbackTimer = 0;
        private readonly int KNOCKBACK_TIME = 800;
        private Color colorToDrawWith = Color.White;
        private Vector2 v2_KnockbackDirection = new Vector2(0, 0);
        

        //[Constructor]
        public EXAMPLE_Player(int x, int y, Texture2D sheet, int width, int height): base(x, y, sheet, width, height)
        {
            //Setting the position of the sprite sheet
            v2_SpriteSheetPos = new Vector2(1, 1);

            //Creating the hitbox
            listHitboxes.Add(new Hitbox(v2_Position, 2, 2, 28, 28, CollisionType.Player));
        }

        //Update()
        public override void Update(GameTime gameTime)
        {
            //Some simple input to move the character
            //::: ::: ::: ::: :::

            //RIGHT
            if (Sparklegore.CurrentKeyboardState.IsKeyDown(Keys.Right) == true
                && Sparklegore.CurrentKeyboardState.IsKeyDown(Keys.Left) == false
                && intKnockbackTimer == 0)
            {
                v2_Position.X += intMoveSpeed;
            }

            //LEFT
            if (Sparklegore.CurrentKeyboardState.IsKeyDown(Keys.Left) == true
                && Sparklegore.CurrentKeyboardState.IsKeyDown(Keys.Right) == false
                && intKnockbackTimer == 0)
            {
                v2_Position.X -= intMoveSpeed;
            }

            //UP
            if (Sparklegore.CurrentKeyboardState.IsKeyDown(Keys.Up) == true
                && Sparklegore.CurrentKeyboardState.IsKeyDown(Keys.Down) == false
                && intKnockbackTimer == 0)
            {
                v2_Position.Y -= intMoveSpeed;
            }

            //DOWN
            if (Sparklegore.CurrentKeyboardState.IsKeyDown(Keys.Down) == true
                && Sparklegore.CurrentKeyboardState.IsKeyDown(Keys.Up) == false
                && intKnockbackTimer == 0)
            {
                v2_Position.Y += intMoveSpeed;
            }

            //::: ::: ::: ::: :::

            //Handling enemy knockback
            if (intKnockbackTimer > 0)
            {
                intKnockbackTimer -= gameTime.ElapsedGameTime.Milliseconds;
                v2_Position.X += v2_KnockbackDirection.X;
                v2_Position.Y += v2_KnockbackDirection.Y;

                //IF the current update set the knockback timer to zero
                if (intKnockbackTimer <= 0)
                {
                    intKnockbackTimer = 0;
                    colorToDrawWith = Color.White;
                }
            }

            //Updating the hitboxes
            foreach (Hitbox hitbox in listHitboxes)
            {
                hitbox.UpdateHitbox(v2_Position);
            }
        }

        //Draw()
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(t2d_SpriteSheet,
                                v2_Position,
                                new Rectangle((int) ((v2_SpriteSheetPos.X - 1)*v2_SpriteSize.X),
                                                (int) ((v2_SpriteSheetPos.Y - 1)*v2_SpriteSize.Y),
                                                (int) v2_SpriteSize.X, (int) v2_SpriteSize.Y),
                                colorToDrawWith);
        }

        //ProcessCollision()
        public override void ProcessCollision(Rectangle collisionArea, CollisionType collisionType)
        {
            //Wall collision
            if (collisionType == CollisionType.Platform)
            {
                //Collision on Right
                if (Sparklegore.CurrentKeyboardState.IsKeyDown(Keys.Right))
                {
                    v2_Position.X -= collisionArea.Width;
                }
                
                //Collision on Left
                if (Sparklegore.CurrentKeyboardState.IsKeyDown(Keys.Left))
                {
                    v2_Position.X += collisionArea.Width;
                }

                //Collision on Top
                if (Sparklegore.CurrentKeyboardState.IsKeyDown(Keys.Up))
                {
                    v2_Position.Y += collisionArea.Height;
                }

                //Collision on Bottom
                if (Sparklegore.CurrentKeyboardState.IsKeyDown(Keys.Down))
                {
                    v2_Position.Y -= collisionArea.Height;
                }
            }

            //Enemy collision
            if (collisionType == CollisionType.Enemy1)
            {
                intKnockbackTimer = KNOCKBACK_TIME;
                colorToDrawWith = Color.Red;
                v2_KnockbackDirection = new Vector2((listHitboxes[0].Current.X - collisionArea.X) * 0.25f,
                                                    (listHitboxes[0].Current.Y - collisionArea.Y) * 0.25f);
            }

            //Updating the hitboxes
            foreach (Hitbox hitbox in listHitboxes)
            {
                hitbox.UpdateHitbox(v2_Position);
            }
        }
    }
}
