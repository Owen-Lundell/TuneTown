using TuneTown.Models;

namespace TuneTown.Repo
{
    public interface ISubmissionRepository
    {
        IQueryable<Submission> Submissions { get; }
        public Task CreateSubmissionAsync(Submission submission);
        public Task DeleteSubmissionAsync(Submission submission);
        public Task UpdateSubmissionAsync(Submission submission);
    }
}
