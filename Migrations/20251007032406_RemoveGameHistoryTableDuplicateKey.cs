using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FYP_MusicGame_Backend.Migrations
{
    /// <inheritdoc />
    public partial class RemoveGameHistoryTableDuplicateKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HistoryId",
                table: "GameHistories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HistoryId",
                table: "GameHistories",
                type: "INTEGER",
                nullable: true);
        }
    }
}
