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
        // Add unit tests
        // Add validation to make sure users cant not enter values
            // Some validation is there i'm just not certain if its working as it should
        // Add comments for complex data requirement
        // Get help with roles not applying properly
        // Get help with making the songs page take two parameters for its distinct method in the view (songname and artist)
        // Implement a way to select filters from one box (html dropdown)
            // possibly a new property in submissions? - ask brian
        // Implement actual file submissions
            // Put those submissions on the home page and playback page
            // Show the filenames for those submissions on the info pages
        // Style the page
        #endregion

        #region Initialization
        ISubmissionRepository SubmissionRepository { get; set; }
        readonly UserManager<AppUser> userManager; //revert this if readonly causes issues
        public InfoController(ISubmissionRepository submissionRepository, UserManager<AppUser> userManager)
        {
            SubmissionRepository = submissionRepository;
            this.userManager = userManager;
        }
        #endregion

        #region Info Views and Filter Methods
        #region Index (Info)
        public async Task<IActionResult> Index(string userName, string dateSubmitted)
        {
            List<Submission> submissions;
            if (userName != null)
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
            }

            return View(submissions);
        }
        #endregion

        #region Albums
        public async Task<IActionResult> Albums(string albumName, string groupName, string releaseDate, int trackTotal, string labelName)
        {
            List<Submission> submissions;
            if (albumName != null)
            {
                submissions = await (
                   from s in SubmissionRepository.Submissions
                   where s.Song.Album.AlbumName == albumName
                   select s
                   ).ToListAsync<Submission>();
            }
            else if (groupName != null)
            {
                submissions = await (
                   from s in SubmissionRepository.Submissions
                   where s.Song.Album.GroupName == groupName
                   select s
                   ).ToListAsync<Submission>();
            }
            else if (releaseDate != null)
            {
                submissions = await (
                  from s in SubmissionRepository.Submissions
                  where s.Song.Album.ReleaseDate == DateOnly.FromDateTime(DateTime.Parse(releaseDate))
                  select s
                  ).ToListAsync<Submission>();
            }
            else if (trackTotal > 0)
            {
                submissions = await (
                   from s in SubmissionRepository.Submissions
                   where s.Song.Album.TrackTotal == trackTotal
                   select s
                   ).ToListAsync<Submission>();
            }
            else if (labelName != null)
            {
                submissions = await (
                   from s in SubmissionRepository.Submissions
                   where s.Song.Album.LabelName == labelName
                   select s
                   ).ToListAsync<Submission>();
            }
            else
            {
                submissions = await SubmissionRepository.Submissions.ToListAsync<Submission>();
            }

            return View(submissions);
        }
        #endregion

        #region Artists
        //not getting data for some reason
        public async Task<IActionResult> Artists(string publicAlias, string firstName, string lastName, string affiliatedLabel)
        {
            List<Submission> submissions;
            if (publicAlias != null)
            {
                submissions = await (
                   from s in SubmissionRepository.Submissions
                   where s.Song.Artist.PublicAlias == publicAlias
                   select s
                   ).ToListAsync<Submission>();
            }
            else if (firstName != null)
            {
                submissions = await (
                   from s in SubmissionRepository.Submissions
                   where s.Song.Artist.FirstName == firstName
                   select s
                   ).ToListAsync<Submission>();
            }
            else if (lastName != null)
            {
                submissions = await (
                   from s in SubmissionRepository.Submissions
                   where s.Song.Artist.LastName == lastName
                   select s
                   ).ToListAsync<Submission>();
            }
            else if (affiliatedLabel != "")
            {
                submissions = await (
                   from s in SubmissionRepository.Submissions
                   where s.Song.Artist.AffiliatedLabels.Contains(affiliatedLabel)
                   select s
                   ).ToListAsync<Submission>();
            }
            else
            {
                submissions = await SubmissionRepository.Submissions.ToListAsync<Submission>();
            }

            return View(submissions);
        }
        #endregion

        #region Songs
        public async Task<IActionResult> Songs(string releaseDate, int bitRate)
        {
            List<Submission> submissions;
            if (releaseDate != null)
            {
                submissions = await (
                  from s in SubmissionRepository.Submissions
                  where s.Song.ReleaseDate == DateOnly.FromDateTime(DateTime.Parse(releaseDate))
                  select s
                  ).ToListAsync<Submission>();
            }
            else if (bitRate > 0)
            {
                submissions = await (
                   from s in SubmissionRepository.Submissions
                   where s.Song.BitRate >= bitRate
                   select s
                   ).ToListAsync<Submission>();
            }
            else
            {
                submissions = await SubmissionRepository.Submissions.ToListAsync<Submission>();
            }

            return View(submissions);
        }
        #endregion
        #endregion

        #region Submission View and Post Methods
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
            if (SubmissionRepository.CreateSubmissionAsync(model).Result > 0)
            {
                return RedirectToAction("Index", new { submissionId = model.SubmissionId });
            }
            else
                return View();
        }
        #endregion
    }
}
