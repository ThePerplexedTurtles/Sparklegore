using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Project2_FinalFramework
{
    class EXAMPLE_Scene1: Scene
    {
        //Attributes
        EXAMPLE_Player player;
        List<EXAMPLE_Wall> listWalls = new List<EXAMPLE_Wall>();
        EXAMPLE_Enemy enemy;
        private Texture2D t2d_PlayerAndEnemy;
        private Texture2D t2d_Wall;
        bool isDead = false;


        //[Cosntructor]
        public EXAMPLE_Scene1(ContentManager cont): base(cont)
        {
            //Getting the assets using the content manager
            t2d_PlayerAndEnemy = Content.Load<Texture2D>("Humanoid0");
            t2d_Wall = Content.Load<Texture2D>("Wall");

            //Calling the "Startup()" method to initialize all of the objects to their starting positions
            Startup();
        }

        //Startup()
        public override void Startup()
        {
            //(Re)Defining the game objects so that they start in their original positions
            player = new EXAMPLE_Player(400, 300, t2d_PlayerAndEnemy, 32, 32);
            listWalls.Add(new EXAMPLE_Wall(400, 236, t2d_Wall, 32, 32));
            listWalls.Add(new EXAMPLE_Wall(464, 300, t2d_Wall, 32, 32));
            listWalls.Add(new EXAMPLE_Wall(400, 364, t2d_Wall, 32, 32));
            listWalls.Add(new EXAMPLE_Wall(336, 300, t2d_Wall, 32, 32));
            enemy = new EXAMPLE_Enemy(600, 450, t2d_PlayerAndEnemy, 32, 32);
            isDead = false;
        }

        //Update()                  == 1st METHOD CALLED -in- GAME LOOP
        public override void Update(GameTime gameTime)
        {
            //Updating all of the objects 
            player.Update(gameTime);
            enemy.Update(gameTime);

            foreach (EXAMPLE_Wall wall in listWalls)
            {
                wall.Update(gameTime);
            }

            if(player.Position.X < 0 || player.Position.Y < 0 || player.Position.X > 800 || player.Position.Y > 600)
            {
                isDead = true;
            }
            else
            {
                isDead = false;
            }
        }

        //Draw()                    == 3rd METHOD CALLED -in- GAME LOOP
        public override void Draw(SpriteBatch spriteBatch)
        {
            //Drawing all of the objects (bottom-to-top)
            foreach(EXAMPLE_Wall wall in listWalls)
            {
                wall.Draw(spriteBatch);
            }

            enemy.Draw(spriteBatch);
            player.Draw(spriteBatch);
        }

        //DetectCollisions()        == 2nd METHOD CALLED -in- GAME LOOP
        public override void DetectCollisions()
        {
            //Getting all of the hitboxes from the single-instance objects
            List<Hitbox> hitboxesPlayer = player.Hitboxes;
            List<Hitbox> hitboxesEnemy = enemy.Hitboxes;

            //Player vs. Wall collision
            foreach(EXAMPLE_Wall wall in listWalls)
            {
                List<Hitbox> hitboxesWalls = wall.Hitboxes;
                foreach(Hitbox hitboxWall in hitboxesWalls)
                {
                    if (hitboxWall.Current.Intersects(hitboxesPlayer[0].Current) == true)
                    {
                        Rectangle rectangleCollision = new Rectangle();
                        Rectangle recWall = hitboxWall.Current;
                        Rectangle recPlayer = hitboxesPlayer[0].Current;
                        Rectangle.Intersect(ref recWall, ref recPlayer, out rectangleCollision);
                        player.ProcessCollision(rectangleCollision, hitboxWall.Type);
                    }
                }
            }

            //Player vs. Enemy collision

            if (hitboxesPlayer[0].Current.Intersects(hitboxesEnemy[0].Current) == true)
            {
                Rectangle rectCollision = new Rectangle();
                Rectangle rectPlayer = hitboxesPlayer[0].Current;
                Rectangle rectEnemy = hitboxesEnemy[0].Current;
                Rectangle.Intersect(ref rectPlayer, ref rectEnemy, out rectCollision);
                player.ProcessCollision(rectCollision, hitboxesEnemy[0].Type);
            }
        }

        internal void Draw(SpriteBatch spriteBatch, bool paused)
        {
            if (paused == false)
            {
                //Drawing all of the objects (bottom-to-top)
                foreach (EXAMPLE_Wall wall in listWalls)
                {
                    wall.Draw(spriteBatch);
                }

                enemy.Draw(spriteBatch);
                player.Draw(spriteBatch);
            }
        }

        public bool IsDead
        {
            get { return isDead; }
        }
    }
}
