using TuneTown.Models;

namespace TuneTown.Repo
{
    public class FakeSubmissionRepository : ISubmissionRepository
    {
        public IQueryable<Submission> Submissions => throw new NotImplementedException();

        public Task<int> CreateSubmissionAsync(Submission submission)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteSubmissionAsync(Submission submission)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateSubmissionAsync(Submission submission)
        {
            throw new NotImplementedException();
        }
    }
}
