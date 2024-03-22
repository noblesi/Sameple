using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sameple
{
    public class Weapon
    {
        public string Name { get; private set; }
        public int BaseAtk {  get; private set; }
        public int AtkBonus {  get; set; }
        public int DefBonus { get; private set; }
        public int SpdBonus { get; private set; }
        public int EnhancementLevel {  get; private set; }
        public int EnhancementCost {  get; private set; }

        public Weapon(string name, int atkBonus, int defBonus, int spdBonus)
        {
            Name = name;
            AtkBonus = atkBonus;
            DefBonus = defBonus;
            SpdBonus = spdBonus;
            EnhancementLevel = 0;
            EnhancementCost = 100;
            
        }
    }

    public class Sword : Weapon
    {
        public Sword(string name, int atkBonus, int defBonus, int spdBonus)
            : base(name, atkBonus, defBonus, spdBonus)
        {

        }
    }

    public class Spear : Weapon
    {
        public Spear(string name, int atkBonus, int defBonus, int spdBonus)
            : base(name, atkBonus, defBonus, spdBonus)
        {

        }
    }

    public class Axe : Weapon
    {
        public Axe(string name, int atkBonus, int defBonus, int spdBonus)
            : base(name, atkBonus, defBonus, spdBonus)
        {

        }
    }
}
