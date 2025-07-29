namespace AniTrack.Services.Core.Admin
{
    using AniTrack.Data.Models;
    using AniTrack.Data.Repository.Interface;
    using AniTrack.Services.Core.Admin.Interfaces;
    using AniTrack.Web.ViewModels.Admin.Home;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using static AniTrack.GCommon.ApplicationConstants;

    public class HomeService : IHomeService
    {
        private readonly IAnimeRepository animeRepository;
        private readonly IAnimeGenreRepository animeGenreRepository;

        public HomeService(IAnimeRepository animeRepository, IAnimeGenreRepository animeGenreRepository)
        {
            this.animeRepository = animeRepository;
            this.animeGenreRepository = animeGenreRepository;
        }
        public async Task<List<RestoreAnimesViewModel>> GetAnimesForRestoreAsync()
        {
            List<RestoreAnimesViewModel> viewModelList = new List<RestoreAnimesViewModel>();
            List<Anime> Animes = await this.animeRepository
                .GetAllAttached()
                .IgnoreQueryFilters()
                .Where(a => a.IsDeleted)
                .ToListAsync();

            foreach (Anime anime in Animes)
            {
                RestoreAnimesViewModel viewModel = new RestoreAnimesViewModel
                {
                    Id = anime.Id.ToString(),
                    Title = anime.Title,
                    ImageUrl = anime.ImageUrl,
                    Episodes = anime.Episodes,
                    AirDate = anime.AirDate.ToString(ApplicationDateFormat),
                    EndDate = anime.EndDate.HasValue
                                            ? anime.EndDate.Value.ToString(ApplicationDateFormat)
                                            : "???"
                };
                viewModelList.Add(viewModel);
            }
            return viewModelList;
        }
    }
}
