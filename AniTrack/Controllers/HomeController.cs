namespace AniTrack.Web.Controllers
{
  
    using AniTrack.Web.ViewModels;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    public class HomeController : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Error(int? statusCode)
        {
            if (statusCode == null)
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
            else if (statusCode == 401)
            {
                Response.StatusCode = 401;
                return View("UnauthorizedError");
            }
            else if (statusCode == 403)
            {
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
    }
}
