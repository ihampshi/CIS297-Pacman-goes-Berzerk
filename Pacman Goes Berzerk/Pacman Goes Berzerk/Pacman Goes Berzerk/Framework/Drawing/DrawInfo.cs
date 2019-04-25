using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_Resources_2
{

    //Conditions and arguments involved in drawing drawable objects
    public class DrawInfo
    {

        //The offset that the object should draw at
        public Vector2 offset;

        //Whether debug drawables should be drawn
        public bool debug;

        //Constructor
        public DrawInfo(Vector2 offset, bool debug)
        {

            //Initialize
            this.offset = offset;
            this.debug = debug;
        }
    }
}
