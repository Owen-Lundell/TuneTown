using TuneTown.Models;

namespace TuneTown.Repo
{
    public class FakeSubmissionRepository : ISubmissionRepository
    {
        public IQueryable<Submission> Submissions => throw new NotImplementedException();

        public Task<int> CreateSubmission(Submission model)
        {
            throw new NotImplementedException();
        }

        public Task<Submission> GetSubmissionById(int submissionId)
        {
            throw new NotImplementedException();
        }
    }
}
