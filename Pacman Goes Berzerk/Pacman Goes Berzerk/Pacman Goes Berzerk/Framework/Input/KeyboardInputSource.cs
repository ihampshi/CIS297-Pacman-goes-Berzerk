using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace Pacman_Goes_Berzerk.Framework.Input
{

    //Stores a list of keybindings and relays input events
    public class KeyboardInputSource : KeyboardEventListener
    {

        //The list of key bindings
        protected List<Keybinding> keybindings;

        //Constructor
        public KeyboardInputSource()
        {

            //Initialize keybindings
            keybindings = new List<Keybinding>();
        }

        //When a key is pressed
        public void OnKeyDown(object sender, VirtualKey key)
        {

            //The current keybinding
            Keybinding currentKeybinding;

            //For each keybinding
            for (int index = 0; index < keybindings.Count; index++)
            {

                //Get the current keybinding
                currentKeybinding = keybindings[index];

                //If the type matches
                if (key.Equals(currentKeybinding.key))
                {

                    //Activate the keybinding function
                    currentKeybinding.function(KeyEventType.DOWN);

                    //End the loop
                    break;
                }
            }
        }

        //When a key is released
        public void OnKeyUp(object sender, VirtualKey key)
        {

            //The current keybinding
            Keybinding currentKeybinding;

            //For each keybinding
            for (int index = 0; index < keybindings.Count; index++)
            {

                //Get the current keybinding
                currentKeybinding = keybindings[index];

                //If the type matches
                if (key.Equals(currentKeybinding.key))
                {

                    //Activate the keybinding function
                    currentKeybinding.function(KeyEventType.UP);

                    //End the loop
                    break;
                }
            }

        }
    }

    //What type of events a key may experience
    public enum KeyEventType
    {

        DOWN = 0,
        UP = 1
    }

    //A function able to be activated via keybinding
    public delegate void KeybindingDelegate(KeyEventType state);

    //A function associated with a key
    public struct Keybinding
    {

        //The key
        public VirtualKey key;

        //The function
        public KeybindingDelegate function;
    }
}
