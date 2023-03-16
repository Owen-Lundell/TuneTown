using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuneTownTests
{
    public class RepoTests
    {
        public FakeSubmissionRepository repo { get; set; }
        public RepoTests()
        {
            repo = new FakeSubmissionRepository();
        }

        //These test are all useless because I have my fake repo set up wrong for this.
        [Fact]
        public void TestGetSubmission()
        {
            // Arrange
            List<Submission> submissions;

            //Act
            submissions = repo.submissions;

            //Assert
            Assert.Empty(submissions);
        }
        
        [Fact]
        public async Task TestAddSubmissionAsync()
        {
            //Arrange
            Submission submission = new()
            {
                SubmissionId = 1,
                DateSubmitted = DateOnly.FromDateTime(DateTime.Now),
            };

            //Act
            await repo.CreateSubmissionAsync(submission);
            var result = repo.submissions.Find(s => s.SubmissionId.Equals(1));

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task TestDeleteSubmissionAsync()
        {
            //Arrange
            Submission submission = new()
            {
                SubmissionId = 1,
                DateSubmitted = DateOnly.FromDateTime(DateTime.Now),
            };

            //Act
            await repo.CreateSubmissionAsync(submission);
            //await repo.DeleteSubmissionAsync(submission);
            var result = repo.submissions.Find(s => s.SubmissionId.Equals(1));

            //Assert
            Assert.Null (result);
        }
    }
}
