using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AniTrack.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedAnimeTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Animes",
                columns: new[] { "Id", "AirDate", "EndDate", "Episodes", "ImageUrl", "Synopsis", "Title" },
                values: new object[] { 1, new DateOnly(2023, 9, 29), new DateOnly(2024, 3, 22), 28, "https://cdn.myanimelist.net/images/anime/1015/138006.jpg", "During their decade-long quest to defeat the Demon King, the members of the hero's party—Himmel himself, the priest Heiter, the dwarf warrior Eisen, and the elven mage Frieren—forge bonds through adventures and battles, creating unforgettable precious memories for most of them.\r\n\r\nHowever, the time that Frieren spends with her comrades is equivalent to merely a fraction of her life, which has lasted over a thousand years. When the party disbands after their victory, Frieren casually returns to her \"usual\" routine of collecting spells across the continent. Due to her different sense of time, she seemingly holds no strong feelings toward the experiences she went through.\r\n\r\nAs the years pass, Frieren gradually realizes how her days in the hero's party truly impacted her. Witnessing the deaths of two of her former companions, Frieren begins to regret having taken their presence for granted; she vows to better understand humans and create real personal connections. Although the story of that once memorable journey has long ended, a new tale is about to begin.", "Sousou no Frieren" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
