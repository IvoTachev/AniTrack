namespace AniTrack.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    public class AniTrackDbContext : IdentityDbContext
    {
        public AniTrackDbContext(DbContextOptions<AniTrackDbContext> options)
            : base(options)
        {
        }
    }
}
