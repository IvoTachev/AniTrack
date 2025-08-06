namespace AniTrack.Services.Core.Admin.Interfaces
{
    using AniTrack.Web.ViewModels.Admin.Home;
    public interface IHomeService
    {
        // Gets all of the soft deleted animes
        Task<List<RestoreAnimesViewModel>> GetAnimesForRestoreAsync();
    }
}
