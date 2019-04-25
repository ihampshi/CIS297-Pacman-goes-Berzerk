using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_Resources_2
{
    public class Vector2
    {

        //Static vectors
        public static Vector2 Zero { get { return new Vector2(0, 0); } }
        public static Vector2 One { get { return new Vector2(1, 1); } }

        //The vector's position
        public double x;
        public double y;

        //Constructor
        public Vector2(double x, double y)
        {

            //Initialize position
            this.x = x;
            this.y = y;
        }

        //Returns the angle of the vector
        public double getAngle()
        {

            //Return angle
            return Math.Atan2(y, x);
        }

        //Returns the magnitude of the vector
        public double getMagnitude()
        {

            //Return magnitude
            return Math.Sqrt(x * x + y * y);
        }

        //Returns the squared magnitude of the vector, faster
        public double getMagnitudeSquared()
        {

            //Return magnitude squared
            return (x * x + y * y);
        }

        //Returns the distance between this vector and the given vector
        public double distanceTo(Vector2 otherVector)
        {

            //Get the difference in position
            Vector2 difference = this - otherVector;

            //Get the magnitude of the difference
            double distance = difference.getMagnitude();

            //Return the distance
            return distance;
        }

        //Returns the distance between this vector and the given vector squared
        public double distanceToSquared(Vector2 otherVector)
        {

            //Get the difference in position
            Vector2 difference = this - otherVector;

            //Get the magnitude of the difference squared
            double distance = difference.getMagnitudeSquared();

            //Return the distance
            return distance;
        }

        //Operator overloads

        //Addition
        public static Vector2 operator +(Vector2 firstVector, Vector2 secondVector)
        {

            //Create a new vector with the sum of both vector's components
            Vector2 result = new Vector2(firstVector.x + secondVector.x, firstVector.y + secondVector.y);

            //Return the sum
            return result;
        }


        //Subtraction
        public static Vector2 operator -(Vector2 firstVector, Vector2 secondVector)
        {

            //Create a new vector with the difference of both vector's components
            Vector2 result = new Vector2(firstVector.x - secondVector.x, firstVector.y - secondVector.y);

            //Return the sum
            return result;
        }

        //Negation
        public static Vector2 operator -(Vector2 vector)
        {

            //Create a new vector with the negated components
            Vector2 result = new Vector2(-vector.x, -vector.y);

            //Return the sum
            return result;
        }

        //Scalar multiplication
        public static Vector2 operator *(Vector2 vector, double scalar)
        {

            //Create a new vector with the scaled components
            Vector2 result = new Vector2(vector.x * scalar, vector.y * scalar);

            //Return the sum
            return result;
        }

        //Scalar division
        public static Vector2 operator /(Vector2 vector, double scalar)
        {

            //Create a new vector with the scaled components
            Vector2 result = new Vector2(vector.x / scalar, vector.y / scalar);

            //Return the sum
            return result;
        }
    }
}
