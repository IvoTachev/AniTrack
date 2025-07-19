namespace AniTrack.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    public class AnimeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
