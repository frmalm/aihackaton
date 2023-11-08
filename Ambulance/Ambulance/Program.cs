using System;
using System.Collections.Generic;
using System.Threading;
class Program
{
    static int width = 19;
    static int height = 10;
    static int ambulanceX = 0;
    static int ambulanceY = 0;
    static int patientX = 0;
    static int patientY = 0;
    static int score = 0;
    static List<string> maze = new List<string>
    {
        "###################",
        "#                 #",
        "# # # ####### # # #",
        "#   #          #  #",
        "#### ### ### # #  #",
        "#        #   # #  #",
        "# ### # ### # #   #",
        "#   # #        ## #",
        "#                 #",
        "###################"
    };
    static void Main(string[] args)
    {
        Console.Title = "Ambulance Maze Game";
        Console.CursorVisible = false;
        DrawMaze();
        GeneratePatient();
        while (score < 10)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(intercept: true).Key;
                MoveAmbulance(key);
                CheckCollision();
            }
        }
        Console.Clear();
        Console.WriteLine("Congratulations! You have reached all the patients.");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
    static void DrawMaze()
    {
        Console.Clear();
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(maze[y][x]);
            }
        }
        Console.SetCursorPosition(ambulanceX, ambulanceY);
        Console.Write("A");
    }
    static void GeneratePatient()
    {
        Random random = new Random();
        int x, y;
        do
        {
            x = random.Next(1, width - 1);
            y = random.Next(1, height - 1);
        } while (maze[y][x] != ' ');
        patientX = x;
        patientY = y;
        Console.SetCursorPosition(patientX, patientY);
        Console.Write("P");
    }
    static void MoveAmbulance(ConsoleKey key)
    {
        int newAmbulanceX = ambulanceX;
        int newAmbulanceY = ambulanceY;
        switch (key)
        {
            case ConsoleKey.UpArrow:
                newAmbulanceY--;
                break;
            case ConsoleKey.DownArrow:
                newAmbulanceY++;
                break;
            case ConsoleKey.LeftArrow:
                newAmbulanceX--;
                break;
            case ConsoleKey.RightArrow:
                newAmbulanceX++;
                break;
            default:
                return;
        }
        if (maze[newAmbulanceY][newAmbulanceX] == '#')
            return;
        Console.SetCursorPosition(ambulanceX, ambulanceY);
        Console.Write(" ");
        ambulanceX = newAmbulanceX;
        ambulanceY = newAmbulanceY;
        Console.SetCursorPosition(ambulanceX, ambulanceY);
        Console.Write("A");
    }
    static void CheckCollision()
    {
        if (ambulanceX == patientX && ambulanceY == patientY)
        {
            score++;
            Console.SetCursorPosition(patientX, patientY);
            Console.Write(" ");
            GeneratePatient();
        }
    }
}
