using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sameple
{
    public class Inventory
    {

        private Dictionary<int, Item> items;
        public Player Owner {  get; set; }

        public Inventory(Dictionary<int, Item> items)
        {
            this.items = items;
            Owner = GameManager.currentPlayer;

            Item AncientSword = new Weapon(1, "고대의 검", 0, "유저와 함께 성장하는 성장형 검입니다.", "Weapon",  10, 0, 3);
            Item AncientSpear = new Weapon(2, "고대의 창", 1, "유저와 함께 성장하는 성장형 창입니다.", "Weapon",  15, 0, 0);
            Item AncientAxe = new Weapon(3, "고대의 도끼", 2, "유저와 함께 성장하는 성장형 도끼입니다.", "Weapon",  20, 0, -3);

            AddItem(AncientSword, 1);
            AddItem(AncientSpear, 1);
            AddItem(AncientAxe, 1);
        }

        public void AddItem(Item item, int quantity)
        {
            if (items.ContainsKey(item.Id))
            {
                items[item.Id].Quantity += quantity;
            }
            else
            {
                items.Add(item.Id, item);
                items[item.Id].Quantity = quantity;
            }
        }

        public void RemoveItem(int itemId, int quantity)
        {
            if (items.ContainsKey(itemId))
            {
                items[itemId].Quantity -= quantity;
                if (items[itemId].Quantity <= 0)
                {
                    items.Remove(itemId);
                }
            }
        }

        public void DisplayInventory()
        {
            Console.SetCursorPosition(GameManager.MapWidth * 2 + 1, 8);
            Console.WriteLine("인벤토리 아이템 목록");
            Console.SetCursorPosition(GameManager.MapWidth * 2 + 1, 9);
            Console.WriteLine("====================");
            int i = 1;
            foreach(KeyValuePair<int, Item> item in items)
            {
                Console.SetCursorPosition(GameManager.MapWidth * 2 + 3, 9 + i); ;
                Console.WriteLine($"[{item.Key}] {item.Value.Name} - 수량 : {item.Value.Quantity}");
                i++;
            }
            Console.SetCursorPosition(GameManager.MapWidth * 2 + 1, 9 + i);
        }

        public static void OpenInventoryForSell(Inventory playerInventory)
        {
            int selectedItemIndex = 0;
            int quantityToSell = 1;

            while (true)
            {
                Utility.ClearConsoleArea(GameManager.MapWidth * 2 + 1, 8, 20, 10);

                playerInventory.DisplayInventory();

                for(int i=0; i<playerInventory.items.Count; i++)
                {
                    Console.SetCursorPosition(GameManager.MapWidth * 2 + 1, 10 + i);

                    if (i == selectedItemIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("->");
                    }
                    else
                    {
                        Console.ForegroundColor= ConsoleColor.White;
                        Console.Write("　");
                    }
                }

                Console.ForegroundColor = ConsoleColor.White;

                Console.SetCursorPosition(GameManager.MapWidth * 2 + 1, 9 + selectedItemIndex);

                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selectedItemIndex > 0)
                            selectedItemIndex--;
                        break;
                    case ConsoleKey.DownArrow:
                        if(selectedItemIndex < playerInventory.items.Count - 1)
                            selectedItemIndex++;
                        break;
                    case ConsoleKey.Enter:
                        Utility.ClearConsoleArea(GameManager.MapWidth * 2 + 1, 8, 50, 10);
                        Console.WriteLine($"선택된 아이템 : {playerInventory[selectedItemIndex].Name}");
                        Console.SetCursorPosition(GameManager.MapWidth * 2 + 1, 9);
                        Console.Write("판매할 수량을 입력하세요 : ");
                        int.TryParse(Console.ReadLine(), out selectedItemIndex);
                        playerInventory.RemoveItem(playerInventory[selectedItemIndex].Id, quantityToSell);
                        Console.SetCursorPosition(GameManager.MapWidth * 2 + 1, 10);
                        Console.WriteLine("아이템을 판매했습니다.");
                        Console.WriteLine("아무키나 누르세요...");
                        Console.ReadKey(true);
                        return;
                    case ConsoleKey.Escape:
                        return;
                }
            }
        }

        public static void EquipWeaponFromInventory(Inventory playerInventory)
        {
            playerInventory.DisplayWeaponsOnly();

            int selectedItemIndex = 0;

            while (true)
            {
                Utility.ClearConsoleArea(GameManager.MapWidth * 2 + 1, 8, 50, 10);

                playerInventory.DisplayWeaponsOnly();

                for (int i = 0; i < playerInventory.items.Count; i++)
                {
                    Console.SetCursorPosition(GameManager.MapWidth * 2 + 1, 10 + i);

                    if (i == selectedItemIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("->");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("　");
                    }
                }

                Console.ForegroundColor = ConsoleColor.White;

                Console.SetCursorPosition(GameManager.MapWidth * 2 + 1, 9 + selectedItemIndex);

                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selectedItemIndex > 0)
                            selectedItemIndex--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (selectedItemIndex < playerInventory.items.Count - 1)
                            selectedItemIndex++;
                        break;
                    case ConsoleKey.Enter:
                        Utility.ClearConsoleArea(GameManager.MapWidth * 2 + 1, 8, 60, 10);

                        Weapon selectedWeapon = null;
                        int idx = 0;
                        foreach(var item in playerInventory.items.Values)
                        {
                            if(item is Weapon)
                            {
                                if(idx == selectedItemIndex)
                                {
                                    selectedWeapon = item as Weapon;
                                    break;
                                }
                                idx++;
                            }
                        }

                        if(selectedWeapon != null)
                        {
                            GameManager.currentPlayer.EquipWeapon(selectedWeapon);
                            Console.SetCursorPosition(GameManager.MapWidth * 2 + 1, 8);
                            Console.WriteLine($"무기를 장착했습니다: {selectedWeapon.Name}");
                        }
                        else
                        {
                            Console.SetCursorPosition(GameManager.MapWidth * 2 + 1, 8);
                            Console.WriteLine("잘못된 선택입니다. 다시 선택해 주세요.");
                        }

                        Console.SetCursorPosition(GameManager.MapWidth * 2 + 1, 9);
                        Console.WriteLine("아무 키나 누르세요...");
                        Console.ReadKey(true);
                        return;
                    case ConsoleKey.Escape:
                        return;
                }
            }
        }


        public void DisplayWeaponsOnly()
        {
            Utility.ClearConsoleArea(GameManager.MapWidth * 2 + 1, 8, 30, 10);

            Console.SetCursorPosition(GameManager.MapWidth * 2 + 1, 8);
            Console.WriteLine("무기 목록");
            Console.SetCursorPosition(GameManager.MapWidth * 2 + 1, 9);
            Console.WriteLine("====================");

            int idx = 1;
            foreach (KeyValuePair<int, Item> item in items)
            {
                if (item.Value is Weapon)
                {
                    Weapon weapon = item.Value as Weapon;
                    Console.SetCursorPosition(GameManager.MapWidth * 2 + 3, 9 + idx);
                    Console.WriteLine($"[{item.Key}] {item.Value.Name} - 공격력: {weapon.AtkBonus}, " +
                        $"방어력: {weapon.DefBonus}, 속도: {weapon.SpdBonus}");
                    idx++;
                }
            }
        }

        public static void OpenInventory(Inventory playerInventory)
        {
            playerInventory.DisplayInventory();

            Console.WriteLine("아무키나 누르세요...");
            Console.ReadKey(true);
            return;
        }

        public bool ContainsKey(int itemId)
        {
            return items.ContainsKey(itemId);
        }

        public Item this[int itemId]
        {
            get { return items[itemId]; }
            set { items[itemId] = value; }
        }
    }
}
