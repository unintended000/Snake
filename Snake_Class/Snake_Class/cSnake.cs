using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Class
{
    public class cSnake
    {
        public cSnake()
        {
            SnakeCellList = new List<cCellSnake>();
        }
        public List<cCellSnake> SnakeCellList
        { get; private set; }

       
    }
}
