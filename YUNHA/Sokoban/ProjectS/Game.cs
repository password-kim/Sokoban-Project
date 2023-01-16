using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ProjectS
{
    public struct Game
    {
        public Game()
        {

        }

        // 게임 구성요소.
        public const int MAX_LIFE_COUNT = 5;
        public const int GOAL_COUNT = 5;
        public const int BOX_COUNT = GOAL_COUNT;
        public const int WALL_COUNT = 15;
        public const int TRAP_COUNT = 20;

        // 맵의 크기.
        public const int MIN_X = 1;
        public const int MIN_Y = 1;
        public const int MAX_X = 38;
        public const int MAX_Y = 18;

        // 각 타일에 오브젝트가 있는지 없는지 확인용 bool배열.
        public bool[,] isTileHasObject = new bool[MAX_Y + 2, MAX_X + 2];
        
        // 타이틀을 그려주기위한 renderer
        RenderManager renderer = new RenderManager();

        /// <summary>
        /// 콘솔창을 초기화해주는 함수.
        /// </summary>
        public void Initialize()
        {
            Console.ResetColor();
            Console.CursorVisible = false;
            Console.Title = "Project S";
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Clear();
        }

        /// <summary>
        /// 게임화면 구성을 도와주는 함수.
        /// </summary>
        public void Title()
        {
            // 게임 타이틀 화면을 출력
            renderer.PrintTitle();

            // 게임을 시작할지 종료할지 입력받는 부분.
            string playerInput = Console.ReadLine();

            // 입력에 따라 게임을 진행.
            switch (playerInput)
            {
                case "1":
                    break;
                case "2":
                    renderer.PrintExit();
                    Environment.Exit(0);
                    break;
                default:
                    Console.Clear();
                    Title();
                    break;
            }
        }

        /// <summary>
        /// 플레이어의 이름을 받아주는 함수.
        /// </summary>
        /// <param name="player"> 플레이어 </param>
        public void CreateNewCharacter(ref Player player)
        {
            Console.Clear();
            Console.WriteLine("당신의 이름은 무엇입니까?");
            Console.Write(">>");
            player.name = Console.ReadLine();
        }

    }
}
