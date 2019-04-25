using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;

namespace Final_Project_Resources_2
{

    //Box collider which can be drawn to the canvas (for debugging)
    public class DrawableBoxCollider : BoxCollider, IDrawable
    {

        //The default drawing color
        private static Color DEFAULT_DRAWING_COLOR = Colors.Green;

        //The drawing color
        private Color drawingColor;

        //Constructors
        public DrawableBoxCollider(Vector2 position, Vector2 size, Color drawingColor) : base(position, size)
        {

            //Set drawing color
            this.drawingColor = drawingColor;
        }

        public DrawableBoxCollider(Vector2 position, Vector2 size) : base(position, size)
        {

            //Set drawing color
            this.drawingColor = DEFAULT_DRAWING_COLOR;
        }

        //Displays this collider (for debugging)
        public void Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args, DrawInfo info)
        {

            //Draw box outline
            Vector2 lowerBound = (Position + info.offset) - (Size / 2);
            args.DrawingSession.DrawRectangle(new Rect(lowerBound.x, lowerBound.y, Size.x, Size.y), drawingColor);
        }

        //Sets the drawing color
        public void setColor(Color newColor)
        {

            //Set drawing color
            drawingColor = newColor;
        }
    }

    //Circle collider which can be drawn to the canvas (for debugging)
    public class DrawableCircleCollider : CircleCollider, IDrawable
    {

        //The default drawing color
        private static Color DEFAULT_DRAWING_COLOR = Colors.Green;

        //The drawing color
        private Color drawingColor;


        //Constructors
        public DrawableCircleCollider(Vector2 position, double radius, Color drawingColor) : base(position, radius)
        {

            //Set drawing color
            this.drawingColor = drawingColor;
        }

        public DrawableCircleCollider(Vector2 position, double radius) : base(position, radius)
        {

            //Set drawing color
            this.drawingColor = DEFAULT_DRAWING_COLOR;
        }

        //Displays this collider (for debugging)
        public void Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args, DrawInfo info)
        {

            //Draw circle outline
            System.Numerics.Vector2 systemPosition = new System.Numerics.Vector2((float)(position.x+info.offset.x), (float)(position.y+info.offset.y));
            args.DrawingSession.DrawCircle(systemPosition, (float)this.radius, drawingColor);
        }

        //Sets the drawing color
        public void setColor(Color newColor)
        {

            //Set drawing color
            drawingColor = newColor;
        }
    }
}
