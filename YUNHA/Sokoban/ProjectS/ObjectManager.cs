using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectS
{
    /// <summary>
    /// 오브젝트 생성/관리 해주는 매니저
    /// </summary>
    public struct ObjectManager
    {
        public ObjectManager()
        {

        }

        Random rand = new Random();

        Player player = new Player();
        Box[] boxes = new Box[Game.BOX_COUNT];
        Goal[] goals = new Goal[Game.GOAL_COUNT];
        Wall[] walls = new Wall[Game.WALL_COUNT];
        Trap[] traps = new Trap[Game.TRAP_COUNT];
        Game game = new Game();

        /// <summary>
        /// 플레이어 생성해주는 함수.
        /// </summary>
        /// <returns>Player구조체</returns>
        public Player SetPlayer()
        {
            player.X = 1;
            player.Y = 1;
            player.MoveDirection = Player.Direction.None;
            player.PushedBoxIndex = 0;
            game.CreateNewCharacter(ref player);
            player.lifeCount = Game.MAX_LIFE_COUNT;
            player.isExistLIFE = new bool[Game.MAX_LIFE_COUNT];
            for(int i = 0; i < Game.MAX_LIFE_COUNT; ++i)
            {
                player.isExistLIFE[i] = true;
            }

            return player;
        }

        /// <summary>
        /// 박스들을 생성해주는 함수.
        /// </summary>
        /// <returns>박스배열</returns>
        public Box[] SetBoxes()
        {
            for(int boxId = 0; boxId < Game.BOX_COUNT; ++boxId)
            {
                int x = rand.Next(Game.MIN_X + 2, Game.MAX_X);
                int y = rand.Next(Game.MIN_Y + 2, Game.MAX_Y);
                while (game.isTileHasObject[y, x] != false)
                {
                    x = rand.Next(Game.MIN_X + 2, Game.MAX_X);
                    y = rand.Next(Game.MIN_Y + 2, Game.MAX_Y);
                }
                boxes[boxId] = new Box(x, y);
                game.isTileHasObject[y, x] = true;
            }
            return boxes;
        }

        /// <summary>
        /// 벽들을 생성해주는 함수.
        /// </summary>
        /// <returns>벽배열</returns>
        public Wall[] SetWalls()
        {
            for(int wallId = 0; wallId < Game.WALL_COUNT; ++wallId)
            {
                int x = rand.Next(Game.MIN_X + 2, Game.MAX_X);
                int y = rand.Next(Game.MIN_Y + 2, Game.MAX_Y);
                while (game.isTileHasObject[y, x] != false)
                {
                    x = rand.Next(Game.MIN_X + 2, Game.MAX_X);
                    y = rand.Next(Game.MIN_Y + 2, Game.MAX_Y);
                }
                walls[wallId] = new Wall(x, y);
                game.isTileHasObject[y, x] = true;
            }
            return walls;
        }

        /// <summary>
        /// 골 목록을 생성해주는 함수.
        /// </summary>
        /// <returns>골배열</returns>
        public Goal[] SetGoals()
        {
            for (int goalId = 0; goalId < Game.GOAL_COUNT; ++goalId)
            {
                int x = rand.Next(Game.MIN_X + 2, Game.MAX_X);
                int y = rand.Next(Game.MIN_Y + 2, Game.MAX_Y);
                while (game.isTileHasObject[y, x] != false)
                {
                    x = rand.Next(Game.MIN_X + 2, Game.MAX_X);
                    y = rand.Next(Game.MIN_Y + 2, Game.MAX_Y);
                }
                goals[goalId] = new Goal(x, y);
                game.isTileHasObject[y, x] = true;
            }
            return goals;
        }

        /// <summary>
        /// 함정을 생성해주는 함수.
        /// </summary>
        /// <returns>함정배열</returns>
        public Trap[] SetTraps()
        {
            for(int trapId = 0; trapId < Game.TRAP_COUNT; ++trapId)
            {
                int x = rand.Next(Game.MIN_X + 2, Game.MAX_X);
                int y = rand.Next(Game.MIN_Y + 2, Game.MAX_Y);
                while (game.isTileHasObject[y, x] != false)
                {
                    x = rand.Next(Game.MIN_X + 2, Game.MAX_X);
                    y = rand.Next(Game.MIN_Y + 2, Game.MAX_Y);
                }
                traps[trapId] = new Trap(x, y);
                game.isTileHasObject[y, x] = true;
            }
            return traps;
        }
    }
}
