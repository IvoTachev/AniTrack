namespace AniTrack.Services.Core.Interfaces
{
    using AniTrack.Web.ViewModels.Home;
    public interface IHomeService
    {
        // Gets the index view model for the home page
        // Uses a wrapper view model to include the top animes and the latest reviews
        Task<HomeIndexViewModel> GetIndexViewModelAsync();
    }
}
