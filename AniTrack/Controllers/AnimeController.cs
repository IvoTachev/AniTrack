namespace AniTrack.Web.Controllers
{
    using AniTrack.Web.ViewModels.Anime;
    using Microsoft.AspNetCore.Mvc;
    using Services.Core.Interfaces;
    using System.Threading.Tasks;
    using static ViewModels.ValidationMessages.Anime;

    public class AnimeController : Controller
    {
       private readonly IAnimeService animeService;
       public AnimeController(IAnimeService animeService)
       {
            this.animeService = animeService;
       }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<TopAnimesViewModel> topAnimes =  await this.animeService
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
            if(!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            try
            {
               await this.animeService.AddAnimeAsync(inputModel);

               return this.RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ServiceCreateError);
                return this.View(inputModel);
            }
        }
    }
}
