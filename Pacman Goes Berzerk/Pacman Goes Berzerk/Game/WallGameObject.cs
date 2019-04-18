using Final_Project_Resources_2;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman_Goes_Berzerk.Game
{
    class WallGameObject : GameObject
    {

        //Constructor
        public WallGameObject(Vector2 firstCorner, Vector2 secondCorner) : base((firstCorner+secondCorner)/2, new BoxCollider(Vector2.Zero, (secondCorner-firstCorner)))
        {

            //Mark as a wall
            Name = "wall";
        }

        public override void Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args, DrawInfo info)
        {
            
            //If debug enabled
            if (info.debug)
            {

                //Draw debug collider
                DebugDraw(sender, args, info);
            }
        }

        public override void OnCollision(ICollisionEventHandler otherCollisionHandler, ICollider otherCollider)
        {
            
        }
    }
}
