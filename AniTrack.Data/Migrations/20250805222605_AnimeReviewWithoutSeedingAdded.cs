using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AniTrack.Data.Migrations
{
    /// <inheritdoc />
    public partial class AnimeReviewWithoutSeedingAdded : Migration
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
