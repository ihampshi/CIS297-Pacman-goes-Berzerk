using Final_Project_Resources_2;
using Final_Project_Resources_2.Framework.Systems;
using Microsoft.Graphics.Canvas;
using Pacman_Goes_Berzerk.Framework;
using Pacman_Goes_Berzerk.Framework.Input;
using Pacman_Goes_Berzerk.Framework.Systems;
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
        public static Vector2 HITBOX_SIZE = new Vector2(25, 25);

        //The movement speed
        public static double SPEED = 100.0;

        //The player index
        PlayerRegistry players;

        //The input manager
        InputManager input;

        //The input source
        KeyboardInputSource keyboardInput;

        //The direction the player is looking in
        public CardinalDirection Direction { get; set; }

        //The player images
        List<CanvasBitmap> images;

        //Which movement buttons are pressed
        bool[] movement;

        //Constructor
        public PlayerGameObject(Vector2 position, GameObjectIndex container, PlayerRegistry players, InputManager inputManager, KeyboardFormat format) : base(position, new BoxCollider(position, HITBOX_SIZE), container)
        {

            //Initialize keyboard input source
            keyboardInput = new PlayerKeyboardInputSource(this, format);

            //Register in subsystems
            players.AddPlayer(this);
            inputManager.registerInputSource(keyboardInput);

            //Set subsystem references
            this.players = players;
            this.input = inputManager;

            //Get player images
            images = new List<CanvasBitmap>();
            images.Add(ImageManager.getImageByName("red_ghost_up"));
            images.Add(ImageManager.getImageByName("red_ghost_right"));
            images.Add(ImageManager.getImageByName("red_ghost_down"));
            images.Add(ImageManager.getImageByName("red_ghost_left"));

            //Initialize image
            Image = images[1];

            //Initialize movement array
            movement = new bool[4] { false, false, false, false };

            //Initialize direction
            Direction = CardinalDirection.EAST;
        }

        //On collision
        public override void OnCollision(ICollisionEventHandler otherCollisionHandler, ICollider otherCollider)
        {

            //Attempt to match other object as a wall
            WallGameObject wall = otherCollisionHandler as WallGameObject;

            //Attempt to match other object as a wall
            BulletGameObject bullet = otherCollisionHandler as BulletGameObject;

            //If the other object is a wall
            if (wall != null)
            {

                //Get collider
                BoxCollider wallBoxCollider = otherCollider as BoxCollider;

                //Move out of it's range
                GameObjectHelper.resolveRectangularCollision(this, wallBoxCollider);
            }

            //If the other object is a bullet
            if (bullet != null)
            {

                //If the other object is an enemy bullet
                if (bullet.Name == "enemy_bullet")
                {

                    //Destroy the bullet
                    bullet.Destroy();

                    //Destroy this object
                    Destroy();
                }
            }
        }

        //When the attack button is pressed
        public void Attack()
        {

            //Create a new bullet
            BulletGameObject newBullet = new BulletGameObject(Position, Container, 1000.0, 300);
            newBullet.Name = "player_bullet";
            newBullet.SetDirection(Direction);

            //Register the bullet
            Container.registerGameObject(newBullet);
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

            //If moving in the new direction
            if (moving)
            {

                //Update the direction
                Direction = direction;

                //Update the image
                updateImage();
            }
        }

        //Changes the player's image to match the direction
        private void updateImage()
        {

            //Convert the direction to an integer
            int numDirection = Direction.ToInt();

            //Set image based on direction
            Image = images[numDirection];
        }

        //When the player is deleted
        public override void Destroy()
        {
            base.Destroy();

            //Unregister from player and input systems
            players.RemovePlayer(this);
            input.removeInputSource(keyboardInput);
        }
    }
}
