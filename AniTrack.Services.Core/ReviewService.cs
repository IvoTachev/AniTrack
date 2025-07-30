namespace AniTrack.Services.Core
{
    using AniTrack.Data.Models;
    using AniTrack.Data.Repository.Interface;
    using AniTrack.Services.Core.Interfaces;
    using AniTrack.Web.ViewModels.Review;

    public class ReviewService : IReviewService
    {
        private readonly IAnimeReviewRepository reviewRepository;
        private readonly IAnimeRepository animeRepository;
        public ReviewService(IAnimeReviewRepository reviewRepository, IAnimeRepository animeRepository)
        {
            this.reviewRepository = reviewRepository;
            this.animeRepository = animeRepository;
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

        public async Task<ReviewPageViewModel> GetUserReviewsPagedAsync(string username, int page, int pageSize)
        {
            List<AnimeReview> allAnimeReviews = await this.reviewRepository
                                                           .GetAllAnimeReviewsAsync();
            List<AnimeReviewViewModel> userAnimeReviewsViewModels = allAnimeReviews
                .Where(r => r.Author.UserName == username)
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

            int totalReviews = userAnimeReviewsViewModels.Count;
            int totalPages = (int)Math.Ceiling(totalReviews / (double)pageSize);

            return new ReviewPageViewModel
            {
                Reviews = userAnimeReviewsViewModels
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList(),
                CurrentPage = page,
                TotalPages = totalPages,
                AuthorName = username
            };
        }

        public async Task<ReviewWriteViewModel> GetWriteFormAsync(string animeId)
        {
            int.TryParse(animeId, out int validId);
            Anime? anime = await this.animeRepository.GetByIdAsync(validId);
            ReviewWriteViewModel viewModel = new ReviewWriteViewModel
            {
                AnimeId = validId,
                AnimeTitle = anime?.Title ?? string.Empty,
                AnimeImageUrl = anime?.ImageUrl ?? string.Empty,
                Content = string.Empty
            };

            return viewModel;
        }

        public async Task WriteReviewAsync(ReviewWriteViewModel inputModel, string userId)
        {
            AnimeReview newReview = new AnimeReview
            {
                Content = inputModel.Content,
                isAnimeRecommended = inputModel.isAnimeRecommended ?? false,
                AuthorId = userId,
                AnimeId = inputModel.AnimeId,
                CreatedOn = DateTime.UtcNow,
            };

            await this.reviewRepository.AddAsync(newReview);
        }
    }
}
