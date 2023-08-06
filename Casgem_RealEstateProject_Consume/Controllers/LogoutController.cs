using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Casgem_RealEstateProject_Consume.Controllers
{
    public class LogoutController : Controller
    {
        public async Task<IActionResult> Index()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Login");
        }
    }
}
