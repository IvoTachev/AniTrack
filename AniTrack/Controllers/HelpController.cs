namespace AniTrack.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
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
