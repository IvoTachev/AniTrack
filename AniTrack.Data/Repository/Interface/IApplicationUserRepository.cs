namespace AniTrack.Data.Repository.Interface
{
    using AniTrack.Data.Models;

    public interface IApplicationUserRepository 
        : IRepository<ApplicationUser, string>, IAsyncRepository<ApplicationUser, string>
    {
        // Gets a user Id by the user's username asynchronously.
        Task<string?> GetUserIdByUsernameAsync(string username);
    }
}
