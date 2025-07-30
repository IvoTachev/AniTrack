namespace AniTrack.Services.Core.Interfaces
{
    using AniTrack.Web.ViewModels.Home;
    public interface IHomeService
    {
        Task<HomeIndexViewModel> GetIndexViewModelAsync();
    }
}
