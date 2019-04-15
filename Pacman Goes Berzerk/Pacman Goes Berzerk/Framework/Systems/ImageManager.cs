using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace Final_Project_Resources_2
{
    
    //Manages the loading of images for the canvas
    public static class ImageManager
    {

        //The amount of time, in milliseconds, to wait before timing out on loading an image
        static private long LOADING_TIMEOUT = 10_000;

        //Stores a single loaded image and relevant information
        struct ImageManagerResource
        {

            //The resource name
            public string name;

            //The file path
            public string path;

            //The target image
            public CanvasBitmap image;
        }

        //Whether the class has been initialized
        static private bool initialized = false;

        //The target canvas
        static ICanvasAnimatedControl canvasControl;

        //The currently loaded images
        static List<ImageManagerResource> loadedImages;

        //Static constructor
        static public void Initialize(ICanvasAnimatedControl canvasControl)
        {

            //If not already initialized
            if (!initialized)
            {

                //Set canvas reference
                ImageManager.canvasControl = canvasControl;

                //Initialize images index
                loadedImages = new List<ImageManagerResource>();

                //Mark as initialized
                initialized = true;
            }
        }

        //Loads an image at the given path
        static public void LoadImage(string resourceName, string path)
        {

            //If initialized
            if (initialized)
            {

                //The loaded image
                CanvasBitmap image = null;

                //Create and start a timer
                Stopwatch timer = new Stopwatch();
                timer.Start();

                //Begin loading image
                IAsyncOperation<CanvasBitmap> loadingOperation = CanvasBitmap.LoadAsync(canvasControl, path);

                //Until loading is completed or times out
                while (loadingOperation.Status != AsyncStatus.Completed && timer.ElapsedMilliseconds < LOADING_TIMEOUT)
                {

                    //Wait
                }

                //Stop the timer
                timer.Stop();

                //Retrieve the image
                image = loadingOperation.GetResults();

                //Create a new image resource
                ImageManagerResource resource = new ImageManagerResource();
                resource.name = resourceName;
                resource.path = path;
                resource.image = image;

                //Add the new resource to the index
                loadedImages.Add(resource);
            }
        }

        //Loads multiple images, given by string tuples for the name and path
        static public void LoadImages(List<Tuple<string, string>> namesAndPaths)
        {

            //If initialized
            if (initialized)
            {

                //For each target image
                for (int index = 0; index < namesAndPaths.Count; index++)
                {

                    //Attempt to load the image
                    LoadImage(namesAndPaths[index].Item1, namesAndPaths[index].Item2);
                }
            }
        }

        //Retrieves the bitmap with the matching resource name
        static public CanvasBitmap getImageByName(string resourceName)
        {

            //The result
            CanvasBitmap result = null;

            //If initialized
            if (initialized)
            {

                //For each loaded image
                for (int index = 0; index < loadedImages.Count; index++)
                {

                    //Get the current image
                    ImageManagerResource currentImageResource = loadedImages[index];

                    //If the resource name matches
                    if (resourceName.Equals(currentImageResource.name))
                    {

                        //Set the result
                        result = currentImageResource.image;

                        //End the loop
                        break;
                    }
                }

            }

            //Return the result
            return result;
        }
    }
}
