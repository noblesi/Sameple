using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sameple
{
    public enum DungeonType
    {
        Forest,
        Cave,
        Castle
    }
    public abstract class CharacterBase
    {
        public string Name { get; set; }
        public int CurrentHp {  get; set; }
        public int MaxHp {  get; set; }
        public int Atk {  get; set; }
        public int Def {  get; set; }
        public int Spd {  get; set; }

        public CharacterBase(string name, int currentHp, int maxHp, int atk, int def, int spd)
        {
            Name = name;
            CurrentHp = currentHp;
            MaxHp = maxHp;
            Atk = atk;
            Def = def;
            Spd = spd;
        }

        public virtual void Attack(CharacterBase target)
        {
            int damage = Math.Max(Atk - target.Def, 0);
            Console.WriteLine($"{Name}이(가) {target.Name}을(를) 공격하여 {damage}의 데미지를 입혔습니다.");
            target.TakeDamage(damage);
            Thread.Sleep(1000);
        }

        public virtual void TakeDamage(int damage)
        {
            CurrentHp -= damage;
            if(CurrentHp <= 0)
            {
                CurrentHp = 0;
                Console.WriteLine($"{Name}이(가) 기절했습니다.");
            }
        }

        public virtual void Heal(int amount)
        {
            CurrentHp = Math.Min(CurrentHp + amount, MaxHp);
            Console.WriteLine($"{Name}의 체력이 {amount}만큼 회복되었습니다.");
        }

        public bool isDead()
        {
            return CurrentHp <= 0;
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"이름 : {Name}");
            Console.WriteLine($"HP : {CurrentHp} / {MaxHp}");
            Console.WriteLine($"ATK : {Atk}");
            Console.WriteLine($"DEF : {Def}");
            Console.WriteLine($"SPD : {Spd}");
        }
    }

    public class Player : CharacterBase
    {
        public int Level { get; private set; }
        public int Exp {  get; private set; }
        public int MaxExp {  get; private set; }
        public int Gold {  get; private set; }
        public Inventory Inventory { get; private set; }
        public List<Weapon> Weapons {  get; private set; }
        public DungeonType CurrentDungeonType { get; set; }

        private double baseEnhancementSuccessRate = 1.0;
        private int weaponAtkBonus;
        private int weaponDefBonus;
        private int weaponSpdBonus;

        public Player(string name,int currentHp, int maxHp, int atk, int def, int spd, int level =1,int exp=0, int maxExp=100, int gold=0)
            : base(name, currentHp, maxHp, atk, def, spd)
        {
            Level = level;
            Exp = exp;
            MaxExp = maxExp;
            Gold = gold;
            Weapons = new List<Weapon>();
            Inventory = new Inventory(new Dictionary<int, Item>());
        }

        public void GainExp(int exp, int lastLog)
        {
            Exp += exp;
            if(Exp >= MaxExp)
            {
                LevelUp(lastLog);
            }
        }

        private void LevelUp(int lastLog)
        {
            Level++;
            Exp = 0;
            MaxExp += 20;

            Random rand = new Random();
            Atk += rand.Next(1, 4);
            Def += rand.Next(1, 4);
            Spd += rand.Next(1, 4);

            Console.SetCursorPosition(1, GameManager.MapHeight + 1 + lastLog);
            Console.WriteLine("Level Up!");
        }

        public void GainGold(int minGold, int maxGold, int lastLog)
        {
            Random rand = new Random();
            int gainedGold = rand.Next(minGold, maxGold + 1);
            Gold += gainedGold;
            Console.SetCursorPosition(1, GameManager.MapHeight + 2 + lastLog);
            Console.WriteLine($"{gainedGold}골드를 획득했습니다.");
        }

        public void AddGold(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("amount", "Amount shoud be a positive value");
            }

            Gold += amount;
        }

        public void RemoveGold(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("amount", "Amount shoud be a positive value");
            }

            if(amount > Gold)
            {
                throw new ArgumentOutOfRangeException("Not Enough Gold.");
            }

            Gold -= amount;
        }

        public int GetGold() { return Gold; }

        public void EquipWeapon(Weapon weapon)
        {
            Atk += weapon.AtkBonus;
            Def += weapon.DefBonus;
            Spd += weapon.SpdBonus;

            weaponAtkBonus = weapon.AtkBonus;
            weaponDefBonus = weapon.DefBonus;
            weaponSpdBonus = weapon.SpdBonus;
        }

        public void EnhanceWeapon(Weapon weapon, int enhancementLevel)
        {
            int enhancementCost = enhancementLevel * 300;

            if(Gold >= enhancementCost)
            {
                double successProbability = CalculateEnhancementSuccessProbability(enhancementLevel);

                if (IsEnhancementSuccess(successProbability))
                {
                    Gold -= enhancementCost;
                    weapon.AtkBonus += (enhancementLevel * 2);
                    Console.WriteLine($"{weapon.Name}의 강화에 성공하였습니다. (ATK + {enhancementLevel * 2})");
                }
                else
                {
                    Gold -= enhancementCost;
                    Console.WriteLine($"{weapon.Name}의 강화에 실패하였습니다.");
                }
            }
            else
            {
                Console.WriteLine("강화에 필요한 골드가 부족합니다.");
            }
        }

        private double CalculateEnhancementSuccessProbability(int enhancementLevel)
        {
            return baseEnhancementSuccessRate - (enhancementLevel - 1) * 0.1;
        }

        private bool IsEnhancementSuccess(double successProbability)
        {
            double randomValue = new Random().NextDouble();

            return randomValue < successProbability;
        }

        public void Victory(int expGained, int minGold, int maxGold, int lastLog)
        {
            GainExp(expGained, lastLog);
            GainGold(minGold, maxGold, lastLog);
        }

        public void Defeat()
        {
            int lostGold = (int)(Gold * 0.1);
            Gold -= lostGold;

            int lostExp = (int)(Exp * 0.1);
            Exp -= lostExp;

            Console.WriteLine($"전투에서 패배하여 {lostGold}골드와 {lostExp}경험치를 잃었습니다.");
        }
    }

    public class Monster : CharacterBase
    {
        public enum MonsterType
        {
            Normal,
            Elite,
            Boss
        }

        public DungeonType dungeonType {  get; private set; }
        public MonsterType Type { get; private set; }

        public Monster(string name, int currentHp, int maxHp, int atk, int def, int spd, DungeonType dungeontype, MonsterType monstertype)
            : base(name, currentHp, maxHp, atk, def, spd)
        {
            dungeonType = dungeontype;
            Type = monstertype;
        }
    }
}
