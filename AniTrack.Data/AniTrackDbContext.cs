namespace AniTrack.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Reflection;

    public class AniTrackDbContext : IdentityDbContext<ApplicationUser>
    {
        public AniTrackDbContext(DbContextOptions<AniTrackDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Anime> Animes { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<AnimeGenre> AnimesGenres { get; set; } = null!;
        public virtual DbSet<UserAnime> UsersAnimes { get; set; } = null!;
        public virtual DbSet<AnimeReview> AnimeReviews { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
