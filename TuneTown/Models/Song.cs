using System.ComponentModel.DataAnnotations;

namespace TuneTown.Models
{
    public class Song
    {
        [Key] 
        public int SongId { get; set; }
        
        [Required(ErrorMessage = "Song title is required.")]
        public string SongName { get; set;}
        [Required(ErrorMessage = "Release date is required.")]
        public DateTime ReleaseDate { get; set; }
        [Required(ErrorMessage = "Artist info is required.")]
        public Artist Artist { get; set; }
        [Required(ErrorMessage = "Album info is required.")]
        public Album Album { get; set; }
        
        public string? SongLength { get; set; }
        public int? BitRate { get; set;}

        [Required(ErrorMessage = "A youtube link is required.")]
        public string SongLink { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
