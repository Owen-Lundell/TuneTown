using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TuneTown.Data;
using TuneTown.Models;
using TuneTown.Repo;

namespace TuneTown.Controllers
{
    public class PlaybackController : Controller
    {
        #region Initialization
        private ISubmissionRepository SubmissionRepository { get; set; }
        UserManager<AppUser> userManager;
        public PlaybackController(ISubmissionRepository s, UserManager<AppUser> userMngr)
        {
            SubmissionRepository = s;
            userManager = userMngr;
        }
        #endregion

        #region View
        public async Task<IActionResult> Index()
        {
            //add the if else just in case this is ever null
            return View(await SubmissionRepository.Submissions.ToListAsync<Submission>());
        }
        #endregion

        #region Comments
        // The issue I'm having is that the songid is getting set to 0 and im unsure how the value is getting passed into it
        [Authorize]
        public IActionResult Comment(int songId)
        {
            var commentVM = new CommentVM { SongId = songId };
            return View(commentVM);
        }

        [HttpPost]
        public async Task<RedirectToActionResult> Comment(CommentVM commentVM)
        {
            var comment = new Comment { CommentText = commentVM.CommentText };
            comment.Commenter = userManager.GetUserAsync(User).Result;
            comment.PostDate = DateTime.Now;

            /*var submission = (from s in SubmissionRepository.Submissions
                                .Include(s => s.Song.Comments)
                                .ThenInclude(c => c.Commenter)
                              where s.Song.SongId == commentVM.SongId
                              select s).First<Submission>();*/
            var submission = (from s in SubmissionRepository.Submissions
                              where s.Song.SongId == commentVM.SongId
                              select s).First<Submission>();

            submission.Song.Comments.Add(comment);
            await SubmissionRepository.UpdateSubmissionAsync(submission);

            return RedirectToAction("Index");
        }
        #endregion
    }
}
