namespace AniTrack.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using AniTrack.Web.ViewModels.Animelist;
    using System.Collections.Generic;
    using AniTrack.Services.Core.Interfaces;
    public class AnimelistController : BaseController
    {
        private readonly IAnimelistService animelistService;

        public AnimelistController(IAnimelistService animelistService)
        {
            this.animelistService = animelistService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                string? userId = this.GetUserId();
                if(userId==null)
                {
                    return this.Forbid();
                }

                IEnumerable<AnimelistViewModel> userAnimelist = await this.animelistService
                                                                          .GetAnimelistAsync(userId);
                return View(userAnimelist); 
            }
            catch (Exception ex)
            {  
                Console.WriteLine(ex);
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
                    return this.RedirectToAction(nameof(Index), "Anime");
                }
                return this.RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                
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
                    return this.RedirectToAction(nameof(Index));
                }
                return this.RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                
                return this.RedirectToAction(nameof(Index), "Home");
            }
        }   
    }
}
