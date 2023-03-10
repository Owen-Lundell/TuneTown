using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TuneTown.Models;
using TuneTown.Repo;

namespace TuneTown.Controllers
{
    public class InfoController : Controller
    {
        ISubmissionRepository SubmissionRepository { get; set; }
        readonly UserManager<AppUser> userManager; //revert this if readonly causes issues
        public InfoController(ISubmissionRepository submissionRepository, UserManager<AppUser> userManager)
        {
            SubmissionRepository = submissionRepository;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(string userName, string songName)
        {
            //on the index i should only filter by user and upload date, things like song name and artist details should happen in ther own controllers
            /*List<Submission> submissions;
            if (userName != null)
            {
                submissions = await(
                   from s in SubmissionRepository.Submissions
                   where s.User.UserName == userName
                   select s
                   ).ToListAsync<Submission>();
            }
            else if (songName != null)
            {
                submissions = await (
                  from s in SubmissionRepository.Submissions
                  where s.Song.SongName == songName
                  select s
                  ).ToListAsync<Submission>();
            }
            else
            {
                submissions = await SubmissionRepository.Submissions.ToListAsync<Submission>();
            }*/

            return View(await SubmissionRepository.Submissions.ToListAsync<Submission>());
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

        public IActionResult Submission()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Submission(Submission model)
        {
            model.User = await userManager.GetUserAsync(User);
            if (await SubmissionRepository.CreateSubmissionAsync(model) > 0)
            {
                return RedirectToAction("Index", new { submissionId = model.SubmissionId });
            }
            else
                return View();
        }

    }
}
