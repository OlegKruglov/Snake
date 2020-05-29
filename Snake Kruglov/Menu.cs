using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Snake_Kruglov;

namespace Snake_Kruglov
{
    public class Menu
    {
        public void MainMenu()
        {
            Score score = new Score();
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
                    score.MenuScoreBoard();
                    break;
                case "cs":
                    Console.Clear();
                    score.ClearSB();
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


            Snake.Walls walls = new Snake.Walls(120, 30);
            walls.Draw();

            // Отрисовка точек
            Snake.Point p = new Snake.Point(4, 5, '*');
            Snake.Snake snake = new Snake.Snake(p, 4, Snake.Direction.RIGHT);
            snake.Draw();

            Snake.FoodCreator foodCreator = new Snake.FoodCreator(120, 30, '$');
            Snake.Point food = foodCreator.CreateFood();
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
            Score score = new Score();
            score.ScoreBoard(points);
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
        static void WriteText(String text, int xOffset, int yOffset)
        {
            Console.SetCursorPosition(xOffset, yOffset);
            Console.WriteLine(text);
        }
    }
}
