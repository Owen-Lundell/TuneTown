using TuneTown.Models;

namespace TuneTown.Repo
{
    public interface ISubmissionRepository
    {
        public Task<Submission> GetSubmissionById(int submissionId);
        IQueryable<Submission> Submissions { get; }
        public Task<int> CreateSubmission(Submission model);
    }
}
