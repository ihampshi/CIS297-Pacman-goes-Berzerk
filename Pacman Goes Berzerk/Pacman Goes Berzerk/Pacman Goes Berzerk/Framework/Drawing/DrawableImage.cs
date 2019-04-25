using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_Resources_2
{

    //A specific image that can be drawn to the canvas
    class DrawableImage : IDrawable
    {

        //The image to draw
        public CanvasBitmap image;

        //The position to draw the image at
        public Vector2 position;

        //Whether to draw the image centered at the position
        bool centered;

        //Constructor
        public DrawableImage(CanvasBitmap image, Vector2 position, bool centered = false)
        {

            //Initialize
            this.image = image;
            this.position = position;
            this.centered = centered;
        }

        //Draws the image at the specified position
        public void Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args, DrawInfo info)
        {

            //If the image is set
            if (image != null)
            {

                //Compute the final position to draw at
                Vector2 drawPosition = position + info.offset;

                //If the drawing should be centered
                if (centered)
                {

                    //Get half of the image's size
                    Vector2 halfSize = new Vector2(image.Size.Width / 2, image.Size.Height / 2);

                    //Move the image back by half of its size
                    drawPosition -= halfSize;
                }

                //Draw the image
                args.DrawingSession.DrawImage(image, (float)drawPosition.x, (float)drawPosition.y);
            }
        }
    }
}
