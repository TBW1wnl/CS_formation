using TD_Morpion;

public class MockUserInterface : IUserInterface
{
    private Queue<int> _moveQueue = new();
    public GameModes GameMode { get; set; } = GameModes.Pvp;
    public bool ReplayResponse { get; set; } = false;
    public int AskGameModeCallCount { get; private set; }
    public int AskMoveCallCount { get; private set; }
    public int AskReplayCallCount { get; private set; }
    public int RenderCallCount { get; private set; }
    public int ShowAIMoveCallCount { get; private set; }
    public int InvalidMoveCallCount { get; private set; }
    public bool WinShown { get; private set; }
    public bool DrawShown { get; private set; }
    public readonly Queue<int> XVictorySequence = new([1, 2, 4, 5, 7]);
    public readonly Queue<int> OVictorySequence = new([1, 2, 4, 5, 6, 8]);
    public readonly Queue<int> DrawSequence = new([1, 5, 2, 3, 7, 4, 9, 8, 6]);

    public void AddMoves(params int[] moves)
    {
        foreach (var move in moves)
        {
            _moveQueue.Enqueue(move);
        }
    }

    public GameModes AskGameMode()
    {
        AskGameModeCallCount++;
        return GameMode;
    }

    public int AskMove(in IPlayer player)
    {
        AskMoveCallCount++;
        if (_moveQueue.Count == 0)
        {
            throw new InvalidOperationException("No more moves in queue");
        }
        return _moveQueue.Dequeue();
    }

    public bool AskReplay()
    {
        AskReplayCallCount++;
        var response = ReplayResponse;
        ReplayResponse = false;
        return response;
    }

    public void Render(in Board board)
    {
        RenderCallCount++;
    }

    public void ShowAIMove(IPlayer ai, int position)
    {
        ShowAIMoveCallCount++;
    }

    public void ShowDraw()
    {
        DrawShown = true;
    }

    public void ShowInvalidInput()
    {
        return;
    }

    public void ShowInvalidMove(in string? reason)
    {
        InvalidMoveCallCount++;
    }

    public void ShowWin(in IPlayer player)
    {
        WinShown = true;
    }
}