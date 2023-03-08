using System.ComponentModel.DataAnnotations;

namespace TuneTown.Models
{
    public class Album
    {
        [Key] 
        public int AlbumId { get; set; }
        public string AlbumName { get; set; }
        public string GroupName { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public int TrackTotal { get; set; }
        public string LabelName { get; set; }
    }
}
