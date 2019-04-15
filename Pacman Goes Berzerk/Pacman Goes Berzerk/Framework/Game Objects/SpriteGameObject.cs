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

        //Constructor
        public SpriteGameObject(Vector2 position, Collider collider) : base(position, collider)
        {

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
    }
}
