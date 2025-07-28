using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AniTrack.Web.Controllers
{
    public class HelpController : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult About()
        {
            return this.View();
        }
    }
}
