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

namespace Sparklegore
{
    class Scene_Game: Scene
    {
        //Attributes
        Texture2D t2d_Wall;
        Texture2D t2d_Humanoids;
        Texture2D t2d_Enemy;
        List<GO_Platform> listPlatforms = new List<GO_Platform>();
        List<GO_Enemy1> listEnemies = new List<GO_Enemy1>();
        GO_Player player;
        EnemyGenerator enemyGenerator;
        Random random = new Random();
        bool isDead;
        Map map;
        List<Rectangle> platforms;
        DiffReader reader = new DiffReader("Content/TestDiff");


        //[Constructor]
        public Scene_Game(ContentManager cont) : base(cont)
        {
            //Getting all of the sprites
            t2d_Wall = cont.Load<Texture2D>("Wall");
            t2d_Humanoids = cont.Load<Texture2D>("PlayerSpritesheet");
            t2d_Enemy = cont.Load<Texture2D>("Enemy1");

            //Defining the map
            map = new Map(t2d_Wall, 32, 32);

            //Reading the diff file
            reader.ReadDiffFile();

            //Starting up the scene for the first time
            Startup();
            boolResetRequested = false;
            
        }

        //Startup()
        public override void Startup()
        {
            //Creating the platforms
            /*
            for (int num = 0; num < 18; num++)
            {
                listPlatforms.Add(new GO_Platform(68 + (32 * num), 350 + 32 * (random.Next(10) - 5), t2d_Wall, 32, 32));
            }
            */ 

            for (int num = 0; num < 18; num++)
            {
                listPlatforms.Add(new GO_Platform(68 + (32 * num), 350 + 32, t2d_Wall, 32, 32));
            }

            //Creating the player
            player = new GO_Player(reader.PlatFormSpeed, 100, 132, t2d_Humanoids, 50, 46);

            //Creating the enemy generator
            List<Texture2D> enemySpritesheets = new List<Texture2D>();
            enemySpritesheets.Add(t2d_Enemy);
            enemyGenerator = new EnemyGenerator(enemySpritesheets, reader.PlatFormSpeed, map.Platforms);

            //Setting the 'is dead' variable to false
            isDead = false;

            //Calling the map
            map.MapReader("Content/map01.txt");

            platforms = map.Platforms;
        }

        //Update()
        public override void Update(GameTime gameTime)
        {
            /*
            //Updating all of the platforms
            for (int num = 0; num < listPlatforms.Count; num++)
            {
                listPlatforms[num].Update(gameTime);
            }
            */
            
            //Updating the player game object
            player.Update(gameTime);

            //Determining if the player is dead
            if(player.Position.X < -60 || player.Position.Y < -60 || player.Position.X > 800 || player.Position.Y > 600)
            {
                isDead = true;
                map.Platforms.Clear();
            }

            //generate new platforms
            map.Generate(map.Platforms, reader.PlatFormSpeed);

            //Updating the enemy generator
            enemyGenerator.UpdatePlatformList(map.Platforms);
            enemyGenerator.Update(gameTime);

            // clear the wall list so we can update it with new platforms
            listPlatforms.Clear();

            //update listWalls with newly generated platforms
            foreach (Rectangle platform in map.Platforms)
            {
                listPlatforms.Add(new GO_Platform(platform.X, platform.Y, t2d_Wall, platform.Width, platform.Height));
            }

            foreach (GO_Platform platform in listPlatforms)
            {
                platform.Update(gameTime);
            }
        }

        //Draw()
        public override void Draw(SpriteBatch spriteBatch)
        {
            //IF the game is not paused...
            if (Sparklegore.isPaused == false)
            {
                //Drawing the platforms
                for (int num = 0; num < listPlatforms.Count; num++)
                {
                    listPlatforms[num].Draw(spriteBatch);
                }

                //Drawing the enemies
                enemyGenerator.Draw(spriteBatch);

                //Drawing the player
                player.Draw(spriteBatch);
            }
        }

        

        //DetectCollisions()
        public override void DetectCollisions()
        {
            //Setting the collision check variables
            bool playerIsAbovePlatform = false;
            bool playerRightCheck = false;
            bool playerLeftCheck = false;

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
                    }

                    //IF the BOTTOM platform check hitbox intersects with ANY platform hitbox...
                    if (hitboxes[num2].Current.Intersects(playerHitboxes[1].Current) == true)
                    {
                        //Set the 'player is above platform' boolean to true
                        playerIsAbovePlatform = true;
                    }

                    //IF the RIGHT platform check hitbox intersects with ANY platform hitbox...
                    if (hitboxes[num2].Current.Intersects(playerHitboxes[2].Current) == true)
                    {
                        //Set the 'player right check' boolean to true
                        playerRightCheck = true;
                    }

                    //IF the LEFT platform check hitbox intersects with ANY platform hitbox...
                    if (hitboxes[num2].Current.Intersects(playerHitboxes[3].Current) == true)
                    {
                        //Set the 'player left check' boolean to true
                        playerLeftCheck = true;
                    }

                    //Updating the relevant hitboxes
                    playerHitboxes = player.Hitboxes;
                }
            }

            //IF the 'player is above platform' boolean was not set to true... (the player isn't above a platform)
            if (playerIsAbovePlatform == false)
            {
                player.ProcessCollision(new Rectangle(0, 0, 0, -1), CollisionType.Null);
            }

            //IF the 'playern right check' boolean was not set to true... (the player is NOT to the left of a platform)
            if (playerRightCheck == false)
            {
                player.ProcessCollision(new Rectangle(1, 0, -1, 0), CollisionType.Null);
            }

            //IF the 'player left check' boolean was not set to true... (the player is NOT to the right of a platform)
            if (playerLeftCheck == false)
            {
                player.ProcessCollision(new Rectangle(-1, 0, -1, 0), CollisionType.Null);
            }
        }

        //Accessors & Modifiers
        //::: ::: ::: ::: :::

        //IS DEAD
        public bool IsDead
        {
            get { return isDead; }
        }

        //::: ::: ::: ::: :::
    }
}
