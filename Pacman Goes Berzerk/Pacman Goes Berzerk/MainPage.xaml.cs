using Final_Project_Resources_2;
using Final_Project_Resources_2.Framework.Systems;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Pacman_Goes_Berzerk.Game;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Pacman_Goes_Berzerk
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        //The number of frames per second
        const int FPS = 60;

        //The amount of time to update all objects each frame, in milliseconds
        const double MILLISECONDS_PER_FRAME = 1000.0 / FPS;

        //The image paths and resource names
        List<Tuple<string, string>> projectImages = new List<Tuple<string, string>>() {
            new Tuple<string, string> ("box", "Assets/LockScreenLogo.scale-200.png" ), };

        //Game subsystems
        DrawableIndex drawIndex;
        CollisionManager collisions;
        GameObjectIndex gameObjects;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void canvas_CreateResources(ICanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
        {

            //Initialize image loader with canvas
            //TODO: The image manager may fail for multiple frames
            ImageManager.Initialize(sender);

            //Initialize game systems
            drawIndex = new DrawableIndex();
            collisions = new CollisionManager();
            gameObjects = new GameObjectIndex(drawIndex, collisions);

            //Load the required images
            ImageManager.LoadImages(projectImages);


            //Create a dummy game object
            DummyGameObject testingObject = new DummyGameObject(new Vector2(100, 100), new Vector2(50, 50));

            //Set the object's image
            testingObject.Image = ImageManager.getImageByName("box");

            //Register the new object in the game object index
            gameObjects.registerGameObject(testingObject);

            //Enable debug drawing
            drawIndex.SetDebugDrawing(true);
        }

        private void canvas_Update(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args)
        {

            //Check for collision events
            collisions.CollisionTest();

            //Update game objects
            gameObjects.Update(MILLISECONDS_PER_FRAME);
        }

        private void canvas_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {

            //Draw all elements within the draw list
            drawIndex.Draw(sender, args);

            args.DrawingSession.DrawText("Not much is here yet, but there's a fair amount of code beneath.", new System.Numerics.Vector2(0, 0), Colors.Black);
        }
    }
}
