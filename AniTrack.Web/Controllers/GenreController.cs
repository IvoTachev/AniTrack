namespace AniTrack.Web.Controllers
{
    using AniTrack.Services.Core.Interfaces;
    using AniTrack.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static AniTrack.GCommon.ApplicationConstants;
    using static AniTrack.GCommon.ExceptionMessages;

    public class GenreController : BaseController
    {
        private readonly IGenreService genreService;

        public GenreController(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        [HttpGet]
        [Route("Genre/{genreName}")]
        [AllowAnonymous]
        public async Task<IActionResult> Index(string genreName)
        {
            try
            {
                GenreViewModel genreViewModel = await this.genreService.GetAnimesDetailsByGenreNameAsync(genreName);
                if (genreViewModel == null)
                {
                    return RedirectToAction("Error", "Home", new { statusCode = 404 });
                }
                return View(genreViewModel);
            }
            catch (Exception)
            {
                TempData[ErrorMessageKey] = GenreAnimesRetrieveErrorMessage;
                return this.RedirectToAction(nameof(Index));
            }
        }
    }
}

