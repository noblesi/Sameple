using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sameple
{
    
    public class GameManager
    {
        public const int MapWidth = 30; // 맵의 가로 길이
        public const int MapHeight = 30; // 맵의 세로 길이
        public const int ConsoleWidth = 150;

        static Map map;
        static int playerX;
        static int playerY;

        public static Player currentPlayer;
        public static Shop shop;
        public static List<Item> items;

        private static GameManager instance;

        private GameManager()
        {
            
        }

        public static GameManager Instance
        {
            get
            {
                if(instance == null)
                    instance = new GameManager();
                return instance;
            }
        }

        public static void Init()
        {
            Console.WriteLine("\n");

            Utility.WriteCenterPosition("플레이어의 이름을 지어주세요");

            Console.WriteLine("\n");

            string playerName = Console.ReadLine();

            Console.WriteLine("\n");

            Utility.WriteCenterPosition($"당신의 이름은 {playerName}이군요");

            CreatePlayer(playerName);

            CreateShop(items);

            StartGame();
        }

        public static void StartGame()
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(ConsoleWidth, MapHeight + 2); // 콘솔 창 크기 설정

            map = new Map(MapWidth, MapHeight);
            playerX = MapWidth / 2;
            playerY = MapHeight / 2;

            map.Initialize(); // 맵 초기화
            map.Display(playerX, playerY); // 맵 표시

            // 게임 루프
            while (true)
            {
                DisplayPlayerInfo(); // 플레이어 정보 표시

                // 키 입력 처리
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    PlayerMovement.ProcessInput(key, ref playerX, ref playerY, MapWidth, MapHeight);
                    map.Display(playerX, playerY); // 맵을 다시 표시하여 플레이어 이동을 반영
                }
            }
        }

        public static void LoadGame()
        {

        }

        public static void ExitGame()
        {
            Console.Clear();
            Utility.WriteOneByOne("게임을 종료합니다.");

            Environment.Exit(0);
        }

        private static Player CreatePlayer(string playerName)
        {
            Random random = new Random();
            int initialHp = random.Next(50, 101);
            int initialAtk = random.Next(10, 21);
            int initialDef = random.Next(5, 11);
            int initialSpd = random.Next(5, 11);
            currentPlayer = new Player(playerName, initialHp, initialHp, initialAtk, initialDef, initialSpd);

            return currentPlayer;
        }

        private static Shop CreateShop(List<Item> items)
        {
            shop = new Shop(items);
            Shop.InitializeShop();

            return shop;
        }

        public static void DisplayPlayerInfo()
        {
            // 플레이어 정보 표시
            Console.SetCursorPosition(MapWidth * 2 + 1, 0);
            Console.WriteLine("플레이어 정보");
            Console.SetCursorPosition(MapWidth * 2 + 1, 1);
            Console.WriteLine($"Name : {currentPlayer.Name}");
            Console.SetCursorPosition(MapWidth * 2 + 1, 2);
            Console.WriteLine($"HP : {currentPlayer.CurrentHp} / {currentPlayer.MaxHp}");
            Console.SetCursorPosition(MapWidth * 2 + 1, 3);
            Console.WriteLine($"EXP : {currentPlayer.Exp} / {currentPlayer.MaxExp}");
            Console.SetCursorPosition(MapWidth * 2 + 1, 4);
            Console.WriteLine($"ATK : {currentPlayer.Atk}");
            Console.SetCursorPosition(MapWidth * 2 + 1, 5);
            Console.WriteLine($"DEF : {currentPlayer.Def}");
            Console.SetCursorPosition(MapWidth * 2 + 1, 6);
            Console.WriteLine($"SPD : {currentPlayer.Spd}");
            Console.SetCursorPosition(MapWidth * 2 + 1, 7);
            Console.WriteLine($"GOLD : {currentPlayer.Gold}");
        }
    }
}
