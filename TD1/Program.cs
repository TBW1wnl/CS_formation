using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TD_Morpion;
using Migrations;

string connectionString = "Host=localhost;Port=5332;Database=postgres;Username=user;Password=1234";

DbContextOptions<GameDbContext> dbContextOptions =
    new DbContextOptionsBuilder<GameDbContext>()
    .UseNpgsql(connectionString)
    .Options;

GameDbContext dbContext = new GameDbContext(dbContextOptions);
GameRepository repository = new GameRepository(dbContext);

ServiceProvider services = new ServiceCollection()
    .AddFluentMigratorCore()
    .ConfigureRunner(rb => rb
        .AddPostgres()
        .WithGlobalConnectionString(connectionString)
        .ScanIn(typeof(Games).Assembly).For.Migrations())
    .AddLogging(lb => lb.AddFluentMigratorConsole())
    .BuildServiceProvider(false);

using (var scope = services.CreateScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}

ConsoleUserInterface ui = new();
Game game = new(ui, repository);

game.Settings.Size = 3;
await game.Start();