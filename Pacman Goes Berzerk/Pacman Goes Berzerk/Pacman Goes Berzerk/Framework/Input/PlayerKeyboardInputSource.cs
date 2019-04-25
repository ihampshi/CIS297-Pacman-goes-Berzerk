using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace Pacman_Goes_Berzerk.Framework.Input
{

    //A potential keyboard format
    public enum KeyboardFormat
    {
        
        ARROWS = 0,
        WASD = 1
    }

    //Represents an input source for a player object taking input from keyboard events
    class PlayerKeyboardInputSource : KeyboardInputSource
    {

        //The target player
        PlayerInputListener target;

        //Constructor
        public PlayerKeyboardInputSource(PlayerInputListener target, KeyboardFormat format)
        {

            //Set target player
            this.target = target;

            //Initialize keybindings
            setupKeybindings(format);
        }

        //Sets up the keybindings for player input given the desired format
        private void setupKeybindings(KeyboardFormat format)
        {

            //For the selected format
            switch (format)
            {

                case KeyboardFormat.ARROWS:

                    //Setup delegates for the arrow keys
                    Keybinding upKeybinding = new Keybinding() { key = VirtualKey.Up, function = OnUpInput};
                    Keybinding downKeybinding = new Keybinding() { key = VirtualKey.Down, function = OnDownInput };
                    Keybinding leftKeybinding = new Keybinding() { key = VirtualKey.Left, function = OnLeftInput };
                    Keybinding rightKeybinding = new Keybinding() { key = VirtualKey.Right, function = OnRightInput };
                    Keybinding attackKeybinding = new Keybinding() { key = VirtualKey.M, function = OnAttackInput };

                    //Add keybindings to list
                    keybindings.Add(upKeybinding);
                    keybindings.Add(downKeybinding);
                    keybindings.Add(leftKeybinding);
                    keybindings.Add(rightKeybinding);
                    keybindings.Add(attackKeybinding);

                    break;

                case KeyboardFormat.WASD:

                    //Setup delegates for the WASD keys
                    Keybinding WKeybinding = new Keybinding() { key = VirtualKey.W, function = OnUpInput };
                    Keybinding SKeybinding = new Keybinding() { key = VirtualKey.S, function = OnDownInput };
                    Keybinding AKeybinding = new Keybinding() { key = VirtualKey.A, function = OnLeftInput };
                    Keybinding DKeybinding = new Keybinding() { key = VirtualKey.D, function = OnRightInput };
                    Keybinding attackKeybinding2 = new Keybinding() { key = VirtualKey.F, function = OnAttackInput };

                    //Add keybindings to list
                    keybindings.Add(WKeybinding);
                    keybindings.Add(SKeybinding);
                    keybindings.Add(AKeybinding);
                    keybindings.Add(DKeybinding);
                    keybindings.Add(attackKeybinding2);
                    break;
            }
        }

        //Translates an UP key event to the target player
        private void OnUpInput(KeyEventType type)
        {

            //If the target is valid
            if (target != null)
            {

                //If the event is a key down event
                if (type == KeyEventType.DOWN)
                {

                    //Send movement event
                    target.SetMovementIn(CardinalDirection.NORTH, true);
                }
                else
                {

                    //Send stopping event
                    target.SetMovementIn(CardinalDirection.NORTH, false);
                }
            }
        }

        //Translates a DOWN key event to the target player
        private void OnDownInput(KeyEventType type)
        {

            //If the target is valid
            if (target != null)
            {

                //If the event is a key down event
                if (type == KeyEventType.DOWN)
                {

                    //Send movement event
                    target.SetMovementIn(CardinalDirection.SOUTH, true);
                }
                else
                {

                    //Send stopping event
                    target.SetMovementIn(CardinalDirection.SOUTH, false);
                }
            }
        }

        //Translates an LEFT key event to the target player
        private void OnLeftInput(KeyEventType type)
        {

            //If the target is valid
            if (target != null)
            {

                //If the event is a key down event
                if (type == KeyEventType.DOWN)
                {

                    //Send movement event
                    target.SetMovementIn(CardinalDirection.WEST, true);
                }
                else
                {

                    //Send stopping event
                    target.SetMovementIn(CardinalDirection.WEST, false);
                }
            }
        }

        //Translates an RIGHT key event to the target player
        private void OnRightInput(KeyEventType type)
        {

            //If the target is valid
            if (target != null)
            {

                //If the event is a key down event
                if (type == KeyEventType.DOWN)
                {

                    //Send movement event
                    target.SetMovementIn(CardinalDirection.EAST, true);
                }
                else
                {

                    //Send stopping event
                    target.SetMovementIn(CardinalDirection.EAST, false);
                }
            }
        }

        //Translates an ATTACK key event to the target player
        private void OnAttackInput(KeyEventType type)
        {

            //If the target is valid
            if (target != null)
            {

                //If the event is a key down event
                if (type == KeyEventType.DOWN)
                {

                    //Send attack event
                    target.Attack();
                }
            }
        }
    }
}
