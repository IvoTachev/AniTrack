namespace AniTrack.Web.Controllers
{
    using AniTrack.Services.Core.Interfaces;
    using AniTrack.Web.ViewModels.Review;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

    public class ReviewController : BaseController
    {
        private readonly IReviewService reviewService;

        public ReviewController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
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

        public async Task<IActionResult> Users(string username,int page = 1)
        {
            if (username == null)
            {
                return RedirectToAction(nameof(Index));
            }

            const int pageSize = 5;
            ReviewPageViewModel viewModel = await reviewService.GetUserReviewsPagedAsync(username, page, pageSize);

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
                return this.View(inputModel);
            }
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                await this.reviewService.WriteReviewAsync(inputModel, userId);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return this.View(inputModel);
            }
        }

    }
}
