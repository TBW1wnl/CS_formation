namespace TD_Morpion;

public sealed class Game
{
    public Settings Settings => Settings.Instance;
    private Board Board { get; init; }
    private IPlayer PlayerX { get; init; }
    private IPlayer PlayerO { get; init; }
    private IPlayer CurrentPlayer { get; set; }
    private bool IsGameFinished { get; set; } = false;
    private readonly IUserInterface UI;

    public Game(in IUserInterface ui)
    {        
        Board = new Board(Settings.Size);
        PlayerX = new Player(Symbol.X);
        PlayerO = new Player(Symbol.O);
        CurrentPlayer = PlayerX;
        UI = ui;
    }

    public void Start()
    {
        Board.Initialize();
        Run();
    }

    private void Run()
    {
        while (true)
        {
            UI.Render(Board);

            if (IsGameFinished)
            {
                if (!UI.AskReplay())
                {
                    break;
                }

                ResetGame();
                continue;
            }

            int position = UI.AskMove(CurrentPlayer);

            if (Board.TryPlaceSymbol(position, CurrentPlayer.Symbol, out string? reason))
            {
                IsGameFinished = CheckGameEnd();
                SwitchPlayer();
            }
            else
            {
                UI.ShowInvalidMove(reason);
            }
        }
    }

    private void ResetGame()
    {
        Board.Initialize();
        IsGameFinished = false;
        CurrentPlayer = PlayerX;
    }

    private bool CheckGameEnd()
    {
        if (Board.HasWinner(CurrentPlayer.Symbol))
        {
            UI.ShowWin(CurrentPlayer);
            return true;
        }

        if (Board.IsFull())
        {
            UI.ShowDraw();
            return true;
        }

        return false;
    }

    private void SwitchPlayer()
    {
        CurrentPlayer = (CurrentPlayer == PlayerX) ? PlayerO : PlayerX;
    }
}