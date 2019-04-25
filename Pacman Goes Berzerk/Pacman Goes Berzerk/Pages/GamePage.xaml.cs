using Final_Project_Resources_2;
using Final_Project_Resources_2.Framework.Systems;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Pacman_Goes_Berzerk.Framework.Input;
using Pacman_Goes_Berzerk.Framework.Systems;
using Pacman_Goes_Berzerk.Game;
using Pacman_Goes_Berzerk.Game.Game_objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI;
using Windows.UI.Core;
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

        //Game subsystems
        DrawableIndex drawIndex;
        CollisionManager collisions;
        GameObjectIndex gameObjects;
        InputManager inputManager;
        PlayerRegistry players;

        //The player object
        PlayerGameObject player;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void canvas_CreateResources(ICanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
        {

            //Initialize image loader with canvas
            //TODO: The image manager may fail for multiple pages
            ImageManager.Initialize(sender);

            //Initialize game systems
            drawIndex = new DrawableIndex();
            collisions = new CollisionManager();
            gameObjects = new GameObjectIndex(drawIndex, collisions);
            inputManager = new InputManager();
            players = new PlayerRegistry();

            //Load the required images
            ImageManager.LoadImages(PacmanImagePaths.Images);

            //Register key event listeners
            Window.Current.CoreWindow.KeyDown += canvas_KeyDown;
            Window.Current.CoreWindow.KeyUp += canvas_KeyUp;


            //Add background image
            DrawableImage background = new DrawableImage(ImageManager.getImageByName("background1"), Vector2.Zero, false);
            drawIndex.AddDrawable(background);

            //Create a player
            player = new PlayerGameObject(new Vector2(200, 300), gameObjects, players, inputManager, KeyboardFormat.WASD);
            gameObjects.registerGameObject(player);


            //Create a dummy game object
            DummyGameObject testingObject = new DummyGameObject(new Vector2(100, 100), new Vector2(45, 45), gameObjects);

            //Register the new object in the game object index
            gameObjects.registerGameObject(testingObject);

            //Register the new object as an input listener
            inputManager.registerInputSource(new PlayerKeyboardInputSource(testingObject, KeyboardFormat.ARROWS));

            //Enable debug drawing
            drawIndex.SetDebugDrawing(true);

            //Add a wall
            WallGameObject wall = new WallGameObject(new Vector2(100, 100), new Vector2(150, 200));
            gameObjects.registerGameObject(wall);
            WallGameObject wall2 = new WallGameObject(new Vector2(150, 200), new Vector2(350, 250));
            gameObjects.registerGameObject(wall2);
            WallGameObject wall3 = new WallGameObject(new Vector2(300, 100), new Vector2(350, 200));
            gameObjects.registerGameObject(wall3);

            //Add an enemy object
            EnemyGameObject enemy = new EnemyGameObject(new Vector2(300, 300), gameObjects, new Random());
            enemy.Target = player;
            gameObjects.registerGameObject(enemy);
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
        }

        private void canvas_KeyDown(CoreWindow sender, KeyEventArgs e)
        {

            //Notify the input manager
            inputManager.OnKeyDown(sender, e.VirtualKey);
        }

        private void canvas_KeyUp(CoreWindow sender, KeyEventArgs e)
        {

            //Notify the input manager
            inputManager.OnKeyUp(sender, e.VirtualKey);
        }
    }
}
