namespace AniTrack.Services.Core
{
    using AniTrack.Data.Models;
    using AniTrack.Data.Repository.Interface;
    using AniTrack.Services.Core.Interfaces;
    using AniTrack.Web.ViewModels.Review;
    using Microsoft.EntityFrameworkCore;

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
                AuthorName = vm.Author.UserName!,
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

    

        public async Task<ReviewUserPageViewModel> GetUserReviewsPagedAsync(string username, int page, int pageSize)
        {
            List<AnimeReview> allAnimeReviews = await this.reviewRepository
                                                           .GetAllAnimeReviewsAsync();
            List<AnimeReviewViewModel> userAnimeReviewsViewModels = allAnimeReviews
                .Where(r => r.Author.UserName == username)
                .OrderByDescending(r => r.CreatedOn)
                .Select(vm => new AnimeReviewViewModel
            {
                AuthorName = vm.Author.UserName!,
                AnimeId = vm.AnimeId.ToString(),
                AnimeTitle = vm.Anime.Title,
                AnimeImageUrl = vm.Anime.ImageUrl,
                Content = vm.Content,
                isAnimeRecommended = vm.isAnimeRecommended,
            }).ToList();

            int totalReviews = userAnimeReviewsViewModels.Count;
            int totalPages = (int)Math.Ceiling(totalReviews / (double)pageSize);

            string authorId = allAnimeReviews
                .FirstOrDefault(r => r.Author.UserName == username)?.AuthorId ?? string.Empty;

            return new ReviewUserPageViewModel
            {
                Reviews = userAnimeReviewsViewModels
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList(),
                CurrentPage = page,
                TotalPages = totalPages,
                AuthorName = username,
                AuthorId = authorId
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

        public async Task<ReviewEditViewModel?> GetEditFormAsync(string animeId,string authorId)
        {
            ReviewEditViewModel? viewModel = null;
            int.TryParse(animeId, out int validId);
            Anime? anime = await this.animeRepository.GetByIdAsync(validId);
            
            AnimeReview? review = await this.reviewRepository
                .GetAllAttached()
                .Where(r => r.AnimeId.ToString() == animeId && r.AuthorId == authorId)
                .FirstOrDefaultAsync();
            //Check if this user has written a review for this anime
            if (review != null)
            {
                viewModel = new ReviewEditViewModel
                {
                    AuthorId = authorId,
                    AnimeId = validId,
                    AnimeTitle = anime?.Title ?? string.Empty,
                    AnimeImageUrl = anime?.ImageUrl ?? string.Empty,
                    Content = review.Content,
                    isAnimeRecommended = review.isAnimeRecommended,
                };
            }
            return viewModel;
        }

        public async Task<bool> EditReviewAsync(ReviewEditViewModel inputModel)
        {
            //Check if the inputModel is valid
            if (inputModel == null)
            {
                return false;
            }
            //Check if the review exists for this anime and author
            AnimeReview? editableReview = await this.reviewRepository
                .GetAllAttached()
                .Where(r => r.AnimeId.ToString() == inputModel.AnimeId.ToString() && r.AuthorId == inputModel.AuthorId)
                .FirstOrDefaultAsync();
            //If the review exists, update it
            if (editableReview != null)
            {
                editableReview.Content = inputModel.Content;
                editableReview.isAnimeRecommended = inputModel.isAnimeRecommended ?? false;
                await this.reviewRepository.UpdateAsync(editableReview);
                return true;
            }
            //If the review does not exist, return false
            return false;
        }

        public async Task<ReviewDeleteViewModel?> GetReviewDetailsForDeleteAsync(string animeId, string authorName)
        {
            AnimeReview? animeReview = await this.reviewRepository
                .GetAllAttached()
                .Include(r => r.Anime)
                .Include(r => r.Author)
                .FirstOrDefaultAsync(r => r.AnimeId.ToString() == animeId && r.Author.UserName == authorName);
            ReviewDeleteViewModel? viewModel = null;
            
            if (animeReview == null)
            {
                return viewModel; 
            }

            viewModel = new ReviewDeleteViewModel
            {
               AnimeId = animeReview.AnimeId.ToString(),
               AnimeTitle = animeReview.Anime.Title,
               AuthorName = authorName,
               ImageUrl = animeReview.Anime.ImageUrl,
               Content = animeReview.Content,
               IsRecommended = animeReview.isAnimeRecommended
            };
            return viewModel;

        }

        public async Task<bool> DeleteReviewAsync(string animeId, string authorName)
        {
            AnimeReview? animeReview = await this.reviewRepository
                .GetAllAttached()
                .FirstOrDefaultAsync(r => r.AnimeId.ToString() == animeId && r.Author.UserName == authorName);
            //Check if the review exists
            if (animeReview == null)
            {
                return false;
            }
            //If the review exists, hard delete it to avoid soft delete complications
            bool result = await this.reviewRepository.HardDeleteAsync(animeReview);
            return result;
        }
    }
}
