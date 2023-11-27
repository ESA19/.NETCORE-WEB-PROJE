using Microsoft.AspNetCore.Mvc;

namespace Deneme.Controllers
{
    public class RandevuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
