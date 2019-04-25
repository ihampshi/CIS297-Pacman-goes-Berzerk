using Final_Project_Resources_2;
using Final_Project_Resources_2.Framework.Systems;
using Pacman_Goes_Berzerk.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman_Goes_Berzerk.Game.Game_objects
{

    //A bullet that was fired by another game object
    class BulletGameObject : SpriteGameObject
    {

        //The hitbox size of the bullet
        private static Vector2 BULLET_SIZE = new Vector2(10, 10);

        //The amount of time the bullet has left (in milliseconds)
        public double Lifetime { get; set; }

        //The speed that the bullet will move
        public double Speed { get; set; }

        //The direction of the bullet
        public CardinalDirection Direction { get; set; }

        //Constructor
        public BulletGameObject(Vector2 position, GameObjectIndex objectIndex, double lifetime, double speed) : base(position, new BoxCollider(position, BULLET_SIZE), objectIndex)
        {

            //Set properties
            Lifetime = lifetime;
            Speed = speed;

            //Set image
            Image = ImageManager.getImageByName("bullet");
        }

        //Every game update
        public override void Update(double deltaTime)
        {
            base.Update(deltaTime);
            
            //Count down lifetime
            Lifetime -= deltaTime;

            //If lifetime has hit 0
            if (Lifetime <= 0.0)
            {

                //Destroy the bullet
                Destroy();
            }
        }

        //On collision with a game object
        public override void OnCollision(ICollisionEventHandler otherCollisionHandler, ICollider otherCollider)
        {
        }

        //Sets the direction of the bullet
        public void SetDirection(CardinalDirection direction)
        {

            //Set the direction
            Direction = direction;

            //Set velocity
            Velocity = direction.ToVector2() * Speed;
        }
    }
}
