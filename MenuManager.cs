namespace Sameple
{
    public class MenuManager
    {
        private string[] menuItems;
        private int selectedItemIndex;
        private ConsoleColor defaultColor;

        public MenuManager(string[] items)
        {
            menuItems = items;
            selectedItemIndex = 0;
            defaultColor = Console.ForegroundColor;
        }

        public void DisplayMenu()
        {   
            for(int i=0; i<menuItems.Length; i++)
            {
                if (i == selectedItemIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = defaultColor;
                }

                //Console.WriteLine($"[{i + 1}] {menuItems[i]}");
                Utility.WriteCenterPosition($"[{i + 1}] {menuItems[i]}");
                Console.WriteLine("\n");
            }

            Console.ForegroundColor = defaultColor;
        }

        public void SelectPreviousMenu()
        {
            selectedItemIndex--;
            if(selectedItemIndex < 0 )
            {
                selectedItemIndex = menuItems.Length - 1;
            }
        }

        public void SelectNextMenu()
        {
            selectedItemIndex++;
            if(selectedItemIndex >= menuItems.Length)
            {
                selectedItemIndex = 0;
            }
        }

        public void ProcessInput(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    SelectPreviousMenu();
                    break;
                case ConsoleKey.DownArrow:
                    SelectNextMenu();
                    break;
                case ConsoleKey.Enter:
                    ExecuteSelectMenu();
                    break;
            }
        }

        public void ExecuteSelectMenu()
        {
            Console.Clear();
            switch (selectedItemIndex)
            {
                case 0:
                    GameManager.Init();
                    break;
                case 1:
                    GameManager.LoadGame();
                    break;
                case 2:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
