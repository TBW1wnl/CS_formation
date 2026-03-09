using FluentMigrator;

namespace Migrations;

[Migration(2)]
public class Games : Migration
{
    public override void Up()
    {
        Create.Table("games")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("size").AsInt32().NotNullable()
            .WithColumn("board_state").AsString(200).NotNullable()
            .WithColumn("current_player").AsString(1).NotNullable()
            .WithColumn("is_finished").AsBoolean().NotNullable()
            .WithColumn("created_at").AsDateTime().NotNullable()
            .WithColumn("finished_at").AsDateTime().Nullable()
            .WithColumn("winner").AsString(1).Nullable();
    }

    public override void Down()
    {
        Delete.Table("Games");
    }
}