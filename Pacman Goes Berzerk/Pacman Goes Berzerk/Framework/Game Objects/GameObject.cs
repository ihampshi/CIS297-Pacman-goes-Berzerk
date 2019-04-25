using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Pacman_Goes_Berzerk.Framework;

namespace Final_Project_Resources_2
{
    abstract public class GameObject : IDrawable, IUpdateable, ICollisionEventHandler
    {

        //The game object's name
        string name;

        public string Name { get { return name; } set { name = value; } }

        //The object's collider
        Collider collider;

        public Collider Collider { get { return collider; } private set { collider = value; } }
        
        //The object's position
        Vector2 position;

        public Vector2 Position { get { return position; } set { position = value; OnChangePosition(); } }

        //The object's velocity (per second)
        Vector2 velocity;

        public Vector2 Velocity { get { return velocity; } set { velocity = value; } }

        //Constructor
        public GameObject(Vector2 position, Collider collider)
        {

            //Initialize position
            this.position = position;

            //Initialize velocity
            velocity = Vector2.Zero;

            //Set collider
            this.collider = collider;
        }

        //The update method
        public virtual void Update(double deltaTime)
        {

            //Move according to velocity
            IntegratePosition(deltaTime);
        }

        //Update the position based on the current velocity
        protected void IntegratePosition(double deltaTime)
        {

            //Add the current velocity to the position
            Position += Velocity * (deltaTime / 1000);
        }

        //The drawing method
        abstract public void Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args, DrawInfo info);

        //Perform debug drawing
        protected void DebugDraw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args, DrawInfo info)
        {

            //If the collider is a box collider
            if (collider is BoxCollider)
            {

                //Cast the collider to a box collider
                BoxCollider boxCollider = (BoxCollider)collider;

                //Copy the collider into a debug collider
                DrawableBoxCollider debugCollider = new DrawableBoxCollider(boxCollider.Position, boxCollider.Size);

                //Draw the collider
                debugCollider.Draw(sender, args, info);
            } else

            //If the collider is a circle collider
            if (collider is CircleCollider)
            {

                //Cast the collider to a circle collider
                CircleCollider circleCollider = (CircleCollider)collider;

                //Copy the collider into a debug collider
                DrawableCircleCollider debugCollider = new DrawableCircleCollider(circleCollider.Position, circleCollider.Radius);

                //Draw the collider
                debugCollider.Draw(sender, args, info);
            }
        }

        //Triggered upon a collision event
        abstract public void OnCollision(ICollisionEventHandler otherCollisionHandler, ICollider otherCollider);

        //Triggered when the sprite should be removed
        abstract public void Destroy();

        //Triggered upon a change in position
        virtual public void OnChangePosition()
        {

            //Update the collider's position
            collider.Position = position;
        }
    }
}
