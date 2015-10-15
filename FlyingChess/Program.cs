using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyingChess
{
    class Program
    {
        //Game map
        public static int[] Map = new int[100];
        //Positions of Gamer A and Gamer B in map
        public static int[] GamerPos = new int[2];
        //Names of Gamer A and Gamer B in map
        public static string[] GamerNames = new string[2];
        //Flags of two gamers (handle pause operation)
        public static bool[] Flags = new bool[2];
        static void Main(string[] args)
        {
            //Game title
            GameShow();
            #region Input gamer names
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Please input name of gamer A");
            GamerNames[0] = Console.ReadLine();
            while (GamerNames[0] == "")
            {
                Console.WriteLine("The name of gamer A can not be empty, please input.");
                GamerNames[0] = Console.ReadLine();
            }
            Console.WriteLine("Please input name of gamer B");
            GamerNames[1] = Console.ReadLine();
            while (GamerNames[1] == "" || GamerNames[1] == GamerNames[0])
            {
                if (GamerNames[1] == "")
                {
                    Console.WriteLine("The name of gamer B can not be empty, please input.");
                    GamerNames[1] = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("The name of gamer B can not be same as the name of gamer A, please input again.");
                    GamerNames[1] = Console.ReadLine();
                }
            }
            #endregion
            Console.Clear();
            GameShow();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Gamer A:{0}", GamerNames[0]);
            Console.WriteLine("Gamer B:{0}", GamerNames[1]);
            InitMap();
            DrawMap();
            //The flying chess game will continue if both gamer A and gamer B do not get to destination
            while (GamerPos[0] < 99 && GamerPos[1] < 99)
            {
                if (Flags[0] == false)
                {
                    PlayGame(0);
                }
                else
                {
                    Flags[0] = false;
                }
                if (GamerPos[0] >= 99)
                {
                    Console.WriteLine("{0} is winner!!!", GamerNames[0]);
                    break;
                }

                if (Flags[1] == false)
                {
                    PlayGame(1);
                }
                else
                {
                    Flags[1] = false;
                }
                if (GamerPos[1] >= 99)
                {
                    Console.WriteLine("{0} is winner!!!", GamerNames[1]);
                    break;
                }
            }
            Console.WriteLine("END, THANK YOU FOR PLAYING!");
            Console.ReadKey();
        }
        /// <summary>
        /// Generate title of game
        /// </summary>
        public static void GameShow()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*************************************");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("*************************************");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("**********Flying Chess Game**********");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("*************************************");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("*************************************");
        }
        /// <summary>
        /// Initialize game map
        /// </summary>
        public static void InitMap()
        {
            /* In map array, using 0-4 to represent different items.
               0:Ordinary Block □
               1:Lucky Roulette ◎ 
               2:Land Mine ☆
               3:Pause ▲
               4:Time Tunnel 卐 */

            int[] luckyRoulette = { 6, 23, 40, 55, 69, 83 };//Lucky Roulette ◎
            for (int i = 0; i < luckyRoulette.Length; i++)
            {
                Map[luckyRoulette[i]] = 1;
            }

            int[] landMine = { 5, 13, 17, 33, 38, 50, 64, 80, 94 };//Land Mine ☆
            for (int i = 0; i < landMine.Length; i++)
            {
                Map[landMine[i]] = 2;
            }

            int[] pause = { 9, 27, 60, 93 };//Pause ▲
            for (int i = 0; i < pause.Length; i++)
            {
                Map[pause[i]] = 3;
            }

            int[] timeTunnel = { 20, 25, 45, 63, 72, 88, 90 };//Time Tunnel 卐
            for (int i = 0; i < timeTunnel.Length; i++)
            {
                Map[timeTunnel[i]] = 4;
            }
        }
        /// <summary>
        /// Draw map
        /// </summary>
        public static void DrawMap()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Lucky Roulette: ◎ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Land Mine: ☆ ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Pause: ▲ ");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("Time Tunnel: 卐");
            Console.WriteLine();
            # region Draw the first horizontal line
            for (int i = 0; i < 30; i++)
            {
                Console.Write(DrawStringMap(i));
            }
            #endregion
            Console.WriteLine();
            #region Draw the first vertical line
            for (int i = 30; i < 35; i++)
            {
                for (int j = 0; j <= 28; j++)
                {
                    Console.Write("  ");
                }
                Console.Write(DrawStringMap(i));
                Console.WriteLine();
            }
            #endregion
            #region Draw the second horizontal line
            for (int i = 64; i >= 35; i--)
            {
                Console.Write(DrawStringMap(i));
            }
            #endregion
            Console.WriteLine();
            #region Draw the second vertical line
            for (int i = 65; i <= 69; i++)
            {
                Console.WriteLine(DrawStringMap(i));
            }
            #endregion
            #region Draw the third horizontal line
            for (int i = 70; i <= 99; i++)
            {
                Console.Write(DrawStringMap(i));
            }
            #endregion
            Console.WriteLine();
        }
        /// <summary>
        /// A method that gets item string from a specific position on map 
        /// </summary>
        /// <param name="i">a specific position on map</param>
        /// <returns>item icon</returns>
        public static string DrawStringMap(int i)
        {
            #region Get map item
            string str = "";
            //The position of Gamer A equals to the position of Gamer B and both A and B are not outside of game map, draw "<>" on map
            if (GamerPos[0] == GamerPos[1] && GamerPos[0] == i)
            {
                str = "<>";
            }
            else if (GamerPos[0] == i)
            {
                str = "Ａ";//Draw Gamer A on map
            }
            else if (GamerPos[1] == i)
            {
                str = "Ｂ";//Draw Gamer B on map
            }
            else
            {
                switch (Map[i])
                {
                    //Draw items on map
                    case 0:
                        Console.ForegroundColor = ConsoleColor.White;
                        str = "□";
                        break;
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Green;
                        str = "◎";
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Red;
                        str = "☆";
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        str = "▲";
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        str = "卐";
                        break;
                }
            }
            return str;
            #endregion
        }
        /// <summary>
        /// Play game
        /// </summary>
        public static void PlayGame(int gamerNumber)
        {
            Random r = new Random();
            int rNumber = r.Next(1, 7);
            Console.WriteLine("{0} starts throwing dice by pressing any key.", GamerNames[gamerNumber]);
            Console.ReadKey(true);
            Console.WriteLine("{0} finishes throwing dice. The dice nmber is {1}", GamerNames[gamerNumber], rNumber);
            GamerPos[gamerNumber] += rNumber;
            ChangePos();
            Console.ReadKey(true);
            Console.WriteLine("{0} strarts moving by pressing any key.", GamerNames[gamerNumber]);
            Console.ReadKey(true);
            Console.WriteLine("{0} finishes moving.", GamerNames[gamerNumber]);
            Console.ReadKey(true);
            // Game rules
            if (GamerPos[gamerNumber] == GamerPos[1 - gamerNumber])
            {
                Console.WriteLine("Gamer {0} steps on gamer {1}, gamer {2} will back up 6 steps.", GamerNames[gamerNumber], GamerNames[1 - gamerNumber], GamerNames[1 - gamerNumber]);
                GamerPos[1 - gamerNumber] -= 6;
                ChangePos();
                Console.ReadKey(true);
            }
            else
            {
                switch (Map[GamerPos[gamerNumber]])
                {
                    case 0:
                        Console.WriteLine("Gamer {0} steps on ordinary block, nothing happens.", GamerNames[gamerNumber]);
                        Console.ReadKey(true);
                        break;
                    case 1:
                        Console.WriteLine("Gamer {0} steps on lucky roulette, please choose 1--switch position 2-- bomb the other gamer.", GamerNames[gamerNumber]);
                        string input = Console.ReadLine();
                        while (true)
                        {
                            if (input == "1")
                            {
                                Console.WriteLine("Gamer {0} chooses to switch position with gamer {1}", GamerNames[gamerNumber], GamerNames[1 - gamerNumber]);
                                Console.ReadKey(true);
                                int temp = GamerPos[gamerNumber];
                                GamerPos[gamerNumber] = GamerPos[1 - gamerNumber];
                                GamerPos[1 - gamerNumber] = temp;
                                Console.WriteLine("Switch complete!!!Continue playing game by pressing any key.");
                                Console.ReadKey(true);
                                break;
                            }
                            else if (input == "2")
                            {
                                Console.WriteLine("Gamer {0} chooses to bomb gamer {1}, gamer {2} will back up 6 steps.", GamerNames[gamerNumber], GamerNames[1 - gamerNumber], GamerNames[1 - gamerNumber]);
                                Console.ReadKey(true);
                                GamerPos[1 - gamerNumber] -= 6;
                                ChangePos();
                                Console.WriteLine("gamer {0} backs up 6 steps.", GamerNames[1 - gamerNumber]);
                                Console.ReadKey(true);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("You can only input 1 or 2, 1--switch position 2-- bomb the other gamer.");
                                input = Console.ReadLine();
                            }
                        }
                        break;

                    case 2:
                        Console.WriteLine("Gamer {0} steps on land mine and will back up 6 steps.", GamerNames[gamerNumber]);
                        Console.ReadKey(true);
                        GamerPos[gamerNumber] -= 6;
                        ChangePos();
                        break;
                    case 3:
                        Console.WriteLine("Gamer {0} steps on pause and will be paused for a round.", GamerNames[gamerNumber]);
                        Flags[gamerNumber] = true;
                        Console.ReadKey(true);
                        break;
                    case 4:
                        Console.WriteLine("Gamer {0} steps on time tunnel and will step forward 10 steps.", GamerNames[gamerNumber]);
                        GamerPos[gamerNumber] += 10;
                        ChangePos();
                        Console.ReadKey(true);
                        break;
                }
            }
            Console.Clear();
            DrawMap();
        }
        /// <summary>
        /// Prevent gamers from kicking off the map
        /// </summary>
        public static void ChangePos()
        {
            if (GamerPos[0] < 0)
            {
                GamerPos[0] = 0;
            }
            if (GamerPos[0] >= 99)
            {
                GamerPos[0] = 99;
            }
            if (GamerPos[1] < 0)
            {
                GamerPos[1] = 0;
            }
            if (GamerPos[1] >= 99)
            {
                GamerPos[1] = 99;
            }
        }

    }
}
