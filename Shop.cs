using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sameple
{
    public class Shop
    {
        public static List<Item> ShopItemList;
        public static void InitializeShop()
        {
            ShopItemList = new List<Item>
            {
                new HealingPotion(1, "소형체력포션", 50, "사용자의 체력 20 회복", "healing", 20),
                new HealingPotion(2, "중형체력포션", 100, "사용자의 체력 40 회복,", "healing", 40),
                new HealingPotion(3, "대형체력포션", 200, "사용자의 체력 60 회복", "healing", 60),
                new BuffPotion(4, "공격력증가포션", 250, "사용자의 공격력 10 회복", "buff", 1, "공격")
            };
        }

        public Shop(List<Item> items)
        {
            ShopItemList = items;
        }

        public void BuyItem(Player player, int itemId, int quantity)
        {
            int PlayerGold = player.GetGold();
            Item itemToBuy = ShopItemList.Find(item => item.Id == itemId);
            if(itemToBuy != null && player.Gold>= itemToBuy.Value * quantity)
            {
                PlayerGold -= itemToBuy.Value * quantity;
                player.Inventory.AddItem(itemToBuy, quantity);
                Console.SetCursorPosition(GameManager.MapWidth * 2 + 1, 10);
                Console.WriteLine($"{itemToBuy.Name} {quantity}개를 구매하였습니다.");
            }
            else
            {
                Console.SetCursorPosition(GameManager.MapWidth * 2 + 1, 10);
                Console.WriteLine("구매에 실패하였습니다. 소지금이 부족하거나 잘못된 아이템 번호입니다.");
            }
        }

        public void SellItem(Player player, int itemId, int quantity)
        {
            if (player.Inventory.ContainsKey(itemId) && player.Inventory[itemId].Quantity >= quantity)
            {
                Item itemToSell = player.Inventory[itemId];
                int PlayerGold = player.GetGold();
                player.Inventory[itemId].Quantity -= quantity;
                PlayerGold += (int)(itemToSell.Value * 0.7) * quantity;
                Console.WriteLine($"{itemToSell.Name} {quantity}개를 판매하였습니다.");
            }
            else
            {
                Console.WriteLine("판매에 실패하였습니다. 잘못된 아이템 번호이거나 아이템 수량이 부족합니다.");
            }
        }

        public void DisplayShopItems()
        {
            Console.SetCursorPosition(GameManager.MapWidth * 2 + 1, 8);
            Console.WriteLine("상점 아이템 목록");
            Console.SetCursorPosition(GameManager.MapWidth * 2 + 1, 9);
            Console.WriteLine("====================");
            int idx = 1;
            foreach(Item item in ShopItemList)
            {
                Console.SetCursorPosition(GameManager.MapWidth * 2 + 3, 9 + idx);
                Console.WriteLine($"[{item.Id}] {item.Name} - 가격 : {item.Value}");
                idx++;
            }
        }

        public static void OpenShopForBuy(Shop shop)
        {
            int selectedItemIndex = 0;
            int quantityToBuy = 1;

            while (true)
            {
                Utility.ClearConsoleArea(GameManager.MapWidth * 2 + 1, 8, 20, 10);

                shop.DisplayShopItems();

                for(int i=0; i<Shop.ShopItemList.Count; i++)
                {
                    Console.SetCursorPosition(GameManager.MapWidth * 2 + 1, 10 + i);

                    if(i == selectedItemIndex)
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
                        if(selectedItemIndex > 0)
                            selectedItemIndex--;
                        break;
                    case ConsoleKey.DownArrow:
                        if(selectedItemIndex < Shop.ShopItemList.Count - 1)
                            selectedItemIndex++;
                        break;
                    case ConsoleKey.Enter:
                        Utility.ClearConsoleArea(GameManager.MapWidth * 2 + 1, 8, 50, 10);
                        Console.WriteLine($"선택된 아이템: {Shop.ShopItemList[selectedItemIndex].Name}");
                        Console.SetCursorPosition(GameManager.MapWidth * 2 + 1, 9);
                        Console.Write("구매할 수량을 입력하세요 : ");
                        int.TryParse(Console.ReadLine(), out quantityToBuy);
                        shop.BuyItem(GameManager.currentPlayer, Shop.ShopItemList[selectedItemIndex].Id, quantityToBuy);
                        Console.SetCursorPosition(GameManager.MapWidth * 2 + 1, 11);
                        Console.WriteLine("아무 키나 누르세요...");
                        Console.ReadKey(true);
                        return;
                    case ConsoleKey.Escape:
                        return;
                }
            }
            
        }
    }
}
