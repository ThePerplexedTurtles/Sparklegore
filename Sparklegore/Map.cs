//Logan Rogers 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.IO;

namespace Sparklegore
{
    class Map
    {
        private List<Rectangle> platforms = new List<Rectangle>();
        Texture2D platformImage;
        private int platformWidth;
        private int platformHeight;
        int distanceCount = 0;
        // constr. that takes and sets default platform image, width, and height from the platform gameobject  
        public Map(Texture2D tempPlatformImage, int tempPlatformWidth, int tempPlatformHeight)
        {
            platformImage = tempPlatformImage;
            platformWidth = tempPlatformWidth;
            platformHeight = tempPlatformHeight;
        }

        // AM for Platforms array
       public List<Rectangle> Platforms
        {
            get { return platforms; } // returns platforms collect
            set { platforms = new List<Rectangle>(value); } // overwrites platform array with a new array (clears all data)  Use when starting new game?
        }

        // read a map file designated by a provided file path stored in the "filePath" variable.  Store platform tiles in platform array
        public void MapReader(string filePath)
        {
            StreamReader fileReader = null;
            try
            {
                // open the file first
                fileReader = new StreamReader(filePath);


                string text = null;


                int line = 0; // counting variable to keep track of the line being read
                while ((text = fileReader.ReadLine()) != null) // loop to read the file
                {
                    line++; // increase the line number (we are now reading a new line)
                    int characterIndex = 0; // create a counter varible to determine location of the character

                    foreach (char character in text) // check if a character in the file reprsents a platform
                    {
                        characterIndex++;  // add 1 too character index
                        if (character == '#') // in this case the character is a platform, add a platform with to the characters index on the line and line number
                        {
                            platforms.Add(new Rectangle(platformWidth * characterIndex, platformHeight * line, platformWidth, platformHeight));
                        }
                    }
                }
            }
            catch (IOException ioe)
            {
                // write out the message and the stack trace
                Console.WriteLine("Message: " + ioe.Message);
                Console.WriteLine("This error took place in the map class (Stack Trace: " + ioe.StackTrace);
            }
            finally
            {
                if (fileReader != null)
                {
                    fileReader.Close();  // close regardless of exceptions
                }

                
            }
        }

        public void Generate(List<Rectangle> list, int platformSpeed)
        {




            const int HEIGHT = 600;
            int moveRate = platformSpeed;
            const int TOGENERATECOUNT = 100;
            const int WIDTH = 800;

            List<Rectangle> tempList = new List<Rectangle>();

            foreach (Rectangle platform in list)
            {
                // check if platform moved off screen
                if (platform.Y + moveRate < HEIGHT + 64)
                {
                    tempList.Add(new Rectangle(platform.X, (platform.Y + moveRate), platform.Width, platform.Height));
                }
                // move platform to top because it is off the screen with random loaction


                if (distanceCount > TOGENERATECOUNT)
                {
                    int baseX = 0;
                    Random ran = new Random();
                    baseX = ran.Next(0, WIDTH);

                    tempList.Add(new Rectangle(baseX, -1 * platform.Height, list[0].Width, list[0].Height));
                    tempList.Add(new Rectangle(baseX + (list[0].Width), -1 * platform.Height, list[0].Width, list[0].Height));
                    tempList.Add(new Rectangle(baseX + (list[0].Width * 2), -1 * platform.Height, list[0].Width, list[0].Height));
                    tempList.Add(new Rectangle(baseX + (list[0].Width * 3), -1 * platform.Height, list[0].Width, list[0].Height));
                    tempList.Add(new Rectangle(baseX + (list[0].Width * 4), -1 * platform.Height, list[0].Width, list[0].Height));
                    distanceCount = 0;
                }
            }


            distanceCount++;
            Platforms = tempList;
        }
        // testing method (see if I didn't mess this up)
        public void DrawMap(SpriteBatch spriteBatch)
        {
            foreach (Rectangle platform in platforms)
            {
                spriteBatch.Draw(platformImage, platform, Color.White);
            }
        }
    }
}
