using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_Resources_2
{

    //Implementors must provide an offset method
    interface DrawingPositionModifier
    {

        //Retrieve the offset position
        Vector2 getOffset();
        
    }
}
