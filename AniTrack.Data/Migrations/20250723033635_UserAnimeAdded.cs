#nullable disable
namespace AniTrack.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;
    /// <inheritdoc />
    public partial class UserAnimeAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "Genres",
                comment: "Represents a Genre in the AniTrack database.");

            migrationBuilder.AlterTable(
                name: "AnimesGenres",
                comment: "Represents the relationship between Anime and Genre in the AniTrack database.");

            migrationBuilder.CreateTable(
                name: "AnimesUsers",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Foreign Key reference to AspNetUser"),
                    AnimeId = table.Column<int>(type: "int", nullable: false, comment: "Foreign Key reference to Anime"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Indicates whether this UserAnime record is deleted (soft delete).")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimesUsers", x => new { x.UserId, x.AnimeId });
                    table.ForeignKey(
                        name: "FK_AnimesUsers_Animes_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Animes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnimesUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Represents User Watchlist relation in database");

            migrationBuilder.CreateIndex(
                name: "IX_AnimesUsers_AnimeId",
                table: "AnimesUsers",
                column: "AnimeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimesUsers");

            migrationBuilder.AlterTable(
                name: "Genres",
                oldComment: "Represents a Genre in the AniTrack database.");

            migrationBuilder.AlterTable(
                name: "AnimesGenres",
                oldComment: "Represents the relationship between Anime and Genre in the AniTrack database.");
        }
    }
}
