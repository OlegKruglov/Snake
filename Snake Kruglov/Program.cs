using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Media;
using Snake_Kruglov;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu();
            Console.ReadLine();
        }
        static void Menu()
        {
            string[] MenuTitles = { "Play-p", "View Scoreboard-vs", "Clear Scoreboard-cs" };
            int yOffSet = 14;
            for (int i = 0; i < MenuTitles.Length; i++)
            {
                Console.SetCursorPosition(53, yOffSet);
                Console.WriteLine(MenuTitles[i]);
                yOffSet++;
            }
            string input = Console.ReadLine();
            switch (input)
            {
                case "p":
                    Console.Clear();
                    PlayGame();
                    break;
                case "vs":
                    Console.Clear();
                    MenuScoreBoard();
                    break;
                case "cs":
                    Console.Clear();
                    ClearSB();
                    break;
                default:
                    Console.WriteLine("Wrong");
                    break;

            }
        }
        static void PlayGame()
        {
            Sounds myPlayer = new Sounds(".\\");
            Sounds MyPlayer2 = new Sounds(".\\");
            myPlayer.Play();
            Console.SetBufferSize(120, 30);


            Walls walls = new Walls(120, 30);
            walls.Draw();

            // Отрисовка точек
            Point p = new Point(4, 5, '*');
            Snake snake = new Snake(p, 4, Direction.RIGHT);
            snake.Draw();

            FoodCreator foodCreator = new FoodCreator(120, 30, '$');
            Point food = foodCreator.CreateFood();
            food.Draw();
            int points = 0;
            while (true)
            {
                Console.SetCursorPosition(56, 28);
                Console.WriteLine($" P: {points}");
                if (walls.IsHit(snake) || snake.IsHitTail())
                {
                    break;
                }
                if (snake.Eat(food))
                {
                    MyPlayer2.Play("PACMAN");
                    food = foodCreator.CreateFood();
                    food.Draw();
                    points++;
                }
                else
                {
                    snake.Move();
                }

                Thread.Sleep(100);
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    snake.HandleKey(key.Key);
                }
            }
            myPlayer.Play("OMG");
            WriteGameOver();
            ScoreBoard(points);
        }

        static void WriteGameOver()
        {
            int xOffset = 25;
            int yOffset = 8;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(xOffset, yOffset++);
            WriteText("============================", xOffset, yOffset++);
            WriteText("И Г Р А О К О Н Ч Е Н А", xOffset + 1, yOffset++);
            yOffset++;
            WriteText("Автор: Олег Круглов", xOffset + 2, yOffset++);
            WriteText("============================", xOffset, yOffset++);
        }
        static void MenuScoreBoard()
        {
            int xOffset = 25;
            int yOffset = 8;
            string[] sr = System.IO.File.ReadAllLines("Nimetus.txt");
            string[] psr = System.IO.File.ReadAllLines("Points.txt");

            int[] psr2 = new int[psr.Length];
            for (int i = 0; i < psr.Length; i++)
            {
                psr2[i] = int.Parse(psr[i]);
            }

            int temp = 0;
            string temp2 = "";

            for (int write = 0; write < psr.Length; write++)
            {
                for (int sort = 0; sort < psr.Length - 1; sort++)
                {
                    if (psr2[sort] < psr2[sort + 1])
                    {
                        temp = psr2[sort + 1];
                        psr2[sort + 1] = psr2[sort];
                        psr2[sort] = temp;

                        temp2 = sr[sort + 1];
                        sr[sort + 1] = sr[sort];
                        sr[sort] = temp2;
                    }
                }
            }
            if (sr.Length != 0)
            {
                if (sr.Length <= 5)
                {
                    for (int i = 0; i < sr.Length; i++)
                    {
                        Console.SetCursorPosition(xOffset, yOffset++);
                        Console.WriteLine($"{sr[i]}: {psr2[i]}");
                        yOffset++;
                    }
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Console.SetCursorPosition(xOffset, yOffset++);
                        Console.WriteLine($"{sr[i]}: {psr2[i]}");
                        yOffset++;
                    }
                }
            }
            else
            {
                Console.WriteLine("Scoreboard is empty!");
            }
        }
        static void ClearSB()
        {
            System.IO.File.WriteAllText("Nimetus.txt", string.Empty);
            System.IO.File.WriteAllText("Points.txt", string.Empty);
            Console.WriteLine("Scoreboard is cleared");
        }
        static void ScoreBoard(int point)
        {
            System.Threading.Thread.Sleep(3000);
        again:
            Console.Clear();
            int xOffset = 25;
            int yOffset = 8;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(xOffset, yOffset++);
            Console.Write("Name : ");
            string input = Console.ReadLine();
            if (input.Length < 3) goto again;
            StreamWriter sw = new StreamWriter("Nimetus.txt", true);
            sw.WriteLine($"{input}");
            sw.Close();
            StreamWriter psw = new StreamWriter("Points.txt", true);
            psw.WriteLine($"{point}");
            psw.Close();
            Console.Clear();

            string[] sr = System.IO.File.ReadAllLines("Nimetus.txt");
            string[] psr = System.IO.File.ReadAllLines("Points.txt");

            int[] psr2 = new int[psr.Length];
            for (int i = 0; i < psr.Length; i++)
            {
                psr2[i] = int.Parse(psr[i]);
            }

            int temp = 0;
            string temp2 = "";

            for (int write = 0; write < psr.Length; write++)
            {
                for (int sort = 0; sort < psr.Length - 1; sort++)
                {
                    if (psr2[sort] < psr2[sort + 1])
                    {
                        temp = psr2[sort + 1];
                        psr2[sort + 1] = psr2[sort];
                        psr2[sort] = temp;

                        temp2 = sr[sort + 1];
                        sr[sort + 1] = sr[sort];
                        sr[sort] = temp2;
                    }
                }
            }
            if (sr.Length <= 5)
            {
                for (int i = 0; i < sr.Length; i++)
                {
                    Console.SetCursorPosition(xOffset, yOffset++);
                    Console.WriteLine($"{sr[i]}: {psr2[i]}");
                    yOffset++;
                }
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.SetCursorPosition(xOffset, yOffset++);
                    Console.WriteLine($"{sr[i]}: {psr2[i]}");
                    yOffset++;
                }
            }
        }

        static void WriteText(String text, int xOffset, int yOffset)
        {
            Console.SetCursorPosition(xOffset, yOffset);
            Console.WriteLine(text);
        }

    }
}
