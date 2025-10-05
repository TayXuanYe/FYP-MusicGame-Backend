using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FYP_MusicGame_Backend.Migrations
{
    /// <inheritdoc />
    public partial class MergeUserGameSettingIntoUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserGameSetting");

            migrationBuilder.AddColumn<float>(
                name: "EffectVolume",
                table: "Users",
                type: "REAL",
                nullable: false,
                defaultValue: 1f);

            migrationBuilder.AddColumn<float>(
                name: "MasterVolume",
                table: "Users",
                type: "REAL",
                nullable: false,
                defaultValue: 1f);

            migrationBuilder.AddColumn<float>(
                name: "MusicVolume",
                table: "Users",
                type: "REAL",
                nullable: false,
                defaultValue: 1f);

            migrationBuilder.AddColumn<string>(
                name: "SuggestedDifficulty",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StepsToReproduce",
                table: "BugReports",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EffectVolume",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MasterVolume",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MusicVolume",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SuggestedDifficulty",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "StepsToReproduce",
                table: "BugReports",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "UserGameSetting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Volume = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGameSetting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserGameSetting_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserGameSetting_UserId",
                table: "UserGameSetting",
                column: "UserId",
                unique: true);
        }
    }
}
