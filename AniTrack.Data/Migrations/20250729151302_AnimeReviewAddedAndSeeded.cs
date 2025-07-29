#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AniTrack.Data.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;
    /// <inheritdoc />
    public partial class AnimeReviewAddedAndSeeded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnimeReviews",
                columns: table => new
                {
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Unique identifier for the review author"),
                    AnimeId = table.Column<int>(type: "int", nullable: false, comment: "Unique identifier for the anime being reviewed"),
                    Content = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false, comment: "Content of the review"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Indicates whether the review is deleted"),
                    isAnimeRecommended = table.Column<bool>(type: "bit", nullable: false, comment: "Indicates whether the review recommends the anime"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()", comment: "The date when the review was created")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeReviews", x => new { x.AuthorId, x.AnimeId });
                    table.ForeignKey(
                        name: "FK_AnimeReviews_Animes_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Animes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnimeReviews_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AnimeReviews",
                columns: new[] { "AnimeId", "AuthorId", "Content", "CreatedOn", "isAnimeRecommended" },
                values: new object[,]
                {
                    { 1, "72f4dc29-8a94-4f93-b5bb-b04e0b78eb59", "A beautiful exploration of life and death, with stunning visuals and deep themes.", new DateTime(2025, 7, 29, 15, 13, 1, 620, DateTimeKind.Utc).AddTicks(7800), true },
                    { 2, "72f4dc29-8a94-4f93-b5bb-b04e0b78eb59", "A masterpiece of storytelling and character development, with a perfect blend of action and emotion.", new DateTime(2025, 7, 29, 15, 13, 1, 620, DateTimeKind.Utc).AddTicks(7806), true },
                    { 4, "72f4dc29-8a94-4f93-b5bb-b04e0b78eb59", "I found the pacing too slow and the story dragged on unnecessarily.", new DateTime(2025, 7, 29, 15, 13, 1, 620, DateTimeKind.Utc).AddTicks(7808), false },
                    { 2, "ebc3f310-2e0d-4dd4-8493-b144cd98d282", "An epic tale of sacrifice, friendship, and the pursuit of knowledge.", new DateTime(2025, 7, 29, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { 3, "ebc3f310-2e0d-4dd4-8493-b144cd98d282", "A mind-bending journey through time with unforgettable characters.", new DateTime(2025, 7, 29, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { 4, "ebc3f310-2e0d-4dd4-8493-b144cd98d282", "An incredible adventure with complex characters and a rich world.", new DateTime(2025, 7, 29, 0, 0, 0, 0, DateTimeKind.Utc), true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimeReviews_AnimeId",
                table: "AnimeReviews",
                column: "AnimeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimeReviews");
        }
    }
}
