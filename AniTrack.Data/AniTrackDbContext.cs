namespace AniTrack.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Reflection;

    public class AniTrackDbContext : IdentityDbContext
    {
        public AniTrackDbContext(DbContextOptions<AniTrackDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Anime> Animes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
