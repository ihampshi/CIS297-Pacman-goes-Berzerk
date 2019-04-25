using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace Pacman_Goes_Berzerk.Framework.Input
{

    //Anything that can be notified of key events
    interface KeyboardEventListener
    {

        //When a key is pressed
        void OnKeyDown(object sender, VirtualKey key);

        //When a key is released
        void OnKeyUp(object sender, VirtualKey key);

    }
}
