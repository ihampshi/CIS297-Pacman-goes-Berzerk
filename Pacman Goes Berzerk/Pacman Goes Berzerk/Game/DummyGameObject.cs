using Final_Project_Resources_2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman_Goes_Berzerk.Game
{

    //A game object that does nothing. For testing.
    class DummyGameObject : SpriteGameObject
    {

        //Constructor
        public DummyGameObject(Vector2 position, Vector2 colliderSize) : base(position, new BoxCollider(position, colliderSize))
        {
        }

        //When a collision occurs
        public override void OnCollision(ICollisionEventHandler otherCollisionHandler, ICollider otherCollider)
        {
        }

    }
}
