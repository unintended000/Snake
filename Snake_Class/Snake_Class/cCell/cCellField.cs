using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Class
{
    public class cCellField : cCell
    {
        public bool Type { get; set; }
        public cCellField(byte xx, byte yy) : base(xx, yy)
        {
            CoordX = xx;
            CoordY = yy;
        }
    }
}
