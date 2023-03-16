using System.ComponentModel.DataAnnotations;

namespace TuneTown.Models
{
    public class Album
    {
        [Key] 
        public int AlbumId { get; set; }

        [Required(ErrorMessage = "Album title is required.")]
        public string AlbumName { get; set; }
        [Required(ErrorMessage = "Group name is required.")]
        public string GroupName { get; set; }
        [Required(ErrorMessage = "Release date is required.")]
        public DateTime ReleaseDate { get; set; }
        public int? TrackTotal { get; set; }
        public string? LabelName { get; set; }
    }
}
