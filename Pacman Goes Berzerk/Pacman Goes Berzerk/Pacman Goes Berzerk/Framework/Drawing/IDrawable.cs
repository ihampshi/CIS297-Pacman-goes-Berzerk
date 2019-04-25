using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas.UI.Xaml;

namespace Final_Project_Resources_2
{

    //An implementor must contain a draw method
    public interface IDrawable
    {

        //The draw method
        void Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args, DrawInfo info);
    }
}
