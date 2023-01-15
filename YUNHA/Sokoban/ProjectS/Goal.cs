using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectS
{
    public struct Goal
    {
        public Goal(int x, int y)
        {
            X = x;
            Y = y;
            isBoxOnGoal = false;
        }

        public int X;               // 골 지점 x좌표.
        public int Y;               // 골 지점 y좌표.
        public bool isBoxOnGoal;    // 골 지점 박스가 있는지 체크하는 bool값.

    }
}
