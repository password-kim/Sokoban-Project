using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectS
{
    public struct Box
    {
        public Box(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X; // 박스의 x좌표.
        public int Y; // 박스의 y좌표.
    }
}
