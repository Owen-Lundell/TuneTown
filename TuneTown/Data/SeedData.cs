using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
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
                #region User Data
                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                //default users, use user secret to hide password later
                //result variable can just be removed, is used only as a success/fail savestate
                const string TEST_PASSWORD = "password";
                AppUser testUser1 = new() { UserName = "Bill Clinton" };
                var result = userManager.CreateAsync(testUser1, TEST_PASSWORD).Result;
                AppUser testUser2 = new() { UserName = "David Lundell" };
                result = userManager.CreateAsync(testUser2, TEST_PASSWORD).Result;
                AppUser testUser3 = new() { UserName = "Static User" };
                result = userManager.CreateAsync(testUser3, TEST_PASSWORD).Result;
                #endregion

                #region Submission Data
                Artist artist = new()
                {
                    PublicAlias = "T Swizzle",
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
                    ReleaseDate = DateOnly.FromDateTime(new DateTime(2008, 11, 11)),
                    TrackTotal = 13,
                    LabelName = "Big Machine Records",
                };
                context.Albums.Add(album);
                context.SaveChanges();

                Song song = new() //uses default album and artist values
                {
                    SongName = "You Belong With Me",
                    //ReleaseDate = DateOnly.FromDateTime(new DateTime(2008, 11, 11)),
                    Artist = artist,
                    Album = album,
                    SongLength = "3:55",
                    BitRate = 256,
                    SongLink = "https://open.spotify.com/track/3GCL1PydwsLodcpv0Ll1ch?si=b7d73cf096114404"
                };
                context.Songs.Add(song);
                context.SaveChanges();
                #endregion

                #region Submissions
                //two submissions with default song, artist, and album values
                Submission submission = new()
                {
                    User = testUser1,
                    Song = song,
                    DateSubmitted = DateOnly.Parse("11/27/2022")
                };
                context.Submissions.Add(submission);

                submission = new Submission
                {
                    User = testUser2,
                    Song = song,
                    DateSubmitted = DateOnly.Parse("11/27/2022")
                };
                context.Submissions.Add(submission);

                context.SaveChanges();

                //two submissions with default user values and unique song, artist, and album values
                //TODO: add unique song, artist, and album values
                submission = new Submission
                {
                    User = testUser3,
                    Song = song,
                    DateSubmitted = DateOnly.Parse("11/27/2022")
                };
                context.Submissions.Add(submission);

                submission = new Submission
                {
                    User = testUser3,
                    Song = song,
                    DateSubmitted = DateOnly.Parse("11/27/2022")
                };
                context.Submissions.Add(submission);
                context.SaveChanges();
                #endregion
            }
        }
    }
}
