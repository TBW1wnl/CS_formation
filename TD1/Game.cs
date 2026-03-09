namespace TD_Morpion;

public sealed class Game
{
    public Settings Settings => Settings.Instance;
    private readonly GameRepository _repository;
    private Guid _gameId = Guid.NewGuid();
    private Board Board { get; init; }
    private IPlayer PlayerX { get; init; }
    private IPlayer PlayerO { get; init; }
    private IPlayer CurrentPlayer { get; set; }

    private GameState gameState { get; set; }

    private bool IsGameFinished { get; set; } = false;
    private readonly IUserInterface UI;

    public Game(IUserInterface ui, GameRepository repository)
    {
        _repository = repository;

        Board = new Board(Settings.Size);
        UI = ui;

        gameState = new GameState
        {
            Id = _gameId,
            Size = Board.Size,
            BoardState = Board.Serialize(),
            IsFinished = IsGameFinished,
            CreatedAt = DateTime.UtcNow
        };

        switch (ui.AskGameMode())
        {
            case GameModes.Pvp:
                PlayerX = new Player(Symbol.X);
                PlayerO = new Player(Symbol.O);
                break;

            case GameModes.EasyAi:
            case GameModes.MediumAi:
            case GameModes.HardAi:
                PlayerX = new Player(Symbol.X);
                PlayerO = new AIPlayer(Symbol.O);
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }

        CurrentPlayer = PlayerX;
    }

    public async Task Start()
    {
        Board.Initialize();
        await Run();
    }

    private  async Task Run()
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

            int position;
            if (CurrentPlayer is AIPlayer ai)
            {
                position = ai.MakeMove(Board).Result;
                UI.ShowAIMove(ai, position);
            }
            else
            {
                position = UI.AskMove(CurrentPlayer);
            }

            if (Board.TryPlaceSymbol(position, CurrentPlayer.Symbol, out string? reason))
            {
                await SaveGame();

                IsGameFinished = CheckGameEnd();
                SwitchPlayer();
            }
            else
            {
                if (CurrentPlayer is not AIPlayer)
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
            gameState.FinishGame(CurrentPlayer.Symbol.ToString());
            return true;
        }

        if (Board.IsFull())
        {
            UI.ShowDraw();
            gameState.FinishGame(CurrentPlayer.Symbol.ToString());
            return true;
        }

        return false;
    }

    private void SwitchPlayer()
    {
        CurrentPlayer = (CurrentPlayer == PlayerX) ? PlayerO : PlayerX;
        gameState.CurrentPlayer = CurrentPlayer.Symbol.ToString();
    }

    private async Task SaveGame()
    {
        gameState.BoardState = Board.Serialize();
        await _repository.SaveAsync(gameState);
    }
}