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
using Microsoft.Xna.Framework.Content;

namespace Project2_FinalFramework
{
    class Scene_Game : Scene
    {
        //Attributes
        Texture2D t2d_Wall;
        Texture2D t2d_Humanoids;
        Texture2D t2d_Enemy;
        List<GO_Platform> listPlatforms = new List<GO_Platform>();
        GO_Player player;
        GO_Enemy enemy;


        //[Constructor]
        public Scene_Game(ContentManager cont)
            : base(cont)
        {
            //Getting all of the sprites
            t2d_Wall = cont.Load<Texture2D>("Wall");
            t2d_Humanoids = cont.Load<Texture2D>("Humanoid0");
            t2d_Enemy = cont.Load<Texture2D>("enemyingame");

            //Starting up the scene for the first time
            Startup();
            boolResetRequested = false;
        }

        //Startup()
        protected override void Startup()
        {
            //Creating the platforms
            for (int num = 0; num < 18; num++)
            {
                listPlatforms.Add(new GO_Platform(68 + (32 * num), 350, t2d_Wall, 32, 32));
            }

            //Creating the player
            player = new GO_Player(100, 100, t2d_Humanoids, 32, 32);

            //Creating the enemy
            enemy = new GO_Enemy(50, 100, t2d_Enemy, 20, 20, listPlatforms);
        }

        //Update()
        public override void Update(GameTime gameTime)
        {
            //Updating all of the platforms
            for (int num = 0; num < listPlatforms.Count; num++)
            {
                listPlatforms[num].Update(gameTime);
            }

            //Updating the player game object
            player.Update(gameTime);

            //Updating the enemy
            enemy.Update(gameTime);
        }

        //Draw()
        public override void Draw(SpriteBatch spriteBatch)
        {
            //Drawing the platforms
            for (int num = 0; num < listPlatforms.Count; num++)
            {
                listPlatforms[num].Draw(spriteBatch);
            }

            //Drawing the player
            player.Draw(spriteBatch);

            //Drawing the enemy
            enemy.Draw(spriteBatch);
        }

        //DetectCollisions()
        public override void DetectCollisions()
        {
            //Setting the collision check variables
            bool playerIsAbovePlatform = false;

            //Getting all of the hitboxes from the single-instance objects
            List<Hitbox> playerHitboxes = player.Hitboxes;

            //PLAYER vs. PLATFORMS
            for (int num = 0; num < listPlatforms.Count; num++)
            {
                List<Hitbox> hitboxes = listPlatforms[num].Hitboxes;
                for (int num2 = 0; num2 < hitboxes.Count; num2++)
                {
                    //IF a platform collision hitbox intersects with the player hitbox...
                    if (hitboxes[num2].Current.Intersects(playerHitboxes[0].Current) == true)
                    {
                        //Getting the space of the collision
                        Rectangle rectangleCollision = new Rectangle();
                        Rectangle recWall = hitboxes[num2].Current;
                        Rectangle recPlayer = playerHitboxes[0].Current;
                        Rectangle.Intersect(ref recWall, ref recPlayer, out rectangleCollision);

                        //Passing the collision rectangle to the object(s) that need to process the collision
                        player.ProcessCollision(rectangleCollision, hitboxes[num2].Type);

                        //Updating the relevant hitboxes
                        playerHitboxes = player.Hitboxes;
                    }

                    //IF the platform check hitbox intersects with ANY platform hitbox...
                    if (hitboxes[num2].Current.Intersects(playerHitboxes[1].Current) == true)
                    {
                        //Set the 'player is above platform' boolean to true
                        playerIsAbovePlatform = true;
                    }
                }
            }

            //IF the 'player is above platform' boolean was not set to true... (the player isn't above a platform)
            if (playerIsAbovePlatform == false)
            {
                player.ProcessCollision(new Rectangle(-1, -1, -1, -1), CollisionType.Player);
            }
        }
    }
}
