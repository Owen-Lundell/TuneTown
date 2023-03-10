using TuneTown.Data;
using TuneTown.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TuneTown.Repo
{
    public class SubmissionRepository : ISubmissionRepository
    {
        private readonly ApplicationDbContext context; //revert this is readonly causes issues
        public SubmissionRepository(ApplicationDbContext appDbContext)
        {
            context = appDbContext;
        }

        public IQueryable<Submission> Submissions
        {
            get
            {
                return context.Submissions
                    .Include(submission => submission.User)
                    .Include(submission => submission.Song)
                    .Include(submission => submission.Song).ThenInclude(song => song.Album)
                    .Include(submission => submission.Song).ThenInclude(song => song.Artist);
            }
        }

        public async Task<int> CreateSubmissionAsync(Submission submission)
        {
            submission.DateSubmitted = DateOnly.FromDateTime(DateTime.Now);
            context.Submissions.Add(submission);
            return await context.SaveChangesAsync();
        }

        public async Task<int> DeleteSubmissionAsync(Submission submission)
        {
            context.Submissions.Remove(submission);
            return await context.SaveChangesAsync();
        }

        public async Task<int> UpdateSubmissionAsync(Submission submission)
        {
            context.Submissions.Update(submission);
            return await context.SaveChangesAsync();
        }
    }
}
