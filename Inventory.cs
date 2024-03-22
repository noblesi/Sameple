﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sameple
{
    public class Inventory
    {
        private Dictionary<int, Item> items;

        public Inventory(Dictionary<int, Item> items)
        {
            this.items = items;
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
            Console.WriteLine("인벤토리 아이템 목록");
            Console.WriteLine("====================");
            foreach(KeyValuePair<int, Item> item in items)
            {
                Console.WriteLine($"[{item.Key}] {item.Value.Name} - 수량 : {item.Value.Quantity}");
            }
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