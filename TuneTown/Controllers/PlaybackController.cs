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
            return View(await SubmissionRepository.Submissions.ToListAsync<Submission>());
        }
        #endregion

        #region Comments
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

            var submission = (from s in SubmissionRepository.Submissions
                                .Include(s => s.Song.Comments)
                                .ThenInclude(c => c.Commenter)
                              where s.Song.SongId == commentVM.SongId
                              select s).First<Submission>();

            submission.Song.Comments.Add(comment);
            await SubmissionRepository.UpdateSubmissionAsync(submission);

            return RedirectToAction("Index");
            // might need to return values like  return RedirectToAction("Stories", new { storyTitle = submission.Story.StoryTitle, userName = submission.User.UserName });
        }
        #endregion
    }
}
