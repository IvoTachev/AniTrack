namespace AniTrack.Services.Core
{
    using AniTrack.Data.Models;
    using AniTrack.Data.Repository.Interface;
    using AniTrack.Services.Core.Interfaces;
    using AniTrack.Web.ViewModels.Review;

    public class ReviewService : IReviewService
    {
        private readonly IAnimeReviewRepository reviewRepository;
        public ReviewService(IAnimeReviewRepository reviewRepository)
        {
            this.reviewRepository = reviewRepository;
        }
        public async Task<ReviewPageViewModel> GetAllReviewsPagedAsync(int page, int pageSize)
        {
            List<AnimeReview> allAnimeReviews = await this.reviewRepository
                                                           .GetAllAnimeReviewsAsync();
            List<AnimeReviewViewModel> allAnimeReviewsViewModels = allAnimeReviews
                .OrderByDescending(r => r.CreatedOn)
                .Select(vm => new AnimeReviewViewModel
            {
                AuthorName = vm.Author.UserName,
                AnimeId = vm.AnimeId.ToString(),
                AnimeTitle = vm.Anime.Title,
                AnimeImageUrl = vm.Anime.ImageUrl,
                Content = vm.Content,
                isAnimeRecommended = vm.isAnimeRecommended,
             }).ToList();

            int totalReviews = allAnimeReviewsViewModels.Count;
            int totalPages = (int)Math.Ceiling(totalReviews / (double)pageSize);

            List<AnimeReviewViewModel> pagedReviews = allAnimeReviewsViewModels
               .Skip((page - 1) * pageSize)
               .Take(pageSize)
               .ToList();

            return new ReviewPageViewModel
            {
                Reviews = pagedReviews,
                CurrentPage = page,
                TotalPages = totalPages
            };
        }
    }
}
