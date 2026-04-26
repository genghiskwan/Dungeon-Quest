using System;

namespace Dungeon_Quest
{
    internal class MainGame
    {
        static List<int> VisitedDungeons = new List<int>();
        static void Main(string[] args)
        {
            bool shouldRestart = true;

            while (shouldRestart)
            {
                VisitedDungeons.Clear();

                GameUI.ColoredWrite("Warrior what is your name? We need your help!\n", ConsoleColor.Green);

                string characterName = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(characterName))
                {
                    Console.WriteLine("What is your real name warrior");
                    characterName = Console.ReadLine();
                }

                Console.WriteLine("");
                GameUI.ColoredWrite("An old man, trembling and covered in soot, grabs your arm.\n", ConsoleColor.Green);
                Thread.Sleep(1500);
                GameUI.ColoredWrite($"Please, {characterName}, you must help us!  These cursed dungeons have opened up all across our land,\n", ConsoleColor.Green);
                Thread.Sleep(1500);
                GameUI.ColoredWrite("and the orcs and wraiths crawl out from the dark to haunt our streets.\n", ConsoleColor.Green);
                Thread.Sleep(1500);
                GameUI.ColoredWrite("They serve ", ConsoleColor.Green);
                Thread.Sleep(2000);
                GameUI.ColoredWrite("Xulgath the Soul Eater", ConsoleColor.Red);
                Thread.Sleep(1500);
                GameUI.ColoredWrite(", a horror that feeds on the life of this valley.\n", ConsoleColor.Green);
                Thread.Sleep(1500);
                GameUI.ColoredWrite("We are but poor farmers... we have no gold to pay a hero...\n", ConsoleColor.Green);
                Thread.Sleep(1500);
                GameUI.ColoredWrite("But stay a while, and we will give you every scrap of food and iron we have left. Please... save our souls.\n", ConsoleColor.Green);
                Thread.Sleep(1500);
                Console.WriteLine("");
                GameUI.ColoredWrite("You draw your sword and enter the first dungeon...\n", ConsoleColor.Blue);
                Console.WriteLine("");


                PlayerCharacter myPlayer = new PlayerCharacter(characterName);
                Random random = new Random();
                int m_totalRooms = random.Next(5, 7); // creating amount of dungeon in this game

                for (int i = 0; i < m_totalRooms; i++)
                {
                    Dungeon dungeon = new Dungeon(myPlayer, VisitedDungeons, i, m_totalRooms - 1);

                    if (myPlayer.GetHealth() < 1)
                    {
                        break;
                    }

                    if (i == (m_totalRooms - 1))
                    {
                        GameUI.ColoredWrite("As the final echo of Xulgath's scream fades, the oppressive weight lifting from your chest.\n", ConsoleColor.Green);
                        Thread.Sleep(1500);
                        GameUI.ColoredWrite("The sun finally breaks through the blackened clouds, bathing the valley in gold once more.\n", ConsoleColor.Green);
                        Thread.Sleep(1500);
                        GameUI.ColoredWrite($"The villagers emerge from their homes, tears in their eyes. They surround you, not as a stranger, but as their savior.\n", ConsoleColor.Green);
                        Thread.Sleep(1500);
                        GameUI.ColoredWrite($"'The dungeons are sealed! {characterName}, you have done the impossible!'\n", ConsoleColor.Green);
                        Thread.Sleep(1500);
                        GameUI.ColoredWrite("'Our children will sleep without fear, and our songs will forever carry your name.'\n", ConsoleColor.Green);
                        Thread.Sleep(1500);
                        GameUI.ColoredWrite("The village is safe. You have won.\n", ConsoleColor.Green);
                        Thread.Sleep(1500);
                        Console.WriteLine("");
                        break;
                    }


                    GameUI.ColoredWrite("Dungeon Cleared!" + "\n", ConsoleColor.Green);
                    Console.WriteLine("");
                    Thread.Sleep(1000);


                }

                Console.WriteLine("Would like to replay? (y/n)");

                if (Console.ReadLine() != "y")
                {
                    Console.WriteLine("");
                    shouldRestart = false;
                }

            }

        }
    }
}
