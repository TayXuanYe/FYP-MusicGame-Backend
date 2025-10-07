using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FYP_MusicGame_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddGameHistoryTableNewFieldTrackNo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrackNo",
                table: "GameHistories",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrackNo",
                table: "GameHistories");
        }
    }
}
