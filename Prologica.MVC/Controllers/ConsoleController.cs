using Microsoft.AspNetCore.Mvc;

namespace Prologica.MVC.Controllers
{
    public class ConsoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
