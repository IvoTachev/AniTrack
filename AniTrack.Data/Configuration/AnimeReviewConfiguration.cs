namespace AniTrack.Data.Configuration
{
    using AniTrack.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using static AniTrack.Common.EntityConstants.AnimeReview;
    public class AnimeReviewConfiguration : IEntityTypeConfiguration<AnimeReview>
    {

        public void Configure(EntityTypeBuilder<AnimeReview> entity)
        {
            // Composite Primary Key
            entity
                .HasKey(ar => new { ar.AuthorId, ar.AnimeId });
            // Relation between AnimeReview and ApplicationUser 
            entity
                .HasOne(ar => ar.Author)
                .WithMany(u => u.Reviews)
                .HasForeignKey(ar => ar.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);
            // Relation between AnimeReview and Anime
            entity
                .HasOne(ar => ar.Anime)
                .WithMany(a => a.Reviews)
                .HasForeignKey(ar => ar.AnimeId)
                .OnDelete(DeleteBehavior.Restrict);
            // Setting up the Content property
            entity.
                Property(ar => ar.Content)
                .IsRequired()
                .HasMaxLength(ReviewContentMaxLength);
            // Setting up the CreatedOn property
            entity
                .Property(ar => ar.CreatedOn)
                .IsRequired()
                .HasDefaultValueSql(CreatedOnDefaultValue); // Default value for CreatedOn

            // Setting up the IsDeleted property for soft deletion
            entity
               .Property(ar => ar.IsDeleted)
               .IsRequired()
               .HasDefaultValue(false);
            // Setting up the isAnimeRecommended property
            entity
                .Property(ar => ar.isAnimeRecommended)
                .IsRequired();
            // Query filter for soft deletion
            entity
                .HasQueryFilter(ar => ar.Anime.IsDeleted == false && ar.IsDeleted == false);
        }
    }
}
