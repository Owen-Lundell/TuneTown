using System.ComponentModel.DataAnnotations;

namespace TuneTown.Models
{
    public class Submission
    {
        [Key]
        public int SubmissionId { get; set; }
        public AppUser User { get; set; }
        public Song Song { get; set; }
        public DateOnly DateSubmitted { get; set; }
    }
}
