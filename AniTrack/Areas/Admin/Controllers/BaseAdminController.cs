namespace AniTrack.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    using static GCommon.ApplicationConstants;

    [Area(AdminRoleName)]
    [Authorize(Roles = AdminRoleName)]
    public abstract class BaseAdminController : Controller
    {
        private bool IsUserAuthenticated()
        {
            bool retResult = false;
            if (this.User.Identity != null)
            {
                retResult = this.User.Identity.IsAuthenticated;
            }
            return retResult;
        }

        protected string? GetUserId()
        {
            string? userId = null;
            if (this.IsUserAuthenticated())
            {
                userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            return userId;
        }
    }
}
