#nullable disable
namespace AniTrack.Data.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;
    /// <inheritdoc />
    public partial class AnimeAndGenreAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Animes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Unique identifier for the anime series")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "Title of the anime series"),
                    Episodes = table.Column<int>(type: "int", nullable: false, comment: "Number of episodes of the anime series"),
                    AirDate = table.Column<DateOnly>(type: "date", nullable: false, comment: "The date when the anime series first aired"),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true, comment: "The date when the anime series ended, if ended"),
                    Synopsis = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false, comment: "Synopsis or description of the anime series"),
                    ImageUrl = table.Column<string>(type: "nvarchar(2083)", maxLength: 2083, nullable: false, comment: "URL of the anime series poster"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Indicates whether the anime series is deleted")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animes", x => x.Id);
                },
                comment: "Anime series in the system");

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnimesGenres",
                columns: table => new
                {
                    AnimeId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimesGenres", x => new { x.AnimeId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_AnimesGenres_Animes_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Animes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimesGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimesGenres_GenreId",
                table: "AnimesGenres",
                column: "GenreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimesGenres");

            migrationBuilder.DropTable(
                name: "Animes");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
