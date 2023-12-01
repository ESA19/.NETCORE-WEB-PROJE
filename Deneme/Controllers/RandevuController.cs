using Microsoft.AspNetCore.Mvc;

namespace Deneme.Controllers
{
    public class RandevuController : Controller
    {
       

        public IActionResult RandevuFormu()
        {
            return View();
        }
    }
}
