#region Contributors
/* Authors:
 * - Michael Berger
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
using Microsoft.Xna.Framework.Input;

namespace Sparklegore
{
    class GO_Player: GameObject
    {
        //Attributes
        bool boolIsInAir = true;
        bool boolCanMoveRight = true;
        bool boolCanMoveLeft = true;
        bool boolIsFacingLeft = true;
        double doubleGravity = 35;
        double doubleAcceleration = 35;
        double doubleVelocityY = 0;
        double doubleVelocityX = 0;
        double doubleMaxFallSpeed = 12;
        double doubleMaxRunSpeed = 12;
        double doubleVelocityPopY = -15;
        double doubleDecelerationTolerance;
        int intAnimationTiming = 0;
        int intAnimationThreshold = 80;
        int intPlatformSpeed;
        //Test
        

        //[Constructor]
        public GO_Player(int platformSpeed, int x, int y, Texture2D sheet, int width, int height): base(x, y, sheet, width, height)
        {
            //Setting the spritesheet location
            v2_SpriteSheetPos = new Vector2(1, 1);

            //Creating the hitboxes
            //::: ::: ::: ::: :::

            //THE PLATFORM COLLISION HITBOX
            listHitboxes.Add(new Hitbox(v2_Position, 
                                        1,
                                        1,
                                        (int) v2_SpriteSize.X - 2,
                                        (int) v2_SpriteSize.Y - 2,
                                        CollisionType.Player));

            //THE BOTTOM PLATFORM CHECK HITBOX
            listHitboxes.Add(new Hitbox(v2_Position,
                                        1,
                                        (int) v2_SpriteSize.Y,
                                        (int) v2_SpriteSize.X - 2,
                                        (int) v2_SpriteSize.Y / 2,
                                        CollisionType.Player));

            //THE RIGHT PLATFORM CHECK HITBOX
            listHitboxes.Add(new Hitbox(v2_Position,
                                        (int) v2_SpriteSize.X,
                                        1,
                                        (int) v2_SpriteSize.X / 2,
                                        (int) v2_SpriteSize.Y - 2, 
                                        CollisionType.Player));
            
            //THE LEFT PLATFORM CHECK HITBOX
            listHitboxes.Add(new Hitbox(v2_Position,
                                        (int) (-v2_SpriteSize.X + v2_SpriteSize.X / 2), 
                                        1, 
                                        (int)v2_SpriteSize.X / 2, 
                                        (int) v2_SpriteSize.Y - 2, 
                                        CollisionType.Player));

            //::: ::: ::: ::: :::

            //Setting the deceleration tolerance
            doubleDecelerationTolerance = ((double)1 / 30) * doubleAcceleration;

            //Setting the platform speed
            intPlatformSpeed = platformSpeed;
        }

        //Update()
        public override void Update(GameTime gameTime)
        {
            //TEST TEST TEST
            if (Sparklegore.CurrentKeyboardState.IsKeyDown(Keys.Q))
            {
                v2_Position.X = 96;
                v2_Position.Y = 0;
            }

            //Moving the player in the Y direction
            MoveY(gameTime);
            
            //Moving the player in the X direction
            MoveX(gameTime);

            //Determining the animations
            DetermineAnimations(gameTime);

            //Update the hitboxes
            UpdateHitboxes();
        }

        //Draw()
        public override void Draw(SpriteBatch spriteBatch)
        {
            //IF the player should be drawn normally... (facing the left)
            if (boolIsFacingLeft == true)
            {
                //Drawing the player
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
            //ELSE... (the player is facing the right)
            else
            {
                //Drawing the player (flipped horizontally)
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

        //ProcessCollision()
        public override void ProcessCollision(Rectangle collisionArea, CollisionType collisionType)
        {
            //IF the collision is a platform collision...
            if (collisionType == CollisionType.Platform)
            {
                //IF the platform collision width is smaller than the height... (player collided with side of platform)
                if (collisionArea.Width < collisionArea.Height)
                {
                    //IF the player collided with the left side of the platform...
                    if (collisionArea.X - v2_Position.X   <   (collisionArea.X + collisionArea.Width) - (v2_Position.X + v2_SpriteSize.X)
                        || doubleVelocityX > 0)
                    {
                        v2_Position.X -= collisionArea.Width;
                        doubleVelocityX = 0;
                        boolCanMoveRight = false;
                    }
                    //ELSE... (player is colliding with right side of platform)
                    else
                    {
                        v2_Position.X += collisionArea.Width;
                        doubleVelocityX = 0;
                        boolCanMoveLeft = false;
                    }
                }

                
                //IF the platform collision height is smaller than or equal to the height... (player is colliding with top/bottom of platform)
                if (collisionArea.Height <= collisionArea.Width)
                {
                    //IF the player collided with the top of the platform...
                    if (collisionArea.Y - v2_Position.Y < (collisionArea.Y + collisionArea.Height) - (v2_Position.Y + v2_SpriteSize.Y)
                        || doubleVelocityY > 0)
                    {
                        v2_Position.Y -= collisionArea.Height;
                        doubleVelocityY = 0;
                        boolIsInAir = false;
                        boolCanMoveRight = true;
                        boolCanMoveLeft = true;
                    }
                    //ELSE... (player is colliding with bottom of platform)
                    else
                    {
                        v2_Position.Y += collisionArea.Height;
                        doubleVelocityY = 0;
                        boolCanMoveRight = true;
                        boolCanMoveLeft = true;
                    }
                }
            }

            //IF the collision is the "Player is not above platform" trigger...
            if (collisionType == CollisionType.Null
                &&   collisionArea.Height == -1
                &&   collisionArea.Width >= 0)
            {
                boolIsInAir = true;
            }

            //IF the collision is the "Platforms are Not To Right" trigger...
            if (collisionType == CollisionType.Null
                &&   collisionArea.Height >= 0
                &&   collisionArea.Width == -1
                &&   collisionArea.X == 1)
            {
                boolCanMoveRight = true;
            }

            //IF the collision is the "Platforms are Not To Left" trigger...
            if (collisionType == CollisionType.Null
                && collisionArea.Height >= 0
                && collisionArea.Width == -1
                && collisionArea.X == -1)
            {
                boolCanMoveLeft = true;
            }

            //Updating the hitboxes
            UpdateHitboxes();
        }

        //MoveY()
        private void MoveY(GameTime gameTime)
        {
            //IF the player is in the air...
            if (boolIsInAir == true && doubleVelocityY < doubleMaxFallSpeed)
            {
                //Setting the velocity to the corresponding amount
                doubleVelocityY += ((double)1 / 60) * doubleGravity;
            }
            //ELSE IF the Y velocity is greater than the max fall speed...
            else if (doubleVelocityY > doubleMaxFallSpeed)
            {
                //Setting the velocity to the max fall speed
                doubleVelocityY = doubleMaxFallSpeed;
            }

            //IF the jump button is down and the 'in the air' boolean is false...
            if (Sparklegore.CurrentKeyboardState.IsKeyDown(Keys.Space) == true
                && Sparklegore.PreviousKeyboardState.IsKeyDown(Keys.Space) == false
                && boolIsInAir == false)
            {
                doubleVelocityY = doubleVelocityPopY;
                boolIsInAir = true;
            }

            //Adding the Y velocity to the Y position
            v2_Position.Y += (float)doubleVelocityY;
            
            //Moving the player down according to the platform speed
            v2_Position.Y += intPlatformSpeed;
        }

        //MoveX()
        private void MoveX(GameTime gameTime)
        {
            //IF the player is moving RIGHT & he can move right...
            if (Sparklegore.CurrentKeyboardState.IsKeyDown(Keys.Right) == true
                && Sparklegore.CurrentKeyboardState.IsKeyDown(Keys.Left) == false
                && boolCanMoveRight == true)
            {
                //Increment the X velocity by a 60th of the acceleration
                doubleVelocityX += ((double)1 / 60) * doubleAcceleration;

                //Setting the 'can move left' and the 'is facing left' booleans
                boolCanMoveLeft = true;
                boolIsFacingLeft = false;
            }
            //ELSE IF the X velocity is greater than the max run speed...
            else if (doubleVelocityX > doubleMaxRunSpeed)
            {
                //Setting the X velocity to the max run speed
                doubleVelocityX = doubleMaxRunSpeed;
            }
            //ELSE IF the X velocity is greater than the speed tolerance...
            else if (doubleVelocityX > doubleDecelerationTolerance)
            {
                //Decrement the X velocity by a 60th of the acceleration
                doubleVelocityX -= ((double)1 / 60) * doubleAcceleration;
            }

            //IF the player is moving LEFT & his velocity is greater than the negative of the max run speed...
            if (Sparklegore.CurrentKeyboardState.IsKeyDown(Keys.Left) == true
                && Sparklegore.CurrentKeyboardState.IsKeyDown(Keys.Right) == false
                && doubleVelocityX > -doubleMaxRunSpeed)
            {
                //Decrement the X velocity by a 60th of the acceleration
                doubleVelocityX -= ((double)1 / 60) * doubleAcceleration;

                //Setting the 'can move right' and the 'is facing left' boolean
                boolCanMoveRight = true;
                boolIsFacingLeft = true;
            }
            //ELSE IF the velocity is less than the negative of the max run speed...
            else if (doubleVelocityX < -doubleMaxRunSpeed)
            {
                //Set the X velocity to the negative of the max run speed
                doubleVelocityX = -doubleMaxRunSpeed;
            }
            //ELSE IF the X velocity is less than the negative of the deceleration tolerance...
            else if (doubleVelocityX < -doubleDecelerationTolerance)
            {
                //Increment the X velocity by a 60th of the acceleration
                doubleVelocityX += ((double)1 / 60) * doubleAcceleration;
            }

            //IF the player is not trying to move & the magnitude of their X velocity is less than the deceleration tolerance...
            if (Sparklegore.CurrentKeyboardState.IsKeyDown(Keys.Left) == false
                && Sparklegore.CurrentKeyboardState.IsKeyDown(Keys.Right) == false
                && Math.Abs(doubleVelocityX) < doubleDecelerationTolerance)
            {
                doubleVelocityX = 0;
            }

            //Adding the X velocity to the X position
            v2_Position.X += (float)doubleVelocityX;
        }

        //DetermineAnimations()
        private void DetermineAnimations(GameTime gameTime)
        {
            //IF the player is not in the air...
            if (boolIsInAir == false)
            {
                //IF the player is NOT moving...
                if (doubleVelocityX == 0)
                {
                    //Setting the Y position of the spritesheet
                    v2_SpriteSheetPos.Y = 1;

                    intAnimationTiming += gameTime.ElapsedGameTime.Milliseconds;

                    //IF the right amount of time has passed...
                    if (intAnimationTiming > intAnimationThreshold)
                    {
                        //IF the spritesheet position is not the last sprite...
                        if (v2_SpriteSheetPos.X < 6)
                        {
                            v2_SpriteSheetPos.X++;
                        }
                        //ELSE... (the spritesheet is on the last sprite)
                        else
                        {
                            v2_SpriteSheetPos.X = 1;
                        }

                        //Resetting the animation timing variable
                        intAnimationTiming = 0;
                    }
                }
                //ELSE... (the player is moving)
                else
                {
                    //Setting the Y position of the spritesheet
                    v2_SpriteSheetPos.Y = 3;

                    intAnimationTiming += gameTime.ElapsedGameTime.Milliseconds;

                    //IF the right amount of time has passed...
                    if (intAnimationTiming > intAnimationThreshold)
                    {
                        //IF the spritesheet position is not the last sprite...
                        if (v2_SpriteSheetPos.X < 6)
                        {
                            v2_SpriteSheetPos.X++;
                        }
                        //ELSE... (the spritesheet is on the last sprite)
                        else
                        {
                            v2_SpriteSheetPos.X = 1;
                        }

                        //Resetting the animation timing variable
                        intAnimationTiming = 0;
                    }
                }
            }
            //ELSE... (the player is in the air)
            else if (boolIsInAir == true)
            {
                //Setting the Y position of the spritesheet
                v2_SpriteSheetPos.Y = 2;

                intAnimationTiming += gameTime.ElapsedGameTime.Milliseconds;

                //IF the right amount of time has passed & the animation is not on its last frame...
                if (intAnimationTiming > intAnimationThreshold
                    && v2_SpriteSheetPos.X < 6)
                {
                    v2_SpriteSheetPos.X++;
                    intAnimationTiming = 0;
                }
            }
        }
    }
}
