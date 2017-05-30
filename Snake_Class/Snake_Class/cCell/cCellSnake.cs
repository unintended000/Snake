using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Class
{
    public class cCellSnake : cCell
    {
        public cCellSnake(byte xx, byte yy) : base(xx, yy)
        {
            CoordX = xx;
            CoordY = yy;
        }
    }
}
