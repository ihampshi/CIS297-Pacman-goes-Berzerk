using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman_Goes_Berzerk.Framework.Input
{

    //An implementor must accept activation methods for player actions
    interface PlayerInputListener
    {

        void SetMovementIn(CardinalDirection direction, bool moving);

        void Attack();
    }
}
