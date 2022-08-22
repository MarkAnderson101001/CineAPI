using Microsoft.AspNetCore.Mvc;

namespace Cine.Controllers
{
    public class SalaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
