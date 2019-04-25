using Final_Project_Resources_2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman_Goes_Berzerk.Framework.Systems
{

    //Manages the players
    public class PlayerRegistry
    {

        //The list of player objects
        List<GameObject> playerObjects;

        //Random number generator
        Random randomNumbers;

        //Constructor
        public PlayerRegistry(Random randomNumbers)
        {

            //Initialize player list
            playerObjects = new List<GameObject>();

            //Assign random numbers
            this.randomNumbers = randomNumbers;
        }

        //Adds a player to the list
        public void AddPlayer(GameObject newPlayerObject)
        {

            //Add to the list
            playerObjects.Add(newPlayerObject);
        }

        //Removes a player from the list
        public void RemovePlayer(GameObject playerObject)
        {

            //Remove from the list
            playerObjects.Remove(playerObject);
        }

        //Checks whether the player list is empty
        public bool IsEmpty()
        {

            //Return whether the list is empty
            return playerObjects.Count == 0;
        }

        //Returns a random player
        public GameObject GetRandom()
        {

            //The result
            GameObject selectedObject = null;

            //If players are contained
            if (playerObjects.Count > 0)
            {

                //Generate random index
                int index = randomNumbers.Next(0, playerObjects.Count);

                //Select at index
                selectedObject = playerObjects[index];
            }

            //Return selected object
            return selectedObject;
        }
    }
}
