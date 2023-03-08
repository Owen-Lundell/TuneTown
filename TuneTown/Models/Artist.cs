using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TuneTown.Models
{
    public class Artist
    {
        [Key] 
        public int ArtistId { get; set; }
        public string PublicAlias { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        [NotMapped]
        public List<string> AffiliatedLabels { get; set; }
    }
}
