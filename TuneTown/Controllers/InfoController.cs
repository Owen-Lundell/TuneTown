using Microsoft.AspNetCore.Mvc;

namespace TuneTown.Controllers
{
    public class InfoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Albums()
        {
            return View();
        }

        public IActionResult Artists()
        {
            return View();
        }

        public IActionResult Songs()
        {
            return View();
        }
    }
}
