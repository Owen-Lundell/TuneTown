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
        #region TODO
        // Add submission posts to song, artist, and album pages
        // Fix user submissions so that the id gets passed in properly - double check that this isnt because i just dropped the database
        // Add relevant filters to all view controllers
        // Implement a way to select filters without typing (html dropdown)
            // possibly a new property in submissions? - ask brian
        // Add validation to make sure users cant not enter values
        // Add comments for complex data requirement
        // Get help with roles not applying properly
        #endregion
        ISubmissionRepository SubmissionRepository { get; set; }
        readonly UserManager<AppUser> userManager; //revert this if readonly causes issues
        public InfoController(ISubmissionRepository submissionRepository, UserManager<AppUser> userManager)
        {
            SubmissionRepository = submissionRepository;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(string userName, string dateSubmitted)
        {
            //on the index i should only filter by user and upload date, things like song name and artist details should happen in ther own controllers
            List<Submission> submissions;
            /*if (userName != null)
            {
                submissions = await(
                   from s in SubmissionRepository.Submissions
                   where s.User.UserName == userName
                   select s
                   ).ToListAsync<Submission>();
            }
            else if (dateSubmitted != null)
            {
                submissions = await (
                  from s in SubmissionRepository.Submissions
                  where s.DateSubmitted == DateOnly.FromDateTime(DateTime.Parse(dateSubmitted))
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

        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Poster")]
        public IActionResult Submission()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Poster")]
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
