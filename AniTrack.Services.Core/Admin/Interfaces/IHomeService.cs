namespace AniTrack.Services.Core.Admin.Interfaces
{
    using AniTrack.Web.ViewModels.Admin.Home;
    public interface IHomeService
    {
        Task<List<RestoreAnimesViewModel>> GetAnimesForRestoreAsync();
    }
}
