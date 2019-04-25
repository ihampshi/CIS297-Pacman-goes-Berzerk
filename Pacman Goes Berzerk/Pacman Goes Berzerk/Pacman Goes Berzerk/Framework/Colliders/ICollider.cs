using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_Resources_2
{

    //Represents an object that may collide with another object
    public interface ICollider
    {

        //Whether this collider is colliding with the given collider
        bool CollidingWith(ICollider otherCollider);
    }

    //The standard implementation of a collider
    public abstract class Collider : ICollider
    {

        //The position of the collider
        protected Vector2 position;

        public Vector2 Position
        {
            get
            {

                return position;
            }
            set
            {

                position = value;
                OnPositionChange();
            }
        }

        //Whether this collider is colliding with the given collider
        abstract public bool CollidingWith(ICollider otherCollider);

        //Triggered when the position changes
        virtual protected void OnPositionChange()
        {
        }
    }

    //A rectangular collider object
    public class BoxCollider : Collider
    {

        //The size of the collider
        private Vector2 size;
        public Vector2 Size { get { return size; } set { size = value; } }

        //Constructor
        public BoxCollider(Vector2 position, Vector2 size)
        {

            //Initialize dimensions
            this.position = position;
            this.size = size;
        }

        //Whether this collider is colliding with the given collider
        public override bool CollidingWith(ICollider otherCollider)
        {

            //The result
            bool result = false;

            //Get the bounds of this box
            double maximumX = position.x + size.x / 2;
            double maximumY = position.y + size.y / 2;
            double minimumX = position.x - size.x / 2;
            double minimumY = position.y - size.y / 2;

            //The other collider extracted as a box collider
            BoxCollider otherBoxCollider;

            //The other collider extracted as a circle collider
            CircleCollider otherCircleCollider;

            //If the other collider is a box collider
            if (otherCollider is BoxCollider)
            {

                //Get the other collider as a box collider
                otherBoxCollider = (BoxCollider)otherCollider;

                //Get the bounds of the other box
                double maximumOtherX = otherBoxCollider.position.x + otherBoxCollider.size.x / 2;
                double maximumOtherY = otherBoxCollider.position.y + otherBoxCollider.size.y / 2;
                double minimumOtherX = otherBoxCollider.position.x - otherBoxCollider.size.x / 2;
                double minimumOtherY = otherBoxCollider.position.y - otherBoxCollider.size.y / 2;

                //If x intervals are intersecting
                if (maximumX > minimumOtherX && minimumX < maximumOtherX)
                {

                    //If y intervals are intersecting
                    if (maximumY > minimumOtherY && minimumY < maximumOtherY)
                    {

                        //Mark as colliding
                        result = true;
                    }
                }
            }

            else if (otherCollider is CircleCollider)
            {

                //Get the other colllider as a circle collider
                otherCircleCollider = (CircleCollider)otherCollider;

                //Get the circle's position
                Vector2 circlePosition = otherCircleCollider.Position;

                //If circle's center is diagonal to this collider
                if ((circlePosition.x < minimumX || circlePosition.x > maximumX) &&( circlePosition.y < minimumY || circlePosition.y > maximumY))
                {

                    //Get the squared radius of the circle collider
                    double radiusSquared = otherCircleCollider.Radius * otherCircleCollider.Radius;

                    //Get the four corners of the box
                    Vector2 upperLeft = new Vector2(minimumX, minimumY);
                    Vector2 upperRight = new Vector2(maximumX, minimumY);
                    Vector2 lowerLeft = new Vector2(minimumX, maximumY);
                    Vector2 lowerRight = new Vector2(maximumX, maximumY);

                    //Get the corner's distances to the circle
                    double distanceUpperLeftSquared = otherCircleCollider.Position.distanceToSquared(upperLeft);
                    double distanceUpperRightSquared = otherCircleCollider.Position.distanceToSquared(upperRight);
                    double distanceLowerLeftSquared = otherCircleCollider.Position.distanceToSquared(lowerLeft);
                    double distanceLowerRightSquared = otherCircleCollider.Position.distanceToSquared(lowerRight);

                    //If any of the corners are within range
                    if (distanceUpperLeftSquared < radiusSquared)
                    {

                        //Mark as colliding
                        result = true;
                    }

                    else if (distanceUpperRightSquared < radiusSquared)
                    {

                        //Mark as colliding
                        result = true;
                    }

                    else if (distanceLowerLeftSquared < radiusSquared)
                    {

                        //Mark as colliding
                        result = true;
                    }

                    else if (distanceLowerRightSquared < radiusSquared)
                    {

                        //Mark as colliding
                        result = true;
                    }

                }

                //If circle's center is lateral to this collider
                else
                {

                    //Get the bounding box of the circle
                    BoxCollider bounds = otherCircleCollider.bounds;

                    //If colliding with bounding box
                    if (CollidingWith(bounds))
                    {

                        //Mark as colliding
                        result = true;
                    }
                }
            }

            //Return result
            return result;
        }
    }

    //A circular collider object
    public class CircleCollider : Collider
    {

        //The size of the circle
        protected double radius;

        public double Radius
        {
            get
            {

                return radius;
            }
            set
            {

                radius = value;
                updateBounds();
            }
        }

        //The bounding box of the circle
        public BoxCollider bounds { get; private set; }

        //Constructor
        public CircleCollider(Vector2 position, double radius)
        {

            //Initialize collider
            this.position = position;
            this.radius = radius;

            //Update bounding box
            updateBounds();
        }

        //Whether this collider is colliding with the given collider
        public override bool CollidingWith(ICollider otherCollider)
        {

            //The result
            bool result = false;

            //The other collider extracted as a box collider
            BoxCollider otherBoxCollider;

            //The other collider extracted as a circle collider
            CircleCollider otherCircleCollider;

            //If the other collider is a box collider
            if (otherCollider is BoxCollider)
            {

                //Get the other collider as a box collider
                otherBoxCollider = (BoxCollider)otherCollider;

                //Use box collider's method for box-circle collision case
                return otherBoxCollider.CollidingWith(this);
            }

            else if (otherCollider is CircleCollider)
            {

                //Get the other colllider as a circle collider
                otherCircleCollider = (CircleCollider)otherCollider;

                //Get the sum of the radii squared
                double radiiSum = radius + otherCircleCollider.radius;
                double radiiSquared = radiiSum * radiiSum;

                //If the distance squared is smaller than the sum of theradii squared
                if (position.distanceToSquared(otherCircleCollider.position) < radiiSquared)
                {

                    //Mark as colliding
                    result = true;
                }
            }

            //Return result
            return result;
        }

        //Triggered when a position changes
        protected override void OnPositionChange()
        {

            //Update the bounds
            updateBounds();
        }

        //Calculates the bounding box of the circle
        private void updateBounds()
        {

            //Initialize new bounds
            bounds = new BoxCollider(position, new Vector2(radius * 2, radius * 2));
        }
    }
}
