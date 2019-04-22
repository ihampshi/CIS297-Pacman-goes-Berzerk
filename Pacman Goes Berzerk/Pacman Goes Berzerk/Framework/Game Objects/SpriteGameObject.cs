using Final_Project_Resources_2.Framework.Systems;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_Resources_2
{

    //A game object inheritor with standard functionality for most game objects
    abstract public class SpriteGameObject : GameObject
    {
        
        //This object's image
        DrawableImage drawableImage;

        public CanvasBitmap Image { get { return drawableImage.image; } set { drawableImage.image = value; } }

        //The game object index that contains this game object
        GameObjectIndex container;

        public GameObjectIndex Container { get { return container; } set { container = value; } }

        //Constructor
        public SpriteGameObject(Vector2 position, Collider collider, GameObjectIndex container) : base(position, collider)
        {

            //Initialize container
            this.container = container;

            //Initialize drawable image
            drawableImage = new DrawableImage(null, position, true);
        }

        //Draws this game object
        public override void Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args, DrawInfo info)
        {

            //Draw the image
            drawableImage.Draw(sender, args, info);

            //If debug drawing is enabled
            if (info.debug)
            {

                //Draw the debug collider
                DebugDraw(sender, args, info);
            }
        }

        //Triggered upon a change in position
        public override void OnChangePosition()
        {

            //Call the base position update
            base.OnChangePosition();

            //Update the image's position
            drawableImage.position = Position;
        }

        //When the object should be removed
        public override void Destroy()
        {

            //Remove the player from the game object index
            container.removeGameObject(this);
        }
    }
}
