
namespace TD_Morpion.Tests;

public class BoardTests
{
    [Fact]
    public void Constructor_ShouldInitializeBoardWithCorrectSize()
    {
        // Arrange & Act
        Board board = new(3);

        // Assert
        Assert.Equal(3, board.Size);
        Assert.NotNull(board.Cells);
        Assert.Equal(3, board.Cells.GetLength(0));
        Assert.Equal(3, board.Cells.GetLength(1));
    }

    [Fact]
    public void Constructor_ShouldInitializeAllCellsAsEmpty()
    {
        // Arrange & Act
        Board board = new(3);

        // Assert
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Assert.Equal(Symbol.Empty, board.Cells[i, j]);
            }
        }
    }

    [Theory]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Constructor_ShouldWorkWithDifferentSizes(int size)
    {
        // Arrange & Act
        Board board = new(size);

        // Assert
        Assert.Equal(size, board.Size);
        Assert.Equal(size, board.Cells.GetLength(0));
        Assert.Equal(size, board.Cells.GetLength(1));
    }

    [Fact]
    public void Initialize_ShouldResetAllCellsToEmpty()
    {
        // Arrange
        Board board = new(3);
        board.Cells[0, 0] = Symbol.X;
        board.Cells[1, 1] = Symbol.O;
        board.Cells[2, 2] = Symbol.X;

        // Act
        board.Initialize();

        // Assert
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Assert.Equal(Symbol.Empty, board.Cells[i, j]);
            }
        }
    }

    [Theory]
    [InlineData(1, Symbol.X, true)]
    [InlineData(5, Symbol.O, true)]
    [InlineData(9, Symbol.X, true)]
    public void TryPlaceSymbol_ShouldPlaceSymbolOnEmptyCell(int position, Symbol symbol, bool expected)
    {
        // Arrange
        Board board = new(3);

        // Act
        bool result = board.TryPlaceSymbol(position, symbol, out string? reason);

        // Assert
        Assert.Equal(expected, result);
        Assert.Null(reason);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(10)]
    [InlineData(-1)]
    [InlineData(100)]
    public void TryPlaceSymbol_ShouldReturnFalse_WhenPositionIsInvalid(int position)
    {
        // Arrange
        Board board = new(3);

        // Act
        bool result = board.TryPlaceSymbol(position, Symbol.X, out string? reason);

        // Assert
        Assert.False(result);
        Assert.NotNull(reason);
        Assert.Contains("Position invalide", reason);
    }

    [Fact]
    public void TryPlaceSymbol_ShouldReturnFalse_WhenCellIsOccupied()
    {
        // Arrange
        Board board = new(3);
        board.TryPlaceSymbol(1, Symbol.X, out _);

        // Act
        bool result = board.TryPlaceSymbol(1, Symbol.O, out string? reason);

        // Assert
        Assert.False(result);
        Assert.NotNull(reason);
        Assert.Contains("déjà occupée", reason);
    }

    [Fact]
    public void IsFull_ShouldReturnFalse_WhenBoardHasEmptyCells()
    {
        // Arrange
        Board board = new(3);
        board.TryPlaceSymbol(1, Symbol.X, out _);

        // Act
        bool result = board.IsFull();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsFull_ShouldReturnTrue_WhenAllCellsAreFilled()
    {
        // Arrange
        Board board = new(3);
        for (int i = 1; i <= 9; i++)
        {
            board.TryPlaceSymbol(i, i % 2 == 0 ? Symbol.O : Symbol.X, out _);
        }

        // Act
        bool result = board.IsFull();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void GetFreePositions_ShouldReturnAllPositions_WhenBoardIsEmpty()
    {
        // Arrange
        Board board = new(3);

        // Act
        List<(int row, int col)> freePositions = [.. board.GetFreePositions()];

        // Assert
        Assert.Equal(9, freePositions.Count);
    }

    [Fact]
    public void GetFreePositions_ShouldReturnNoPositions_WhenBoardIsFull()
    {
        // Arrange
        Board board = new(3);
        for (int i = 1; i <= 9; i++)
        {
            board.TryPlaceSymbol(i, Symbol.X, out _);
        }

        // Act
        List<(int row, int col)> freePositions = [.. board.GetFreePositions()];

        // Assert
        Assert.Empty(freePositions);
    }

    [Fact]
    public void GetFreePositions_ShouldReturnCorrectCount_WhenSomeCellsAreFilled()
    {
        // Arrange
        Board board = new(3);
        board.TryPlaceSymbol(1, Symbol.X, out _);
        board.TryPlaceSymbol(5, Symbol.O, out _);
        board.TryPlaceSymbol(9, Symbol.X, out _);

        // Act
        List<(int row, int col)> freePositions = [.. board.GetFreePositions()];

        // Assert
        Assert.Equal(6, freePositions.Count);
    }

    [Fact]
    public void HasWinner_ShouldReturnTrue_WhenRowIsComplete()
    {
        // Arrange
        Board board = new(3);

        board.TryPlaceSymbol(7, Symbol.X, out _);
        board.TryPlaceSymbol(8, Symbol.X, out _);
        board.TryPlaceSymbol(9, Symbol.X, out _);

        // Act
        bool result = board.HasWinner(Symbol.X);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void HasWinner_ShouldReturnTrue_WhenColumnIsComplete()
    {
        // Arrange
        Board board = new(3);

        board.TryPlaceSymbol(1, Symbol.O, out _);
        board.TryPlaceSymbol(4, Symbol.O, out _);
        board.TryPlaceSymbol(7, Symbol.O, out _);

        // Act
        bool result = board.HasWinner(Symbol.O);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void HasWinner_ShouldReturnTrue_WhenMainDiagonalIsComplete()
    {
        // Arrange
        Board board = new(3);

        board.TryPlaceSymbol(7, Symbol.X, out _);
        board.TryPlaceSymbol(5, Symbol.X, out _);
        board.TryPlaceSymbol(3, Symbol.X, out _);

        // Act
        bool result = board.HasWinner(Symbol.X);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void HasWinner_ShouldReturnTrue_WhenAntiDiagonalIsComplete()
    {
        // Arrange
        Board board = new(3);

        board.TryPlaceSymbol(9, Symbol.O, out _);
        board.TryPlaceSymbol(5, Symbol.O, out _);
        board.TryPlaceSymbol(1, Symbol.O, out _);

        // Act
        bool result = board.HasWinner(Symbol.O);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void HasWinner_ShouldReturnFalse_WhenNoWinningCondition()
    {
        // Arrange
        Board board = new(3);
        board.TryPlaceSymbol(1, Symbol.X, out _);
        board.TryPlaceSymbol(2, Symbol.O, out _);
        board.TryPlaceSymbol(5, Symbol.X, out _);

        // Act
        bool result = board.HasWinner(Symbol.X);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void HasWinner_ShouldReturnFalse_WhenBoardIsEmpty()
    {
        // Arrange
        Board board = new(3);

        // Act
        bool result = board.HasWinner(Symbol.X);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void HasWinner_ShouldReturnFalse_WhenDifferentSymbolsInLine()
    {
        // Arrange
        Board board = new(3);
        board.TryPlaceSymbol(1, Symbol.X, out _);
        board.TryPlaceSymbol(2, Symbol.O, out _);
        board.TryPlaceSymbol(3, Symbol.X, out _);

        // Act
        bool resultX = board.HasWinner(Symbol.X);
        bool resultO = board.HasWinner(Symbol.O);

        // Assert
        Assert.False(resultX);
        Assert.False(resultO);
    }

    [Theory]
    [InlineData(4)]
    [InlineData(5)]
    public void HasWinner_ShouldWorkWithDifferentBoardSizes(int size)
    {
        // Arrange
        Board board = new(size);

        for (int i = 1; i <= size; i++)
        {
            board.TryPlaceSymbol(size * (size - 1) + i, Symbol.X, out _);
        }

        // Act
        bool result = board.HasWinner(Symbol.X);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TryPlaceSymbol_ShouldPlaceInCorrectCell()
    {
        // Arrange
        Board board = new(3);

        // Act
        board.TryPlaceSymbol(1, Symbol.X, out _);

        // Assert
        Assert.Equal(Symbol.X, board.Cells[2, 0]);
    }

    [Fact]
    public void TryPlaceSymbol_ShouldMapPositionsCorrectly()
    {
        // Arrange
        Board board = new(3);

        // Act & Assert
        board.TryPlaceSymbol(1, Symbol.X, out _);
        Assert.Equal(Symbol.X, board.Cells[2, 0]);

        board.TryPlaceSymbol(5, Symbol.O, out _);
        Assert.Equal(Symbol.O, board.Cells[1, 1]);

        board.TryPlaceSymbol(9, Symbol.X, out _);
        Assert.Equal(Symbol.X, board.Cells[0, 2]);
    }
}