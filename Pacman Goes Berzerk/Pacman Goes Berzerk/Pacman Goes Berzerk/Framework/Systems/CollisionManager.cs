using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_Resources_2
{
    
    //Tests collisions between a list of registered colliders
    public class CollisionManager
    {

        //A registered collider with a collision handler
        struct RegisteredCollider
        {

            //The collider to test
            public ICollider collider;

            //The handler to send collision events to
            public ICollisionEventHandler collisionHandler;
        }
        
        //The index of registered colliders
        List<RegisteredCollider> colliders;

        //Constructor
        public CollisionManager()
        {

            //Initialize collider list
            colliders = new List<RegisteredCollider>();
        }

        //Registers a new collider with an event handler
        public void registerCollider(ICollider collider, ICollisionEventHandler collisionHandler)
        {

            //Create a new registered collider
            RegisteredCollider newRegisteredCollider = new RegisteredCollider();
            newRegisteredCollider.collider = collider;
            newRegisteredCollider.collisionHandler = collisionHandler;

            //Add the new collider to the index
            colliders.Add(newRegisteredCollider);
        }

        //Removes a collider from the list
        public void removeCollider(ICollider collider)
        {

            //For each registered collider
            for (int index = 0; index < colliders.Count; index++)
            {

                //If the collider is the target collider
                if (colliders[index].collider == collider)
                {

                    //Remove the registered collider from the list
                    colliders.RemoveAt(index);

                    //End the loop
                    break;
                }
            }
        }

        //Tests each collider for collision against other colliders
        public void CollisionTest()
        {

            //The current registered colliders
            RegisteredCollider currentCollider;
            RegisteredCollider currentCollider2;

            //The current colliders
            ICollider outerCollider;
            ICollider innerCollider;

            //For each registered collider
            for (int outerIndex = 0; outerIndex < colliders.Count; outerIndex++)
            {

                //Get the current registered collider
                currentCollider = colliders[outerIndex];

                //For each registered collider
                for (int innerIndex = 0; innerIndex < colliders.Count; innerIndex++)
                {

                    //If the collider is not the current collider
                    if (outerIndex != innerIndex)
                    {

                        //Get the inner registered collider
                        currentCollider2 = colliders[innerIndex];

                        //Get the inner and outer colliders
                        outerCollider = currentCollider.collider;
                        innerCollider = currentCollider2.collider;

                        //If the outer collider is colliding with the inner collider
                        if (outerCollider.CollidingWith(innerCollider))
                        {

                            //Set off collision events
                            currentCollider.collisionHandler.OnCollision(currentCollider2.collisionHandler, currentCollider2.collider);
                            currentCollider2.collisionHandler.OnCollision(currentCollider.collisionHandler, currentCollider.collider);
                        }
                    }
                }
            }
        }
        
        
    }
}
