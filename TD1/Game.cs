namespace TD_Morpion;

class Game
{
    private char[,] board;

    public Game()
    {
        board = new char[3, 3];
        Initialize();
    }

    public void Initialize()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                board[i, j] = ' ';
            }
        }
    }

    public void Display()
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
        Console.WriteLine(" 1 | 2 | 3 ");
        Console.WriteLine("---+---+---");
        Console.WriteLine(" 4 | 5 | 6 ");
        Console.WriteLine("---+---+---");
        Console.WriteLine(" 7 | 8 | 9 ");
    }

    public char GetBox(int ligne, int colonne)
    {
        return board[ligne, colonne];
    }
    
    public void SetBox(int ligne, int colonne, char symbole)
    {
        board[ligne, colonne] = symbole;
    }

    public bool IsEmptyBox(int position)
    {
        int[] coords = PositionToCoordinates(position);
        return board[coords[0], coords[1]] == ' ';
    }

    public bool PlayMove(int position, char symbole)
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

    public bool IsFull()
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

    private int[] PositionToCoordinates(int position)
    {
        int ligne = (position - 1) / 3;
        int colonne = (position - 1) % 3;
        return new int[] { ligne, colonne };
    }
}