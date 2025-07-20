
namespace AniTrack.Data.Configuration
{
    using AniTrack.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using static Common.EntityConstants.Genre;
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> entity)
        {
            entity
                .HasKey(g => g.Id);
            entity
                .Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(GenreNameMaxLength);
        }
    }
}
