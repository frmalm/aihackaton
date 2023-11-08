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
    bool showHelp;
    public void Play()
    {
        InitializeMaze();
        while (!gameOver)
        {
            Console.Clear();
            DrawMaze();
            // Get user input for game control
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.UpArrow || keyInfo.Key == ConsoleKey.DownArrow ||
                keyInfo.Key == ConsoleKey.LeftArrow || keyInfo.Key == ConsoleKey.RightArrow)
            {
                if (!showHelp && CanAmbulanceMove(keyInfo.Key))
                {
                    MoveAmbulance(keyInfo.Key);
                    CheckCollision();
                }
            }
            else if (keyInfo.Key == ConsoleKey.Escape)
            {
                gameOver = true;
            }
            else if (keyInfo.Key == ConsoleKey.R)
            {
                InitializeMaze();
            }
            else if (keyInfo.Key == ConsoleKey.P)
            {
                PauseGame();
            }
            else if (keyInfo.Key == ConsoleKey.S)
            {
                ResumeGame();
            }
            else if (keyInfo.Key == ConsoleKey.H)
            {
                ToggleHelp();
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
        showHelp = false;
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
        Console.WriteLine("Press 'R' to restart the game");
        Console.WriteLine("Press 'P' to pause the game");
        Console.WriteLine("Press 'S' to resume the game");
        Console.WriteLine("Press 'H' to toggle help");

        if (showHelp)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Var snabb och alert!!!!");
        }
    }
    ConsoleColor GetSymbolColor(char symbol)
    {
        if (symbol == AmbulanceSymbol)
            return ConsoleColor.Red;
        else if (symbol == PatientSymbol)
            return ConsoleColor.Yellow;
        else if (symbol == WallSymbol)
            return ConsoleColor.White;
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
    void PauseGame()
    {
        Console.Clear();
        DrawMaze();
        Console.WriteLine("Game paused. Press 'S' to resume.");
        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.S)
            {
                break;
            }
        }
    }
    void ResumeGame()
    {
        Console.Clear();
        DrawMaze();
    }
    void ToggleHelp()
    {
        showHelp = !showHelp;
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
