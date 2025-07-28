namespace AniTrack.Web.Controllers
{
    using AniTrack.Services.Core;
    using AniTrack.Web.ViewModels.Anime;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Components.Forms;
    using Microsoft.AspNetCore.Mvc;
    using Services.Core.Interfaces;
    using System.Threading.Tasks;
    using static ViewModels.ValidationMessages.Anime;

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
        public async Task<IActionResult> Index()
        {
            IEnumerable<TopAnimesViewModel> topAnimes = await this.animeService
                .GetTopAnimesAsync();
            return View(topAnimes);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return this.View();
        }

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

                return this.RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, ServiceCreateError);
                return this.View(inputModel);
            }
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(string? id)
        {
            try
            {   
                AnimeDetailsViewModel? animeDetails = await this.animeService.GetAnimeDetailsAsync(id);
                if (animeDetails == null)
                {
                    //Not found view
                    return this.View("NotFoundError");
                }

                bool isInAnimelist = false;
                if (User.Identity?.IsAuthenticated == true)
                {
                    string? userId = this.GetUserId();
                    if (userId != null)
                    {
                        isInAnimelist = await IsAnimeInUserList(userId, animeDetails.Id);
                    }
                }
                ViewBag.IsInAnimelist = isInAnimelist;

                return this.View(animeDetails);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return this.RedirectToAction(nameof(Index));
            }

        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            try
            {
                EditAnimeFormModel? editAnimeModel = await this.animeService.GetAnimeDetailsByIdAsync(id);
                if (editAnimeModel == null)
                {
                    //Not found view
                    return this.View("NotFoundError");
                }

                return this.View(editAnimeModel);
            }
            catch (Exception e)
            {
                //Todo: Log the error
                Console.WriteLine(e.Message);
                return this.RedirectToAction(nameof(Index));
            }
        }
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
                    //Not found view
                    return this.View("NotFoundError");
                }
                return this.RedirectToAction(nameof(Details), new { id = inputModel.Id });
            }
            catch (Exception ex)
            {
                //TODO: Log the error
                Console.WriteLine(ex.Message);

                return this.RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            try
            {
                DeleteAnimeViewModel? animeDetails = await this.animeService.GetAnimeDetailsForDeleteByIdAsync(id);
                if (animeDetails == null)
                {
                    //Not found view
                    return this.View("NotFoundError");
                }
                return this.View(animeDetails);
            }
            catch (Exception e)
            {
                //Todo: Log the error
                Console.WriteLine(e.Message);
                return this.RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteAnimeViewModel inputModel)
        {
            try
            {
                if(!this.ModelState.IsValid)
                {
                    return this.RedirectToAction(nameof(Index));
                }

                bool deleteResult = await this.animeService.SoftDeleteAnimeAsync(inputModel.Id);
                if (!deleteResult)
                {
                    //TODO: Add a notification for unsuccessful deletion
                    return this.RedirectToAction(nameof(Index));
                }
                //TODO: Add a notification for successful deletion
                return this.RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                //TODO: Log the error
                Console.WriteLine(ex.Message);
                return this.RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Restore(string? id)
        {
            try
            {
                DeleteAnimeViewModel? animeDetails = await this.animeService.GetAnimeDetailsForRestoreByIdAsync(id);
                if (animeDetails == null)
                {
                    //Not found view
                    return this.View("NotFoundError");
                }
                return this.View(animeDetails);
            }
            catch (Exception e)
            {
                //Todo: Log the error
                Console.WriteLine(e.Message);
                return this.RedirectToAction(nameof(Index));
            }
        }

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
                    //TODO: Add a notification for unsuccessful restoration
                    return this.RedirectToAction(nameof(Index));
                }
                //TODO: Add a notification for successful restoration
                return this.RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                //TODO: Log the error
                Console.WriteLine(ex.Message);
                return this.RedirectToAction(nameof(Index));
            }
        }
    }
}
