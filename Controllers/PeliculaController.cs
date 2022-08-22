using Microsoft.AspNetCore.Mvc;

namespace Cine.Controllers
{
    public class PeliculaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
