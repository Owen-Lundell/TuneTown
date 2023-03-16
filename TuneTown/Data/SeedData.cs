using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Update;
using TuneTown.Models;

namespace TuneTown.Data
{
    public class SeedData
    {
        public static void Seed(ApplicationDbContext context, IServiceProvider services)//this is called provider in brians examples
        {
            if (!context.Submissions.Any())
            {
                #region Initialization
                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                const string TEST_PASSWORD = "password";
                const string ADMIN_ROLE = "Admin";
                const string POSTER_ROLE = "Poster";
                List<Comment> comments = new List<Comment>();
                #endregion

                #region User Data
                AppUser testUser1 = new() 
                    {
                        UserName = "Bill Clinton", 
                        RoleNames = new[] { POSTER_ROLE }
                    };
                var result = userManager.CreateAsync(testUser1, TEST_PASSWORD).Result;
                
                AppUser testUser2 = new() 
                    { 
                        UserName = "Barack Obama", 
                        RoleNames = new[] { POSTER_ROLE }
                    };
                result = userManager.CreateAsync(testUser2, TEST_PASSWORD).Result;
                
                AppUser testUser3 = new() 
                    { 
                        UserName = "Owen Lundell" ,
                        RoleNames = new[] { ADMIN_ROLE, POSTER_ROLE }
                    };
                result = userManager.CreateAsync(testUser3, TEST_PASSWORD).Result;
                #endregion

                #region Submission Data
                #region Taylor Swift
                Artist artist = new()
                {
                    PublicAlias = "Taylor Swft",
                    FirstName = "Taylor",
                    LastName = "Swift",
                    AffiliatedLabels = "Big Machine Records, Universal Music Group, Republic Records, RCA Records, Mercury Records, Virgin EMI Records"
                };
                context.Artists.Add(artist);
                context.SaveChanges();

                Album album = new()
                {
                    AlbumName = "Fearless",
                    GroupName = "Taylor Swift",
                    ReleaseDate = new DateTime(2008, 11, 11),
                    TrackTotal = 13,
                    LabelName = "Big Machine Records",
                };
                context.Albums.Add(album);
                context.SaveChanges();

                //uses default album and artist values
                Song song = new() 
                {
                    SongName = "You Belong With Me",
                    ReleaseDate = new DateTime(2008, 11, 11),
                    Artist = artist,
                    Album = album,
                    SongLength = "3:55",
                    BitRate = 256,
                    SongLink = "https://www.youtube.com/watch?v=uD8d1KrDQcY"
                };
                context.Songs.Add(song);
                context.SaveChanges();
                #endregion

                #region Delta Sleep
                Artist deltaSleep = new()
                {
                    PublicAlias = "Devin Yüceil",
                    FirstName = "Devin",
                    LastName = "Yüceil",
                    AffiliatedLabels = "Sofa Boy Records, Big Scary Monsters Recording Company"
                };
                context.Artists.Add(deltaSleep);
                context.SaveChanges();

                Album twinGalaxies = new()
                {
                    AlbumName = "Twin Galaxies",
                    GroupName = "Delta Sleep",
                    ReleaseDate = new DateTime(2015, 06, 08),
                    TrackTotal = 11,
                    LabelName = "Big Scary Monsters Recording Company",
                };
                context.Albums.Add(twinGalaxies);
                context.SaveChanges();

                Song strongthany = new()
                {
                    SongName = "Strongthany",
                    ReleaseDate = new DateTime(2015, 06, 08),
                    Artist = deltaSleep,
                    Album = twinGalaxies,
                    SongLength = "5:12",
                    BitRate = 320,
                    SongLink = "https://www.youtube.com/watch?v=kZqQajj1oec"
                };
                context.Songs.Add(strongthany);
                context.SaveChanges();
                #endregion

                #region Nekomata Master
                Artist nekomataMaster = new()
                {
                    PublicAlias = "Nekomata Master",
                    FirstName = "Naoyuki",
                    LastName = "Sato",
                    AffiliatedLabels = "Konami"
                };
                context.Artists.Add(nekomataMaster);
                context.SaveChanges();

                Album backdrops = new()
                {
                    AlbumName = "Backdrops",
                    GroupName = "Nekomata Master",
                    ReleaseDate = new DateTime(2009, 09, 25),
                    TrackTotal = 15,
                    LabelName = "Konami",
                };
                context.Albums.Add(backdrops);
                context.SaveChanges();

                Song goodbyeChalon = new()
                {
                    SongName = "Good-bye Chalon",
                    ReleaseDate = new DateTime(2010, 11, 21),
                    Artist =  nekomataMaster,
                    Album = backdrops,
                    SongLength = "5:12",
                    BitRate = 320,
                    SongLink = "https://youtu.be/ZQQ_4lnab1c"
                };
                context.Songs.Add(goodbyeChalon);
                context.SaveChanges();
                #endregion

                #region goreshit
                Artist goreshit = new()
                {
                    PublicAlias = "goreshit",
                    FirstName = "Leon",
                    LastName = "Makepeace",
                    AffiliatedLabels = "Kitty on Fire Records"
                };
                context.Artists.Add(goreshit);
                context.SaveChanges();

                Album rituals = new()
                {
                    AlbumName = "Rituals",
                    GroupName = "goreshit",
                    ReleaseDate = new DateTime(2013, 10, 2),
                    TrackTotal = 15,
                    LabelName = "Kitty on Fire Records",
                };
                context.Albums.Add(rituals);
                context.SaveChanges();

                Song fleshbound = new()
                {
                    SongName = "Fleshbound",
                    ReleaseDate = new DateTime(2013, 10, 2),
                    Artist = goreshit,
                    Album = rituals,
                    SongLength = "6:16",
                    BitRate = 196,
                    SongLink = "https://www.youtube.com/watch?v=siCf8EW3_MA"
                };
                context.Songs.Add(fleshbound);
                context.SaveChanges();
                #endregion
                #endregion

                #region Submissions
                //two submissions with default song, artist, and album values
                Submission submission = new()
                {
                    User = testUser1,
                    Song = song,
                    DateSubmitted = DateOnly.Parse("3/15/2023")
                };
                context.Submissions.Add(submission);

                submission = new Submission
                {
                    User = testUser2,
                    Song = song,
                    DateSubmitted = DateOnly.Parse("3/15/2023")
                };
                context.Submissions.Add(submission);
                context.SaveChanges();

                //submissions with different values
                submission = new Submission
                {
                    User = testUser3,
                    Song = strongthany,
                    DateSubmitted = DateOnly.FromDateTime(DateTime.Now)
                };
                context.Submissions.Add(submission);

                submission = new Submission
                {
                    User = testUser3,
                    Song = goodbyeChalon,
                    DateSubmitted = DateOnly.FromDateTime(DateTime.Now)
                };
                context.Submissions.Add(submission);

                submission = new Submission
                {
                    User = testUser3,
                    Song = fleshbound,
                    DateSubmitted = DateOnly.FromDateTime(DateTime.Now)
                };
                context.Submissions.Add(submission);
                context.SaveChanges();
                #endregion

                #region Comments
                Comment comment = new Comment
                {
                    Commenter = testUser1,
                    CommentText = "This is a comment",
                    PostDate = DateTime.Now,
                    SongId = 1
                };
                context.Add(comment);
                comments.Add(comment);

                comment = new Comment
                {
                    Commenter = testUser2,
                    CommentText = "This is another comment",
                    PostDate = DateTime.Now,
                    SongId = 1
                };
                context.Add(comment);
                comments.Add(comment);
                context.SaveChanges();
                #endregion
            }
        }
    }
}
