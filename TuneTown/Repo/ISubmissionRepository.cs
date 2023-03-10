using TuneTown.Models;

namespace TuneTown.Repo
{
    public interface ISubmissionRepository
    {
        IQueryable<Submission> Submissions { get; }
        public Task<int> CreateSubmissionAsync(Submission submission);
        public Task<int> DeleteSubmissionAsync(Submission submission);
        public Task<int> UpdateSubmissionAsync(Submission submission);
    }
}
