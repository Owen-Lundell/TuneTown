using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuneTownTests
{
    public class TuneTownTests
    {
        ISubmissionRepository repo = new FakeSubmissionRepository();
        InfoController controller;

        public TuneTownTests() 
        {
            controller = new InfoController(repo, null);
        }

        [Fact]
        public void Submission_PostTest_Success()
        {
            // Test to see if submission will post

            // Arrange
            // Done in the constructor

            // Act
            var result = controller.Submission(new Submission { Song = new Song() }).Result;

            // Assert
            // This result is returned if the review was stored successfully
            Assert.True(result.GetType() == typeof(RedirectToActionResult));
        }

        // none of these will pass until i figure out why my repo is forcing me to use an iqueryable
        #region Album Tests
        [Fact]
        public void FilterByAlbumNameTest()
        {
            // Test to see if only artists with the selected name are returned 

            // Arrange
            // Done in the constructor

            // We don't need need to add all the properties to the models since we aren't testing that.
            var submission1 = new Submission { Song = new Song { Album = new Album { AlbumName = "Album 1" } } };
            repo.CreateSubmissionAsync(submission1).Wait();
            repo.CreateSubmissionAsync(submission1).Wait();
            var submission2 = new Submission { Song = new Song { Album = new Album { AlbumName = "Album 2" } } };
            repo.CreateSubmissionAsync(submission2).Wait();
            repo.CreateSubmissionAsync(submission2).Wait();
            var submission3 = new Submission { Song = new Song { Album = new Album { AlbumName = "Album 3" } } };
            repo.CreateSubmissionAsync(submission3).Wait();
            repo.CreateSubmissionAsync(submission3).Wait();

            var controller = new InfoController(repo, null);

            // Act
            var filteredArtistsView = controller.Albums(submission1.Song.Album.AlbumName, null, null, 0, null).Result as ViewResult;
            List<Submission> filteredSubmissions = filteredArtistsView.Model as List<Submission>;

            // Assert
            Assert.Equal(2, filteredSubmissions.Count);
            Assert.Equal(filteredSubmissions[0].Song.Album.AlbumName, submission2.Song.Album.AlbumName);
            Assert.Equal(filteredSubmissions[1].Song.Album.AlbumName, submission3.Song.Album.AlbumName);
        }

        [Fact]
        public void FilterByGroupNameTest()
        {
            // Test to see if only artists with the selected name are returned 

            // Arrange
            // Done in the constructor

            // We don't need need to add all the properties to the models since we aren't testing that.
            var submission1 = new Submission { Song = new Song { Album = new Album { GroupName = "Group 1" } } };
            repo.CreateSubmissionAsync(submission1).Wait();
            repo.CreateSubmissionAsync(submission1).Wait();
            var submission2 = new Submission { Song = new Song { Album = new Album { GroupName = "Group 2" } } };
            repo.CreateSubmissionAsync(submission2).Wait();
            repo.CreateSubmissionAsync(submission2).Wait();
            var submission3 = new Submission { Song = new Song { Album = new Album { GroupName = "Group 3" } } };
            repo.CreateSubmissionAsync(submission3).Wait();
            repo.CreateSubmissionAsync(submission3).Wait();

            var controller = new InfoController(repo, null);

            // Act
            var filteredArtistsView = controller.Albums(null, submission1.Song.Album.GroupName, null, 0, null).Result as ViewResult;
            List<Submission> filteredSubmissions = filteredArtistsView.Model as List<Submission>;

            // Assert
            Assert.Equal(2, filteredSubmissions.Count);
            Assert.Equal(filteredSubmissions[0].Song.Album.GroupName, submission2.Song.Album.GroupName);
            Assert.Equal(filteredSubmissions[1].Song.Album.GroupName, submission3.Song.Album.GroupName);
        }

        [Fact]
        public void FilterByAlbumReleaseDateTest()
        {
            // Test to see if only artists with the selected name are returned 

            // Arrange
            // Done in the constructor

            // We don't need need to add all the properties to the models since we aren't testing that.
            var submission1 = new Submission { Song = new Song { Album = new Album { ReleaseDate = DateTime.Parse("01/01/2023") } } };
            repo.CreateSubmissionAsync(submission1).Wait();
            repo.CreateSubmissionAsync(submission1).Wait();
            var submission2 = new Submission { Song = new Song { Album = new Album { ReleaseDate = DateTime.Parse("06/15/2023") } } };
            repo.CreateSubmissionAsync(submission2).Wait();
            repo.CreateSubmissionAsync(submission2).Wait();
            var submission3 = new Submission { Song = new Song { Album = new Album { ReleaseDate = DateTime.Parse("12/31/2023") } } };
            repo.CreateSubmissionAsync(submission3).Wait();
            repo.CreateSubmissionAsync(submission3).Wait();

            var controller = new InfoController(repo, null);

            // Act
            var filteredArtistsView = controller.Albums(null, null, submission1.Song.Album.ReleaseDate.ToString(), 0, null).Result as ViewResult;
            List<Submission> filteredSubmissions = filteredArtistsView.Model as List<Submission>;

            // Assert
            Assert.Equal(2, filteredSubmissions.Count);
            Assert.Equal(filteredSubmissions[0].Song.Album.ReleaseDate, submission2.Song.Album.ReleaseDate);
            Assert.Equal(filteredSubmissions[1].Song.Album.ReleaseDate, submission3.Song.Album.ReleaseDate);
        }

        [Fact]
        public void FilterByTrackTotalTest()
        {
            // Test to see if only artists with the selected name are returned 

            // Arrange
            // Done in the constructor

            // We don't need need to add all the properties to the models since we aren't testing that.
            var submission1 = new Submission { Song = new Song { Album = new Album { TrackTotal = 1 } } };
            repo.CreateSubmissionAsync(submission1).Wait();
            repo.CreateSubmissionAsync(submission1).Wait();
            var submission2 = new Submission { Song = new Song { Album = new Album { TrackTotal = 2 } } };
            repo.CreateSubmissionAsync(submission2).Wait();
            repo.CreateSubmissionAsync(submission2).Wait();
            var submission3 = new Submission { Song = new Song { Album = new Album { TrackTotal = 3 } } };
            repo.CreateSubmissionAsync(submission3).Wait();
            repo.CreateSubmissionAsync(submission3).Wait();

            var controller = new InfoController(repo, null);

            // Act
            var filteredArtistsView = controller.Albums(null, null, null, 1, null).Result as ViewResult;
            List<Submission> filteredSubmissions = filteredArtistsView.Model as List<Submission>;

            // Assert
            Assert.Equal(2, filteredSubmissions.Count);
            Assert.Equal(filteredSubmissions[0].Song.Album.TrackTotal, submission2.Song.Album.TrackTotal);
            Assert.Equal(filteredSubmissions[1].Song.Album.TrackTotal, submission3.Song.Album.TrackTotal);
        }

        [Fact]
        public void FilterByLabelNameTest()
        {
            // Test to see if only artists with the selected name are returned 

            // Arrange
            // Done in the constructor

            // We don't need need to add all the properties to the models since we aren't testing that.
            var submission1 = new Submission { Song = new Song { Album = new Album { LabelName = "Label 1" } } };
            repo.CreateSubmissionAsync(submission1).Wait();
            repo.CreateSubmissionAsync(submission1).Wait();
            var submission2 = new Submission { Song = new Song { Album = new Album { LabelName = "Label 2" } } };
            repo.CreateSubmissionAsync(submission2).Wait();
            repo.CreateSubmissionAsync(submission2).Wait();
            var submission3 = new Submission { Song = new Song { Album = new Album { LabelName = "Label 3" } } };
            repo.CreateSubmissionAsync(submission3).Wait();
            repo.CreateSubmissionAsync(submission3).Wait();

            var controller = new InfoController(repo, null);

            // Act
            var filteredArtistsView = controller.Albums(null, null, null, 0, submission1.Song.Album.LabelName).Result as ViewResult;
            List<Submission> filteredSubmissions = filteredArtistsView.Model as List<Submission>;

            // Assert
            Assert.Equal(2, filteredSubmissions.Count);
            Assert.Equal(filteredSubmissions[0].Song.Album.LabelName, submission2.Song.Album.LabelName);
            Assert.Equal(filteredSubmissions[1].Song.Album.LabelName, submission3.Song.Album.LabelName);
        }
        #endregion

        #region Artist Tests
        [Fact]
        public void FilterByPublicAliasTest()
        {
            // Test to see if only artists with the selected name are returned 

            // Arrange
            // Done in the constructor

            // We don't need need to add all the properties to the models since we aren't testing that.
            var submission1 = new Submission { Song = new Song { Artist = new Artist { PublicAlias = "Artist 1" } } };
            repo.CreateSubmissionAsync(submission1).Wait();
            repo.CreateSubmissionAsync(submission1).Wait();
            var submission2 = new Submission { Song = new Song { Artist = new Artist { PublicAlias = "Artist 2" } } };
            repo.CreateSubmissionAsync(submission2).Wait();
            repo.CreateSubmissionAsync(submission2).Wait();
            var submission3 = new Submission { Song = new Song { Artist = new Artist { PublicAlias = "Artist 3" } } };
            repo.CreateSubmissionAsync(submission3).Wait();
            repo.CreateSubmissionAsync(submission3).Wait();

            var controller = new InfoController(repo, null);

            // Act
            var filteredArtistsView = controller.Artists(submission1.Song.Artist.PublicAlias, null, null, null).Result as ViewResult;
            List<Submission> filteredSubmissions = filteredArtistsView.Model as List<Submission>;

            // Assert
            Assert.Equal(2, filteredSubmissions.Count);
            Assert.Equal(filteredSubmissions[0].Song.Artist.PublicAlias, submission2.Song.Artist.PublicAlias);
            Assert.Equal(filteredSubmissions[1].Song.Artist.PublicAlias, submission3.Song.Artist.PublicAlias);
        }

        [Fact]
        public void FilterByFirstNameTest()
        {
            // Test to see if only artists with the selected name are returned 

            // Arrange
            // Done in the constructor

            // We don't need need to add all the properties to the models since we aren't testing that.
            var submission1 = new Submission { Song = new Song { Artist = new Artist { FirstName = "Artist 1" } } };
            repo.CreateSubmissionAsync(submission1).Wait();
            repo.CreateSubmissionAsync(submission1).Wait();
            var submission2 = new Submission { Song = new Song { Artist = new Artist { FirstName = "Artist 2" } } };
            repo.CreateSubmissionAsync(submission2).Wait();
            repo.CreateSubmissionAsync(submission2).Wait();
            var submission3 = new Submission { Song = new Song { Artist = new Artist { FirstName = "Artist 3" } } };
            repo.CreateSubmissionAsync(submission3).Wait();
            repo.CreateSubmissionAsync(submission3).Wait();

            var controller = new InfoController(repo, null);

            // Act
            var filteredArtistsView = controller.Artists(null, submission1.Song.Artist.FirstName, null, null).Result as ViewResult;
            List<Submission> filteredSubmissions = filteredArtistsView.Model as List<Submission>;

            // Assert
            Assert.Equal(2, filteredSubmissions.Count);
            Assert.Equal(filteredSubmissions[0].Song.Artist.FirstName, submission2.Song.Artist.FirstName);
            Assert.Equal(filteredSubmissions[1].Song.Artist.FirstName, submission3.Song.Artist.FirstName);
        }

        [Fact]
        public void FilterByLastNameTest()
        {
            // Test to see if only artists with the selected name are returned 

            // Arrange
            // Done in the constructor

            // We don't need need to add all the properties to the models since we aren't testing that.
            var submission1 = new Submission { Song = new Song { Artist = new Artist { LastName = "Artist 1" } } };
            repo.CreateSubmissionAsync(submission1).Wait();
            repo.CreateSubmissionAsync(submission1).Wait();
            var submission2 = new Submission { Song = new Song { Artist = new Artist { LastName = "Artist 2" } } };
            repo.CreateSubmissionAsync(submission2).Wait();
            repo.CreateSubmissionAsync(submission2).Wait();
            var submission3 = new Submission { Song = new Song { Artist = new Artist { LastName = "Artist 3" } } };
            repo.CreateSubmissionAsync(submission3).Wait();
            repo.CreateSubmissionAsync(submission3).Wait();

            var controller = new InfoController(repo, null);

            // Act
            var filteredArtistsView = controller.Artists(null, null, submission1.Song.Artist.LastName, null).Result as ViewResult;
            List<Submission> filteredSubmissions = filteredArtistsView.Model as List<Submission>;

            // Assert
            Assert.Equal(2, filteredSubmissions.Count);
            Assert.Equal(filteredSubmissions[0].Song.Artist.LastName, submission2.Song.Artist.LastName);
            Assert.Equal(filteredSubmissions[1].Song.Artist.LastName, submission3.Song.Artist.LastName);
        }

        [Fact]
        public void FilterByLabelTest()
        {
            // Test to see if only artists with the selected name are returned 

            // Arrange
            // Done in the constructor

            // We don't need need to add all the properties to the models since we aren't testing that.
            var submission1 = new Submission { Song = new Song { Artist = new Artist { AffiliatedLabels = "Label 1" } } };
            repo.CreateSubmissionAsync(submission1).Wait();
            repo.CreateSubmissionAsync(submission1).Wait();
            var submission2 = new Submission { Song = new Song { Artist = new Artist { AffiliatedLabels = "Artist 2" } } };
            repo.CreateSubmissionAsync(submission2).Wait();
            repo.CreateSubmissionAsync(submission2).Wait();
            var submission3 = new Submission { Song = new Song { Artist = new Artist { AffiliatedLabels = "Artist 3" } } };
            repo.CreateSubmissionAsync(submission3).Wait();
            repo.CreateSubmissionAsync(submission3).Wait();

            var controller = new InfoController(repo, null);

            // Act
            var filteredArtistsView = controller.Artists(null, null, null, submission1.Song.Artist.AffiliatedLabels).Result as ViewResult;
            List<Submission> filteredSubmissions = filteredArtistsView.Model as List<Submission>;

            // Assert
            Assert.Equal(2, filteredSubmissions.Count);
            Assert.Equal(filteredSubmissions[0].Song.Artist.AffiliatedLabels, submission2.Song.Artist.AffiliatedLabels);
            Assert.Equal(filteredSubmissions[1].Song.Artist.AffiliatedLabels, submission3.Song.Artist.AffiliatedLabels);
        }
        #endregion

        #region Song Tests
        [Fact]
        public void FilterBySongReleaseDateTest()
        {
            // Test to see if only artists with the selected name are returned 

            // Arrange
            // Done in the constructor

            // We don't need need to add all the properties to the models since we aren't testing that.
            var submission1 = new Submission { Song = new Song { ReleaseDate = DateTime.Parse("01/01/2023") } };
            repo.CreateSubmissionAsync(submission1).Wait();
            repo.CreateSubmissionAsync(submission1).Wait();
            var submission2 = new Submission { Song = new Song { ReleaseDate = DateTime.Parse("06/15/2023") } };
            repo.CreateSubmissionAsync(submission2).Wait();
            repo.CreateSubmissionAsync(submission2).Wait();
            var submission3 = new Submission { Song = new Song { ReleaseDate = DateTime.Parse("12/31/2023") } };
            repo.CreateSubmissionAsync(submission3).Wait();
            repo.CreateSubmissionAsync(submission3).Wait();

            var controller = new InfoController(repo, null);

            // Act
            var filteredArtistsView = controller.Songs(submission1.Song.ReleaseDate.ToString(), 0).Result as ViewResult;
            List<Submission> filteredSubmissions = filteredArtistsView.Model as List<Submission>;

            // Assert
            Assert.Equal(2, filteredSubmissions.Count);
            Assert.Equal(filteredSubmissions[0].Song.ReleaseDate, submission2.Song.ReleaseDate);
            Assert.Equal(filteredSubmissions[1].Song.ReleaseDate, submission3.Song.ReleaseDate);
        }

        [Fact]
        public void FilterByBitRateTest()
        {
            // Test to see if only artists with the selected name are returned 

            // Arrange
            // Done in the constructor

            // We don't need need to add all the properties to the models since we aren't testing that.
            var submission1 = new Submission { Song = new Song { BitRate = 1 } };
            repo.CreateSubmissionAsync(submission1).Wait();
            repo.CreateSubmissionAsync(submission1).Wait();
            var submission2 = new Submission { Song = new Song { BitRate = 2 } };
            repo.CreateSubmissionAsync(submission2).Wait();
            repo.CreateSubmissionAsync(submission2).Wait();
            var submission3 = new Submission { Song = new Song { BitRate = 3 } };
            repo.CreateSubmissionAsync(submission3).Wait();
            repo.CreateSubmissionAsync(submission3).Wait();

            var controller = new InfoController(repo, null);

            // Act
            var filteredArtistsView = controller.Songs(null, 1).Result as ViewResult;
            List<Submission> filteredSubmissions = filteredArtistsView.Model as List<Submission>;

            // Assert
            Assert.Equal(2, filteredSubmissions.Count);
            Assert.Equal(filteredSubmissions[0].Song.BitRate, submission2.Song.BitRate);
            Assert.Equal(filteredSubmissions[1].Song.BitRate, submission3.Song.BitRate);
        }
        #endregion
    }
}
