namespace AniTrack.Web.Areas.Admin.Controllers
{
    using AniTrack.Services.Core.Admin;
    using AniTrack.Services.Core.Admin.Interfaces;
    using AniTrack.Web.ViewModels.Admin.Home;
    using AniTrack.Web.ViewModels.Animelist;
    using Microsoft.AspNetCore.Mvc;
    public class HomeController : BaseAdminController
    {
        private readonly IHomeService homeService;

        public HomeController(IHomeService homeService)
        {
            this.homeService = homeService;
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
                Console.WriteLine(ex);
                return this.RedirectToAction(nameof(Index), "Home");
            }
        }
    }
}
