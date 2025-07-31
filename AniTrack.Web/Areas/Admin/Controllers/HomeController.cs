namespace AniTrack.Web.Areas.Admin.Controllers
{
    using AniTrack.Services.Core.Admin.Interfaces;
    using AniTrack.Web.ViewModels.Admin.Home;
    using Microsoft.AspNetCore.Mvc;
    using static AniTrack.GCommon.ApplicationConstants;
    using static AniTrack.GCommon.ExceptionMessages;
    public class HomeController : BaseAdminController
    {
        private readonly IHomeService homeService;
        private readonly ILogger<HomeController> logger;
        public HomeController(IHomeService homeService, ILogger<HomeController> logger)
        {
            this.homeService = homeService;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> RestoreAnimes()
        {
            try
            {
                List<RestoreAnimesViewModel> viewModel = await homeService.GetAnimesForRestoreAsync();
                return View(viewModel);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while getting animes for restore.");
                TempData[ErrorMessageKey] = AdminGetAnimesForRestoreErrorMessage;
                return this.RedirectToAction(nameof(Index), "Home");
            }
        }
    }
}
