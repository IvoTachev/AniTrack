namespace AniTrack.Web.Controllers
{
    using AniTrack.Web.ViewModels.Anime;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Core.Interfaces;
    using System.Threading.Tasks;
    using static AniTrack.GCommon.ApplicationConstants;
    using static AniTrack.GCommon.ExceptionMessages;
   


    public class AnimeController : BaseController
    {

        private readonly IAnimeService animeService;
        private readonly IAnimelistService animelistService;

        public AnimeController(IAnimeService animeService, IAnimelistService animelistService)
        {
            this.animeService = animeService;
            this.animelistService = animelistService;
        }

        private async Task<bool> IsAnimeInUserList(string userId, string animeId)
        {
            return await this.animelistService.IsAnimeInUserAnimelist(userId, animeId);
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            const int pageSize = 10;
            AnimePageViewModel viewModel = await animeService.GetPagedAnimesAsync(page, pageSize);
            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Add()
        {
            return this.View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]

        public async Task<IActionResult> Add(AddAnimeFormModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            try
            {
                await this.animeService.AddAnimeAsync(inputModel);

                return this.RedirectToAction(nameof(Details), new { id = inputModel.Id });
            }
            catch (Exception)
            {
                TempData[ErrorMessageKey] = AnimeAddErrorMessage;
                return this.View(inputModel);
            }
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(string? id)
        {
            try
            {
                AnimeDetailsWithReviewViewModel? animeDetailsWithReview = await this.animeService.GetAnimeDetailsWithReviewViewModelAsync(id);
                if (animeDetailsWithReview == null || animeDetailsWithReview.AnimeDetails == null)
                {
                    // Return 404 Not Found
                    return NotFound();
                }

                bool isInAnimelist = false;
                if (User.Identity?.IsAuthenticated == true)
                {
                    string? userId = this.GetUserId();
                    if (userId != null)
                    {
                        isInAnimelist = await IsAnimeInUserList(userId, animeDetailsWithReview.AnimeDetails.Id);
                    }
                }
                ViewBag.IsInAnimelist = isInAnimelist;

                return this.View(animeDetailsWithReview);
            }
            catch (Exception)
            {
                TempData[ErrorMessageKey] = AnimeDetailsErrorMessage;
                return this.RedirectToAction(nameof(Index));
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            try
            {
                EditAnimeFormModel? editAnimeModel = await this.animeService.GetAnimeDetailsByIdAsync(id);
                if (editAnimeModel == null)
                {
                    // Return 404 Not Found
                    return NotFound();
                }

                return this.View(editAnimeModel);
            }
            catch (Exception)
            {
                //Todo: Log the error
                TempData[ErrorMessageKey] = AnimeEditErrorMessage;
                return this.RedirectToAction(nameof(Index));
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditAnimeFormModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }
            try
            {
                bool isEdited = await this.animeService.EditAnimeAsync(inputModel);
                if (!isEdited)
                {
                    TempData[ErrorMessageKey] = AnimeEditErrorMessage;
                    return this.RedirectToAction(nameof(Index));
                }
                TempData[SuccessMessageKey] = AnimeEditSuccessMessage;
                return this.RedirectToAction(nameof(Details), new { id = inputModel.Id });
            }
            catch (Exception)
            {
                //TODO: Log the error
                TempData[ErrorMessageKey] = AnimeEditErrorMessage;
                return this.RedirectToAction(nameof(Index));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            try
            {
                DeleteAnimeViewModel? animeDetails = await this.animeService.GetAnimeDetailsForDeleteByIdAsync(id);
                if (animeDetails == null)
                {
                    // Return 404 Not Found
                    return NotFound();
                }
                return this.View(animeDetails);
            }
            catch (Exception)
            {
                //Todo: Log the error
                TempData[ErrorMessageKey] = AnimeDeleteErrorMessage;
                return this.RedirectToAction(nameof(Index));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteAnimeViewModel inputModel)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.RedirectToAction(nameof(Index));
                }

                bool deleteResult = await this.animeService.SoftDeleteAnimeAsync(inputModel.Id);
                if (!deleteResult)
                {
                    TempData[ErrorMessageKey] = AnimeDeleteErrorMessage;
                    return this.RedirectToAction(nameof(Index));
                }

                TempData[SuccessMessageKey] = AnimeDeleteSuccessMessage;
                return this.RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                //TODO: Log the error
                TempData[ErrorMessageKey] = AnimeDeleteErrorMessage;
                return this.RedirectToAction(nameof(Index));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Restore(string? id)
        {
            try
            {
                DeleteAnimeViewModel? animeDetails = await this.animeService.GetAnimeDetailsForRestoreByIdAsync(id);
                if (animeDetails == null)
                {
                    // Return 404 Not Found
                    return NotFound();
                }
                return this.View(animeDetails);
            }
            catch (Exception)
            {
                //Todo: Log the error
                TempData[ErrorMessageKey] = AnimeRestoreErrorMessage;
                return this.RedirectToAction(nameof(Index));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Restore(DeleteAnimeViewModel inputModel)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.RedirectToAction(nameof(Index));
                }

                bool restoreResult = await this.animeService.RestoreAnimeAsync(inputModel.Id);
                if (!restoreResult)
                {
                    TempData[ErrorMessageKey] = AnimeRestoreErrorMessage;
                    return this.RedirectToAction(nameof(Index));
                }
                TempData[SuccessMessageKey] = AnimeRestoreSuccessMessage;
                return this.RedirectToAction(nameof(Details), new { id = inputModel.Id });
            }
            catch (Exception)
            {
                //TODO: Log the error
                TempData[ErrorMessageKey] = AnimeRestoreErrorMessage;
                return this.RedirectToAction(nameof(Index));
            }
        }

        [AllowAnonymous]
        [HttpGet]

        public async Task<IActionResult> Search(string? searchTerm)
        {
            try
            {
                SearchAnimeViewModel searchResult = await this.animeService.SearchAnimeByWordAsync(searchTerm);
                return this.View(searchResult);
            }
            catch (Exception)
            {
                TempData[ErrorMessageKey] = AnimeSearchErrorMessage;
                return this.RedirectToAction(nameof(Index));
            }
        }
    }
}
