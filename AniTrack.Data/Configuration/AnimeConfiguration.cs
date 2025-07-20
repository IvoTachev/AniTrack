
namespace AniTrack.Data.Configuration
{
    using Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using static Common.EntityConstants.Anime;
    public class AnimeConfiguration : IEntityTypeConfiguration<Anime>
    {
        public void Configure(EntityTypeBuilder<Anime> entity)
        {
            entity
                .HasKey(a => a.Id);

            entity
                .Property(a => a.Title)
                .IsRequired()
                .HasMaxLength(TitleMaxLength);

            entity
                .Property(a => a.Episodes)
                .IsRequired();

            entity
                .Property(a => a.AirDate)
                .IsRequired();

            entity
                .Property(a => a.EndDate)
                .IsRequired(false);

            entity
                .Property(a => a.Synopsis)
                .IsRequired()
                .HasMaxLength(SynopsisMaxLength);

            entity
                .Property(a => a.ImageUrl)
                .IsRequired()
                .HasMaxLength(ImageUrlMaxLength);

            entity
                .Property(a => a.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            entity
                .HasQueryFilter(g => g.IsDeleted == false);

            entity.HasData(this.SeedAnimes());
        }
             public List<Anime> SeedAnimes()
        {
            List<Anime> animes = new List<Anime>()
                {
                    new Anime()
                    {
                        Id = 1,
                        Title = "Sousou no Frieren",
                        Episodes= 28,
                        AirDate = new DateOnly(2023, 9, 29),
                        EndDate = new DateOnly(2024, 3, 22),
                        Synopsis = "During their decade-long quest to defeat the Demon King, the members of the hero's party—Himmel himself, the priest Heiter, the dwarf warrior Eisen, and the elven mage Frieren—forge bonds through adventures and battles, creating unforgettable precious memories for most of them.\r\n\r\nHowever, the time that Frieren spends with her comrades is equivalent to merely a fraction of her life, which has lasted over a thousand years. When the party disbands after their victory, Frieren casually returns to her \"usual\" routine of collecting spells across the continent. Due to her different sense of time, she seemingly holds no strong feelings toward the experiences she went through.\r\n\r\nAs the years pass, Frieren gradually realizes how her days in the hero's party truly impacted her. Witnessing the deaths of two of her former companions, Frieren begins to regret having taken their presence for granted; she vows to better understand humans and create real personal connections. Although the story of that once memorable journey has long ended, a new tale is about to begin.",
                        ImageUrl = "https://cdn.myanimelist.net/images/anime/1015/138006.jpg"
                    },
                    new Anime()
                    {
                        Id = 2,
                        Title = "Fullmetal Alchemist: Brotherhood",
                        Episodes = 64,
                        AirDate = new DateOnly(2009, 4, 5),
                        EndDate = new DateOnly(2010, 7, 4),
                        Synopsis = "After a horrific alchemy experiment goes wrong in the Elric household, brothers Edward and Alphonse are left in a catastrophic new reality. Ignoring the alchemical principle banning human transmutation, the boys attempted to bring their recently deceased mother back to life. Instead, they suffered brutal personal loss: Alphonse's body disintegrated while Edward lost a leg and then sacrificed an arm to keep Alphonse's soul in the physical realm by binding it to a hulking suit of armor.\r\n\r\nThe brothers are rescued by their neighbor Pinako Rockbell and her granddaughter Winry. Known as a bio-mechanical engineering prodigy, Winry creates prosthetic limbs for Edward by utilizing \"automail,\" a tough, versatile metal used in robots and combat armor. After years of training, the Elric brothers set off on a quest to restore their bodies by locating the Philosopher's Stone—a powerful gem that allows an alchemist to defy the traditional laws of Equivalent Exchange.\r\n\r\nAs Edward becomes an infamous alchemist and gains the nickname \"Fullmetal,\" the boys' journey embroils them in a growing conspiracy that threatens the fate of the world.",
                        ImageUrl = "https://cdn.myanimelist.net/images/anime/1223/96541.jpg"
                    },
                    new Anime()
                    {
                        Id = 3,
                        Title = "Steins;Gate",
                        Episodes = 24,
                        AirDate = new DateOnly(2011, 4, 6),
                        EndDate = new DateOnly(2011, 9, 14),
                        Synopsis = "Eccentric scientist Rintarou Okabe has a never-ending thirst for scientific exploration. Together with his ditzy but well-meaning friend Mayuri Shiina and his roommate Itaru Hashida, Okabe founds the Future Gadget Laboratory in the hopes of creating technological innovations that baffle the human psyche. Despite claims of grandeur, the only notable \"gadget\" the trio have created is a microwave that has the mystifying power to turn bananas into green goo.\r\n\r\nHowever, when Okabe attends a conference on time travel, he experiences a series of strange events that lead him to believe that there is more to the \"Phone Microwave\" gadget than meets the eye. Apparently able to send text messages into the past using the microwave, Okabe dabbles further with the \"time machine,\" attracting the ire and attention of the mysterious organization SERN.\r\n\r\nDue to the novel discovery, Okabe and his friends find themselves in an ever-present danger. As he works to mitigate the damage his invention has caused to the timeline, Okabe fights a battle to not only save his loved ones but also to preserve his degrading sanity.",
                        ImageUrl = "https://cdn.myanimelist.net/images/anime/5/73199.jpg"
                    },
                    new Anime()
                    {
                        Id = 4,
                        Title = "Hunter x Hunter (2011)",
                        Episodes = 148,
                        AirDate = new DateOnly(2011, 10, 2),
                        EndDate = new DateOnly(2014, 9, 24),
                        Synopsis = "Hunters devote themselves to accomplishing hazardous tasks, all from traversing the world's uncharted territories to locating rare items and monsters. Before becoming a Hunter, one must pass the Hunter Examination—a high-risk selection process in which most applicants end up handicapped or worse, deceased.\r\n\r\nAmbitious participants who challenge the notorious exam carry their own reason. What drives 12-year-old Gon Freecss is finding Ging, his father and a Hunter himself. Believing that he will meet his father by becoming a Hunter, Gon takes the first step to walk the same path.\r\n\r\nDuring the Hunter Examination, Gon befriends the medical student Leorio Paladiknight, the vindictive Kurapika, and ex-assassin Killua Zoldyck. While their motives vastly differ from each other, they band together for a common goal and begin to venture into a perilous world.",
                        ImageUrl = "https://cdn.myanimelist.net/images/anime/1337/99013.jpg"
                    },
                    new Anime()
                    {
                        Id = 5,
                        Title = "Attack On Titan",
                        Episodes = 25,
                        AirDate = new DateOnly(2013, 4, 7),
                        EndDate = new DateOnly(2013, 9, 29),
                        Synopsis = "Centuries ago, mankind was slaughtered to near extinction by monstrous humanoid creatures called Titans, forcing humans to hide in fear behind enormous concentric walls. What makes these giants truly terrifying is that their taste for human flesh is not born out of hunger but what appears to be out of pleasure. To ensure their survival, the remnants of humanity began living within defensive barriers, resulting in one hundred years without a single titan encounter. However, that fragile calm is soon shattered when a colossal Titan manages to breach the supposedly impregnable outer wall, reigniting the fight for survival against the man-eating abominations.\r\n\r\nAfter witnessing a horrific personal loss at the hands of the invading creatures, Eren Yeager dedicates his life to their eradication by enlisting into the Survey Corps, an elite military unit that combats the merciless humanoids outside the protection of the walls. Eren, his adopted sister Mikasa Ackerman, and his childhood friend Armin Arlert join the brutal war against the Titans and race to discover a way of defeating them before the last walls are breached.",
                        ImageUrl = "https://cdn.myanimelist.net/images/anime/10/47347.jpg"
                    },
                    new Anime()
                    {
                        Id = 6,
                        Title = "Death Note",
                        Episodes = 37,
                        AirDate = new DateOnly(2006, 10, 4),
                        EndDate = new DateOnly(2007, 6, 27),
                        Synopsis = "Brutal murders, petty thefts, and senseless violence pollute the human world. In contrast, the realm of death gods is a humdrum, unchanging gambling den. The ingenious 17-year-old Japanese student Light Yagami and sadistic god of death Ryuk share one belief: their worlds are rotten.\r\n\r\nFor his own amusement, Ryuk drops his Death Note into the human world. Light stumbles upon it, deeming the first of its rules ridiculous: the human whose name is written in this note shall die. However, the temptation is too great, and Light experiments by writing a felon's name, which disturbingly enacts his first murder.\r\n\r\nAware of the terrifying godlike power that has fallen into his hands, Light—under the alias Kira—follows his wicked sense of justice with the ultimate goal of cleansing the world of all evil-doers. The meticulous mastermind detective L is already on his trail, but as Light's brilliance rivals L's, the grand chase for Kira turns into an intense battle of wits that can only end when one of them is dead.",
                        ImageUrl = "https://cdn.myanimelist.net/images/anime/9/9453.jpg"
                    },
                    new Anime()
                    {
                        Id = 7,
                        Title = "Demon Slayer",
                        Episodes = 26,
                        AirDate = new DateOnly(2019, 4, 6),
                        EndDate = new DateOnly(2019, 9, 28),
                        Synopsis = "Ever since the death of his father, the burden of supporting the family has fallen upon Tanjirou Kamado's shoulders. Though living impoverished on a remote mountain, the Kamado family are able to enjoy a relatively peaceful and happy life. One day, Tanjirou decides to go down to the local village to make a little money selling charcoal. On his way back, night falls, forcing Tanjirou to take shelter in the house of a strange man, who warns him of the existence of flesh-eating demons that lurk in the woods at night.\r\n\r\nWhen he finally arrives back home the next day, he is met with a horrifying sight—his whole family has been slaughtered. Worse still, the sole survivor is his sister Nezuko, who has been turned into a bloodthirsty demon. Consumed by rage and hatred, Tanjirou swears to avenge his family and stay by his only remaining sibling. Alongside the mysterious group calling themselves the Demon Slayer Corps, Tanjirou will do whatever it takes to slay the demons and protect the remnants of his beloved sister's humanity.",
                        ImageUrl = "https://cdn.myanimelist.net/images/anime/1286/99889.jpg"
                    },
                    new Anime()
                    {
                        Id = 8,
                        Title = "Jujutsu Kaisen",
                        Episodes = 24,
                        AirDate = new DateOnly(2020, 10, 3),
                        EndDate = new DateOnly(2021, 3, 27),
                        Synopsis = "Idly indulging in baseless paranormal activities with the Occult Club, high schooler Yuuji Itadori spends his days at either the clubroom or the hospital, where he visits his bedridden grandfather. However, this leisurely lifestyle soon takes a turn for the strange when he unknowingly encounters a cursed item. Triggering a chain of supernatural occurrences, Yuuji finds himself suddenly thrust into the world of Curses—dreadful beings formed from human malice and negativity—after swallowing the said item, revealed to be a finger belonging to the demon Sukuna Ryoumen, the King of Curses.\r\n\r\nYuuji experiences first-hand the threat these Curses pose to society as he discovers his own newfound powers. Introduced to the Tokyo Prefectural Jujutsu High School, he begins to walk down a path from which he cannot return—the path of a Jujutsu sorcerer.",
                        ImageUrl = "https://cdn.myanimelist.net/images/anime/1171/109222.jpg"
                    },
                    new Anime()
                    {
                        Id = 9,
                        Title = "My Hero Academia",
                        Episodes = 13,
                        AirDate = new DateOnly(2016, 4, 3),
                        EndDate = new DateOnly(2016, 6, 26),
                        Synopsis = "The appearance of \"quirks,\" newly discovered super powers, has been steadily increasing over the years, with 80 percent of humanity possessing various abilities from manipulation of elements to shapeshifting. This leaves the remainder of the world completely powerless, and Izuku Midoriya is one such individual.\r\n\r\nSince he was a child, the ambitious middle schooler has wanted nothing more than to be a hero. Izuku's unfair fate leaves him admiring heroes and taking notes on them whenever he can. But it seems that his persistence has borne some fruit: Izuku meets the number one hero and his personal idol, All Might. All Might's quirk is a unique ability that can be inherited, and he has chosen Izuku to be his successor!\r\n\r\nEnduring many months of grueling training, Izuku enrolls in UA High, a prestigious high school famous for its excellent hero training program, and this year's freshmen look especially promising. With his bizarre but talented classmates and the looming threat of a villainous organization, Izuku will soon learn what it really means to be a hero.",
                        ImageUrl = "https://cdn.myanimelist.net/images/anime/10/78745.jpg"
                    },
                    new Anime()
                    {
                        Id = 10,
                        Title = "One Punch Man",
                        Episodes = 12,
                        AirDate = new DateOnly(2015, 10, 5),
                        EndDate = new DateOnly(2015, 12, 21),
                        Synopsis = "The seemingly unimpressive Saitama has a rather unique hobby: being a hero. In order to pursue his childhood dream, Saitama relentlessly trained for three years, losing all of his hair in the process. Now, Saitama is so powerful, he can defeat any enemy with just one punch. However, having no one capable of matching his strength has led Saitama to an unexpected problem—he is no longer able to enjoy the thrill of battling and has become quite bored.\r\n\r\nOne day, Saitama catches the attention of 19-year-old cyborg Genos, who witnesses his power and wishes to become Saitama's disciple. Genos proposes that the two join the Hero Association in order to become certified heroes that will be recognized for their positive contributions to society. Saitama, who is shocked that no one knows who he is, quickly agrees. Meeting new allies and taking on new foes, Saitama embarks on a new journey as a member of the Hero Association to experience the excitement of battle he once felt.",
                        ImageUrl = "https://cdn.myanimelist.net/images/anime/12/76049.jpg"
                    }
                };
            return animes;
        }
    }
}
