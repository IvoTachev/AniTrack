using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AniTrack.Data.Migrations
{
    /// <inheritdoc />
    public partial class Seed10AnimesWithGenres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Animes",
                columns: new[] { "Id", "AirDate", "EndDate", "Episodes", "ImageUrl", "Synopsis", "Title" },
                values: new object[,]
                {
                    { 2, new DateOnly(2009, 4, 5), new DateOnly(2010, 7, 4), 64, "https://cdn.myanimelist.net/images/anime/1223/96541.jpg", "After a horrific alchemy experiment goes wrong in the Elric household, brothers Edward and Alphonse are left in a catastrophic new reality. Ignoring the alchemical principle banning human transmutation, the boys attempted to bring their recently deceased mother back to life. Instead, they suffered brutal personal loss: Alphonse's body disintegrated while Edward lost a leg and then sacrificed an arm to keep Alphonse's soul in the physical realm by binding it to a hulking suit of armor.\r\n\r\nThe brothers are rescued by their neighbor Pinako Rockbell and her granddaughter Winry. Known as a bio-mechanical engineering prodigy, Winry creates prosthetic limbs for Edward by utilizing \"automail,\" a tough, versatile metal used in robots and combat armor. After years of training, the Elric brothers set off on a quest to restore their bodies by locating the Philosopher's Stone—a powerful gem that allows an alchemist to defy the traditional laws of Equivalent Exchange.\r\n\r\nAs Edward becomes an infamous alchemist and gains the nickname \"Fullmetal,\" the boys' journey embroils them in a growing conspiracy that threatens the fate of the world.", "Fullmetal Alchemist: Brotherhood" },
                    { 3, new DateOnly(2011, 4, 6), new DateOnly(2011, 9, 14), 24, "https://cdn.myanimelist.net/images/anime/5/73199.jpg", "Eccentric scientist Rintarou Okabe has a never-ending thirst for scientific exploration. Together with his ditzy but well-meaning friend Mayuri Shiina and his roommate Itaru Hashida, Okabe founds the Future Gadget Laboratory in the hopes of creating technological innovations that baffle the human psyche. Despite claims of grandeur, the only notable \"gadget\" the trio have created is a microwave that has the mystifying power to turn bananas into green goo.\r\n\r\nHowever, when Okabe attends a conference on time travel, he experiences a series of strange events that lead him to believe that there is more to the \"Phone Microwave\" gadget than meets the eye. Apparently able to send text messages into the past using the microwave, Okabe dabbles further with the \"time machine,\" attracting the ire and attention of the mysterious organization SERN.\r\n\r\nDue to the novel discovery, Okabe and his friends find themselves in an ever-present danger. As he works to mitigate the damage his invention has caused to the timeline, Okabe fights a battle to not only save his loved ones but also to preserve his degrading sanity.", "Steins;Gate" },
                    { 4, new DateOnly(2011, 10, 2), new DateOnly(2014, 9, 24), 148, "https://cdn.myanimelist.net/images/anime/1337/99013.jpg", "Hunters devote themselves to accomplishing hazardous tasks, all from traversing the world's uncharted territories to locating rare items and monsters. Before becoming a Hunter, one must pass the Hunter Examination—a high-risk selection process in which most applicants end up handicapped or worse, deceased.\r\n\r\nAmbitious participants who challenge the notorious exam carry their own reason. What drives 12-year-old Gon Freecss is finding Ging, his father and a Hunter himself. Believing that he will meet his father by becoming a Hunter, Gon takes the first step to walk the same path.\r\n\r\nDuring the Hunter Examination, Gon befriends the medical student Leorio Paladiknight, the vindictive Kurapika, and ex-assassin Killua Zoldyck. While their motives vastly differ from each other, they band together for a common goal and begin to venture into a perilous world.", "Hunter x Hunter (2011)" },
                    { 5, new DateOnly(2013, 4, 7), new DateOnly(2013, 9, 29), 25, "https://cdn.myanimelist.net/images/anime/10/47347.jpg", "Centuries ago, mankind was slaughtered to near extinction by monstrous humanoid creatures called Titans, forcing humans to hide in fear behind enormous concentric walls. What makes these giants truly terrifying is that their taste for human flesh is not born out of hunger but what appears to be out of pleasure. To ensure their survival, the remnants of humanity began living within defensive barriers, resulting in one hundred years without a single titan encounter. However, that fragile calm is soon shattered when a colossal Titan manages to breach the supposedly impregnable outer wall, reigniting the fight for survival against the man-eating abominations.\r\n\r\nAfter witnessing a horrific personal loss at the hands of the invading creatures, Eren Yeager dedicates his life to their eradication by enlisting into the Survey Corps, an elite military unit that combats the merciless humanoids outside the protection of the walls. Eren, his adopted sister Mikasa Ackerman, and his childhood friend Armin Arlert join the brutal war against the Titans and race to discover a way of defeating them before the last walls are breached.", "Attack On Titan" },
                    { 6, new DateOnly(2006, 10, 4), new DateOnly(2007, 6, 27), 37, "https://cdn.myanimelist.net/images/anime/9/9453.jpg", "Brutal murders, petty thefts, and senseless violence pollute the human world. In contrast, the realm of death gods is a humdrum, unchanging gambling den. The ingenious 17-year-old Japanese student Light Yagami and sadistic god of death Ryuk share one belief: their worlds are rotten.\r\n\r\nFor his own amusement, Ryuk drops his Death Note into the human world. Light stumbles upon it, deeming the first of its rules ridiculous: the human whose name is written in this note shall die. However, the temptation is too great, and Light experiments by writing a felon's name, which disturbingly enacts his first murder.\r\n\r\nAware of the terrifying godlike power that has fallen into his hands, Light—under the alias Kira—follows his wicked sense of justice with the ultimate goal of cleansing the world of all evil-doers. The meticulous mastermind detective L is already on his trail, but as Light's brilliance rivals L's, the grand chase for Kira turns into an intense battle of wits that can only end when one of them is dead.", "Death Note" },
                    { 7, new DateOnly(2019, 4, 6), new DateOnly(2019, 9, 28), 26, "https://cdn.myanimelist.net/images/anime/1286/99889.jpg", "Ever since the death of his father, the burden of supporting the family has fallen upon Tanjirou Kamado's shoulders. Though living impoverished on a remote mountain, the Kamado family are able to enjoy a relatively peaceful and happy life. One day, Tanjirou decides to go down to the local village to make a little money selling charcoal. On his way back, night falls, forcing Tanjirou to take shelter in the house of a strange man, who warns him of the existence of flesh-eating demons that lurk in the woods at night.\r\n\r\nWhen he finally arrives back home the next day, he is met with a horrifying sight—his whole family has been slaughtered. Worse still, the sole survivor is his sister Nezuko, who has been turned into a bloodthirsty demon. Consumed by rage and hatred, Tanjirou swears to avenge his family and stay by his only remaining sibling. Alongside the mysterious group calling themselves the Demon Slayer Corps, Tanjirou will do whatever it takes to slay the demons and protect the remnants of his beloved sister's humanity.", "Demon Slayer" },
                    { 8, new DateOnly(2020, 10, 3), new DateOnly(2021, 3, 27), 24, "https://cdn.myanimelist.net/images/anime/1171/109222.jpg", "Idly indulging in baseless paranormal activities with the Occult Club, high schooler Yuuji Itadori spends his days at either the clubroom or the hospital, where he visits his bedridden grandfather. However, this leisurely lifestyle soon takes a turn for the strange when he unknowingly encounters a cursed item. Triggering a chain of supernatural occurrences, Yuuji finds himself suddenly thrust into the world of Curses—dreadful beings formed from human malice and negativity—after swallowing the said item, revealed to be a finger belonging to the demon Sukuna Ryoumen, the King of Curses.\r\n\r\nYuuji experiences first-hand the threat these Curses pose to society as he discovers his own newfound powers. Introduced to the Tokyo Prefectural Jujutsu High School, he begins to walk down a path from which he cannot return—the path of a Jujutsu sorcerer.", "Jujutsu Kaisen" },
                    { 9, new DateOnly(2016, 4, 3), new DateOnly(2016, 6, 26), 13, "https://cdn.myanimelist.net/images/anime/10/78745.jpg", "The appearance of \"quirks,\" newly discovered super powers, has been steadily increasing over the years, with 80 percent of humanity possessing various abilities from manipulation of elements to shapeshifting. This leaves the remainder of the world completely powerless, and Izuku Midoriya is one such individual.\r\n\r\nSince he was a child, the ambitious middle schooler has wanted nothing more than to be a hero. Izuku's unfair fate leaves him admiring heroes and taking notes on them whenever he can. But it seems that his persistence has borne some fruit: Izuku meets the number one hero and his personal idol, All Might. All Might's quirk is a unique ability that can be inherited, and he has chosen Izuku to be his successor!\r\n\r\nEnduring many months of grueling training, Izuku enrolls in UA High, a prestigious high school famous for its excellent hero training program, and this year's freshmen look especially promising. With his bizarre but talented classmates and the looming threat of a villainous organization, Izuku will soon learn what it really means to be a hero.", "My Hero Academia" },
                    { 10, new DateOnly(2015, 10, 5), new DateOnly(2015, 12, 21), 12, "https://cdn.myanimelist.net/images/anime/12/76049.jpg", "The seemingly unimpressive Saitama has a rather unique hobby: being a hero. In order to pursue his childhood dream, Saitama relentlessly trained for three years, losing all of his hair in the process. Now, Saitama is so powerful, he can defeat any enemy with just one punch. However, having no one capable of matching his strength has led Saitama to an unexpected problem—he is no longer able to enjoy the thrill of battling and has become quite bored.\r\n\r\nOne day, Saitama catches the attention of 19-year-old cyborg Genos, who witnesses his power and wishes to become Saitama's disciple. Genos proposes that the two join the Hero Association in order to become certified heroes that will be recognized for their positive contributions to society. Saitama, who is shocked that no one knows who he is, quickly agrees. Meeting new allies and taking on new foes, Saitama embarks on a new journey as a member of the Hero Association to experience the excitement of battle he once felt.", "One Punch Man" }
                });

            migrationBuilder.InsertData(
                table: "AnimesGenres",
                columns: new[] { "AnimeId", "GenreId" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 2, 2 },
                    { 2, 7 },
                    { 2, 8 },
                    { 3, 7 },
                    { 3, 14 },
                    { 3, 18 },
                    { 4, 1 },
                    { 4, 2 },
                    { 4, 8 },
                    { 5, 1 },
                    { 5, 4 },
                    { 5, 7 },
                    { 5, 18 },
                    { 6, 17 },
                    { 6, 18 },
                    { 7, 1 },
                    { 7, 4 },
                    { 7, 17 },
                    { 8, 1 },
                    { 8, 4 },
                    { 8, 17 },
                    { 9, 1 },
                    { 10, 1 },
                    { 10, 6 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AnimesGenres",
                keyColumns: new[] { "AnimeId", "GenreId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "AnimesGenres",
                keyColumns: new[] { "AnimeId", "GenreId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "AnimesGenres",
                keyColumns: new[] { "AnimeId", "GenreId" },
                keyValues: new object[] { 2, 7 });

            migrationBuilder.DeleteData(
                table: "AnimesGenres",
                keyColumns: new[] { "AnimeId", "GenreId" },
                keyValues: new object[] { 2, 8 });

            migrationBuilder.DeleteData(
                table: "AnimesGenres",
                keyColumns: new[] { "AnimeId", "GenreId" },
                keyValues: new object[] { 3, 7 });

            migrationBuilder.DeleteData(
                table: "AnimesGenres",
                keyColumns: new[] { "AnimeId", "GenreId" },
                keyValues: new object[] { 3, 14 });

            migrationBuilder.DeleteData(
                table: "AnimesGenres",
                keyColumns: new[] { "AnimeId", "GenreId" },
                keyValues: new object[] { 3, 18 });

            migrationBuilder.DeleteData(
                table: "AnimesGenres",
                keyColumns: new[] { "AnimeId", "GenreId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "AnimesGenres",
                keyColumns: new[] { "AnimeId", "GenreId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "AnimesGenres",
                keyColumns: new[] { "AnimeId", "GenreId" },
                keyValues: new object[] { 4, 8 });

            migrationBuilder.DeleteData(
                table: "AnimesGenres",
                keyColumns: new[] { "AnimeId", "GenreId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "AnimesGenres",
                keyColumns: new[] { "AnimeId", "GenreId" },
                keyValues: new object[] { 5, 4 });

            migrationBuilder.DeleteData(
                table: "AnimesGenres",
                keyColumns: new[] { "AnimeId", "GenreId" },
                keyValues: new object[] { 5, 7 });

            migrationBuilder.DeleteData(
                table: "AnimesGenres",
                keyColumns: new[] { "AnimeId", "GenreId" },
                keyValues: new object[] { 5, 18 });

            migrationBuilder.DeleteData(
                table: "AnimesGenres",
                keyColumns: new[] { "AnimeId", "GenreId" },
                keyValues: new object[] { 6, 17 });

            migrationBuilder.DeleteData(
                table: "AnimesGenres",
                keyColumns: new[] { "AnimeId", "GenreId" },
                keyValues: new object[] { 6, 18 });

            migrationBuilder.DeleteData(
                table: "AnimesGenres",
                keyColumns: new[] { "AnimeId", "GenreId" },
                keyValues: new object[] { 7, 1 });

            migrationBuilder.DeleteData(
                table: "AnimesGenres",
                keyColumns: new[] { "AnimeId", "GenreId" },
                keyValues: new object[] { 7, 4 });

            migrationBuilder.DeleteData(
                table: "AnimesGenres",
                keyColumns: new[] { "AnimeId", "GenreId" },
                keyValues: new object[] { 7, 17 });

            migrationBuilder.DeleteData(
                table: "AnimesGenres",
                keyColumns: new[] { "AnimeId", "GenreId" },
                keyValues: new object[] { 8, 1 });

            migrationBuilder.DeleteData(
                table: "AnimesGenres",
                keyColumns: new[] { "AnimeId", "GenreId" },
                keyValues: new object[] { 8, 4 });

            migrationBuilder.DeleteData(
                table: "AnimesGenres",
                keyColumns: new[] { "AnimeId", "GenreId" },
                keyValues: new object[] { 8, 17 });

            migrationBuilder.DeleteData(
                table: "AnimesGenres",
                keyColumns: new[] { "AnimeId", "GenreId" },
                keyValues: new object[] { 9, 1 });

            migrationBuilder.DeleteData(
                table: "AnimesGenres",
                keyColumns: new[] { "AnimeId", "GenreId" },
                keyValues: new object[] { 10, 1 });

            migrationBuilder.DeleteData(
                table: "AnimesGenres",
                keyColumns: new[] { "AnimeId", "GenreId" },
                keyValues: new object[] { 10, 6 });

            migrationBuilder.DeleteData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
