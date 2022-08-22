using Microsoft.AspNetCore.Mvc;

namespace Cine.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
