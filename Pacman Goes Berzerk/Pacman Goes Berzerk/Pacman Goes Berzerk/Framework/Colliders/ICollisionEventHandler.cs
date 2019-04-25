using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_Resources_2
{

    //Implementor must be able to respond to collision events
    public interface ICollisionEventHandler
    {

        //Triggered upon a collision event
        void OnCollision(ICollisionEventHandler otherCollisionHandler, ICollider otherCollider);
    }
}
