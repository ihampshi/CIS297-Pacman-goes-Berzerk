using Final_Project_Resources_2;
using Final_Project_Resources_2.Framework.Systems;
using Microsoft.Graphics.Canvas;
using Pacman_Goes_Berzerk.Framework;
using Pacman_Goes_Berzerk.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman_Goes_Berzerk.Game.Game_objects
{
    class PlayerGameObject : SpriteGameObject, PlayerInputListener
    {

        //The collider size
        public static Vector2 HITBOX_SIZE = new Vector2(50, 50);

        //The movement speed
        public static double SPEED = 100.0;

        //The player images
        List<CanvasBitmap> images;

        //Which movement buttons are pressed
        bool[] movement;

        //Constructor
        public PlayerGameObject(Vector2 position, GameObjectIndex container) : base(position, new BoxCollider(position, HITBOX_SIZE), container)
        {

            //Get player images
            images = new List<CanvasBitmap>();
            images.Add(ImageManager.getImageByName("red_ghost_up"));
            images.Add(ImageManager.getImageByName("red_ghost_down"));
            images.Add(ImageManager.getImageByName("red_ghost_left"));
            images.Add(ImageManager.getImageByName("red_ghost_right"));

            //Initialize image
            Image = images[3];

            //Initialize movement array
            movement = new bool[4] { false, false, false, false };
        }

        //On collision
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

        //When the attack button is pressed
        public void Attack()
        {


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

        //When the player is deleted
        public override void Destroy()
        {
            
            base.Destroy();
        }
    }
}
