using System.CodeDom.Compiler;
using static ProjectS.Player;

namespace ProjectS
{
    internal class _Sokoban
    {
        static void Main()
        {
            // 매니저 생성.
            Game game = new Game();
            ObjectManager objManager = new ObjectManager();
            InteractManager interactManager = new InteractManager();
            RenderManager renderer = new RenderManager();

            // 콘솔창 초기화 & 시작화면 출력
            game.Initialize();
            game.Title();
            
            Reset_Game:

            // 오브젝트 셋팅.
            Player player = objManager.SetPlayer();

            Wall[] walls = objManager.SetWalls();

            Box[] boxes = objManager.SetBoxes();

            Goal[] goals = objManager.SetGoals();

            Trap[] traps = objManager.SetTraps();

            // 게임 루프
            while (true)
            {
                Console.Clear();

                // 테두리 출력.
                renderer.RenderWall();
                
                // 오브젝트 렌더.
                renderer.Render(player, boxes, goals, walls, traps);

                // 설명서 출력.
                renderer.RenderManual(ref player);

                // 키입력을 받는다.
                ConsoleKey key = ConsoleKey.NoName;
                if(Console.KeyAvailable)
                {
                    key = Console.ReadKey().Key;
                }

                // R키를 누르면 캐릭터 생성위치로 돌아간다.
                if(key == ConsoleKey.R)
                {
                    goto Reset_Game;

                }

                // 게임 Update.
                Update(key);

                
                Thread.Sleep(1);
            }

            // Update할 것들을 처리해주는 함수.
            void Update(ConsoleKey key)
            {
                //MovePlayer(key, ref player);
                player.MovePlayer(key);

                // 박스 이동 처리
                interactManager.InteractPlayerToBox(ref player, boxes);

                //플레이어와 벽의 충돌처리
                interactManager.InteractPlayerToWall(ref player, walls);

                // 박스와 벽의 충돌처리
                interactManager.InteractBoxToWall(ref player, boxes, walls);

                // 박스끼리 충돌 처리
                interactManager.InteractBoxToBox(ref player, boxes);

                // 플레이어와 함정의 상호작용
                interactManager.InteractPlayerToTrap(ref player, traps);

                // 클리어 여부 확인.
                interactManager.JudgeClear(goals, boxes);

                // 게임오버 여부 확인.
                interactManager.JudgeGameOver(ref player);
            }
        }
    }
}