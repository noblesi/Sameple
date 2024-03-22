using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sameple
{
    public class GameManager
    {
        private static GameManager instance;

        public static GameManager Instance
        {
            get
            {
                if(instance == null)
                    instance = new GameManager();
                return instance;
            }
        }

        private GameManager()
        {

        }

        public static void Init()
        {
            
        }

        private static Player CreatePlayer(string playerName)
        {
            Random random = new Random();
            int initialHp = random.Next(50, 101);
            int initialAtk = random.Next(10, 21);
            int initialDef = random.Next(5, 11);
            int initialSpd = random.Next(5, 11);
            return new Player(playerName, initialHp, initialHp, initialAtk, initialDef, initialSpd);
        }
    }
}
