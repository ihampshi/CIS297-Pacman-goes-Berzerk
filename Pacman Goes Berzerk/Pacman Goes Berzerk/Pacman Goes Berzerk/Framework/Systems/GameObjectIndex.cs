using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_Resources_2.Framework.Systems
{
    
    //Manages a group of game objects
    public class GameObjectIndex
    {

        //The drawable index
        DrawableIndex drawableIndex;

        //The collision manager
        CollisionManager collisionManager;

        //The list of game objects
        List<GameObject> gameObjects;

        //Constructor
        public GameObjectIndex(DrawableIndex drawableIndex, CollisionManager collisionManager)
        {

            //Set system references
            this.drawableIndex = drawableIndex;
            this.collisionManager = collisionManager;

            //Initialize index
            gameObjects = new List<GameObject>();
        }

        //Add a game object to the index
        public void registerGameObject(GameObject newGameObject)
        {

            //Add as a drawable
            drawableIndex.AddDrawable(newGameObject);

            //Register the game object's collider
            collisionManager.registerCollider(newGameObject.Collider, newGameObject);

            //Add the new object to the index
            gameObjects.Add(newGameObject);
        }

        //Removes a game object from the index
        public void removeGameObject(GameObject gameObject)
        {

            //Remove from drawable list
            drawableIndex.RemoveDrawable(gameObject);

            //Remove from collider manager
            collisionManager.removeCollider(gameObject.Collider);

            //Remove from the game objects list
            gameObjects.Remove(gameObject);
        }
        
        //Updates the game objects within the list
        public void Update(double deltaTime)
        {

            //For each game object
            for (int index = 0; index < gameObjects.Count; index++)
            {

                //Update the current object
                gameObjects[index].Update(deltaTime);
            }
        }
    }
}
