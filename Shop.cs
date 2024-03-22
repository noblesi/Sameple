using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sameple
{
    public class Shop
    {
        private List<Item> ShopItemList;

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
                Console.WriteLine($"{itemToBuy.Name} {quantity}개를 구매하였습니다.");
            }
            else
            {
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
                PlayerGold += itemToSell.Value * quantity;
                Console.WriteLine($"{itemToSell.Name} {quantity}개를 판매하였습니다.");
            }
            else
            {
                Console.WriteLine("판매에 실패하였습니다. 잘못된 아이템 번호이거나 아이템 수량이 부족합니다.");
            }
        }

        public void DisplayShopItems()
        {
            Console.WriteLine("상점 아이템 목록");
            Console.WriteLine("====================");
            foreach(Item item in ShopItemList)
            {
                Console.WriteLine($"[{item.Id}] {item.Name} - 가격 : {item.Value}");
            }
        }
    }
}
