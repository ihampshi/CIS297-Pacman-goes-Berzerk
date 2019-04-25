using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_Resources_2
{

    //Manages a list of drawable objects
    public class DrawableIndex
    {

        //The list of drawable objects to be displayed
        List<IDrawable> drawables;

        //The list of position modifiers
        List<DrawingPositionModifier> positionModifiers;

        //Whether debug drawing is enabled
        bool debugMode;

        //Constructor
        public DrawableIndex()
        {

            //Initialize drawables list
            drawables = new List<IDrawable>();

            //Initialize position modifiers list
            positionModifiers = new List<DrawingPositionModifier>();

            //Initialize without debug drawing
            debugMode = false;
        }

        //Set the debug drawing flag
        public void SetDebugDrawing(bool value)
        {

            //Set flag
            debugMode = value;
        }

        //Draw all the elements in the index
        public void Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {

            //The offset position
            Vector2 offset = Vector2.Zero;

            //For each position modifier
            for (int index = 0; index < positionModifiers.Count; index++)
            {

                //Add the current modifier's offset
                offset += positionModifiers[index].getOffset();
            }

            //Create the drawing info
            DrawInfo drawingInfo = new DrawInfo(offset, debugMode);

            //For each element in the index
            for (int index = 0; index < drawables.Count; index++)
            {

                //Draw the element
                drawables[index].Draw(sender, args, drawingInfo);
            }
        }

        //Adds a drawable element
        public void AddDrawable(IDrawable drawableObject)
        {

            //Add the new element to the index
            drawables.Add(drawableObject);
        }

        //Removes a drawable element
        public void RemoveDrawable(IDrawable drawableObject)
        {

            //Removes the element from the index
            drawables.Remove(drawableObject);
        }
    }
}
