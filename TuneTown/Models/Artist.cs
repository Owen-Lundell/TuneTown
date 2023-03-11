using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TuneTown.Models
{
    public class Artist
    {
        [Key] 
        public int ArtistId { get; set; }

        [Required(ErrorMessage = "An artist's stage name is required. If their real name is the same, simply put that here as well.")]
        public string PublicAlias { get; set; }
        
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        //possibly convert this to a list later?
        public string? AffiliatedLabels { get; set; }
    }
}
