using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CinemaApp.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public string? GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        
    }
}
