namespace AniTrack.Web.Controllers
{
    using AniTrack.Services.Core.Interfaces;
    using AniTrack.Web.ViewModels;
    using AniTrack.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    public class HomeController : BaseController
    {
        private readonly IHomeService homeService;

        public HomeController(IHomeService homeService)
        {
            this.homeService = homeService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HomeIndexViewModel viewModel = await homeService.GetIndexViewModelAsync();
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Error(int? statusCode, string? ReturnUrl)
        {
            if (statusCode == null)
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
            else if (statusCode == 401)
            {
                // Potentially useless, cookie middleware redirects to Login page
                Response.StatusCode = 401;
                return View("UnauthorizedError");
            }
            else if (statusCode == 403)
            {
                // This is handled by the cookie middleware, but we can still return a view
                Response.StatusCode = 403;
                return View("UnauthorizedError");
            }
            else if (statusCode == 404)
            {
                Response.StatusCode = 404;

                return View("NotFoundError");
            }
            else if (statusCode == 500)
            {
                Response.StatusCode = 500;
                return View("ServerError");
            }
            else
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            // Cookie middleware does not handle query parameters, so we handle it here

            Response.StatusCode = 403;
            return View("UnauthorizedError");
        }
    }
}
