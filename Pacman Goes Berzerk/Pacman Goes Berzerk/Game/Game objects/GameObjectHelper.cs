using Final_Project_Resources_2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman_Goes_Berzerk.Game.Game_objects
{
    public static class GameObjectHelper
    {

        //Move outside of a rectangular area
        public static void resolveRectangularCollision(GameObject target, BoxCollider otherBoxCollider)
        {

            //Get our collider
            BoxCollider boxCollider = (BoxCollider)target.Collider;

            //Get our collider's information
            Vector2 position = boxCollider.Position;
            Vector2 size = boxCollider.Size;

            //Get the bounds of this box
            double maximumX = position.x + size.x / 2;
            double maximumY = position.y + size.y / 2;
            double minimumX = position.x - size.x / 2;
            double minimumY = position.y - size.y / 2;

            //Get the bounds of the other box
            double maximumOtherX = otherBoxCollider.Position.x + otherBoxCollider.Size.x / 2;
            double maximumOtherY = otherBoxCollider.Position.y + otherBoxCollider.Size.y / 2;
            double minimumOtherX = otherBoxCollider.Position.x - otherBoxCollider.Size.x / 2;
            double minimumOtherY = otherBoxCollider.Position.y - otherBoxCollider.Size.y / 2;

            //Compute penetration depths
            double rightPenetration = maximumX - minimumOtherX;
            double leftPenetration = maximumOtherX - minimumX;
            double upperPenetration = maximumY - minimumOtherY;
            double lowerPenetration = maximumOtherY - minimumY;

            //Compute the smallest penetration depth
            double[] penetrationDepths = new double[4] { rightPenetration, leftPenetration, upperPenetration, lowerPenetration };
            int smallestPositiveDepth = 0;
            double smallestDepth = rightPenetration;

            for (int index = 1; index < penetrationDepths.Length; index++)
            {

                if (penetrationDepths[index] > 0 && penetrationDepths[index] < smallestDepth)
                {

                    smallestDepth = penetrationDepths[index];
                    smallestPositiveDepth = index;
                }
            }

            //Resolve each collision case
            switch (smallestPositiveDepth)
            {

                case 0:

                    //Right penetration
                    target.Position.x -= smallestDepth;
                    break;
                case 1:

                    //Left penetration
                    target.Position.x += smallestDepth;
                    break;
                case 2:

                    //Upper penetration
                    target.Position.y -= smallestDepth;
                    break;
                case 3:

                    //Lower penetration
                    target.Position.y += smallestDepth;
                    break;
            }
        }
    }
}
