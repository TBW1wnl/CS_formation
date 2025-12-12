using System;
using System.Collections.Generic;
using System.Text;

namespace TD_Morpion;

public sealed class AIPlayer : IPlayer
{
    private readonly Random _rnd = new();
    public string Name => "AI Player";
    public Symbol Symbol { get; }

    public AIPlayer(Symbol symbol)
    {
        Symbol = symbol;
    }

    public async Task<int> MakeMove(Board board)
    {
        List<(int row, int col)> freeCells = [.. board.GetFreePositions()];

        if (freeCells.Count == 0)
        {
            throw new InvalidOperationException("Aucun coup possible");
        }
        await Task.Delay(500);

        (int row, int col) = freeCells[_rnd.Next(freeCells.Count)];

        return (row * board.Size + col) + 1;

    }

}
