namespace Sameple
{
    public class Program
    {
        
        static void Main()
        {
            #region 맵을 만들기 및 플레이어의 이동 처리
            
            #endregion

            Console.CursorVisible = false;

            string[] menuItems = { "게임시작", "불러오기", "게임종료" };
            MenuManager menuManager = new MenuManager(menuItems);

            TitleMenu(menuManager);
        }

        static void TitleMenu(MenuManager menuManager)
        {
            Console.Clear();

            Utility.WriteCenterPosition("######################################################################");
            Utility.WriteCenterPosition("#                                                                    #");
            Utility.WriteCenterPosition("#     ####### ####### #     # #######    ######  ######   #####      #");
            Utility.WriteCenterPosition("#        #    #        #   #     #       #     # #     # #     #     #");
            Utility.WriteCenterPosition("#        #    #         # #      #       #     # #     # #           #");
            Utility.WriteCenterPosition("#        #    #####      #       #       ######  ######  #  ####     #");
            Utility.WriteCenterPosition("#        #    #         # #      #       #   #   #       #     #     #");
            Utility.WriteCenterPosition("#        #    #        #   #     #       #    #  #       #     #     #");
            Utility.WriteCenterPosition("#        #    ####### #     #    #       #     # #        #####      #");
            Utility.WriteCenterPosition("#                                                                    #");
            Utility.WriteCenterPosition("######################################################################");

            Console.WriteLine();
            Console.WriteLine();

            while (true)
            {
                //Utility.ClearConsoleArea(10, 15, 10, 20);

                menuManager.DisplayMenu();

                ConsoleKeyInfo key = Console.ReadKey(true);

                ProcessMainMenuInput(key, menuManager);

                Utility.ClearConsoleArea(25, 13, 30, 8);
            }
        }

        static void ProcessMainMenuInput(ConsoleKeyInfo key, MenuManager menuManager)
        {
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    menuManager.SelectPreviousMenu();
                    break;
                case ConsoleKey.DownArrow:
                    menuManager.SelectNextMenu();
                    break;
                case ConsoleKey.Enter:
                    menuManager.ExecuteSelectMenu();
                    break;
            }
        }
    }
}
