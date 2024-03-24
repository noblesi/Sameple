using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sameple
{
    public class Weapon : Item
    {
        public int BaseAtk {  get; private set; }
        public int AtkBonus {  get; set; }
        public int DefBonus { get; private set; }
        public int SpdBonus { get; private set; }
        public int EnhancementLevel {  get; private set; }
        public int EnhancementCost {  get; private set; }

        public Weapon(int id, string name, int value, string description, string type, int atkBonus, int defBonus, int spdBonus)
            : base(id, name, value, description, type)
        {
            AtkBonus = atkBonus;
            DefBonus = defBonus;
            SpdBonus = spdBonus;
            EnhancementLevel = 0;
            EnhancementCost = 100;
        }
    }

    public class Sword : Weapon
    {
        public Sword(int id, string name, int value, string description, string type, int atkBonus, int defBonus, int spdBonus)
            : base(id, name, value, description, type, atkBonus, defBonus, spdBonus)
        {

        }
    }

    public class Spear : Weapon
    {
        public Spear(int id, string name, int value, string description, string type, int atkBonus, int defBonus, int spdBonus)
            : base(id, name, value, description, type, atkBonus, defBonus, spdBonus)
        {

        }
    }

    public class Axe : Weapon
    {
        public Axe(int id, string name, int value, string description, string type, int atkBonus, int defBonus, int spdBonus)
            : base(id, name, value, description, type, atkBonus, defBonus, spdBonus)
        {

        }
    }
}
