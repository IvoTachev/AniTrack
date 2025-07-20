namespace AniTrack.Services.Core.Interfaces
{
    using AniTrack.Web.ViewModels.Anime;
    public interface IAnimeService
    {
        Task<IEnumerable<TopAnimesViewModel>> GetTopAnimesAsync();
    }
}
