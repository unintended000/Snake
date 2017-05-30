using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Class
{
    public class cApple : cCell
    {
        /// <summary>
        /// Яблоко
        /// </summary>
        /// <param name="xx">Координата Х</param>
        /// <param name="yy">Координата Y</param>
        public cApple(byte xx, byte yy) : base(xx, yy)
        {
            CoordX = xx;
            CoordY = yy;
        }
        /// <summary>
        /// Тип яблока
        /// </summary>
        public type Type
        { get; set; }
     
    }
     public enum type
    {
        Point, //+10
        Life,  //+ Жизнь
        Surprise, //Сюрприз
        BigPoint //+50
    }




}
