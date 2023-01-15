using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectS
{
    public struct Trap
    {
        public Trap(int x, int y)
        {
            X = x;
            Y = y;
            isTrapExist = true;
        }

        public int X;               // 함정의 x좌표.
        public int Y;               // 함정의 y좌표.
        public bool isTrapExist;    // 함정의 발동 유무.
    }
}
