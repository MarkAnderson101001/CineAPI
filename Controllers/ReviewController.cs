    using Microsoft.AspNetCore.Mvc;

namespace Cine.Controllers
{
    public class ReviewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
