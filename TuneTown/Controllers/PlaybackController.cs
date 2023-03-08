using Microsoft.AspNetCore.Mvc;

namespace TuneTown.Controllers
{
    public class PlaybackController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
