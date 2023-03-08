using Microsoft.AspNetCore.Mvc;

namespace TuneTown.Controllers
{
    public class SubmitController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SubmissionState()
        {
            return View();
        }
    }
}
