namespace TD_Morpion;

class Game
{
    private readonly char[,] board;
    private char currentPlayer = 'X';
    private bool isGameFinished = false;

    public Game()
    {
        board = new char[3, 3];
        Initialize();
    }

    public void Start()
    {
        Initialize();
        GameLoop();
    }

    private void Initialize()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                board[i, j] = ' ';
            }
        }
    }

    private void GameLoop()
    {
        while (true)
        {
            Display();

            if (isGameFinished)
            {
                Console.WriteLine("Appuyez sur une touche pour rejouer ou 'q' pour quitter...");
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.KeyChar == 'q' || key.KeyChar == 'Q')
                {
                    break;
                }
                else
                {
                    Initialize();
                    isGameFinished = false;
                    currentPlayer = 'X';
                    continue;
                }
            }

            Console.WriteLine($"\nJoueur {currentPlayer}, choisissez une position (1-9) : ");

            if (int.TryParse(Console.ReadLine(), out int position))
            {
                if (PlayMove(position, currentPlayer))
                {
                    isGameFinished = IsGameFinished();
                    currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
                }
                else
                {
                    Console.WriteLine("Appuyez sur une touche pour réessayer...");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Entrée invalide ! Appuyez sur une touche pour réessayer...");
                Console.ReadKey();
            }
        }
    }

    private bool IsGameFinished()
    {
        if (HasCurrentPlayerWon())
        {
            Console.WriteLine($"Félicitation au joureur {currentPlayer} pour avoir gagné la partie !");
            return true;
        }
        if (IsFull())
        {
            Console.WriteLine("Match nul ! Le plateau est plein.");
            return true;
        }

        return false;
    }

    private void Display()
    {
        Console.Clear();
        Console.WriteLine("=== MORPION ===\n");

        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine("     |     |     ");
            Console.WriteLine($"  {board[i, 0]}  |  {board[i, 1]}  |  {board[i, 2]}  ");
            Console.WriteLine("     |     |     ");

            if (i < 2)
            {
                Console.WriteLine("-----+-----+-----");
            }
        }

        Console.WriteLine("\nPositions :");
        Console.WriteLine(" 7 | 8 | 9 ");
        Console.WriteLine("---+---+---");
        Console.WriteLine(" 4 | 5 | 6 ");
        Console.WriteLine("---+---+---");
        Console.WriteLine(" 1 | 2 | 3 ");
    }

    private char GetBox(int ligne, int colonne)
    {
        return board[ligne, colonne];
    }

    private void SetBox(int ligne, int colonne, char symbole)
    {
        board[ligne, colonne] = symbole;
    }

    private bool IsEmptyBox(int position)
    {
        int[] coords = PositionToCoordinates(position);
        return board[coords[0], coords[1]] == ' ';
    }

    private bool PlayMove(int position, char symbole)
    {
        if (position < 1 || position > 9)
        {
            Console.WriteLine("Position invalide ! Choisissez entre 1 et 9.");
            return false;
        }

        if (!IsEmptyBox(position))
        {
            Console.WriteLine("Cette case est déjà occupée !");
            return false;
        }

        int[] coords = PositionToCoordinates(position);
        board[coords[0], coords[1]] = symbole;
        return true;
    }

    private bool IsFull()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == ' ')
                {
                    return false;
                }
            }
        }
        return true;
    }

    private bool HasCurrentPlayerWon()
    {
        char symbole = currentPlayer;
        for (int i = 0; i < 3; i++)
        {
            if ((board[i, 0] == symbole && board[i, 1] == symbole && board[i, 2] == symbole) ||
                (board[0, i] == symbole && board[1, i] == symbole && board[2, i] == symbole))
            {
                return true;
            }
        }
        if ((board[0, 0] == symbole && board[1, 1] == symbole && board[2, 2] == symbole) ||
            (board[0, 2] == symbole && board[1, 1] == symbole && board[2, 0] == symbole))
        {
            return true;
        }
        return false;
    }

    private int[] PositionToCoordinates(int position)
    {
        int ligne = 2 - (position - 1) / 3;
        int colonne = (position - 1) % 3;
        return [ligne, colonne];
    }
}