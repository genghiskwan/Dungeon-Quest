using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Quest
{
    internal class Dungeon
    {
        public const int AttackNerf = 0;
        public const int MagicNerf = 1;
        public const int AttackBuff = 2;
        public const int MagicBuff = 3;
        public const int Healing = 4;
        public const int Mana = 5;

        private string m_dungeonName;
        private int[] dungeonStats;
        private int m_totalRooms = 0;

        public Dungeon(PlayerCharacter myPlayer, List<int> visitedDungeon, int firstDungeonCheck, int finalDungeonCheck)
        {
            EnemyNpc npcEnemy = null;
            Random rand = new Random();
            int randomDungeon = 0;
            bool foundNew = false;

            while (!foundNew)
            {
                if (firstDungeonCheck == 0 || firstDungeonCheck == finalDungeonCheck)
                {
                    randomDungeon = rand.Next(0, 4);
                }
                else
                {
                    randomDungeon = rand.Next(0, 6);
                }

                if (visitedDungeon.Count == 0 || randomDungeon != visitedDungeon[visitedDungeon.Count - 1])
                {
                    // Still keep 4 and 5 (Safe Rooms) unique if you want
                    if (randomDungeon >= 4 && visitedDungeon.Contains(randomDungeon))
                    {
                        continue; // Re-roll if a Safe Room was already used
                    }

                    foundNew = true;
                }
            }

            visitedDungeon.Add(randomDungeon);

            DungeonType(randomDungeon);

            if (m_dungeonName == "Aether Gardens")
            {
                GameUI.ColoredWrite("====================================================\n", ConsoleColor.Green);
                GameUI.ColoredWrite($"You have entered {m_dungeonName}" + "\n", ConsoleColor.Green);
                GameUI.ColoredWrite("====================================================\n", ConsoleColor.Green);
                Thread.Sleep(1000);
                GameUI.ColoredWrite("Soft, glowing flora covers the floor, and the scent of lilies brings an immediate sense of peace. \n", ConsoleColor.Green);
                Thread.Sleep(1000);
                GameUI.ColoredWrite("This is a sacred sanctuary where the earth itself reaches out to close your wounds and restore your vitality.\n", ConsoleColor.Green);
                Thread.Sleep(1000);

                myPlayer.DungeonHeal();
                Console.Write("Your wounds are healed ");
                GameUI.ColoredWrite(myPlayer.GetHealth().ToString() + "\n", ConsoleColor.Blue);
                Console.WriteLine("");

                Thread.Sleep(2000);

                return;
            }
            else if (m_dungeonName == "The Leyline Nexus")
            {
                GameUI.ColoredWrite("====================================================\n", ConsoleColor.Green);
                GameUI.ColoredWrite($"You have entered {m_dungeonName}" + "\n", ConsoleColor.Green);
                GameUI.ColoredWrite("====================================================\n", ConsoleColor.Green);
                Thread.Sleep(1000);
                GameUI.ColoredWrite("This ancient arena was built to test the greatest heroes of the realm. The spirits of past warriors linger here, \n", ConsoleColor.Green);
                Thread.Sleep(1000);
                GameUI.ColoredWrite("filling you with a supernatural surge of both physical and magical power.\n", ConsoleColor.Green);
                Thread.Sleep(1000);

                myPlayer.Mana(10 - myPlayer.GetMana());
                Console.Write("Your Mana have returned ");
                GameUI.ColoredWrite(myPlayer.GetMana().ToString() + "\n", ConsoleColor.Blue);
                Console.WriteLine("");

                Thread.Sleep(1000);

                return;
            }

            GameUI.ColoredWrite("====================================================\n", ConsoleColor.Green);
            GameUI.ColoredWrite($"You have entered {m_dungeonName}" + "\n", ConsoleColor.Green);
            GameUI.ColoredWrite("====================================================\n", ConsoleColor.Green);
            Thread.Sleep(1000);

            if (m_dungeonName == "Stonefang Dungeon")
            {
                GameUI.ColoredWrite("The walls are made of jagged, cold granite that seems to dampen all sound. It is a neutral, \n", ConsoleColor.Green);
                Thread.Sleep(1000);
                GameUI.ColoredWrite("unforgiving place where only raw strength and basic skill will see you through.\n", ConsoleColor.Green);
                Thread.Sleep(1000);
            }
            else if (m_dungeonName == "Magma Claw Lair")
            {
                GameUI.ColoredWrite("The air is thick with sulfur, and the heat makes it difficult to swing a heavy blade. \n", ConsoleColor.Green);
                Thread.Sleep(1000);
                GameUI.ColoredWrite("While the flames sap your physical energy, the intense heat actually empowers your destructive fire magic.\n", ConsoleColor.Green);
                Thread.Sleep(1000);
            }
            else if (m_dungeonName == "Champion’s Gate")
            {
                GameUI.ColoredWrite("This ancient arena was built to test the greatest heroes of the realm. The spirits of past warriors linger here,  \n", ConsoleColor.Green);
                Thread.Sleep(1000);
                GameUI.ColoredWrite("filling you with a supernatural surge of both physical and magical power.\n", ConsoleColor.Green);
                Thread.Sleep(1000);
            }
            else if (m_dungeonName == "Whisperwind Vault")
            {
                GameUI.ColoredWrite("A constant, ghostly breeze swirls through these halls, making it nearly impossible to focus on complex spells. \n", ConsoleColor.Green);
                Thread.Sleep(1000);
                GameUI.ColoredWrite("However, the wind seems to guide your weapon, making your physical strikes faster and more accurate.\n", ConsoleColor.Green);
                Thread.Sleep(1000);
            }

                Console.Write("Room Attributes: Physical Attk Nerf:");
            GameUI.ColoredWrite(GetDungeonStat(AttackNerf).ToString(), ConsoleColor.Blue);
            Console.Write(" Magic Nerf: ");
            GameUI.ColoredWrite(GetDungeonStat(MagicNerf).ToString(), ConsoleColor.Blue);
            Console.Write(" Attack Buff: ");
            GameUI.ColoredWrite("+" + GetDungeonStat(AttackBuff).ToString(), ConsoleColor.Blue);
            Console.Write(" Magic Buff: ");
            GameUI.ColoredWrite("+" + GetDungeonStat(MagicBuff).ToString() + "\n", ConsoleColor.Blue);
            Console.WriteLine("");

            myPlayer.BonusAttack(GetDungeonStat(AttackNerf) + GetDungeonStat(AttackBuff));
            myPlayer.BlizzardBonus(GetDungeonStat(MagicNerf) + GetDungeonStat(MagicBuff));


            if (firstDungeonCheck != finalDungeonCheck)
            {
                //Pick new enemy
                Random randEnemy = new Random();
                int randomEnemy = randEnemy.Next(0, 2);

                npcEnemy = new EnemyNpc(randomEnemy, firstDungeonCheck);
            }
            else
            {
                npcEnemy = new EnemyNpc(2, firstDungeonCheck);
            }

            Battle currentBattle = new Battle(myPlayer, npcEnemy, this);
            Thread.Sleep(1000);

        }

        private void DungeonType(int dungeonType)
        {
            dungeonStats = new int[6];

            switch (dungeonType)
            {
                case 0:
                    m_dungeonName = "Stonefang Dungeon";
                    dungeonStats = new int[] { 0, 0, 0, 0, 0, 0 };
                    break;
                case 1:
                    m_dungeonName = "Magma Claw Lair";
                    dungeonStats = new int[] { -1, 0, 0, 1, 0, 0 };
                    break;
                case 2:
                    m_dungeonName = "Champion’s Gate";
                    dungeonStats = new int[] { 0, 0, 1, 1, 0, 0 };
                    break;
                case 3:
                    m_dungeonName = "Whisperwind Vault";
                    dungeonStats = new int[] { 0, -1, 1, 0, 0, 0 };
                    break;
                case 4:
                    m_dungeonName = "Aether Gardens";
                    dungeonStats = new int[] { 0, 0, 0, 0, 1, 0 };
                    break;
                case 5:
                    m_dungeonName = "The Leyline Nexus";
                    dungeonStats = new int[] { 0, 0, 0, 0, 0, 1 };
                    break;

            }
        }

        public int GetDungeonStat(int index)
        {
            return dungeonStats[index];
        }
    }
}
