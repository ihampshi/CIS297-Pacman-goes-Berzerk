using Final_Project_Resources_2;
using Final_Project_Resources_2.Framework.Systems;
using Microsoft.Graphics.Canvas;
using Pacman_Goes_Berzerk.Framework;
using Pacman_Goes_Berzerk.Framework.Drawing;
using Pacman_Goes_Berzerk.Framework.Input;
using Pacman_Goes_Berzerk.Game.Game_objects;
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
        public DummyGameObject(Vector2 position, Vector2 colliderSize, GameObjectIndex container) : base(position, new BoxCollider(position, colliderSize), container)
        {

            //Load the animation frames
            List<CanvasBitmap> frames = new List<CanvasBitmap>();
            frames.Add(ImageManager.getImageByName("pacman_closed"));
            frames.Add(ImageManager.getImageByName("pacman_halfopen_right"));
            frames.Add(ImageManager.getImageByName("pacman_open_right"));
            frames.Add(ImageManager.getImageByName("pacman_halfopen_right"));

            animation = new Animation(frames, 200);
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
                GameObjectHelper.resolveRectangularCollision(this, wallBoxCollider);
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
