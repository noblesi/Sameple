using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sameple
{
    public class Item
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Value { get; private set; }
        public string Description {  get; private set; }
        public string Type { get; private set; }
        public int Quantity { get; set; }

        public Item(int id, string name, int value, string description, string type)
        {
            Id = id;
            Name = name;
            Value = value;
            Description = description;
            Type = type;
            Quantity = 1;
        }
    }

    public class Potion : Item
    {
        public Potion(int id, string name, int value, string description, string type)
            : base(id, name, value, description, type)
        {
            
        }
    }

    public class HealingPotion : Potion
    {
        public int HealingAmount { get; private set; }

        public HealingPotion(int id, string name, int value, string description, string type, int healingAmount)
            : base(id, name, value, description, type)
        {
            HealingAmount = healingAmount;
        }
    }

    public class BuffPotion : Potion
    {
        public int BuffDuration { get; private set; }
        public string BuffType { get; private set; }

        public BuffPotion(int id, string name, int value, string description, string type, int buffDuration, string buffType)
            : base(id, name, value, description, type)
        {
            BuffDuration = buffDuration;
            BuffType = buffType;
        }
    }
}
