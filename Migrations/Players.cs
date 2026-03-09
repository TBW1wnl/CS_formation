using FluentMigrator;

[Migration(1)]
public class Players : Migration
{
    public override void Up()
    {
        Create.Table("Players")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Name").AsString(50).NotNullable()
            .WithColumn("GamesPlayed").AsInt32().WithDefaultValue(0)
            .WithColumn("GamesWon").AsInt32().WithDefaultValue(0)
            .WithColumn("GamesLost").AsInt32().WithDefaultValue(0)
            .WithColumn("GamesDraw").AsInt32().WithDefaultValue(0);
    }

    public override void Down()
    {
        Delete.Table("Players");
    }
}