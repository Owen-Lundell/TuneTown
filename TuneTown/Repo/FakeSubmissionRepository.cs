using TuneTown.Models;

namespace TuneTown.Repo
{
    public class FakeSubmissionRepository : ISubmissionRepository
    {
        List<Submission> submissions = new List<Submission>();

        public IQueryable<Submission> Submissions
        {
            get { return submissions.AsQueryable<Submission>(); }
        }

#pragma warning disable CS1998
        public async Task CreateSubmissionAsync(Submission submission)
        {
            submission.SubmissionId = submissions.Count;
            submissions.Add(submission);
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
