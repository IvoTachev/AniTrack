namespace AniTrack.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using AniTrack.Web.ViewModels.Animelist;
    using System.Collections.Generic;
    using AniTrack.Services.Core.Interfaces;

    using static AniTrack.GCommon.ApplicationConstants;
    using static AniTrack.GCommon.ExceptionMessages;

    public class AnimelistController : BaseController
    {
        private readonly IAnimelistService animelistService;
        private readonly ILogger<AnimelistController> logger;

        public AnimelistController(IAnimelistService animelistService, ILogger<AnimelistController> logger)
        {
            this.animelistService = animelistService;
            this.logger = logger;
        }

        [HttpGet]
        [Route("Animelist/{username?}")]
        public async Task<IActionResult> Index(string? username)
        {
            try
            {
                if (string.IsNullOrEmpty(username))
                {
                    // Redirect to current user's animelist
                    // Action can be used only by logged in users and you can not register without Username.
                    string? currentUsername = User?.Identity?.Name;
                    return this.RedirectToAction(nameof(Index), new { username = currentUsername });
                }

                IEnumerable<AnimelistViewModel> userAnimelist = await this.animelistService.GetAnimelistAsync(username);

                //If a username is invalid, or that user has no animes added to his list throw a 404 not found.
                //Ignore that if the user is checking his own animelist, because he can see his own empty list.
                if ((userAnimelist == null || !userAnimelist.Any()) && (username != User?.Identity?.Name)) 
                {
                    return NotFound(); // Returns 404 error page
                }

                ViewData["Username"] = username;
                ViewData["IsOwnList"] = string.Equals(username, User?.Identity?.Name, StringComparison.OrdinalIgnoreCase);

                return View(userAnimelist); 
            }
            catch (Exception ex)
            {  
                logger.LogError(ex, "Error retrieving animelist for user {Username}", username);
                TempData[ErrorMessageKey] = AnimelistRetrieveErrorMessage;
                return this.RedirectToAction(nameof(Index), "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(string? animeId)
        {
            try
            {
                string? userId = this.GetUserId();
                if (userId == null)
                {
                    return this.Forbid();
                }

                bool result = await this.animelistService.AddAnimeToUserAnimelistAsync(animeId, userId);
                if (result == false)
                {
                    TempData[ErrorMessageKey] = AnimelistAddErrorMessage;
                    return this.RedirectToAction(nameof(Index), "Anime");
                }
                TempData[SuccessMessageKey] = AnimelistAddSuccessMessage;
                return this.RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error adding anime {AnimeId} to animelist for user {UserId}", animeId, this.GetUserId());
                TempData[ErrorMessageKey] = AnimelistAddErrorMessage;
                return this.RedirectToAction(nameof(Index), "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Remove(string? animeId)
        {
            try
            {
                string? userId = this.GetUserId();
                if (userId == null)
                {
                    return this.Forbid();
                }
                bool result = await this.animelistService.RemoveAnimeFromUserAnimelistAsync(animeId, userId);
                if (result == false)
                {
                    TempData[ErrorMessageKey] = AnimelistRemoveErrorMessage;
                    return this.RedirectToAction(nameof(Index));
                }
                TempData[SuccessMessageKey] = AnimelistRemoveSuccessMessage;
                return this.RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error removing anime {AnimeId} from animelist for user {UserId}", animeId, this.GetUserId());
                TempData[ErrorMessageKey] = AnimelistRemoveErrorMessage;
                return this.RedirectToAction(nameof(Index), "Home");
            }
        }   
    }
}
