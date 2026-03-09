using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

public class GameDbContext : DbContext
{
    public DbSet<GameState> Games => Set<GameState>();

    public GameDbContext(DbContextOptions<GameDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GameState>(entity =>
        {
            entity.ToTable("games");

            entity.HasKey(g => g.Id);
            entity.Property(g => g.Id)
                  .HasColumnName("id");

            entity.Property(g => g.Size)
                  .HasColumnName("size");

            entity.Property(g => g.BoardState)
                  .HasColumnName("board_state");

            entity.Property(g => g.CurrentPlayer)
                  .HasColumnName("current_player");

            entity.Property(g => g.IsFinished)
                  .HasColumnName("is_finished");

            entity.Property(g => g.CreatedAt)
                  .HasColumnName("created_at");

            entity.Property(g => g.FinishedAt)
                  .HasColumnName("finished_at");

            entity.Property(g => g.Winner)
                  .HasColumnName("winner");
        });
    }
}