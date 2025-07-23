namespace AniTrack.Data.Configuration
{
    using AniTrack.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserAnimeConfiguration : IEntityTypeConfiguration<UserAnime>
    {
        public void Configure(EntityTypeBuilder<UserAnime> entity)
        {
            // Composite Primary Key
            entity
                .HasKey(ua => new { ua.UserId, ua.AnimeId });

            // Relation between UserAnime and IdentityUser 
            entity
                .HasOne(ua => ua.User)
                .WithMany()
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            //Relation between UserAnime and Anime
            entity
                .HasOne(ua => ua.Anime)
                .WithMany(a => a.UserWatchlists)
                .HasForeignKey(ua => ua.AnimeId)
                .OnDelete(DeleteBehavior.Restrict);
            // Setting up the IsDeleted property for soft deletion
            entity
               .Property(au => au.IsDeleted)
               .IsRequired()
               .HasDefaultValue(false);

            // Query filter for soft deletion
            entity
                .HasQueryFilter(au => au.Anime.IsDeleted == false && au.IsDeleted == false);
        }
    }
}
