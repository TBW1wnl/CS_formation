namespace TD_Morpion.Tests;

public class GameTests
{
    private MockUserInterface CreateMockUI(GameModes mode = GameModes.Pvp)
    {
        return new MockUserInterface { GameMode = mode };
    }

    [Fact]
    public void Constructor_ShouldInitializePvPMode_WhenPvpSelected()
    {
        // Arrange & Act
        var ui = CreateMockUI(GameModes.Pvp);
        var game = new Game(ui);

        // Assert
        Assert.NotNull(game);
        Assert.Equal(1, ui.AskGameModeCallCount);
    }

    [Fact]
    public void Start_PlayerXWins_WithHorizontalLine()
    {
        // Arrange
        var ui = CreateMockUI();
        ui.AddMoves(1, 4, 2, 5, 3);
        var game = new Game(ui);

        // Act
        game.Start();

        // Assert
        Assert.True(ui.WinShown);
        Assert.False(ui.DrawShown);
        Assert.True(ui.RenderCallCount >= 5);
    }

    [Fact]
    public void Start_PlayerXWins()
    {
        // Arrange
        var ui = CreateMockUI();
        ui.AddMoves([.. ui.XVictorySequence]);
        var game = new Game(ui);

        // Act
        game.Start();

        // Assert
        Assert.True(ui.WinShown);
    }

    [Fact]
    public void Start_ShouldResultInDraw_WhenBoardIsFull()
    {
        // Arrange
        var ui = CreateMockUI();
        ui.AddMoves([.. ui.DrawSequence]);
        var game = new Game(ui);

        // Act
        game.Start();

        // Assert
        Assert.True(ui.DrawShown);
        Assert.False(ui.WinShown);
    }

    [Fact]
    public void Start_ShouldShowInvalidMove_WhenPositionAlreadyTaken()
    {
        // Arrange
        var ui = CreateMockUI();
        ui.AddMoves(1, 1, 2, 3, 4, 5, 6, 7, 8, 9);
        var game = new Game(ui);

        // Act
        game.Start();

        // Assert
        Assert.True(ui.InvalidMoveCallCount > 0);
    }

    [Fact]
    public void Start_ShouldResetAndContinue_WhenPlayerWantsReplay()
    {
        // Arrange
        var ui = CreateMockUI();
        ui.AddMoves([.. ui.XVictorySequence]);
        ui.ReplayResponse = true;
        ui.AddMoves([.. ui.DrawSequence]);
        var game = new Game(ui);

        // Act
        game.Start();

        // Assert
        Assert.True(ui.AskReplayCallCount >= 1);
        Assert.True(ui.RenderCallCount > 6);
    }

    [Fact]
    public void Start_WithAIPlayer_ShouldAllowAIToMakeMove()
    {
        // Arrange
        var ui = CreateMockUI(GameModes.EasyAi);
        ui.AddMoves([.. ui.DrawSequence]);
        var game = new Game(ui);

        // Act
        game.Start();

        // Assert
        Assert.True(ui.ShowAIMoveCallCount > 0);
    }

    [Fact]
    public void Start_ShouldAlternateBetweenPlayers()
    {
        // Arrange
        var ui = CreateMockUI();
        ui.AddMoves([.. ui.XVictorySequence]);
        var game = new Game(ui);

        // Act
        game.Start();

        // Assert
        Assert.True(ui.AskMoveCallCount >= 3);
    }

    [Fact]
    public void Start_ShouldRenderBoardMultipleTimes()
    {
        // Arrange
        var ui = CreateMockUI();
        ui.AddMoves([.. ui.XVictorySequence]);
        var game = new Game(ui);

        // Act
        game.Start();

        // Assert
        Assert.True(ui.RenderCallCount >= 3);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(10)]
    public void AskMove_ShouldHandleInvalidPositions(int invalidPosition)
    {
        // Arrange
        var ui = CreateMockUI();
        ui.AddMoves(invalidPosition);
        ui.AddMoves([.. ui.XVictorySequence]);
        var game = new Game(ui);

        // Act
        game.Start();

        // Assert
        Assert.True(ui.InvalidMoveCallCount > 0);
    }
}
