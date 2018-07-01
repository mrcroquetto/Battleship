using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipCore
{
    public class Ship
    {
        public int Size { get; set; }
        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }
        public Enumerations.ORIENTATION Orientation { get; set; }

        public Ship(int size, int cX, int cY, Enumerations.ORIENTATION orientation)
        {
            Size = size;
            CoordinateX = cX;
            CoordinateY = cY;
            Orientation = orientation;
        }
    }
}
