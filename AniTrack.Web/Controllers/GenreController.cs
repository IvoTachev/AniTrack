namespace AniTrack.Web.Controllers
{
    using AniTrack.Services.Core.Interfaces;
    using AniTrack.Web.ViewModels.Anime;
    using AniTrack.Web.ViewModels.Genre;
    using AniTrack.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static AniTrack.GCommon.ApplicationConstants;
    using static AniTrack.GCommon.ExceptionMessages;

    public class GenreController : BaseController
    {
        private readonly IGenreService genreService;
        private readonly ILogger<GenreController> logger;
        public GenreController(IGenreService genreService, ILogger<GenreController> logger)
        {
            this.genreService = genreService;
            this.logger = logger;
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
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error retrieving animes for genre: {genreName}");
                TempData[ErrorMessageKey] = GenreAnimesRetrieveErrorMessage;
                return this.RedirectToAction(nameof(Index));
            }
        }
        [Authorize(Roles = "Admin")]
        [Route("Genre/Add")]
        [HttpGet]

        public IActionResult Add()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [Route("Genre/Add")]
        [HttpPost]

        public async Task<IActionResult> Add(AddGenreFormModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            try
            {
                bool result = await this.genreService.AddGenreAsync(inputModel.Name);
                if (result == false)
                {
                    TempData[ErrorMessageKey] = GenreAlreadyExistsErrorMessage;
                    return this.View(inputModel);
                }

                TempData[SuccessMessageKey] = GenreAddSuccessMessage;
                return RedirectToAction("Search", "Anime", new { searchTerm = "" });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while adding a genre.");
                TempData[ErrorMessageKey] = GenreAddErrorMessage;
                return this.View(inputModel);
            }
        }

        [Authorize(Roles = "Admin")]
        [Route("Genre/Delete")]
        [HttpGet]

        public async Task<IActionResult> Delete(string? SelectedGenreName)
        {
            DeleteGenreViewModel? viewModel = await this.genreService.GetAllGenreDetailsForDeleteAsync(SelectedGenreName);
            if(viewModel == null)
            {
                // If the view model is null, it means the selected genre does not exist
                // return 404
                return NotFound();
            }
            // If the view model is not null, it means either the selected genre exists or nothing was selected.
            // We can return the view model and the view will display based on a genre is selected or not.
            return View(viewModel);

        }

        [Authorize(Roles = "Admin")]
        [Route("Genre/Delete")]
        [HttpPost]
        [ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(string? SelectedGenreName)
        {
            if(SelectedGenreName == null || SelectedGenreName.Trim() == string.Empty)
            {
                TempData[ErrorMessageKey] = GenreSelectAGenreErrorMessage;
                return RedirectToAction("Delete");
            }
            try
            {
                bool result = await this.genreService.DeleteGenreByNameAsync(SelectedGenreName);
                if (result)
                {
                    TempData[SuccessMessageKey] = GenreDeleteSuccessMessage;
                    return RedirectToAction("Search", "Anime", new { searchTerm = "" });
                }
                else
                {
                    TempData[ErrorMessageKey] = GenreInvalidDeleteErrorMessage;
                    return RedirectToAction("Delete");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while trying to delete genre: {GenreName}.", SelectedGenreName);
                TempData[ErrorMessageKey] = GenreDeleteErrorMessage;
                return RedirectToAction("Search", "Anime", new { searchTerm = "" });
            }
        }
    }
}

