using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace Pacman_Goes_Berzerk.Framework.Input
{

    //Manages player input
    class InputManager : KeyboardEventListener
    {

        //The list of managed keyboard input sources
        List<KeyboardInputSource> inputSources;

        //Constructor
        public InputManager()
        {

            //Initialize input sources list
            inputSources = new List<KeyboardInputSource>();
        }

        //When a key is pressed
        public void OnKeyDown(object sender, VirtualKey key)
        {

            //For each registered input source
            for (int index = 0; index < inputSources.Count; index++)
            {

                //Send the input event
                inputSources[index].OnKeyDown(sender, key);
            }
        }

        //When a key is released
        public void OnKeyUp(object sender, VirtualKey key)
        {

            //For each registered input source
            for (int index = 0; index < inputSources.Count; index++)
            {

                //Send the input event
                inputSources[index].OnKeyUp(sender, key);
            }
        }

        //Adds a new input source
        public void registerInputSource(KeyboardInputSource inputSource)
        {

            //Add to the list
            inputSources.Add(inputSource);
        }

        //Removes an input source
        public void removeInputSource(KeyboardInputSource inputSource)
        {

            //Remove from the list
            inputSources.Remove(inputSource);
        }
    }
}
