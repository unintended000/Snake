using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Class
{
    public class  cCell
    {
        public byte CoordX { get; set; }
        public byte CoordY { get; set; }
        public cCell(byte xx, byte yy)
        {
           CoordX = xx;
           CoordY = yy;
        }
    }
}
