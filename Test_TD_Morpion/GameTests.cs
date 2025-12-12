using Xunit;
using Moq;
using TD_Morpion;

namespace TD_Morpion.Tests;

public class GameTests
{
    private Mock<IUserInterface> CreateMockUI(GameModes mode = GameModes.Pvp)
    {
        var mockUI = new Mock<IUserInterface>();
        mockUI.Setup(ui => ui.AskGameMode()).Returns(mode);
        mockUI.Setup(ui => ui.AskReplay()).Returns(false);
        return mockUI;
    }

    [Fact]
    public void Constructor_ShouldInitializePvPGame_WhenPvpModeSelected()
    {
        // Arrange
        var mockUI = CreateMockUI(GameModes.Pvp);

        // Act
        var game = new Game(mockUI.Object);

        // Assert
        Assert.NotNull(game);
        Assert.NotNull(game.Settings);
    }

    [Fact]
    public void Constructor_ShouldInitializeEasyAIGame_WhenEasyAiModeSelected()
    {
        // Arrange
        var mockUI = CreateMockUI(GameModes.EasyAi);

        // Act
        var game = new Game(mockUI.Object);

        // Assert
        Assert.NotNull(game);
    }

    [Fact]
    public void Constructor_ShouldInitializeMediumAIGame_WhenMediumAiModeSelected()
    {
        // Arrange
        var mockUI = CreateMockUI(GameModes.MediumAi);

        // Act
        var game = new Game(mockUI.Object);

        // Assert
        Assert.NotNull(game);
    }

    [Fact]
    public void Constructor_ShouldInitializeHardAIGame_WhenHardAiModeSelected()
    {
        // Arrange
        var mockUI = CreateMockUI(GameModes.HardAi);

        // Act
        var game = new Game(mockUI.Object);

        // Assert
        Assert.NotNull(game);
    }
}