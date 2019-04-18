using Final_Project_Resources_2;
using Microsoft.Graphics.Canvas;
using Pacman_Goes_Berzerk.Framework;
using Pacman_Goes_Berzerk.Framework.Drawing;
using Pacman_Goes_Berzerk.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman_Goes_Berzerk.Game
{

    //A game object that does nothing. For testing.
    class DummyGameObject : SpriteGameObject, PlayerInputListener
    {

        const double SPEED = 100.0;

        Animation animation;

        bool[] movement;

        //Constructor
        public DummyGameObject(Vector2 position, Vector2 colliderSize) : base(position, new BoxCollider(position, colliderSize))
        {

            //Load the animation frames
            List<CanvasBitmap> frames = new List<CanvasBitmap>();
            frames.Add(ImageManager.getImageByName("box"));
            //frames.Add(ImageManager.getImageByName("box2"));
            //frames.Add(ImageManager.getImageByName("box3"));
            //frames.Add(ImageManager.getImageByName("box4"));

            animation = new Animation(frames, 1000);
            animation.Play();

            //Initialize motion array
            movement = new bool[4] { false, false, false, false };
        }

        //When the game object is updated
        public override void Update(double deltaTime)
        {

            //Update animation
            animation.Update(deltaTime);

            //Update image
            Image = animation.GetCurrentFrame();

            base.Update(deltaTime);
        }

        //When a collision occurs
        public override void OnCollision(ICollisionEventHandler otherCollisionHandler, ICollider otherCollider)
        {

            //Attempt to match other object as a wall
            WallGameObject wall = otherCollisionHandler as WallGameObject;
            
            //If other object is a wall
            if (wall != null)
            {

                //Get collider
                BoxCollider wallBoxCollider = otherCollider as BoxCollider;

                //Move out of it's range
                resolveRectangularCollision(wallBoxCollider);
            }
        }

        //Move outside of a rectangular area
        private void resolveRectangularCollision(BoxCollider otherBoxCollider)
        {

            //Get our collider
            BoxCollider boxCollider = (BoxCollider)Collider;

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
            switch(smallestPositiveDepth)
            {

                case 0:

                    //Right penetration
                    Position.x -= smallestDepth;
                    break;
                case 1:

                    //Left penetration
                    Position.x += smallestDepth;
                    break;
                case 2:

                    //Upper penetration
                    Position.y -= smallestDepth;
                    break;
                case 3:

                    //Lower penetration
                    Position.y += smallestDepth;
                    break;
            }
        }

        //Sets movement status in a direction
        public void SetMovementIn(CardinalDirection direction, bool moving)
        {
            
            if (direction == CardinalDirection.NORTH)
            {

                movement[0] = moving;
            }

            if (direction == CardinalDirection.SOUTH)
            {

                movement[1] = moving;
            }

            if (direction == CardinalDirection.EAST)
            {

                movement[2] = moving;
            }

            if (direction == CardinalDirection.WEST)
            {

                movement[3] = moving;
            }

            //Add up velocity vector
            Vector2 newVelocity = Vector2.Zero;

            if (movement[0])
            {
                newVelocity += CardinalDirection.NORTH.ToVector2() * SPEED;
            }

            if (movement[1])
            {
                newVelocity += CardinalDirection.SOUTH.ToVector2() * SPEED;
            }

            if (movement[2])
            {
                newVelocity += CardinalDirection.EAST.ToVector2() * SPEED;
            }

            if (movement[3])
            {
                newVelocity += CardinalDirection.WEST.ToVector2() * SPEED;
            }

            //Set new velocity
            Velocity = newVelocity;
        }

        //Attack
        public void Attack()
        {
            Position.x += 100;
        }

    }
}
