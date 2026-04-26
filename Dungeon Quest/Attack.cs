using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Quest
{
    internal class Attack
    {
        int playerDefend = 0;
        public void PlayerPhysicalAttack(PlayerCharacter myPlayer, EnemyNpc npc)
        {  
            // Player physical attack method
            Random rnd = new Random();
            int criticalRandom = rnd.Next(0, 4);
            switch (criticalRandom)
            {
                case 0:
                    // critical hit
                    npc.NpcHealth(myPlayer.PlayerAttack()+4);
                    Console.Write($"Critical hits {npc.NpcType()}'s health ");
                    GameUI.ColoredWrite("-"+(myPlayer.PlayerAttack()+ npc.GetStat(EnemyNpc.AttackTakeBonus)+2).ToString(), ConsoleColor.Red);
                    Console.Write("!"+"\n");
                    Console.WriteLine();
                    Thread.Sleep(1000);
                    break;
                default:
                    //avearge attack
                    npc.NpcHealth(myPlayer.PlayerAttack()+ npc.GetStat(EnemyNpc.AttackTakeBonus));
                    Console.Write($"{npc.NpcType()}'s health ");
                    GameUI.ColoredWrite("-" + myPlayer.PlayerAttack().ToString(), ConsoleColor.Red);
                    Console.Write("!" + "\n");
                    Console.WriteLine();
                    Thread.Sleep(1000);
                    break;

            }
        }
        public void PlayerMagicAttack(PlayerCharacter myPlayer, EnemyNpc npc)
        {
            // Player magic attack method
            npc.NpcHealth(myPlayer.GetBlizzard()+ npc.GetStat(EnemyNpc.MagicTakeBonus));
            Console.Write($"{npc.NpcType()}'s health ");
            GameUI.ColoredWrite("-", ConsoleColor.Red);
            GameUI.ColoredWrite((myPlayer.GetBlizzard() + npc.GetStat(EnemyNpc.MagicTakeBonus)).ToString(), ConsoleColor.Red);
            Console.Write("!" + "\n");
            Console.WriteLine();

        }
               
        public void OrcAttackMethod(PlayerCharacter myPlayer, EnemyNpc npc)
        {
            // Orc attack method
            Random rnd = new Random();
            int npcCritical = rnd.Next(0, 12);

            // check if character is blocking
            int defenseValue = myPlayer.GetBlock();           
            int shieldBonus = myPlayer.HasShield() ? 1 : 0;

            switch (npcCritical)
            {
                case 0:
                    // Critical hit
                    Console.Write($"Critical hit on {myPlayer.GetCharacterName()}! ");
                    GameUI.ColoredWrite((-2 - npc.GetStat(EnemyNpc.Attack) + defenseValue + shieldBonus).ToString() + "\n", ConsoleColor.Red);
                    myPlayer.Health(defenseValue + shieldBonus - npc.GetStat(EnemyNpc.Attack)-2);
                    break;
                default:
                    // average attack
                    Console.Write($"{myPlayer.GetCharacterName()}! ");
                    GameUI.ColoredWrite((-npc.GetStat(EnemyNpc.Attack) + defenseValue + shieldBonus).ToString() + "\n", ConsoleColor.Red);
                    myPlayer.Health(defenseValue + shieldBonus - npc.GetStat(EnemyNpc.Attack));
                    break;
            }
        }

        public void WraithAttackMethod(PlayerCharacter myPlayer, EnemyNpc npcWraith)
        {
            Random rnd = new Random();
            int wraithCritical = rnd.Next(0, 3);

            if (npcWraith.GetStat(EnemyNpc.Attack) < 5)
            {
                wraithCritical = 2;
            }

            // check if character is blocking
            int defenseValue = myPlayer.GetBlock();
            int shieldBonus = myPlayer.HasShield() ? 1 : 0;

            switch (wraithCritical)
            {
                case 0:
                    // average attack
                    Console.WriteLine($"{npcWraith.NpcType()} Attacks!");
                    Console.Write($"{myPlayer.GetCharacterName()}! ");
                    GameUI.ColoredWrite(("-"+npcWraith.GetStat(EnemyNpc.Attack) + defenseValue + shieldBonus).ToString() + "\n", ConsoleColor.Red);
                    myPlayer.Health(defenseValue + shieldBonus - npcWraith.GetStat(EnemyNpc.Attack));
                    break;
                default:
                    // magic attack
                    Console.WriteLine($"{npcWraith.NpcType()} use Arcane Bolt!");
                    Console.Write($"Critical hit on {myPlayer.GetCharacterName()}! ");
                    GameUI.ColoredWrite((npcWraith.GetStat(EnemyNpc.Skill) + defenseValue + shieldBonus).ToString() + "\n", ConsoleColor.Red);
                    myPlayer.Health(defenseValue + shieldBonus - npcWraith.GetStat(EnemyNpc.Skill));
                    npcWraith.StatsMod(EnemyNpc.Mana, -4);
                    break;
            }

            if (npcWraith.GetStat(EnemyNpc.Mana) < 4)
            {
                npcWraith.StatsMod(EnemyNpc.Mana, 1);
            }



        }

        public void BossAttackMethod(PlayerCharacter myPlayer, EnemyNpc npcBoss)
        {
            // boss attack method
            Random rnd = new Random();
            int npcCritical = rnd.Next(0, 5);

            // check if character is blocking
            int defenseValue = myPlayer.GetBlock();
            int shieldBonus = myPlayer.HasShield() ? 1 : 0;

            switch (npcCritical)
            {
                case 0:
                    // Critical hit
                    Console.Write($"Critical hit on {myPlayer.GetCharacterName()}! ");
                    GameUI.ColoredWrite((-2 - npcBoss.GetStat(EnemyNpc.Attack) + defenseValue + shieldBonus).ToString() + "\n", ConsoleColor.Red);
                    myPlayer.Health(defenseValue + shieldBonus - npcBoss.GetStat(EnemyNpc.Attack) - 2);
                    break;
                case 1:
                    // magic attack
                    Console.WriteLine($"{npcBoss.NpcType()} use Arcane Bolt!");
                    Console.Write($"Critical hit on {myPlayer.GetCharacterName()}! ");
                    GameUI.ColoredWrite((npcBoss.GetStat(EnemyNpc.Skill) + defenseValue + shieldBonus).ToString() + "\n", ConsoleColor.Red);
                    myPlayer.Health(defenseValue + shieldBonus - npcBoss.GetStat(EnemyNpc.Skill));
                    npcBoss.StatsMod(EnemyNpc.Mana, -4);
                    break;
                default:
                    // average attack
                    Console.Write($"{myPlayer.GetCharacterName()}! ");
                    GameUI.ColoredWrite((-npcBoss.GetStat(EnemyNpc.Attack) + defenseValue + shieldBonus).ToString() + "\n", ConsoleColor.Red);
                    myPlayer.Health(defenseValue + shieldBonus - npcBoss.GetStat(EnemyNpc.Attack));
                    break;
            }

            npcBoss.StatsMod(EnemyNpc.Mana, 1);
        }
    }
}
