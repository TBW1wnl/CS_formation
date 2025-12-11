namespace TD_Morpion;

public sealed class ConsoleUserInterface : IUserInterface
{
    public void Render(in Board board)
    {
        Console.Clear();
        Console.WriteLine("=== MORPION ===\n");
        DisplayBoard(board);

        Console.WriteLine("\nPositions disponibles :");
        DisplayPositionGrid(board);

        Console.WriteLine();
    }

    public int AskMove(in IPlayer player)
    {
        Console.WriteLine($"Joueur {player.Name}, choisissez une position :");

        Console.Write("> ");
        string? input = Console.ReadLine();
        if (!int.TryParse(input, out int pos))
            return -1;

        return pos;
    }

    public void ShowInvalidInput()
    {
        Console.WriteLine("Entrée invalide !");
        Console.WriteLine("Appuyez sur une touche pour continuer...");
        Console.ReadKey();
    }

    public void ShowInvalidMove(in string? reason)
    {
        Console.WriteLine(reason ?? "Coup invalide !");
        Console.WriteLine("Appuyez sur une touche pour continuer...");
        Console.ReadKey();
    }

    public void ShowWin(in IPlayer player)
    {
        Console.WriteLine($"\nFélicitations au joueur {player.Name} !");
        Console.WriteLine("Appuyez sur une touche pour continuer...");
        Console.ReadKey();
    }

    public void ShowDraw()
    {
        Console.WriteLine("\nMatch nul ! Le plateau est plein.");
        Console.WriteLine("Appuyez sur une touche pour continuer...");
        Console.ReadKey();
    }

    public bool AskReplay()
    {
        Console.WriteLine("\nAppuyez sur une touche pour rejouer ou 'q' pour quitter...");
        ConsoleKeyInfo key = Console.ReadKey();

        return !(key.KeyChar == 'q' || key.KeyChar == 'Q');
    }

    private void DisplayBoard(in Board board)
    {
        int size = board.Size;

        for (int i = 0; i < size; i++)
        {
            Console.WriteLine(string.Join("|", Enumerable.Repeat("     ", size)));

            for (int j = 0; j < size; j++)
            {
                char symbol = (char)board.Cells[i, j];
                Console.Write($"  {symbol}  ");
                if (j < size - 1) Console.Write("|");
            }

            Console.WriteLine();
            Console.WriteLine(string.Join("|", Enumerable.Repeat("     ", size)));

            if (i < size - 1)
                Console.WriteLine(string.Join("+", Enumerable.Repeat("-----", size)));
        }
    }

    private void DisplayPositionGrid(in Board board)
    {
        for (int i = board.Size - 1; i >= 0; i--)
        {
            for (int j = 0; j < board.Size; j++)
            {
                int position = i * board.Size + j + 1;
                Console.Write($" {position,2} ");
                if (j < board.Size - 1)
                    Console.Write("|");
            }

            Console.WriteLine();

            if (i > 0)
                Console.WriteLine(string.Join("+", Enumerable.Repeat("----", board.Size)));
        }
    }
}
