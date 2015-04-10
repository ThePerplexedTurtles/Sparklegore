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

namespace Project2_FinalFramework
{
    //Enum "CollisionType" starts
    public enum CollisionType
    {
        Platform,
        Player,
        Enemy1,
        Bullet
    }
    //Enum "CollisionType" ends


    //Class "Hitbox" starts
    class Hitbox
    {
        //Attributes
        private Rectangle rectangleRelative;
        private Rectangle rectangleCurrent;
        private CollisionType collisionType;


        //[Constructor]
        public Hitbox(Vector2 posObject, int posX, int posY, int width, int height, CollisionType type)
        {
            //Creating the relative rectangle
            rectangleRelative = new Rectangle(posX, posY, width, height);

            //Creating the world rectangle
            rectangleCurrent = new Rectangle((int) (posObject.X + posX), (int) (posObject.Y + posY), width, height);

            //Setting the collision type
            collisionType = type;
        }

        //UpdateHitbox()
        public void UpdateHitbox(Vector2 posObject)
        {
            //Updateing the world rectangle
            rectangleCurrent = new Rectangle((int)(posObject.X + rectangleRelative.X), (int)(posObject.Y + rectangleRelative.Y),
                                            rectangleRelative.Width, rectangleRelative.Height);
        }

        //Accessors & Modifiers
        //::: ::: ::: ::: :::

        //Current
        public Rectangle Current
        {
            get { return rectangleCurrent; }
        }

        //COLLISION TYPe
        public CollisionType Type
        {
            get { return collisionType; }
        }

        //::: ::: ::: ::: :::
    }
    //Class "Hitbox" ends
}
