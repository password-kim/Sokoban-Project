using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProjectS
{
    /// <summary>
    /// 콘솔창에 필요한 것들을 그려주는 매니저
    /// </summary>
    public struct RenderManager
    {
        public RenderManager()
        {

        }

        /// <summary>
        /// 테두리를 그려주는 함수.
        /// </summary>
        public void RenderWall()
        {
            for (int y = 0; y <= Game.MAX_Y + 1; ++y)
            {
                for (int x = 0; x <= Game.MAX_X + 1; ++x)
                {
                    if (y == Game.MIN_Y - 1)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write("П");

                    }
                    else if (y == Game.MAX_Y + 1)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write("П");
                    }
                    else
                    {
                        if (x == Game.MIN_X - 1 || x == Game.MAX_X + 1)
                        {
                            Console.SetCursorPosition(x, y);
                            Console.Write("П");
                        }
                    }

                }
            }
        }

        /// <summary>
        /// 게임의 오브젝트를 그려주는 함수.
        /// </summary>
        /// <param name="player">플레이어</param>
        /// <param name="boxes">박스배열</param>
        /// <param name="goals">골인지점배열</param>
        /// <param name="walls">벽배열</param>
        /// <param name="traps">함정배열</param>
        public void Render(Player player, Box[] boxes, Goal[] goals, Wall[] walls, Trap[] traps)
        {
            // 한번 밟은 함정을 그려준다.
            for (int trapId = 0; trapId < Game.TRAP_COUNT; ++trapId)
            {
                // 유저용 함정렌더
                if (traps[trapId].isTrapExist == false)
                {
                    RenderObject(traps[trapId].X, traps[trapId].Y, "▲");
                }

                // 개발자용 함정렌더
                //RenderObject(traps[trapId].X, traps[trapId].Y, "▲");

            }

            // 플레이어를 그린다.
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            RenderObject(player.X, player.Y, "д");

            // 박스를 그린다.
            for (int boxId = 0; boxId < Game.BOX_COUNT; ++boxId)
            {
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow;
                RenderObject(boxes[boxId].X, boxes[boxId].Y, "■");
            }

            //골을 그린다.
            for (int goalId = 0; goalId < Game.GOAL_COUNT; ++goalId)
            {
                string goalShape = goals[goalId].isBoxOnGoal ? "▣" : "□";
                if(goalShape == "□")
                {
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    RenderObject(goals[goalId].X, goals[goalId].Y, goalShape);
                }
                else
                {
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    RenderObject(goals[goalId].X, goals[goalId].Y, goalShape);
                }
            }

            // 벽을 그린다.
            for (int wallId = 0; wallId < Game.WALL_COUNT; ++wallId)
            {
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow;
                RenderObject(walls[wallId].X, walls[wallId].Y, "П");
            }

        }

        /// <summary>
        /// 오브젝트의 종류에 따라 렌더해주는 함수.
        /// </summary>
        /// <param name="x">오브젝트의 x좌표</param>
        /// <param name="y">오브젝트의 y좌표</param>
        /// <param name="obj">오브젝트의 이미지</param>
        public void RenderObject(int x, int y, string obj)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(obj);
        }

        /// <summary>
        /// 타이틀을 그려주는 함수
        /// </summary>
        public void PrintTitle()
        {
            Console.WriteLine("     ####    ####    ####   #####    ###   #####       ####       ");
            Console.WriteLine("     #   #   #   #     #    #       #        #        #           ");
            Console.WriteLine("     ####    ####      #    ####    #        #   ###  #####       ");
            Console.WriteLine("     #       #   #   # #    #       #        #            #       ");
            Console.WriteLine("     #       #   #    #     #####    ###     #        ####        ");
            Console.WriteLine("\n\n\n");
            Console.WriteLine("     !!숫자를 누르고 엔터를 눌러주세요!!");
            Console.WriteLine("     [1] 게임시작");
            Console.WriteLine("     [2] 게임종료");
            Console.Write("     >> ");
        }

        /// <summary>
        /// 게임종료했을때 나오는 화면을 그려주는 함수.
        /// </summary>
        public void PrintExit()
        {
            Console.Clear();
            Console.WriteLine("    ####   #####   #####     #   #   ####    #    #   ##         ");
            Console.WriteLine("   #       #       #          # #   #    #   #    #   ##         ");
            Console.WriteLine("   #####   ####    ####        #    #    #   #    #   ##         ");
            Console.WriteLine("       #   #       #           #    #    #   #    #              ");
            Console.WriteLine("   ####    #####   #####       #     ####     ####    ##         ");
        }

        /// <summary>
        /// 게임설명해주는 부분을 그려주는 함수.
        /// </summary>
        /// <param name="player"></param>
        public void RenderManual(ref Player player)
        {
            Console.SetCursorPosition(Game.MAX_X + 10, 0);
            Console.Write("===== 'Player Info' =====");
            Console.SetCursorPosition(Game.MAX_X + 10, 1);
            Console.Write("ID   : " + player.name);
            Console.SetCursorPosition(Game.MAX_X + 10, 2);
            Console.Write("LIFE : ");
            for (int lifeId = 0; lifeId < Game.MAX_LIFE_COUNT; ++lifeId)
            {
                if (player.isExistLIFE[lifeId])
                {
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("♥");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                else
                {
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("♡");
                }
            }
            Console.SetCursorPosition(Game.MAX_X + 10, 4);
            Console.Write("===== 'How To Control' =====");
            Console.SetCursorPosition(Game.MAX_X + 10, 5);
            Console.WriteLine("Move : ← → ↑ ↓");
            Console.SetCursorPosition(Game.MAX_X + 10, 6);
            Console.Write("Reset : R");
            Console.SetCursorPosition(Game.MAX_X + 10, 8);
            Console.Write("===== 'How To Clear' =====");
            Console.SetCursorPosition(Game.MAX_X + 10, 9);
            Console.Write("1. 보이지 않는 함정을 피하세요.");
            Console.SetCursorPosition(Game.MAX_X + 10, 10);
            Console.Write("2. 한번 밟은 함정은 표시됩니다.");
            Console.SetCursorPosition(Game.MAX_X + 10, 11);
            Console.Write("3. 박스들을 골지점으로 모두 옮겨주세요.");
            Console.SetCursorPosition(Game.MAX_X + 10, 13);
            Console.Write("===== 'Object Info' =====");
            Console.SetCursorPosition(Game.MAX_X + 10, 14);
            Console.Write("д : 플레이어");
            Console.SetCursorPosition(Game.MAX_X + 10, 15);
            Console.Write("П : 벽");
            Console.SetCursorPosition(Game.MAX_X + 10, 16);
            Console.Write("□ : 골지점");
            Console.SetCursorPosition(Game.MAX_X + 10, 17);
            Console.Write("■ : 박스");
            Console.SetCursorPosition(Game.MAX_X + 10, 18);
            Console.Write("▲ : 밟은 함정");

        }

        /// <summary>
        /// 게임클리어 화면을 그려주는 함수.
        /// </summary>
        public void PrintGameClear()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n");
            Console.WriteLine("          ###   #      #####     #     ####    ##       ");
            Console.WriteLine("         #      #      #        # #    #   #   ##       ");
            Console.WriteLine("         #      #      #####   #####   ####    ##       ");
            Console.WriteLine("         #      #      #       #   #   #   #            ");
            Console.WriteLine("          ###   ####   #####   #   #   #   #   ##       ");
        }

        /// <summary>
        /// 게임오버시 화면을 그려주는 함수.
        /// </summary>
        public void PrintGameOver()
        {
            Console.Clear();
            Console.WriteLine("\n\n");
            // GAME
            Console.WriteLine("     #####     #     ##   ##   #####     ");
            Console.WriteLine("    #         # #    # # # #   #         ");
            Console.WriteLine("    #  ###   #####   #  #  #   ####      ");
            Console.WriteLine("    #    #   #   #   #     #   #         ");
            Console.WriteLine("     #####   #   #   #     #   #####     ");
            Console.WriteLine("");
            Console.WriteLine("     ####    #   #   #####   ####        ");
            Console.WriteLine("    #    #   #   #   #       #   #       ");
            Console.WriteLine("    #    #   #   #   ####    ####        ");
            Console.WriteLine("    #    #    # #    #       #   #       ");
            Console.WriteLine("     ####      #     #####   #   # # #   ");
        }

    }
}
