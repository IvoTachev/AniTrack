namespace AniTrack.Data.Repository
{
    using AniTrack.Data.Models;
    using AniTrack.Data.Repository.Interface;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationUserRepository : BaseRepository<ApplicationUser, string>, IApplicationUserRepository
    {
        public ApplicationUserRepository(AniTrackDbContext dbContext)
            : base(dbContext)
        {
        }
        public async Task<string?> GetUserIdByUsernameAsync(string username)
        {
            // Users are not allowed to register without Username.
            return await this
                .GetAllAttached()
                .Where(u => u.UserName.ToLower() == username.ToLower())
                .Select(u => u.Id)
                .FirstOrDefaultAsync();
        }
    }
}
