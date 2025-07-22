using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AniTrack.Data.Migrations
{
    /// <inheritdoc />
    public partial class CommentsForGenres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Genres",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "The name of the Genre.",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Genres",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Indicates whether this Genre record is deleted (soft delete).",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Genres",
                type: "int",
                nullable: false,
                comment: "The unique identifier for the Genre.",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AnimesGenres",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Indicates whether this AnimeGenre record is deleted (soft delete).",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "GenreId",
                table: "AnimesGenres",
                type: "int",
                nullable: false,
                comment: "The unique identifier for the Genre associated with this Anime.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AnimeId",
                table: "AnimesGenres",
                type: "int",
                nullable: false,
                comment: "The unique identifier for the Anime associated with this Genre.",
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Genres",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "The name of the Genre.");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Genres",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false,
                oldComment: "Indicates whether this Genre record is deleted (soft delete).");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Genres",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The unique identifier for the Genre.")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AnimesGenres",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false,
                oldComment: "Indicates whether this AnimeGenre record is deleted (soft delete).");

            migrationBuilder.AlterColumn<int>(
                name: "GenreId",
                table: "AnimesGenres",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The unique identifier for the Genre associated with this Anime.");

            migrationBuilder.AlterColumn<int>(
                name: "AnimeId",
                table: "AnimesGenres",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The unique identifier for the Anime associated with this Genre.");
        }
    }
}
