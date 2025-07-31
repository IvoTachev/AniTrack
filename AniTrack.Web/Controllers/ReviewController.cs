namespace AniTrack.Web.Controllers
{
    using AniTrack.Services.Core.Interfaces;
    using AniTrack.Web.ViewModels.Review;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    using static AniTrack.GCommon.ExceptionMessages;
    using static AniTrack.GCommon.ApplicationConstants;

    public class ReviewController : BaseController
    {
        private readonly IReviewService reviewService;
        private readonly ILogger<ReviewController> logger;
        public ReviewController(IReviewService reviewService, ILogger<ReviewController> logger)
        {
            this.reviewService = reviewService;
            this.logger = logger;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            const int pageSize = 5;
            ReviewPageViewModel viewModel = await reviewService.GetAllReviewsPagedAsync(page, pageSize);
            return View(viewModel);
        }

        [HttpGet("Review/Users/{username}")]

        public async Task<IActionResult> Users(string username, int page = 1)
        {
            if (username == null)
            {
                TempData[ErrorMessageKey] = ReviewUsersErrorMessage;
                return RedirectToAction(nameof(Index));
            }

            const int pageSize = 5;
            ReviewUserPageViewModel viewModel = await reviewService.GetUserReviewsPagedAsync(username, page, pageSize);

            string? currentUsername = User.Identity?.Name;

            if ((viewModel == null || viewModel.Reviews.Count() == 0) && currentUsername != username)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        [HttpGet("Review/Write/{animeId}")]
        public async Task<IActionResult> Write(string animeId)
        {
            ReviewWriteViewModel viewModel = await this.reviewService.GetWriteFormAsync(animeId);
            if (viewModel == null || viewModel.AnimeTitle == string.Empty)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        [HttpPost("Review/Write/{animeId}")]
        public async Task<IActionResult> Write(string animeId, ReviewWriteViewModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                // Returns view with validation errors
                return this.View(inputModel);
            }
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            try
            {
                await this.reviewService.WriteReviewAsync(inputModel, userId!);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log the error and return an error message
                logger.LogError(ex, $"Error writing review for AnimeId {animeId} by UserId {userId}");
                TempData[ErrorMessageKey] = ReviewWriteErrorMessage;
                return this.View(inputModel);
            }
        }

        [HttpGet("Review/Edit/{animeId}/{authorId}")]
        public async Task<IActionResult> Edit(string animeId, string authorId)
        {
            string? currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId != authorId)
            {
                TempData[ErrorMessageKey] = ReviewWrongAuthorErrorMessage;
                return Forbid();
            }
            ReviewEditViewModel? viewModel = await this.reviewService.GetEditFormAsync(animeId, authorId);

            if (viewModel == null || viewModel.AnimeTitle == string.Empty)
            {
                return NotFound();
            }
            
            return View(viewModel);


        }

        [HttpPost("Review/Edit/{animeId}/{authorId}")]
        public async Task<IActionResult> Edit(int animeId, string authorId, ReviewEditViewModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                // Returns view with validation errors
                return this.View(inputModel);
            }
            string? currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId != authorId)
            {
                TempData[ErrorMessageKey] = ReviewWrongAuthorErrorMessage;
                return Forbid();
            }
            try
            {
                bool isEdited = await this.reviewService.EditReviewAsync(inputModel);
                if (isEdited)
                {
                    TempData[SuccessMessageKey] = ReviewEditSuccessMessage;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData[ErrorMessageKey] = ReviewWriteErrorMessage;
                    return this.View(inputModel);
                }
            }
            catch (Exception ex)
            {
                // Log the error and return an error message
                logger.LogError(ex, $"Error editing review for AnimeId {animeId} by UserId {authorId}");
                TempData[ErrorMessageKey] = ReviewWriteErrorMessage;
                return this.RedirectToAction(nameof(Index));
            }
        }
    }
}
