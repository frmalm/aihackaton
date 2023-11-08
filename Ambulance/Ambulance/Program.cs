using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AmbulanceGame
{
    class Program
    {
        static int width = 60;
        static int height = 20;
        static char[,] maze;
        static int ambulanceX;
        static int ambulanceY;
        static int patientX;
        static int patientY;
        static int score;
        static bool running;
        static bool paused;
        static bool helpVisible;
        static void Main(string[] args)
        {
            InitializeGame();
            DrawMaze();
            while (running)
            {
                if (!paused)
                {
                    Update();
                    DrawMaze();
                }
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.Escape)
                    {
                        running = false;
                    }
                    else if (key == ConsoleKey.R)
                    {
                        InitializeGame();
                        DrawMaze();
                    }
                    else if (key == ConsoleKey.P)
                    {
                        paused = !paused;
                    }
                    else if (key == ConsoleKey.S)
                    {
                        paused = false;
                    }
                    else if (key == ConsoleKey.H)
                    {
                        helpVisible = !helpVisible;
                        DrawMaze();
                    }
                    else if (!paused)
                    {
                        MoveAmbulance(key);
                    }
                }
            }
        }
        static void InitializeGame()
        {
            maze = new char[height, width];
            ambulanceX = 0;
            ambulanceY = 0;
            patientX = 0;
            patientY = 0;
            score = 0;
            running = true;
            paused = false;
            helpVisible = false;
            GenerateMaze();
            PlaceAmbulance();
            PlacePatient();
        }
        static void GenerateMaze()
        {
            Random random = new Random();
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (i == 0 || i == height - 1 || j == 0 || j == width - 1)
                    {
                        maze[i, j] = '#';
                    }
                    else if (random.Next(100) < 20)
                    {
                        maze[i, j] = '#';
                    }
                    else
                    {
                        maze[i, j] = ' ';
                    }
                }
            }
        }
        static void PlaceAmbulance()
        {
            Random random = new Random();
            do
            {
                ambulanceX = random.Next(1, width - 1);
                ambulanceY = random.Next(1, height - 1);
            } while (maze[ambulanceY, ambulanceX] != ' ');
            maze[ambulanceY, ambulanceX] = 'A';
        }
        static void PlacePatient()
        {
            Random random = new Random();
            do
            {
                patientX = random.Next(1, width - 1);
                patientY = random.Next(1, height - 1);
            } while (maze[patientY, patientX] != ' ');
            maze[patientY, patientX] = 'P';
        }
        static void MoveAmbulance(ConsoleKey key)
        {
            int newAmbulanceX = ambulanceX;
            int newAmbulanceY = ambulanceY;
            if (key == ConsoleKey.UpArrow)
            {
                newAmbulanceY--;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                newAmbulanceY++;
            }
            else if (key == ConsoleKey.LeftArrow)
            {
                newAmbulanceX--;
            }
            else if (key == ConsoleKey.RightArrow)
            {
                newAmbulanceX++;
            }
            if (maze[newAmbulanceY, newAmbulanceX] == '#')
            {
                return;
            }
            if (newAmbulanceX == patientX && newAmbulanceY == patientY)
            {
                score++;
                PlacePatient();
            }
            maze[ambulanceY, ambulanceX] = ' ';
            ambulanceX = newAmbulanceX;
            ambulanceY = newAmbulanceY;
            maze[ambulanceY, ambulanceX] = 'A';
            if (score == 3)
            {
                running = false;
                Console.WriteLine("Congratulations, you won!");
            }
        }
        static void Update()
        {
            // Add any game logic here
        }
        static void DrawMaze()
        {
            Console.Clear();
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (ambulanceX == j && ambulanceY == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (patientX == j && patientY == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if (maze[i, j] == '#')
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else if (maze[i, j] == ' ')
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.Write(maze[i, j]);
                }
                Console.WriteLine();
            }
            if (helpVisible)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Var snabb och alert!!!!");
                Console.WriteLine("Keys available: UpArrow, DownArrow, LeftArrow, RightArrow, Escape, R, P, S, H");
            }
        }
    }
}
