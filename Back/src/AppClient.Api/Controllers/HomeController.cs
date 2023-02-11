using Microsoft.AspNetCore.Mvc;

namespace Autoglass.Api.Controllers
{
    public class HomeController : ControllerBase
    {
        public IActionResult Index()
        {
            return Redirect("/swagger");
        }
    }
}
