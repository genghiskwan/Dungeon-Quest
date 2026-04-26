using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Quest
{
    internal class EnemyNpc
    {
        public const int Health = 0;
        public const int Attack = 1;
        public const int Skill = 2;
        public const int Mana = 3;
        public const int TypeIndex = 4;
        public const int MagicTakeBonus = 5;
        public const int AttackTakeBonus = 6;
        private string m_npcType="";
        
        private int[] Stats;
        public bool isDead = false;

        public EnemyNpc(int npcClass, int firstDungeonCheck)
        {
            Stats = new int[7];
            switch (npcClass)
            {
                case 0:
                    m_npcType = "Orc";
                    Stats = new int[] {12,2,0,0,0,2,0};
                    break;
                case 1:
                    m_npcType = "Wraith";
                    Stats = new int[] {9,1,6,20,1,-2,1}; 
                    break;
                case 2:
                    m_npcType = "Xulgath the Soul Eater";
                    Stats = new int[] { 22, 7, 10, 12, 0, 0, 0 };
                    break;
            }

            if (m_npcType == "Orc")
            {
                Stats[Health] += firstDungeonCheck;
                Stats[Attack] += firstDungeonCheck;
            }
            else if (m_npcType == "Wraith")
            {
                Stats[Health] += firstDungeonCheck;
                Stats[Attack] += firstDungeonCheck;
                Stats[Skill] += firstDungeonCheck;
                Stats[Mana] += firstDungeonCheck;
                Stats[MagicTakeBonus] += firstDungeonCheck;
            }
        }

        public void StatsMod(int index, int modAmount)
        {   
            // npc stats increase after each dungeon
            Stats[index] += modAmount;
        }
        public int GetStat(int index)
        {
            return Stats[index];
        }
        public void NpcHealth(int playerAttack)
        {
            Stats[Health] -= playerAttack;
        }
        public string NpcType()
        {
            return m_npcType;
        }
    }
}
