namespace Sameple
{
    internal class Program
    {
        const int MapWidth = 30; // 맵의 가로 길이
        const int MapHeight = 30; // 맵의 세로 길이
        const int InfoWidth = 50; // 플레이어 정보 창의 가로 길이
        const int ConsoleWidth = MapWidth + InfoWidth + 2; // 전체 콘솔의 가로 길이

        static Map map;
        static int playerX;
        static int playerY;
        static void Main()
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(ConsoleWidth, MapHeight + 2); // 콘솔 창 크기 설정

            map = new Map(MapWidth, MapHeight);
            playerX = MapWidth / 2;
            playerY = MapHeight / 2;

            InitializeMap(); // 맵 초기화
            DisplayMap(); // 맵 표시

            // 게임 루프
            while (true)
            {
                DisplayPlayerInfo(); // 플레이어 정보 표시

                // 키 입력 처리
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    ProcessInput(key);
                    DisplayMap(); // 맵을 다시 표시하여 플레이어 이동을 반영
                }
            }
        }

        static void InitializeMap()
        {
            map.Initialize();
        }

        static void DisplayMap()
        {
            char[,] currentMap = map.GetMap();
            int width = map.GetWidth();
            int height = map.GetHeight();

            Console.Clear();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (x == playerX && y == playerY)
                    {
                        Console.ForegroundColor = ConsoleColor.Green; // 플레이어 색상 설정
                        Console.Write('P'); // 플레이어
                        Console.ResetColor(); // 색상 초기화
                    }
                    else
                    {
                        Console.Write(currentMap[x, y]);
                    }
                }
                Console.WriteLine();
            }
        }

        static void DisplayPlayerInfo()
        {
            // 플레이어 정보 표시
            Console.SetCursorPosition(MapWidth + 1, 0);
            Console.WriteLine("Player Information:");
            Console.SetCursorPosition(MapWidth + 1, 1);
            Console.WriteLine($"Position: ({playerX}, {playerY})");
        }

        static void ProcessInput(ConsoleKeyInfo key)
        {
            // 입력에 따른 행동 처리
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    MovePlayer(0, -1);
                    break;
                case ConsoleKey.DownArrow:
                    MovePlayer(0, 1);
                    break;
                case ConsoleKey.LeftArrow:
                    MovePlayer(-1, 0);
                    break;
                case ConsoleKey.RightArrow:
                    MovePlayer(1, 0);
                    break;
                    // 다른 키에 대한 처리 추가 가능
            }
        }

        static void MovePlayer(int deltaX, int deltaY)
        {
            // 플레이어 이동
            int newPlayerX = playerX + deltaX;
            int newPlayerY = playerY + deltaY;
            if (newPlayerX >= 1 && newPlayerX < MapWidth - 1 && newPlayerY >= 1 && newPlayerY < MapHeight - 1)
            {
                playerX = newPlayerX;
                playerY = newPlayerY;
            }
        }
    }
}
