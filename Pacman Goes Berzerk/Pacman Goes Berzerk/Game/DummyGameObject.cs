using Final_Project_Resources_2;
using Microsoft.Graphics.Canvas;
using Pacman_Goes_Berzerk.Framework.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman_Goes_Berzerk.Game
{

    //A game object that does nothing. For testing.
    class DummyGameObject : SpriteGameObject
    {

        Animation animation;

        //Constructor
        public DummyGameObject(Vector2 position, Vector2 colliderSize) : base(position, new BoxCollider(position, colliderSize))
        {

            //Load the animation frames
            List<CanvasBitmap> frames = new List<CanvasBitmap>();
            frames.Add(ImageManager.getImageByName("box"));
            frames.Add(ImageManager.getImageByName("box2"));
            frames.Add(ImageManager.getImageByName("box3"));
            frames.Add(ImageManager.getImageByName("box4"));

            animation = new Animation(frames, 1000);
            animation.Play();
        }

        //When the game object is updated
        public override void Update(double deltaTime)
        {

            //Adjust velocity
            Velocity.x = 100;

            //Update animation
            animation.Update(deltaTime);

            //Update image
            Image = animation.GetCurrentFrame();

            base.Update(deltaTime);
        }

        //When a collision occurs
        public override void OnCollision(ICollisionEventHandler otherCollisionHandler, ICollider otherCollider)
        {
        }

    }
}
