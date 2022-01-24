using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NeoSoft.Masterminds.Controllers
{
    public class ValueController : Controller
    {
        [ApiController]
        [Route("api/[controller]")]
        public class ValuesController : Controller
        {
            [Authorize]
            [Route("getlogin")]
            public IActionResult GetLogin()
            {
                return Ok($"Ваш логин: {User.Identity.Name}");
            }

            [Authorize(Roles = "Mentor")]
            [Route("getrole")]
            public IActionResult GetRole()
            {
                return Ok("Ваша роль: Наставник");
            }
        }
    }
}
