using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static ProjectS.Player;

namespace ProjectS
{
    /// <summary>
    /// 오브젝트들간의 상호작용처리해주는 매니저
    /// </summary>
    public struct InteractManager
    {
        public InteractManager()
        {

        }

        RenderManager renderer = new RenderManager();

        /// <summary>
        /// 플레이어와 벽 충돌시 처리해주는 함수.
        /// </summary>
        /// <param name="player">Player 객체</param>
        /// <param name="walls">Wall이 담긴 배열</param>
        public void InteractPlayerToWall(ref Player player, Wall[] walls)
        {
            for (int wallId = 0; wallId < Game.WALL_COUNT; ++wallId)
            {
                if (false == IsCollided(player.X, player.Y, walls[wallId].X, walls[wallId].Y))
                {
                    continue;
                }

                switch (player.MoveDirection)
                {
                    case Player.Direction.Left:
                        player.X = walls[wallId].X + 1;
                        break;
                    case Player.Direction.Right:
                        player.X = walls[wallId].X - 1;
                        break;
                    case Player.Direction.Up:
                        player.Y = walls[wallId].Y + 1;
                        break;
                    case Player.Direction.Down:
                        player.Y = walls[wallId].Y - 1;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine($"[ERROR] 플레이어 이동 방향 데이터가 오류입니다. : {player.MoveDirection}");
                        return;
                }
                break;
            }
        }

        /// <summary>
        /// 플레이어와 박스 충돌시 처리해주는 함수.
        /// </summary>
        /// <param name="player">Player 객체</param>
        /// <param name="boxes">Box가 담긴 배열</param>
        public void InteractPlayerToBox(ref Player player, Box[] boxes)
        {
            for (int i = 0; i < Game.BOX_COUNT; ++i)
            {
                if (false == IsCollided(player.X, player.Y, boxes[i].X, boxes[i].Y))
                {
                    continue;
                }

                switch (player.MoveDirection)
                {
                    case Player.Direction.Left:
                        boxes[i].X = Math.Clamp(boxes[i].X - 1, Game.MIN_X, Game.MAX_X);
                        player.X = boxes[i].X + 1;
                        break;
                    case Player.Direction.Right:
                        boxes[i].X = Math.Clamp(boxes[i].X + 1, Game.MIN_X, Game.MAX_X);
                        player.X = boxes[i].X - 1;
                        break;
                    case Player.Direction.Up:
                        boxes[i].Y = Math.Clamp(boxes[i].Y - 1, Game.MIN_Y, Game.MAX_Y);
                        player.Y = boxes[i].Y + 1;
                        break;
                    case Player.Direction.Down:
                        boxes[i].Y = Math.Clamp(boxes[i].Y + 1, Game.MIN_Y, Game.MAX_Y);
                        player.Y = boxes[i].Y - 1;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine($"[ERROR] 플레이어의 이동 방향 데이터가 오류입니다. : {player.MoveDirection}");
                        return;
                }
                player.PushedBoxIndex = i;
                break;
            }
        }

        /// <summary>
        /// 박스와 벽 충돌시 처리해주는 함수,
        /// </summary>
        /// <param name="player">Player 객체</param>
        /// <param name="boxes">Box가 담긴 배열</param>
        /// <param name="walls">Wall이 담긴 배열</param>
        public void InteractBoxToWall(ref Player player, Box[] boxes, Wall[] walls)
        {
            for (int wallId = 0; wallId < Game.WALL_COUNT; ++wallId)
            {
                if (false == IsCollided(boxes[player.PushedBoxIndex].X, boxes[player.PushedBoxIndex].Y, walls[wallId].X, walls[wallId].Y))
                {
                    continue;
                }

                switch (player.MoveDirection)
                {
                    case Direction.Left:
                        boxes[player.PushedBoxIndex].X = walls[wallId].X + 1;
                        player.X = boxes[player.PushedBoxIndex].X + 1;
                        break;
                    case Direction.Right:
                        boxes[player.PushedBoxIndex].X = walls[wallId].X - 1;
                        player.X = boxes[player.PushedBoxIndex].X - 1;
                        break;
                    case Direction.Up:
                        boxes[player.PushedBoxIndex].Y = walls[wallId].Y + 1;
                        player.Y = boxes[player.PushedBoxIndex].Y + 1;
                        break;
                    case Direction.Down:
                        boxes[player.PushedBoxIndex].Y = walls[wallId].Y - 1;
                        player.Y = boxes[player.PushedBoxIndex].Y - 1;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine($"[Error] 플레이어 이동 방향 데이터가 오류입니다. : {player.MoveDirection}");

                        return;
                }

                break;
            }
        }


        /// <summary>
        /// 박스끼리의 충돌을 처리해주는 함수. 
        /// </summary>
        /// <param name="player">Player 객체</param>
        /// <param name="boxes">Box가 담긴 배열</param>
        public void InteractBoxToBox(ref Player player, Box[] boxes)
        {
            for (int collidedBoxId = 0; collidedBoxId < Game.BOX_COUNT; ++collidedBoxId)
            {
                // 같은 박스라면 처리할 필요가 X
                if (player.PushedBoxIndex == collidedBoxId)
                {
                    continue;
                }

                if (false == IsCollided(boxes[player.PushedBoxIndex].X, boxes[player.PushedBoxIndex].Y, boxes[collidedBoxId].X, boxes[collidedBoxId].Y))
                {
                    continue;
                }

                switch (player.MoveDirection)
                {
                    case Direction.Left:
                        boxes[player.PushedBoxIndex].X = boxes[collidedBoxId].X + 1;
                        player.X = boxes[player.PushedBoxIndex].X + 1;

                        break;
                    case Direction.Right:
                        boxes[player.PushedBoxIndex].X = boxes[collidedBoxId].X - 1;
                        player.X = boxes[player.PushedBoxIndex].X - 1;

                        break;
                    case Direction.Up:
                        boxes[player.PushedBoxIndex].Y = boxes[collidedBoxId].Y + 1;
                        player.Y = boxes[player.PushedBoxIndex].Y + 1;

                        break;
                    case Direction.Down:
                        boxes[player.PushedBoxIndex].Y = boxes[collidedBoxId].Y - 1;
                        player.Y = boxes[player.PushedBoxIndex].Y - 1;

                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine($"[Error] 플레이어 이동 방향 데이터가 오류입니다. : {player.MoveDirection}");

                        return;
                }

                break;
            }
        }

        /// <summary>
        /// 플레이어가 함정을 밟았을때 처리해주는 함수.
        /// </summary>
        /// <param name="player">Player 객체</param>
        /// <param name="traps">Trap이 담긴 배열 </param>
        public void InteractPlayerToTrap(ref Player player, Trap[] traps)
        {
            for(int trapId = 0; trapId < Game.TRAP_COUNT; ++trapId)
            {
                if(false == IsCollided(player.X, player.Y, traps[trapId].X, traps[trapId].Y))
                {
                    continue;
                }
                if (traps[trapId].isTrapExist)
                {
                    player.lifeCount--;
                    traps[trapId].isTrapExist = false;
                }
                else
                {
                    continue;
                }
                for(int i = 0; i < Game.MAX_LIFE_COUNT; ++i)
                {
                    if(i < player.lifeCount)
                    {
                        player.isExistLIFE[i] = true;
                    }
                    else
                    {
                        player.isExistLIFE[i] = false;
                    }
                }
            }
        }

        /// <summary>
        /// 게임 클리어 여부를 확인해주는 함수.
        /// </summary>
        /// <param name="goals">Goal이 담긴 배열</param>
        /// <param name="boxes">Box가 담긴 배열</param>
        public void JudgeClear(Goal[] goals, Box[] boxes)
        {
            // 박스와 골의 처리
            int boxOnGoalCount = 0;

            // 골 지점에 박스에 존재하냐?
            for (int goalId = 0; goalId < Game.BOX_COUNT; ++goalId) // 모든 골 지점에 대해서
            {
                // 현재 박스가 골 위에 올라와 있는지 체크한다.
                goals[goalId].isBoxOnGoal = false; // 없을 가능성이 높기 때문에 false로 초기화 한다.

                for (int boxId = 0; boxId < Game.GOAL_COUNT; ++boxId) // 모든 박스에 대해서
                {
                    // 박스가 골 지점 위에 있는지 확인한다.
                    if (IsCollided(boxes[boxId].X, boxes[boxId].Y, goals[goalId].X, goals[goalId].Y))
                    {
                        ++boxOnGoalCount;

                        goals[goalId].isBoxOnGoal = true; // 박스가 골 위에 있다는 사실을 저장해둔다.

                        break;
                    }
                }
            }

            // 모든 골 지점에 박스가 올라와 있다면?
            if (boxOnGoalCount == Game.GOAL_COUNT)
            {
                renderer.PrintGameClear();

                Environment.Exit(0);
            }
        }

        /// <summary>
        /// 게임 오버 여부를 확인해주는 함수.
        /// </summary>
        /// <param name="player">Player 객체</param>
        public void JudgeGameOver(ref Player player)
        {
            bool isGameOver = false;

            for (int i = 0; i < Game.MAX_LIFE_COUNT; ++i)
            {
                if (player.isExistLIFE[i])
                {
                    break;
                }
                else
                {
                    isGameOver = true;
                }
            }

            if(isGameOver)
            {
                renderer.PrintGameOver();
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// 물체끼리 충돌했는지 판별해주는 함수.
        /// </summary>
        /// <param name="x1">1번째 객체의 x좌표</param>
        /// <param name="y1">1번째 객체의 y좌표</param>
        /// <param name="x2">2번째 객체의 x좌표</param>
        /// <param name="y2">2번째 객체의 y좌표</param>
        /// <returns></returns>
        public bool IsCollided(int x1, int y1, int x2, int y2)
        {
            if (x1 == x2 && y1 == y2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
