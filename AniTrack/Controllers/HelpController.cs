using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AniTrack.Web.Controllers
{
    public class HelpController : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> About()
        {
            return this.View();
        }
    }
}
