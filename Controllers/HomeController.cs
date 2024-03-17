using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        // /home/index
        public IActionResult Index()
        {
            return View("Index");

        }
    }
}
