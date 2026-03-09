public class GameState
{
    public Guid Id { get; set; }

    public int Size { get; set; }

    public string BoardState { get; set; } = "";

    public string CurrentPlayer { get; set; } = "";

    public bool IsFinished { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? FinishedAt { get; set; }

    public string? Winner { get; set; }

    public void FinishGame(string? winner)
    {
        IsFinished = true;
        FinishedAt = DateTime.UtcNow;
        Winner = winner;
    }
}