namespace AniTrack.Web.Controllers
{
    using AniTrack.Services.Core.Interfaces;
    using AniTrack.Web.ViewModels.Review;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
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
    }
}
