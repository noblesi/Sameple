namespace Sameple
{
    public struct DungeonEntrance
    {
        public int X, Y;
        public char Symbol;

        public DungeonEntrance(int x, int y, char symbol)
        {
            X = x; Y = y; Symbol = symbol;
        }
    }

    public class Map
    {
        private const char BorderSymbol = '■';
        private const char EmptySymbol = '　';

        private char[,] map;
        private int width;
        private int height;

        private DungeonEntrance[] dungeonEntrances;

        public Map(int width, int height)
        {
            this.width = width;
            this.height = height;
            map = new char[width, height];
            GenerateDungeonEntrances();
        }

        private void GenerateDungeonEntrances()
        {
            Random random = new Random();
            dungeonEntrances = new DungeonEntrance[3]; 

            
            for (int i = 0; i < 3; i++)
            {
                int x = random.Next(1, width - 1); 
                int y = random.Next(1, height - 1); 

                char symbol;
                switch ((DungeonType)i)
                {
                    case DungeonType.Forest:
                        symbol = 'Ⅰ';
                        break;
                    case DungeonType.Cave:
                        symbol = 'Ⅱ';
                        break;
                    case DungeonType.Castle:
                        symbol = 'Ⅲ'; 
                        break;
                    default:
                        symbol = '?'; 
                        break;
                }
                dungeonEntrances[i] = new DungeonEntrance(x, y, symbol);
            }
        }

        public void Initialize()
        {
            // 맵 초기화
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                    {
                        map[x, y] = BorderSymbol; 
                    }
                    else
                    {
                        map[x, y] = EmptySymbol; 
                    }
                }
            }
        }

        public void Display(int playerX, int playerY)
        {
            Console.Clear();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (x == playerX && y == playerY)
                    {
                        Console.ForegroundColor = ConsoleColor.Green; 
                        Console.Write('●'); 
                        Console.ResetColor(); 
                    }
                    else
                    {
                        bool isDungeonEntrance = false;
                        foreach(var entrance in dungeonEntrances)
                        {
                            if(x==entrance.X && y == entrance.Y)
                            {
                                isDungeonEntrance = true;
                                Console.ForegroundColor= ConsoleColor.Yellow;
                                Console.Write(entrance.Symbol);
                                Console.ResetColor();
                                break;
                            }
                        }
                        if (!isDungeonEntrance)
                        {
                            Console.Write(map[x, y]);
                        }
                    }
                }
                Console.WriteLine();
            }

            foreach (var entrance in dungeonEntrances)
            {
                if (playerX == entrance.X && playerY == entrance.Y)
                {
                    Console.SetCursorPosition(GameManager.MapWidth * 2 + 1, 8);
                    Console.WriteLine("던전에 입장하시겠습니까?");
                    Console.SetCursorPosition(GameManager.MapWidth * 2 + 1, 9);
                    Console.WriteLine("Y / N");
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Y)
                    {
                        Utility.ClearConsoleArea(GameManager.MapWidth * 2 + 1, 7, 40, 10);
                        Dungeon dungeon = new Dungeon();
                        dungeon.EnterDungeon(GameManager.currentPlayer);
                    }
                    else if (key.Key == ConsoleKey.N)
                    {
                        Console.SetCursorPosition(GameManager.MapWidth * 2 + 1, 9);
                        Console.WriteLine("던전 입장을 취소합니다.");
                        Thread.Sleep(1000);
                        Utility.ClearConsoleArea(GameManager.MapWidth * 2 + 1, 7, 40, 10);

                    }
                }
            }
        }

        public char[,] GetMap()
        {
            return map;
        }

        public int GetWidth()
        {
            return width;
        }

        public int GetHeight()
        {
            return height;
        }
    }
}
