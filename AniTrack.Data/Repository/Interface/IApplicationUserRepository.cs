namespace AniTrack.Data.Repository.Interface
{
    using AniTrack.Data.Models;

    public interface IApplicationUserRepository 
        : IRepository<ApplicationUser, string>, IAsyncRepository<ApplicationUser, string>
    {
        Task<string?> GetUserIdByUsernameAsync(string username);
    }
}
