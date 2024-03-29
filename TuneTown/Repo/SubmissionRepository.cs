﻿using TuneTown.Data;
using TuneTown.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TuneTown.Repo
{
    public class SubmissionRepository : ISubmissionRepository
    {
        private readonly ApplicationDbContext context;
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
                    .Include(submission => submission.Song).ThenInclude(song => song.Artist)
                    .Include(Submission => Submission.Song).ThenInclude(song => song.Comments).ThenInclude(comments => comments.Commenter);
            }
        }

        public async Task<int> CreateSubmissionAsync(Submission submission)
        {
            submission.DateSubmitted = DateOnly.FromDateTime(DateTime.Now);
            context.Submissions.Add(submission);
            return await context.SaveChangesAsync();
        }

        public async Task DeleteSubmissionAsync(Submission submission)
        {
            var submissionForDeletion = await context.Submissions.FindAsync(submission.SubmissionId);
            context.Submissions.Remove(submissionForDeletion);
            await context.SaveChangesAsync();
        }

        public async Task UpdateSubmissionAsync(Submission submission)
        {
            context.Submissions.Update(submission);
            await context.SaveChangesAsync();
        }
    }
}
