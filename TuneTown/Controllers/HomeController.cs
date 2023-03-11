using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TuneTown.Models;
using TuneTown.Repo;

namespace TuneTown.Controllers
{
    public class HomeController : Controller
    {
        private ISubmissionRepository SubmissionRepository { get; set; }
        public HomeController(ISubmissionRepository submissionRepository)
        {
            SubmissionRepository = submissionRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await SubmissionRepository.Submissions.ToListAsync<Submission>());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}