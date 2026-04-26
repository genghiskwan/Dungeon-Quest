using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Quest
{
    internal class Battle
    {
        Attack attackAmount = new Attack();
        string moveInput = "";
        int moveInputInt;
        //int enemyHealth = 0;
        public Battle(PlayerCharacter myPlayer, EnemyNpc npcEnemy, Dungeon dungeon)
        {
            int round = 1;

            if (npcEnemy.NpcType() == "Orc")
            {
                GameUI.ColoredWrite($"An {npcEnemy.NpcType()} has appeared!\n", ConsoleColor.Red);                
                Console.WriteLine("");
            }
            else if (npcEnemy.NpcType() == "Wraith")
            {
                
                GameUI.ColoredWrite($"A {npcEnemy.NpcType()} has appeared!\n", ConsoleColor.Red);                
                Console.WriteLine("");
            }
            else if (npcEnemy.NpcType() == "Xulgath the Soul Eater")
            {
                GameUI.ColoredWrite("====================================================\n", ConsoleColor.Red);
                GameUI.ColoredWrite($"A {npcEnemy.NpcType()} has appeared!\n", ConsoleColor.Red);
                GameUI.ColoredWrite("====================================================\n", ConsoleColor.Red);
                Console.WriteLine("");
                GameUI.ColoredWrite("The last orc falls, but the air suddenly turns heavy.\n", ConsoleColor.Red);
                Thread.Sleep(2000);
                GameUI.ColoredWrite("A shadow stretches across the stone as Xulgath emerges from the dark.\n", ConsoleColor.Red);
                Thread.Sleep(2000);
                GameUI.ColoredWrite("His armor clanks like a funeral bell as he draws a jagged blade\n", ConsoleColor.Red);
                Thread.Sleep(2000);
                GameUI.ColoredWrite("\nYou tighten your grip, shield raised and heart pounding.\n", ConsoleColor.Red);
                Thread.Sleep(2000);
                GameUI.ColoredWrite("This is the end of the line.", ConsoleColor.Red);
                Thread.Sleep(2000);
                Console.WriteLine("");
            }

                while (myPlayer.GetHealth() > 0)
            {
                myPlayer.Block(0);
                string haveShield = myPlayer.HasShield() ? "Equipped" : "Unequipped";

                GameUI.ColoredWrite("====================================================\n", ConsoleColor.Green);
                GameUI.ColoredWrite($"Round {round}\n", ConsoleColor.Green);
                GameUI.ColoredWrite("====================================================", ConsoleColor.Green);
                Console.WriteLine("");
                Console.Write($"{myPlayer.GetCharacterName()} Health: ");
                GameUI.ColoredWrite(myPlayer.GetHealth().ToString(), ConsoleColor.Blue);
                Console.Write(" Mana: ");
                GameUI.ColoredWrite(myPlayer.GetMana().ToString() + "\n", ConsoleColor.Blue);
                Console.Write($"{myPlayer.GetCharacterName()} Attack: Sword Slash: ");
                GameUI.ColoredWrite(myPlayer.PlayerAttack().ToString(), ConsoleColor.Blue);
                Console.Write(" Blizzard: ");
                GameUI.ColoredWrite(myPlayer.GetBlizzard().ToString() + "\n", ConsoleColor.Blue);
                Console.Write("Inventory: Health Potion: ");
                GameUI.ColoredWrite(myPlayer.GetHealthPotion().ToString(), ConsoleColor.Blue);
                Console.Write(" Mana Potion: ");
                GameUI.ColoredWrite(myPlayer.GetManaPotion().ToString(), ConsoleColor.Blue);
                Console.Write(" Shield: ");
                GameUI.ColoredWrite(haveShield + "\n", ConsoleColor.Blue);
                Console.WriteLine("");
                Console.Write($"{npcEnemy.NpcType()}'s Health ");
                GameUI.ColoredWrite(npcEnemy.GetStat(EnemyNpc.Health).ToString(), ConsoleColor.Blue);
                Console.Write(" Attack: ");
                GameUI.ColoredWrite(npcEnemy.GetStat(EnemyNpc.Attack).ToString(), ConsoleColor.Blue);
                Console.Write(" Skill: ");
                GameUI.ColoredWrite(npcEnemy.GetStat(EnemyNpc.Skill).ToString(), ConsoleColor.Blue);
                Console.Write(" Mana: ");
                GameUI.ColoredWrite(npcEnemy.GetStat(EnemyNpc.Mana).ToString(), ConsoleColor.Blue);
                Console.Write(" Magic Resistance: ");
                GameUI.ColoredWrite((-npcEnemy.GetStat(EnemyNpc.MagicTakeBonus)).ToString(), ConsoleColor.Blue);
                Console.Write(" Attack Resistance: ");
                GameUI.ColoredWrite((-npcEnemy.GetStat(EnemyNpc.AttackTakeBonus)).ToString() + "\n", ConsoleColor.Blue);
                Console.WriteLine("");
                Console.WriteLine("Choose your Attack. [Slash: 1] [Blizzard: 2] [Block: 3] [Health Potion: 4] [Mana Potion: 5]");
                Console.WriteLine("");

                moveInput = Console.ReadLine();
                Console.WriteLine("");

                while (!int.TryParse(moveInput, out moveInputInt) || (moveInputInt < 0 && moveInputInt > 5))
                {
                    // make sure to get a valid input
                    Console.WriteLine("Choose Again");
                    moveInput = Console.ReadLine() ?? "";
                    Console.WriteLine("");
                }

                switch (moveInputInt)
                {
                    // player's decision
                    case 1:
                        Console.WriteLine($"{myPlayer.GetCharacterName()} lands attack!");
                        attackAmount.PlayerPhysicalAttack(myPlayer, npcEnemy);
                        break;
                    case 2:
                        if (myPlayer.GetMana() < 4)
                        {
                            Console.WriteLine("Not Enough Mana!");
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"{myPlayer.GetCharacterName()} casts Blizzard!");
                            attackAmount.PlayerMagicAttack(myPlayer, npcEnemy);
                            myPlayer.Mana(-4);
                            break;
                        }
                    case 3:
                        Console.WriteLine($"{myPlayer.GetCharacterName()} defends!");
                        myPlayer.Block(1);
                        break;
                    case 4:
                        if (myPlayer.GetHealthPotion() > 0)
                        {
                            myPlayer.HealthPotion();
                            Console.Write($"{myPlayer.GetCharacterName()} Health: ");
                            GameUI.ColoredWrite(myPlayer.GetHealth().ToString() + "\n", ConsoleColor.Blue);
                            Console.WriteLine("");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("No Health Potion in inventory!");
                            break;
                        }
                    case 5:
                        if (myPlayer.GetManaPotion() > 0)
                        {
                            myPlayer.ManaPotion();
                            Console.Write($"{myPlayer.GetCharacterName()} Mana: ");
                            GameUI.ColoredWrite(myPlayer.GetMana().ToString() + "\n", ConsoleColor.Blue);
                            Console.WriteLine("");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("No Mana Potion in inventory!");
                            Console.WriteLine("");
                            break;
                        }
                }

                if (npcEnemy.GetStat(EnemyNpc.Health) <= 0)
                {
                    //check npc's health
                    myPlayer.BonusAttack(2);
                    myPlayer.BlizzardBonus(2);
                    myPlayer.IncreaseHealth();
                    myPlayer.HealthLvlUp();

                    GameUI.ColoredWrite(npcEnemy.NpcType() + " has died!" + "\n", ConsoleColor.Green);
                    Console.Write($"{myPlayer.GetCharacterName()} has leveled up! Health Heal:");
                    GameUI.ColoredWrite("+4", ConsoleColor.Blue);
                    Console.Write(" Max Health: ");
                    GameUI.ColoredWrite("+2", ConsoleColor.Blue);
                    Console.Write(" Sword Slash: ");
                    GameUI.ColoredWrite("+2", ConsoleColor.Blue);
                    Console.Write(" Blizzard: ");
                    GameUI.ColoredWrite("+2" + "\n", ConsoleColor.Blue);
                    Console.WriteLine("");
                    Thread.Sleep(1000);

                    Random rand = new Random();
                    int randomDrop = rand.Next(0, 3);

                    if (npcEnemy.NpcType() != "Xulgath the Soul Eater")
                    {
                        if (randomDrop == 2 && !myPlayer.HasShield())
                        {
                            // shield drop
                            myPlayer.ShieldEquipped();
                            Console.WriteLine($"{myPlayer.GetCharacterName()} found a Shield! Defense +1");
                            Console.WriteLine("");
                            Thread.Sleep(1000);

                        }
                        else
                        {
                            randomDrop = rand.Next(0, 2);


                            if (randomDrop == 0)
                            {
                                //health and mana drop
                                myPlayer.AddHealtPotion();
                                Console.WriteLine($"{myPlayer.GetCharacterName()} found a Health Potion!");
                                Console.WriteLine("");
                            }
                            else
                            {
                                myPlayer.AddManaPotion();
                                Console.WriteLine($"{myPlayer.GetCharacterName()} found a Mana Potion!");
                                Console.WriteLine("");
                            }

                            Thread.Sleep(1000);
                        }
                    }

                    myPlayer.BonusAttack(-dungeon.GetDungeonStat(Dungeon.AttackNerf) - dungeon.GetDungeonStat(Dungeon.AttackBuff));
                    myPlayer.BlizzardBonus(-dungeon.GetDungeonStat(Dungeon.MagicNerf) + -dungeon.GetDungeonStat(Dungeon.MagicBuff));

                    break;
                }

                if (npcEnemy.GetStat(EnemyNpc.TypeIndex) == 0)
                {
                    // Orc Npc attack
                    Console.Write($"{npcEnemy.NpcType()}'s Health ");
                    GameUI.ColoredWrite(npcEnemy.GetStat(EnemyNpc.Health).ToString() + "\n", ConsoleColor.Blue);
                    Console.WriteLine("");
                    Console.WriteLine($"{npcEnemy.NpcType()} Attacks!");

                    attackAmount.OrcAttackMethod(myPlayer, npcEnemy);

                    Console.WriteLine("");
                    Thread.Sleep(1000);
                }
                else if (npcEnemy.GetStat(EnemyNpc.TypeIndex) == 1)
                {
                    //Wraifh Npc Attack
                    Console.Write($"{npcEnemy.NpcType()}'s Health ");
                    GameUI.ColoredWrite(npcEnemy.GetStat(EnemyNpc.Health).ToString() + "\n", ConsoleColor.Blue);
                    Console.WriteLine("");

                    attackAmount.WraithAttackMethod(myPlayer, npcEnemy);

                    Console.WriteLine("");
                    Thread.Sleep(1000);
                }

                round++;
            }

            if (myPlayer.GetHealth() < 1)
            {
                Console.Write($"{myPlayer.GetCharacterName()} Health: ");
                GameUI.ColoredWrite(myPlayer.GetHealth().ToString() + "\n", ConsoleColor.Red);
                Console.WriteLine($"{myPlayer.GetCharacterName()} dies alone in this dungeon with other previous adventurer's remains.");
                Console.WriteLine("");
            }
        }
    }
}