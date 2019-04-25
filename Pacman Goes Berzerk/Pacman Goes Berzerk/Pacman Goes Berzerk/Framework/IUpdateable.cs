using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman_Goes_Berzerk.Framework
{
    interface IUpdateable
    {

        //The object can be updated with a given amount of time (in milliseconds)
        void Update(double deltaTime);
    }
}
