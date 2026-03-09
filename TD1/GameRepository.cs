using Microsoft.EntityFrameworkCore;

public class GameRepository
{
    private readonly GameDbContext _context;

    public GameRepository(GameDbContext context)
    {
        _context = context;
    }

    public async Task SaveAsync(GameState state)
    {
        GameState? existing = await _context.Games.FindAsync(state.Id);

        if (existing == null)
            _context.Games.Add(state);
        else
            _context.Entry(existing).CurrentValues.SetValues(state);

        await _context.SaveChangesAsync();
    }

    public async Task<GameState?> GetRunningGameAsync()
    {
        return await _context.Games
            .Where(g => !g.IsFinished)
            .FirstOrDefaultAsync<GameState>();
    }
}