using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FYP_MusicGame_Backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGameHistoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PerfectCount",
                table: "GameHistories",
                newName: "TapPerfectCount");

            migrationBuilder.RenameColumn(
                name: "MissCount",
                table: "GameHistories",
                newName: "TapMissCount");

            migrationBuilder.RenameColumn(
                name: "GreatCount",
                table: "GameHistories",
                newName: "TapGreatCount");

            migrationBuilder.RenameColumn(
                name: "GoodCount",
                table: "GameHistories",
                newName: "TapGoodCount");

            migrationBuilder.RenameColumn(
                name: "CriticalPerfectCount",
                table: "GameHistories",
                newName: "TapCriticalPerfectCount");

            migrationBuilder.AddColumn<int>(
                name: "HistoryId",
                table: "GameHistories",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HitTimings",
                table: "GameHistories",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<int>(
                name: "HoldCriticalPerfectCount",
                table: "GameHistories",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HoldGoodCount",
                table: "GameHistories",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HoldGreatCount",
                table: "GameHistories",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HoldMissCount",
                table: "GameHistories",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HoldPerfectCount",
                table: "GameHistories",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "GameHistories",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HistoryId",
                table: "GameHistories");

            migrationBuilder.DropColumn(
                name: "HitTimings",
                table: "GameHistories");

            migrationBuilder.DropColumn(
                name: "HoldCriticalPerfectCount",
                table: "GameHistories");

            migrationBuilder.DropColumn(
                name: "HoldGoodCount",
                table: "GameHistories");

            migrationBuilder.DropColumn(
                name: "HoldGreatCount",
                table: "GameHistories");

            migrationBuilder.DropColumn(
                name: "HoldMissCount",
                table: "GameHistories");

            migrationBuilder.DropColumn(
                name: "HoldPerfectCount",
                table: "GameHistories");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "GameHistories");

            migrationBuilder.RenameColumn(
                name: "TapPerfectCount",
                table: "GameHistories",
                newName: "PerfectCount");

            migrationBuilder.RenameColumn(
                name: "TapMissCount",
                table: "GameHistories",
                newName: "MissCount");

            migrationBuilder.RenameColumn(
                name: "TapGreatCount",
                table: "GameHistories",
                newName: "GreatCount");

            migrationBuilder.RenameColumn(
                name: "TapGoodCount",
                table: "GameHistories",
                newName: "GoodCount");

            migrationBuilder.RenameColumn(
                name: "TapCriticalPerfectCount",
                table: "GameHistories",
                newName: "CriticalPerfectCount");
        }
    }
}
