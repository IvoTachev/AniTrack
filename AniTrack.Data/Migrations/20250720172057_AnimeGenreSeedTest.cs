using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AniTrack.Data.Migrations
{
    /// <inheritdoc />
    public partial class AnimeGenreSeedTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AnimesGenres",
                columns: new[] { "AnimeId", "GenreId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 1, 7 },
                    { 1, 8 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AnimesGenres",
                keyColumns: new[] { "AnimeId", "GenreId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "AnimesGenres",
                keyColumns: new[] { "AnimeId", "GenreId" },
                keyValues: new object[] { 1, 7 });

            migrationBuilder.DeleteData(
                table: "AnimesGenres",
                keyColumns: new[] { "AnimeId", "GenreId" },
                keyValues: new object[] { 1, 8 });
        }
    }
}
