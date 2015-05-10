using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sparklegore
{
    class EnemyGenerator: GameObject
    {
        //Attributes
        private List<Rectangle> listValidPlatforms = new List<Rectangle>();
        private Random random = new Random();
        private GO_Enemy1[] arrayEnemy1;
        private List<Texture2D> listEnemySpritesheets;
        private int Enemy1_Amount = 5;
        private int Enemy2_Amount = 3;
        private int Enemy1_TimerAmount = 2500;
        private int Enemy2_TimerAmount = 3500;
        private int Enemy1_TimerCurrent = 0;
        private int Enemy2_TimerCurrent = 0;
        private int platformSpeed = 0;


        //[Constructor]
        public EnemyGenerator(List<Texture2D> spritesheets, int platSpeed, List<Rectangle> platforms): base(-100, -100, null, -1, -1)
        {
            //Defining the enemy spritesheets
            listEnemySpritesheets = spritesheets;

            //Defining the difficulty variables
            platformSpeed = platSpeed;

            //Getting the list of available platforms
            UpdatePlatformList(platforms);

            //Defining the enemy arrays
            arrayEnemy1 = new GO_Enemy1[Enemy1_Amount];
            for (int num = 0; num < arrayEnemy1.Length; num++)
            {
                arrayEnemy1[num] = new GO_Enemy1(platformSpeed, -1000, -1000, listEnemySpritesheets[0], 32, 32);
                arrayEnemy1[num].IsOffScreen = true;
            }
        }

        //Update()
        public override void Update(GameTime gameTime)
        {
            //Updating the spawn timers
            Enemy1_TimerCurrent += gameTime.ElapsedGameTime.Milliseconds;
            Enemy2_TimerCurrent += gameTime.ElapsedGameTime.Milliseconds;

            //IF another enemy1 game object can be spawned & there are valid platforms to spawn the enemy on...
            if (Enemy1_TimerCurrent >= Enemy1_TimerAmount   &&   listValidPlatforms.Count > 0)
            {
                //FOR each entry in the enemy1 array...
                for (int num = 0; num < arrayEnemy1.Length; num++)
                {
                    //IF the chosen entry in the array is empty...
                    if (arrayEnemy1[num].IsOffScreen == true)
                    {
                        //Getting a random (valid) platform to spawn the enemy
                        int platformNum = random.Next(listValidPlatforms.Count);

                        //Creating the enemy above the designated block
                        arrayEnemy1[num] = new GO_Enemy1(platformSpeed,
                                                            (int) listValidPlatforms[platformNum].X,
                                                            (int) listValidPlatforms[platformNum].Y - 32,
                                                            listEnemySpritesheets[0],
                                                            32,
                                                            32);

                        //Setting the enemy's 'is off screen' variable to false
                        arrayEnemy1[num].IsOffScreen = false;

                        //Exiting the FOR loop
                        num = arrayEnemy1.Length;
                    }
                }

                //Setting the enemy1 timer to zero
                Enemy1_TimerCurrent = 0;
            }

            //IF another enemy2 game object can be spawned & there are valid platforms to spawn the enemy on...
            if (Enemy2_TimerCurrent >= Enemy2_TimerAmount   &&   listValidPlatforms.Count > 0)
            {

                //Setting the enemy2 timer to zero
                Enemy2_TimerCurrent = 0;
            }

            //Updating the enemy1 array
            for (int num = 0; num < arrayEnemy1.Length; num++)
            {
                //IF the enemy is on screen...
                if (arrayEnemy1[num].IsOffScreen == false)
                {
                    //Updating the enemy
                    arrayEnemy1[num].Update(gameTime);
                }
            }
        }

        //Draw()
        public override void Draw(SpriteBatch spriteBatch)
        {
            //Drawing the enemies
            for (int num = 0; num < arrayEnemy1.Length; num++)
            {
                arrayEnemy1[num].Draw(spriteBatch);
            }
        }

        //ProcessCollisions() -- Basically Useless
        public override void ProcessCollision(Rectangle collisionArea, CollisionType collisionType)
        {
            
        }

        //UpdatePlatformList()
        public void UpdatePlatformList(List<Rectangle> platforms)
        {
            //Clearing the previous list of valid platforms
            listValidPlatforms.Clear();
            
            //Getting a list of the valid platforms
            for (int num = 0; num < platforms.Count; num++)
            {
                //IF the platform is valid (Y is below 0)...
                if (platforms[num].Y < 0)
                {
                    listValidPlatforms.Add(platforms[num]);
                }
            }


        }

        //Accessors & Modifiers
        //::: ::: ::: ::: :::

        //ENEMY 1 ARRAY
        public GO_Enemy1[] Enemy1Array
        {
            get { return arrayEnemy1; }
        }

        //::: ::: ::: ::: :::
    }
}
