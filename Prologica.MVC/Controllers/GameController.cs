using Microsoft.AspNetCore.Mvc;

namespace Prologica.MVC.Controllers
{
    public class GameController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
