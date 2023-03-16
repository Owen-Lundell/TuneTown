using TuneTown.Migrations;
using System;
using TuneTown.Models;

namespace TuneTown.Repo
{
    public class FakeSubmissionRepository : ISubmissionRepository
    {
        public List<Submission> submissions = new();
        public IQueryable<Submission> Submissions
        {
            get { return submissions.AsQueryable<Submission>(); }
        }

#pragma warning disable CS1998
        public async Task<int> CreateSubmissionAsync(Submission submission)
        {
            int initialCount = submissions.Count;
            submission.SubmissionId = submissions.Count;
            submissions.Add(submission);

            //replicates the context submission creation, though the context probably returns the total amount of submissions
            if (submissions.Count > initialCount)
                return 1;
            else
                return 0;
        }

        public async Task DeleteSubmissionAsync(Submission submission)
        {
            submissions.RemoveAt(submission.SubmissionId - 1);
        }

        public async Task UpdateSubmissionAsync(Submission submission)
        {
            //figure out how to implement this
            throw new NotImplementedException();
        }
#pragma warning restore CS1998
    }
}