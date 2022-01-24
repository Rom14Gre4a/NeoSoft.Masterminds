using Microsoft.AspNetCore.Mvc;

namespace NeoSoft.Masterminds.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
