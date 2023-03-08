using System.ComponentModel.DataAnnotations;

namespace TuneTown.Models
{
    public class Song
    {
        [Key] 
        public int SongId { get; set; }
        public string SongName { get; set;}
        public DateOnly ReleaseDate { get; set; }
        public Artist Artist { get; set; }
        public Album Album { get; set; }
        public TimeSpan SongLength { get; set; }
        public int BitRate { get; set;}
        public string SongLink { get; set; } //file upload will go here
    }
}
