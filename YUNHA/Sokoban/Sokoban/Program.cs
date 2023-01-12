using System.Numerics;

namespace Sokoban
{
    enum Direction
    {
        None,
        Left,
        Right,
        Up,
        Down
    };

    //class Object
    //{
    //    protected int _x;
    //    protected int _y;

    //    public int X
    //    {
    //        get
    //        {
    //            return _x;
    //        }
    //        set
    //        {
    //            _x = value;
    //        }
    //    }

    //    public int Y
    //    {
    //        get
    //        {
    //            return _y;
    //        }
    //        set
    //        {
    //            _y = value;
    //        }
    //    }
    //}

    class Player
    {
        private int _x = 1;
        private int _y = 1;
        private int _pushedBoxId;
        private string _icon = "K";
        private Direction _moveDirection;

        public Direction Dir
        {
            get
            {
                return _moveDirection;
            }
            set
            {
                _moveDirection = value;
            }
        }

        public int X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }

        public int Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }

        public int PushedBoxId
        {
            get
            {
                return _pushedBoxId;
            }
            set
            {
                _pushedBoxId = value;
            }
        }

        /// <summary>
        /// 플레이어를 그린다.
        /// </summary>
        public void Render()
        {
            Console.SetCursorPosition(_x, _y);
            Console.Write(_icon);
        }

        /// <summary>
        /// 플레이어를 이동시킨다.
        /// </summary>
        /// <param name="key"> 사용자가 입력한 키</param>
        public void Move(ConsoleKey key)
        {
            if (key == ConsoleKey.RightArrow)
            {
                // 오른쪽으로 이동
                _x = Math.Clamp(_x + 1, 0, 39);
                _moveDirection = Direction.Right;
            }
            // 아래쪽 키를 눌렀을 때
            if (key == ConsoleKey.DownArrow)
            {
                // 아래로 이동
                _y = Math.Clamp(_y + 1, 0, 19);
                _moveDirection = Direction.Down;
            }
            // 왼쪽 키를 눌렀을 때
            if (key == ConsoleKey.LeftArrow)
            {
                // 왼쪽으로 이동
                _x = Math.Clamp(_x - 1, 0, 39);
                _moveDirection = Direction.Left;
            }
            // 위쪽 키를 눌렀을 때
            if (key == ConsoleKey.UpArrow)
            {
                // 위로 이동
                _y = Math.Clamp(_y - 1, 0, 19);
                _moveDirection = Direction.Up;
            }
        }
    }

    class Box
    {
        private int _x;
        private int _y;
        private string _icon = "B";

        public Box(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public int X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }

        public int Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }

        public void Render()
        {
            Console.SetCursorPosition(_x, _y);
            Console.Write(_icon);
        }
    }
    
    class Wall
    {
        private int _x;
        private int _y;
        private string _icon = "X";

        public Wall(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public int X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }

        public int Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }


        public void Render()
        {
            Console.SetCursorPosition(_x, _y);
            Console.Write(_icon);
        }
    }

    class Goal
    {
        private int _x;
        private int _y;
        private string _nonGolaInIcon = "G";
        private string _goalInIcon = "O";

        public Goal(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public int X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }

        public int Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }

        public void Render(Box[] boxes, Goal[] goals)
        {
            for (int goalId = 0; goalId < 3; ++goalId)
            {
                for (int boxId = 0; boxId < 3; ++boxId)
                {
                    if (goals[goalId].X == boxes[boxId].X && goals[goalId].Y == boxes[boxId].Y)
                    {
                        Console.SetCursorPosition(goals[goalId].X, goals[goalId].Y);
                        Console.Write(_goalInIcon);
                        break;
                    }
                    else
                    {
                        Console.SetCursorPosition(goals[goalId].X, goals[goalId].Y);
                        Console.Write(_nonGolaInIcon);
                    }
                }
            }
        }
    }

    class Program
    {
        static void Main()
        {
            // 콘솔창 세팅.
            SetConsole();
            Random rand = new Random();
            // 맵 설정
            // 가로 40, 세로 20
            const int MAP_RIGHT_END = 39, MAP_DOWN_END = 19, MAP_LEFT_END = 0, MAP_UP_END = 0;
            const int GOAL_COUNT = 3;
            const int BOX_COUNT = GOAL_COUNT;
            const int WALL_COUNT = 3;

            // 플레이어 설정
            Player player = new Player();

            // 박스 설정
            Box[] boxes = new Box[BOX_COUNT];
            for(int i = 0; i < BOX_COUNT; ++i)
            {
                int x = rand.Next(MAP_LEFT_END, MAP_RIGHT_END);
                int y = rand.Next(MAP_UP_END, MAP_DOWN_END);
                if(x == 0 || y == 0)
                {
                    while(x != 0 && y != 0)
                    {
                        x = rand.Next(MAP_LEFT_END, MAP_RIGHT_END);
                        y = rand.Next(MAP_UP_END, MAP_DOWN_END);
                    }
                }
                boxes[i] = new Box(x, y);
            }

            // 벽 설정
            Wall[] walls = new Wall[WALL_COUNT];
            for(int i = 0; i < WALL_COUNT; ++i)
            {
                int x = rand.Next(MAP_LEFT_END, MAP_RIGHT_END);
                int y = rand.Next(MAP_UP_END, MAP_DOWN_END);
                walls[i] = new Wall(x, y);
            }

            // 골인 지점
            Goal[] goals = new Goal[GOAL_COUNT];
            for(int i = 0; i < GOAL_COUNT; ++i)
            {
                int x = rand.Next(MAP_LEFT_END, MAP_RIGHT_END);
                int y = rand.Next(MAP_UP_END, MAP_DOWN_END);
                goals[i] = new Goal(x, y);
            }

            ConsoleKey key;
            // 게임 루프 == 프레임(Frame)
            while (true)
            {
                // 이전 프레임을 지운다.
                Console.Clear();

                // 화면 렌더링.
                RenderObject();

                // 플레이어의 키를 입력 받는다.
                key = InputKey();
                
                // 입력값에 따라 데이터 업데이트.
                Update();
            }
            
            // 콘솔 세팅
            void SetConsole()
            {
                // 초기 세팅
                const string VERSION = "2023.01.09";
                Console.ResetColor();                                   // 컬러를 초기화한다.
                Console.CursorVisible = false;                          // 커서를 숨긴다.
                Console.Title = "Sokoban " + VERSION;                   // 타이틀을 설정한다.
                Console.BackgroundColor = ConsoleColor.DarkBlue;        // 배경색을 설정한다.
                Console.ForegroundColor = ConsoleColor.Yellow;          // 글꼴색을 설정한다.
                Console.Clear();                                        // 출력된 모든 내용을 지운다
            }

            void RenderObject()
            {
                // 플레이어 출력
                player.Render();

                // 벽 출력
                for(int i = 0; i < WALL_COUNT; ++i)
                {
                    walls[i].Render();
                }

                // 박스 출력
                for(int i = 0; i < BOX_COUNT; ++i)
                {
                    boxes[i].Render();
                }

                // 골인 출력
                for(int i = 0; i < GOAL_COUNT; ++i)
                {
                    goals[i].Render(boxes, goals);
                }
            }

            #region 키입력
            ConsoleKey InputKey()
            {
                ConsoleKey key = Console.ReadKey().Key;
                return key;
            }
            #endregion


            void Update()
            {
                // 플레이어 이동 처리.
                player.Move(key);

                // 플레이어 <=> 박스 상호작용.
                InteractBoxToPlayer();

                // 박스 <=> 박스 상호작용.
                InteractBoxToBox();

                // 플레이어 <=> 벽 상호작용.
                InteractPlayerToWall();

                // 박스 <=> 벽 상호작용.
                InteractBoxToWall();

                // 골 판별.
                JudgeGoal();
            }

            void InteractBoxToPlayer()
            {
                // 플레이어가 이동한 후
                for (int boxId = 0; boxId < BOX_COUNT; ++boxId)
                {
                    int boxX = boxes[boxId].X;
                    int boxY = boxes[boxId].Y;
                    if (player.X == boxX && player.Y == boxY)
                    {
                        player.PushedBoxId = boxId;
                        switch (player.Dir)
                        {
                            case Direction.Left:         // 플레이어가 왼쪽으로 이동중
                                boxX = Math.Max(MAP_LEFT_END, boxX - 1);
                                player.X = boxX + 1;
                                break;
                            case Direction.Right:         // 플레이어가 오른쪽으로 이동 중
                                boxX = Math.Min(boxX + 1, MAP_RIGHT_END);
                                player.X = boxX - 1;
                                break;
                            case Direction.Up:         // 플레이어가 위로 이동 중
                                boxY = Math.Max(MAP_UP_END, boxY - 1);
                                player.Y = boxY + 1;
                                break;
                            case Direction.Down:         // 플레이어가 아래로 이동 중
                                boxY = Math.Min(MAP_DOWN_END, boxY + 1);
                                player.Y = boxY - 1;
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다. {player.Dir}");
                                return;
                        }
                        boxes[boxId].X = boxX;
                        boxes[boxId].Y = boxY;
                        break;
                    }

                }
            }

            void InteractBoxToBox()
            {
                for (int collisionBoxId = 0; collisionBoxId < BOX_COUNT; collisionBoxId++)
                {
                    int pushedBoxX = boxes[player.PushedBoxId].X;
                    int pushedBoxY = boxes[player.PushedBoxId].Y;
                    if (player.PushedBoxId != collisionBoxId)
                    {
                        if (boxes[player.PushedBoxId].X == boxes[collisionBoxId].X && boxes[player.PushedBoxId].Y == boxes[collisionBoxId].Y)
                        {
                            int collisionBoxX = boxes[collisionBoxId].X;
                            int collisionBoxY = boxes[collisionBoxId].Y;
                            switch (player.Dir)
                            {
                                case Direction.Left:
                                    pushedBoxX = collisionBoxX + 1;
                                    player.X = pushedBoxX + 1;
                                    break;
                                case Direction.Right:
                                    pushedBoxX = collisionBoxX - 1;
                                    player.X = pushedBoxX - 1;
                                    break;
                                case Direction.Up:
                                    pushedBoxY = collisionBoxY + 1;
                                    player.Y = pushedBoxY + 1;
                                    break;
                                case Direction.Down:
                                    pushedBoxY = collisionBoxY - 1;
                                    player.Y = pushedBoxY - 1;
                                    break;
                                default:
                                    return;
                            }
                        }
                        boxes[player.PushedBoxId].X = pushedBoxX;
                        boxes[player.PushedBoxId].Y = pushedBoxY;
                    }

                }
            }

            void InteractPlayerToWall()
            {
                // 벽 상호작용
                for (int wallId = 0; wallId < WALL_COUNT; ++wallId)
                {
                    if (player.X == walls[wallId].X && player.Y == walls[wallId].Y)
                    {
                        switch (player.Dir)
                        {
                            case Direction.Left:
                                player.X = walls[wallId].X + 1;
                                break;
                            case Direction.Right:
                                player.X = walls[wallId].X - 1;
                                break;
                            case Direction.Up:
                                player.Y = walls[wallId].Y + 1;
                                break;
                            case Direction.Down:
                                player.Y = walls[wallId].Y - 1;
                                break;
                            default:
                                return;
                        }
                    }
                }
            }

            void InteractBoxToWall()
            {
                for (int boxId = 0; boxId < BOX_COUNT; ++boxId)
                {
                    for (int wallId = 0; wallId < WALL_COUNT; ++wallId)
                    {
                        if (boxes[boxId].X == walls[wallId].X && boxes[boxId].Y == walls[wallId].Y)
                        {
                            switch (player.Dir)
                            {
                                case Direction.Left:
                                    boxes[boxId].X = walls[wallId].X + 1;
                                    player.X = boxes[boxId].X + 1;
                                    break;
                                case Direction.Right:
                                    boxes[boxId].X = walls[wallId].X - 1;
                                    player.X = boxes[boxId].X - 1;
                                    break;
                                case Direction.Up:
                                    boxes[boxId].Y = walls[wallId].Y + 1;
                                    player.Y = boxes[boxId].Y + 1;
                                    break;
                                case Direction.Down:
                                    boxes[boxId].Y = walls[wallId].Y - 1;
                                    player.Y = boxes[boxId].Y - 1;
                                    break;
                                default:
                                    return;
                            }
                        }
                    }
                }
            }

            void JudgeGoal()
            {
                bool[] goaledArr = new bool[GOAL_COUNT];
                bool isGameClear = false;

                for (int goalId = 0; goalId < GOAL_COUNT; ++goalId)
                {
                    for (int boxId = 0; boxId < BOX_COUNT; ++boxId)
                    {
                        if (goals[goalId].X == boxes[boxId].X && goals[goalId].Y == boxes[boxId].Y)
                        {
                            goaledArr[goalId] = true;
                            break;
                        }
                    }
                }

                for (int goalId = 0; goalId < GOAL_COUNT; ++goalId)
                {
                    if (goaledArr[goalId] == false)
                    {
                        isGameClear = false;
                        break;
                    }
                    else
                    {
                        isGameClear = true;
                    }
                }

                if (isGameClear == true)
                {
                    Console.Clear();
                    Console.WriteLine("GAME CLEAR!");
                    Environment.Exit(0);
                }
            }

        }
    }
}
