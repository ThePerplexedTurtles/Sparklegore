#region Contributors
/* Authors:
 * - Michael Berger
 * - Felipe Yoon
 */
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project2_FinalFramework
{
    class GO_Player : GameObject
    {
        //Attributes
        bool boolIsInAir = true;
        bool boolDoubleJumpUsed = false;
        double doubleGravity = 35;
        double doubleAcceleration = 9.81;
        double doubleVelocityY = 0;
        double doubleVelocityX = 0;
        double doubleMaxFallSpeed = 10;
        double doubleMaxRunSpeed = 10;
        KeyboardState keyboardCurr;
        KeyboardState keyboardPrev;
        playerState playerKey;

        enum playerState { turnLeft, turnRight, walkLeft, walkRight, jump, attack, ability };

        //[Constructor]
        public GO_Player(int x, int y, Texture2D sheet, int width, int height)
            : base(x, y, sheet, width, height)
        {
            //Setting the spritesheet location
            v2_SpriteSheetPos = new Vector2(1, 1);

            //Creating the hitboxes
            //::: ::: ::: ::: :::

            //THE PLATFORM COLLISION HITBOX
            listHitboxes.Add(new Hitbox(v2_Position, 0, 16, 30, 16, CollisionType.Player));

            //THE PLATFORM CHECK HITBOX
            listHitboxes.Add(new Hitbox(v2_Position, 0, 32, 30, 16, CollisionType.Player));

            //::: ::: ::: ::: :::
        }

        //player jump
        public void PlayerJump()
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

            //IF the jump button is down and the in the air boolean is false...
            if (Sparklegore.CurrentKeyboardState.IsKeyDown(Keys.Space) == true
                && Sparklegore.PreviousKeyboardState.IsKeyDown(Keys.Space) == false
                && (boolIsInAir == false || boolDoubleJumpUsed == false))
            {
                if (boolIsInAir == true)
                {
                    boolDoubleJumpUsed = true;
                }

                doubleVelocityY = -15;
                boolIsInAir = true;
            }

            //Adding the Y velocity to the Y position
            v2_Position.Y += (float)doubleVelocityY;

            //IF the player is moving RIGHT & his velocity is less than the max run speed...
            if (Sparklegore.CurrentKeyboardState.IsKeyDown(Keys.Right) == true
                && doubleVelocityX < doubleMaxRunSpeed)
            {
                //Increment the X velocity by a 25th of the acceleration
                doubleVelocityX += ((double)1 / 25) * doubleAcceleration;
            }
            //ELSE IF the velocity is greater than the max run speed...
            else if (doubleVelocityX > doubleMaxRunSpeed)
            {
                //Set the X velocity to the max run speed
                doubleVelocityX = doubleMaxRunSpeed;
            }
            //ELSE IF the X velocity is greater than zero...
            else if (doubleVelocityX > 0)
            {
                //Decrement the X velocity by a 25th of the acceleration
                doubleVelocityX -= ((double)1 / 25) * doubleAcceleration;
            }

            //IF the player is moving LEFT & his velocity is greater than the negative of the max run speed...
            if (Sparklegore.CurrentKeyboardState.IsKeyDown(Keys.Left) == true
                && doubleVelocityX > -doubleMaxRunSpeed)
            {
                //Decrement the X velocity by a 25th of the acceleration
                doubleVelocityX -= ((double)1 / 25) * doubleAcceleration;
            }
            //ELSE IF the velocity is greater than the max run speed...
            else if (doubleVelocityX < -doubleMaxRunSpeed)
            {
                //Set the X velocity to the max run speed
                doubleVelocityX = -doubleMaxRunSpeed;
            }
            //ELSE IF the X velocity is greater than zero...
            else if (doubleVelocityX < 0)
            {
                //Increment the X velocity by a 25th of the acceleration
                doubleVelocityX += ((double)1 / 25) * doubleAcceleration;
            }
            //ELSE... ()

            //Adding the X velocity to the X position
            v2_Position.X += (float)doubleVelocityX;
        }

        //player process input method
        public void ProcessInput()
        {
            keyboardCurr = Keyboard.GetState();

            if (keyboardCurr.IsKeyDown(Keys.Left))
            {
                if (keyboardCurr.IsKeyDown(Keys.Left) && keyboardPrev.IsKeyDown(Keys.Left))
                {
                    playerKey = playerState.walkLeft;
                }
                else
                {
                    playerKey = playerState.turnLeft;
                }
            }
            if (keyboardCurr.IsKeyDown(Keys.Right))
            {
                if (keyboardCurr.IsKeyDown(Keys.Right) && keyboardPrev.IsKeyDown(Keys.Right))
                {
                    playerKey = playerState.walkRight;
                }
                else
                {
                    playerKey = playerState.turnRight;
                }
            }
            if (keyboardCurr.IsKeyDown(Keys.Space))
            {
                playerKey = playerState.jump;
            }

            //need to add attack and ability enum states



        }

        //Update()
        public override void Update(GameTime gameTime)
        {
            //run the player jump check
            PlayerJump();

            //Update the hitboxes
            UpdateHitboxes();

            //Update playerstates
            switch (playerKey)
            {
                case playerState.turnLeft:
                    //shifts to left side animations
                    break;
                case playerState.walkLeft:
                    //runs left walk animation
                    break;
                case playerState.turnRight:
                    //shifts to right side animations
                    break;
                case playerState.walkRight:
                    //runs right walk animation
                    break;
                case playerState.jump:
                    //runs jump animation as long as space is held or player is not colliding with a platform
                    break;
                case playerState.attack:
                    //runs the animation and attack command
                    break;
                case playerState.ability:
                    //runs the ability animation and ability command
                    break;
                default:
                    //idle animation loops
                    break;
            }

        }

        //Draw()
        public override void Draw(SpriteBatch spriteBatch)
        {
            //Drawing the player
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
            //IF the collision is a platform collision...
            if (collisionType == CollisionType.Platform)
            {
                v2_Position.Y -= collisionArea.Height;
                boolIsInAir = false;
                boolDoubleJumpUsed = false;
                doubleVelocityY = 0.0;
            }

            //IF the collision is the "Player is not above platform" trigger...
            if (collisionType == CollisionType.Player
                && collisionArea.X == -1
                && collisionArea.Y == -1)
            {
                boolIsInAir = true;
            }

            //Updating the hitboxes
            foreach (Hitbox hitbox in Hitboxes)
            {
                hitbox.UpdateHitbox(v2_Position);
            }
        }
    }
}
