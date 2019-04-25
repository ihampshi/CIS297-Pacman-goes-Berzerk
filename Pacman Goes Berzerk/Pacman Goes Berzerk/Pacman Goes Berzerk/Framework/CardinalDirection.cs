using Final_Project_Resources_2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman_Goes_Berzerk.Framework
{

    //Represents a cardinal direction
    public class CardinalDirection
    {

        //The possible values
        public static CardinalDirection NORTH = new CardinalDirection(0);
        public static CardinalDirection EAST = new CardinalDirection(1);
        public static CardinalDirection SOUTH = new CardinalDirection(2);
        public static CardinalDirection WEST = new CardinalDirection(3);

        //The direction
        int direction;

        //Constructor
        public CardinalDirection(int direction)
        {

            //If given direction is valid
            if (direction >= 0 && direction <= 3)
            {

                //Initialize with direction
                this.direction = direction;
            } else
            {

                //Throw exception
                throw new ArgumentOutOfRangeException();
            }
        }

        //Converts the direction into a unit vector in the corresponding direction
        public Vector2 ToVector2()
        {

            //The resulting vector
            Vector2 result = Vector2.Zero;

            //For each direction
            switch(direction)
            {

                case 0:

                    //North
                    result = new Vector2(0, -1);
                    break;
                case 1:

                    //East
                    result = new Vector2(1, 0);
                    break;
                case 2:

                    //South
                    result = new Vector2(0, 1);
                    break;
                case 3:

                    //West
                    result = new Vector2(-1, 0);
                    break;
            }

            //Return the result
            return result;
        }

        //Convert the direction to an integer
        public int ToInt()
        {

            //Return the numeric direction
            return direction;
        }
    }
}
