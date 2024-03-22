using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sameple
{
    public class Dungeon
    {
        private List<Monster> monsters;
        private Random random;

        public Dungeon()
        {
            monsters = new List<Monster>();
            random = new Random();
            GenerateMonsters();
        }

        private void GenerateMonsters()
        {
            monsters.Add(new Monster("고블린", 50, 50, 10, 5, 8, DungeonType.Forest, Monster.MonsterType.Normal));
            monsters.Add(new Monster("늑대", 60, 60, 15, 8, 10, DungeonType.Forest, Monster.MonsterType.Normal));
            monsters.Add(new Monster("트롤", 100, 100, 20, 15, 5, DungeonType.Forest, Monster.MonsterType.Elite));
            monsters.Add(new Monster("마녀", 150, 150, 25, 20, 10, DungeonType.Forest, Monster.MonsterType.Boss));

            monsters.Add(new Monster("박쥐", 40, 40, 8, 3, 12, DungeonType.Cave, Monster.MonsterType.Normal));
            monsters.Add(new Monster("거미", 55, 55, 12, 7, 9, DungeonType.Cave, Monster.MonsterType.Normal));
            monsters.Add(new Monster("흑요석 골렘", 90, 90, 18, 12, 6, DungeonType.Cave, Monster.MonsterType.Elite));
            monsters.Add(new Monster("드래곤", 200, 200, 30, 25, 15, DungeonType.Cave, Monster.MonsterType.Boss));

            monsters.Add(new Monster("팬서", 70, 70, 15, 10, 12, DungeonType.Castle, Monster.MonsterType.Normal));
            monsters.Add(new Monster("감시병", 80, 80, 18, 12, 10, DungeonType.Castle, Monster.MonsterType.Normal));
            monsters.Add(new Monster("마법사", 120, 120, 22, 18, 15, DungeonType.Castle, Monster.MonsterType.Elite));
            monsters.Add(new Monster("드래곤 킹", 300, 300, 40, 35, 20, DungeonType.Castle, Monster.MonsterType.Boss));
        }

        public void EnterDungeon(Player player)
        {
            Console.WriteLine("던전에 입장합니다.");

            DungeonType dungeonType = player.CurrentDungeonType;
            List<Monster> dungeonMonsters = monsters.Where(monster => monster.dungeonType == dungeonType).ToList();

            for(int i=1; i<=3; i++)
            {
                Console.WriteLine($"=== {i}번째 전투 시작 ===");
                Monster monster = ChooseRandomMonster(dungeonMonsters);
                Battle(player, monster);
                Console.WriteLine($"=== {i}번째 전투 종료 ===");
            }
        }

        public Monster ChooseRandomMonster(List<Monster> dungeonMonsters)
        {
            int totalWeight = dungeonMonsters.Count;
            int randomNumber = random.Next(1, totalWeight + 1);

            for(int i=0; i<dungeonMonsters.Count; i++)
            {
                if(randomNumber <= (i + 1))
                {
                    return dungeonMonsters[i];
                }
                else
                {
                    randomNumber -= (i + 1);
                }
            }
            return dungeonMonsters.First();
        }

        private void Battle(Player player, Monster monster)
        {
            bool playerFirst = player.Spd >= monster.Spd;

            while(!player.isDead() && !monster.isDead())
            {
                if (playerFirst)
                {
                    player.Attack(monster);

                    if(!monster.isDead())
                    {
                        monster.Attack(monster);
                    }
                }
                else
                {
                    monster.Attack(player);

                    if(!player.isDead())
                    {
                        player.Attack(monster);
                    }
                }
            }

            if (player.isDead())
            {
                player.Defeat();
            }
            else if(monster.isDead())
            {
                int baseExpGained = CalculateBaseExpGained(monster.Type);
                int baseGoldGained = CalculateBaseGoldGained(monster.Type);

                double expMultiplier = CalculateExpMultiplier(monster.dungeonType);
                double goldMultiplier = CalculateGoldMultiplier(monster.dungeonType);

                int expGained = (int)(baseExpGained * expMultiplier);
                int minGoldGained = (int)(baseGoldGained * goldMultiplier);
                int maxgoldGained = minGoldGained + random.Next(10, 31);

                player.Victory(expGained, minGoldGained, maxgoldGained);
            }
        }

        private int CalculateBaseExpGained(Monster.MonsterType type)
        {
            int baseExpGained = 0;

            switch (type)
            {
                case Monster.MonsterType.Normal:
                    baseExpGained = 30;
                    break;
                case Monster.MonsterType.Elite:
                    baseExpGained = 50;
                    break;
                case Monster.MonsterType.Boss:
                    baseExpGained = 100;
                    break;
                default:
                    break;
            }
            return baseExpGained;
        }

        private int CalculateBaseGoldGained(Monster.MonsterType type)
        {
            int baseGoldGained = 0;

            switch (type)
            {
                case Monster.MonsterType.Normal:
                    baseGoldGained = 10;
                    break;
                case Monster.MonsterType.Elite:
                    baseGoldGained = 20;
                    break;
                case Monster.MonsterType.Boss:
                    baseGoldGained = 50;
                    break;
                default:
                    break;
            }
            return baseGoldGained;
        }

        private double CalculateExpMultiplier(DungeonType dungeonType)
        {
            double expMultiplier = 1.0;

            switch (dungeon)
            {
                case DungeonType.Forest:
                    expMultiplier = 1.2;
                    break;
                case DungeonType.Cave:
                    expMultiplier = 1.5;
                    break;
                case DungeonType.Castle:
                    expMultiplier = 2.0;
                    break;
                default:
                    break;
            }

            return expMultiplier;
        }

        private double CalculateGoldMultiplier(DungeonType dungeonType)
        {
            double goldMultiplier = 1.0;

            switch (dungeon)
            {
                case DungeonType.Forest:
                    goldMultiplier = 1.0;
                    break;
                case DungeonType.Cave:
                    goldMultiplier = 1.2;
                    break;
                case DungeonType.Castle:
                    goldMultiplier = 1.5;
                    break;
                default:
                    break;
            }

            return goldMultiplier;
        }
    }
}
