using System;
class Game
{
    // Constants for maze size and symbols
    const int MazeSize = 20;
    const char WallSymbol = '#';
    const char EmptySymbol = ' ';
    const char AmbulanceSymbol = 'A';
    const char PatientSymbol = 'P';
    // Coordinates for ambulance and patient
    int ambulanceX, ambulanceY;
    int patientX, patientY;
    // Maze array
    char[,] maze;
    // Variables for game state
    int score;
    bool gameOver;
    public void Play()
    {
        InitializeMaze();
        while (!gameOver)
        {
            Console.Clear();
            DrawMaze();
            // Get user input for ambulance movement
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            if (CanAmbulanceMove(keyInfo.Key))
            {
                MoveAmbulance(keyInfo.Key);
                CheckCollision();
            }
        }
        Console.WriteLine("Game over! Final score: " + score);
    }
    void InitializeMaze()
    {
        // Create maze
        maze = new char[MazeSize, MazeSize];
        // Generate random walls
        Random random = new Random();
        for (int i = 0; i < MazeSize; i++)
        {
            for (int j = 0; j < MazeSize; j++)
            {
                if (random.NextDouble() < 0.2)
                {
                    maze[i, j] = WallSymbol;
                }
                else
                {
                    maze[i, j] = EmptySymbol;
                }
            }
        }
        // Place ambulance and patient
        ambulanceX = random.Next(MazeSize);
        ambulanceY = random.Next(MazeSize);
        maze[ambulanceX, ambulanceY] = AmbulanceSymbol;
        patientX = random.Next(MazeSize);
        patientY = random.Next(MazeSize);
        maze[patientX, patientY] = PatientSymbol;
        score = 0;
        gameOver = false;
    }
    void DrawMaze()
    {
        for (int i = 0; i < MazeSize; i++)
        {
            for (int j = 0; j < MazeSize; j++)
            {
                Console.ForegroundColor = GetSymbolColor(maze[i, j]);
                Console.Write(maze[i, j]);
            }
            Console.WriteLine();
        }
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Score: " + score);
        Console.WriteLine("Use arrow keys to move the ambulance");
    }
    ConsoleColor GetSymbolColor(char symbol)
    {
        if (symbol == AmbulanceSymbol)
            return ConsoleColor.Red;
        else if (symbol == PatientSymbol)
            return ConsoleColor.Yellow;
        else if (symbol == WallSymbol)
            return ConsoleColor.Gray;
        else
            return ConsoleColor.Black;
    }
    bool CanAmbulanceMove(ConsoleKey key)
    {
        if (key == ConsoleKey.UpArrow)
        {
            return (ambulanceX > 0 && maze[ambulanceX - 1, ambulanceY] != WallSymbol);
        }
        else if (key == ConsoleKey.DownArrow)
        {
            return (ambulanceX < MazeSize - 1 && maze[ambulanceX + 1, ambulanceY] != WallSymbol);
        }
        else if (key == ConsoleKey.LeftArrow)
        {
            return (ambulanceY > 0 && maze[ambulanceX, ambulanceY - 1] != WallSymbol);
        }
        else if (key == ConsoleKey.RightArrow)
        {
            return (ambulanceY < MazeSize - 1 && maze[ambulanceX, ambulanceY + 1] != WallSymbol);
        }
        return false;
    }

    void MoveAmbulance(ConsoleKey key)
    {
        // Clear old ambulance position
        maze[ambulanceX, ambulanceY] = EmptySymbol;
        // Move ambulance
        if (key == ConsoleKey.UpArrow)
            ambulanceX--;
        else if (key == ConsoleKey.DownArrow)
            ambulanceX++;
        else if (key == ConsoleKey.LeftArrow)
            ambulanceY--;
        else if (key == ConsoleKey.RightArrow)
            ambulanceY++;
        // Place ambulance in new position
        maze[ambulanceX, ambulanceY] = AmbulanceSymbol;
    }
    void CheckCollision()
    {
        // Check if ambulance reached patient
        if (ambulanceX == patientX && ambulanceY == patientY)
        {
            score++;
            if (score >= 10)
            {
                gameOver = true;
            }
            else
            {
                // Generate new patient position
                Random random = new Random();
                while (true)
                {
                    patientX = random.Next(MazeSize);
                    patientY = random.Next(MazeSize);
                    if (maze[patientX, patientY] == EmptySymbol)
                    {
                        maze[patientX, patientY] = PatientSymbol;
                        break;
                    }
                }
            }
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        Game game = new Game();
        game.Play();
    }
}
