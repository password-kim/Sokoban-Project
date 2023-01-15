using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ProjectS
{

    public struct Player
    {
        // 플레이어의 이동방향.
        public enum Direction
        {
            None,
            Left,
            Right,
            Up,
            Down
        }

        public int X;                       // 플레이어의 x좌표.
        public int Y;                       // 플레이어의 y좌표.
        public Direction MoveDirection;     // 플레이어의 이동방향.
        public int PushedBoxIndex;          // 플레이어가 밀고있는 박스ID.
        public string name;                 // 플레이어의 이름.
        public int lifeCount;               // 플레이어의 생명.
        public bool[] isExistLIFE;          // 플레이어의 생명을 체크하는 bool배열.


        /// <summary>
        /// 플레이어의 좌표를 이동시켜줌.
        /// </summary>
        /// <param name="key">유저가 입력한 key</param>
        public void MovePlayer(ConsoleKey key)
        {
            if (key == ConsoleKey.LeftArrow)
            {
                X = Math.Clamp(X - 1, Game.MIN_X, Game.MAX_X);
                MoveDirection = Player.Direction.Left;
            }

            if (key == ConsoleKey.RightArrow)
            {
                X = Math.Clamp(X + 1, Game.MIN_X, Game.MAX_X);
                MoveDirection = Player.Direction.Right;
            }

            if (key == ConsoleKey.UpArrow)
            {
                Y = Math.Clamp(Y - 1, Game.MIN_Y, Game.MAX_Y);
                MoveDirection = Player.Direction.Up;
            }

            if (key == ConsoleKey.DownArrow)
            {
                Y = Math.Clamp(Y + 1, Game.MIN_Y, Game.MAX_Y);
                MoveDirection = Player.Direction.Down;
            }
        }
    }
}
