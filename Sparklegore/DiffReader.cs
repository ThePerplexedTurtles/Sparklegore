using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Sparklegore
{
    // This class allows us to read a difficulty file and store it's data to local variables within this class.
    // a get method can be called for all of these variables and they may be changed at a latter time if needed
    // simply create a DiffReader object, pass it the file name of the difficulty file to read, call the ReadDiffFile method
    // calling the ReadDiffFile method stores the data of the file to the private int variables coresponding to the diff variables
    // as needed in the scene class call a get for a needed vale EX(int enemySpeed = diffReaderObj.enemySpeed;)
    class DiffReader
    {
        private string fileName = null;
        string varName = null;
        private int varValue = -1;
        private int platFormSpeed = -1;
        private int enemySpeed = -1;
        private int enemyKnockBack = -1;
        private int playerSpeed = -1;
        private int playerJumpHeight = -1;

        // cons for the difficulty reader (takes a file name and stores it to the local filename var.
        public DiffReader(string tempFilename)
        {
            fileName = tempFilename;
        }

        // reads the diffile and stores int values too variables
        public void ReadDiffFile()
        {
            StreamReader input = null;
            try
            {
                // open the file first
                input = new StreamReader(fileName);

                // loop to read the file
                string text = "";
                while ((text = input.ReadLine()) != null)
                {


                    // split data into words
                    string[] words = text.Split(':');

                    // set var value
                    varValue = int.Parse(words[1]);

                    // set the varible we just read
                    varName = words[0];

                    // depending on varName's value we store varValue to a variable
                    if (varName.ToLower() == "platformspeed")
                    {
                        platFormSpeed = varValue;
                    }
                    else if (varName.ToLower() == "enemyspeed")
                    {
                        enemySpeed = varValue;
                    }
                    else if (varName.ToLower() == "enemyknockback")
                    {
                        enemyKnockBack = varValue;
                    }
                    else if (varName.ToLower() == "playerspeed")
                    {
                        playerSpeed = varValue;
                    }
                    else if (varName.ToLower() == "playerjumpheight")
                    {
                        playerJumpHeight = varValue;
                    }
                }
            }
            catch (IOException ioe)
            {
                // write out the message and the stack trace
                Console.WriteLine("Message: " + ioe.Message);
                Console.WriteLine("Stack Trace: " + ioe.StackTrace);
            }
            finally
            {
                if (input != null)
                {
                    input.Close();  // close regardless of exceptions
                }
            }
        }

        // acc and mute methods for the diff variables
        public int PlatFormSpeed { get { return platFormSpeed; } set { platFormSpeed = value; } }
        public int EnemySpeed { get { return enemySpeed; } set { enemySpeed = value; } }
        public int EnemyKnockBack { get { return enemyKnockBack; } set { enemyKnockBack = value; } }
        public int PlayerSpeed { get { return playerSpeed; } set { PlayerSpeed = value; } }
        public int PlayerJumpHeight{get { return playerJumpHeight; }}
    }
}

