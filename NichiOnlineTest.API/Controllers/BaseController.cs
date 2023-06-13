using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace NichiOnlineTest.API.Controllers
{
    [Authorize]
    public class BaseController : ControllerBase
    {
        public string UserId
        {
            get { return this.User.FindFirstValue(ClaimTypes.PrimarySid); }
        }
    }
}