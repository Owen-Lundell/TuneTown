namespace TuneTown.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string CommentText { get; set; }
        public DateTime PostDate { get; set; }
        public AppUser Commenter { get; set; }
    }
}
