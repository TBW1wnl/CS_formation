namespace TD_Morpion;

public sealed class Board
{
    public Symbol[,] Cells { get; init; }
    public int Size { get; init; }
    private int MaxPosition => Size * Size;

    public Board(in int size)
    {
        Size = size;
        Cells = new Symbol[size, size];
        Initialize();
    }

    public void Initialize()
    {
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                Cells[i, j] = Symbol.Empty;
            }
        }
    }

    public bool TryPlaceSymbol(in int position, in Symbol symbol, out string? reason)
    {
        reason = null;
        if (!IsValidPosition(position))
        {
            reason = $"Position invalide ! Choisissez entre 1 et {MaxPosition}.";
            return false;
        }

        var pos = PositionToCoordinates(position);
        if (Cells[pos.Row, pos.Col] != Symbol.Empty)
        {
            reason = "Cette case est déjà occupée !";
            return false;
        }

        Cells[pos.Row, pos.Col] = symbol;
        return true;
    }

    private bool IsValidPosition(in int position) => position >= 1 && position <= MaxPosition;

    private CellPos PositionToCoordinates(in int position)
    {
        int line = Size - 1 - (position - 1) / Size;
        int column = (position - 1) % Size;
        return new CellPos(line, column);
    }

    public bool IsFull() => Cells.Cast<Symbol>().All(cell => cell != Symbol.Empty);



    public IEnumerable<(int row, int col)> GetFreePositions() =>
        from i in Enumerable.Range(0, Size)
        from j in Enumerable.Range(0, Size)
        where Cells[i, j] == Symbol.Empty
        select (i, j);

    public bool HasWinner(in Symbol symbol)
    {
        for (int i = 0; i < Size; i++)
        {
            bool rowWin = true;
            for (int j = 0; j < Size; j++)
            {
                if (Cells[i, j] != symbol)
                {
                    rowWin = false;
                    break;
                }
            }
            if (rowWin) return true;

            bool colWin = true;
            for (int j = 0; j < Size; j++)
            {
                if (Cells[j, i] != symbol)
                {
                    colWin = false;
                    break;
                }
            }
            if (colWin) return true;
        }

        bool diagWin1 = true;
        for (int i = 0; i < Size; i++)
        {
            if (Cells[i, i] != symbol)
            {
                diagWin1 = false;
                break;
            }
        }
        if (diagWin1) return true;

        bool diagWin2 = true;
        for (int i = 0; i < Size; i++)
        {
            if (Cells[i, Size - 1 - i] != symbol)
            {
                diagWin2 = false;
                break;
            }
        }
        if (diagWin2) return true;

        return false;
    }
}