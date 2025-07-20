namespace AniTrack.Web.Controllers
{
    using AniTrack.Web.ViewModels.Anime;
    using Microsoft.AspNetCore.Mvc;
    using Services.Core.Interfaces;
    using System.Threading.Tasks;

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
    }
}
