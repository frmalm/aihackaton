using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambulance
{
    internal class Program
    {
        static int score = 0;
        static bool gameOver = false;

        static int playerX = 0;
        static int playerY = 0;

        static int ghost1X = 5;
        static int ghost1Y = 5;

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(20, 20);
            Console.SetBufferSize(20, 20);

            DrawGame();

            while (!gameOver)
            {


                MovePlayer();
                MoveGhost();

                if (playerX == ghost1X && playerY == ghost1Y)
                {
                    gameOver = true;
                }

                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Score: " + score);

                if (gameOver)
                {
                    Console.WriteLine("Game Over!");
                }
            }
        }

        static void DrawGame()
        {
            Console.Clear();

            // Draw maze walls
            Console.WriteLine("--------------------");
            for (int y = 0; y < 20; y++)
            {
                Console.Write("|");
                for (int x = 0; x < 18; x++)
                {
                    if (x == playerX && y == playerY)
                    {
                        Console.Write("P"); // Player
                    }
                    else if (x == ghost1X && y == ghost1Y)
                    {
                        Console.Write("G"); // Ghost
                    }
                    else
                    {
                        Console.Write("."); // Dots
                    }
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("--------------------");
        }

        static void MovePlayer()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (playerY > 0)
                        {
                            playerY--;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (playerY < 19)
                        {
                            playerY++;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (playerX > 0)
                        {
                            playerX--;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (playerX < 17)
                        {
                            playerX++;
                        }
                        break;
                }
            }
        }

        static void MoveGhost()
        {
            Random random = new Random();

            int direction = random.Next(4);

            switch (direction)
            {
                case 0:
                    if (ghost1Y > 0)
                    {
                        ghost1Y--;
                    }
                    break;
                case 1:
                    if (ghost1Y < 19)
                    {
                        ghost1Y++;
                    }
                    break;
                case 2:
                    if (ghost1X > 0)
                    {
                        ghost1X--;
                    }
                    break;
                case 3:
                    if (ghost1X < 17)
                    {
                        ghost1X++;
                    }
                    break;
            }
        }
    }
}
