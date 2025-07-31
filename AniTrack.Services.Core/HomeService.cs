namespace AniTrack.Services.Core
{
    using AniTrack.Data.Models;
    using AniTrack.Data.Repository.Interface;
    using AniTrack.Services.Core.Interfaces;
    using AniTrack.Web.ViewModels.Home;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    using static AniTrack.GCommon.ApplicationConstants;
    public class HomeService : IHomeService
    {
        private readonly IAnimeReviewRepository reviewRepository;
        private readonly IAnimeRepository animeRepository;
        public HomeService(IAnimeReviewRepository reviewRepository, IAnimeRepository animeRepository)
        {
            this.reviewRepository = reviewRepository;
            this.animeRepository = animeRepository;
        }

        public async Task<HomeIndexViewModel> GetIndexViewModelAsync()
        {
            // Take the 3 animes with most positive reviews
            List<Anime> animes = await animeRepository
                .GetAllAttached()
                .OrderByDescending(a => a.Reviews.Count(r => r.isAnimeRecommended == true))
                .Take(3)
                .ToListAsync();

            List<RecommendedAnimeViewModel> recommendedAnimes = animes
                .Select(ra => new RecommendedAnimeViewModel
                {
                    AnimeId = ra.Id.ToString(),
                    AnimeTitle = ra.Title,
                    ImageUrl = ra.ImageUrl,
                    Episodes = ra.Episodes,
                    AirDate = ra.AirDate.ToString(ApplicationDateFormat),
                    EndDate = ra.EndDate?.ToString(ApplicationDateFormat) ?? "???"

                })
                .ToList();

            List<AnimeReview> reviews = await reviewRepository
                .GetAllAnimeReviewsAsync();
            List<AnimeReview> topReviews = reviews.OrderByDescending(r => r.CreatedOn).Take(3).ToList();
           

            List<RecentReviewsViewModel> recentReviews = topReviews
                .Select(rr => new RecentReviewsViewModel
                {
                    AuthorName = rr.Author.UserName!,
                    ImageUrl = rr.Anime.ImageUrl,
                    AnimeId = rr.AnimeId.ToString(),
                    AnimeTitle = rr.Anime.Title,
                    Content = rr.Content,
                    IsRecommended = rr.isAnimeRecommended
                })
                .ToList();

            return new HomeIndexViewModel()
            {
                RecommendedAnimes = recommendedAnimes,
                RecentReviews = recentReviews
            };


        }
    }
}
