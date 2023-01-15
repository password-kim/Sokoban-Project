using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectS
{
    public struct Wall
    {
        public Wall(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X; //벽의 X좌표.
        public int Y; //벽의 Y좌표.
    }
}
