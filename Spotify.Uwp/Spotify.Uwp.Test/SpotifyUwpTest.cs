using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spotify.NetStandard.Sdk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Spotify.Uwp.Test
{
    /// <summary>
    /// Spotify UWP Tests
    /// </summary>
    [TestClass]
    public class SpotifyUwpTest
    {
        private const string country = "GB";
        private const int count = 10;

        private ISpotifySdkClient _client = null;

        /// <summary>
        /// Initialise Unit Test and Configuration
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            // Configuration
            var configBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            IConfiguration config = configBuilder.Build();
            _client = SpotifySdkClientFactory.CreateSpotifySdkClient(
                config["client_id"],
                config["client_secret"]
            ).Set(country);
            Assert.IsNotNull(_client);
            // Spotify Client Token
            var authenticationToken = new AuthenticationTokenResponse()
            {
                Token = config["token"],
                Refresh = config["refresh"],
                Expiration = DateTime.Parse(config["expires"]),
                AuthenticationTokenType = (AuthenticationTokenType)Enum.Parse(typeof(AuthenticationTokenType), config["type"])
            };
            _client.AuthenticationToken = authenticationToken;
        }

        #region List Album View Model
        /// <summary>
        /// List Album View Model (Favourite)
        /// </summary>
        /// <param name="id">Album Id</param>
        [TestMethod]
        [DataRow("2C5HYffMBumERQlNfceyrO")]
        public async Task ListAlbumViewModel_Favourite_Test(string id)
        {
            // Arrange
            _client.Favourites.AlbumIds = new List<string> { id };
            var model = new ListAlbumViewModel(_client, AlbumType.Favourite);
            // Act
            var result = await ((ISupportIncrementalLoading)model.Collection).LoadMoreItemsAsync(count);
            // Assert
            Assert.IsTrue(model.Collection.Any(a => a.Id == id));
        }

        /// <summary>
        /// List Album View Model (Multiple)
        /// </summary>
        /// <param name="id">Album Id</param>
        [TestMethod]
        [DataRow("2C5HYffMBumERQlNfceyrO")]
        public async Task ListAlbumViewModel_Multiple_Test(string id)
        {
            // Arrange
            var ids = new List<string> { id };
            var model = new ListAlbumViewModel(_client, AlbumType.Multiple, multipleAlbumIds: ids);
            // Act
            await ((ISupportIncrementalLoading)model.Collection).LoadMoreItemsAsync(count);
            // Assert
            Assert.IsTrue(model.Collection.Any(a => a.Id == id));
        }

        /// <summary>
        /// List Album View Model (Search)
        /// </summary>
        /// <param name="search">Album Name</param>
        [TestMethod]
        [DataRow("Tubular Bells")]
        public async Task ListAlbumViewModel_Search_Test(string search)
        {
            // Arrange
            var model = new ListAlbumViewModel(_client, AlbumType.Search, value: search);
            // Act
            await ((ISupportIncrementalLoading)model.Collection).LoadMoreItemsAsync(count);
            // Assert
            Assert.IsTrue(model.Collection.Any(a => a.Name == search));
        }

        /// <summary>
        /// List Album View Model (New Releases)
        /// </summary>
        [TestMethod]
        public async Task ListAlbumViewModel_NewReleases_Test()
        {
            // Arrange
            var model = new ListAlbumViewModel(_client, AlbumType.NewReleases);
            // Act
            await ((ISupportIncrementalLoading)model.Collection).LoadMoreItemsAsync(count);
            // Assert
            Assert.IsTrue(model.Collection.Count > 0);
        }

        /// <summary>
        /// List Albums View Model (Artist)
        /// </summary>
        /// <param name="id">Artist Id</param>
        [TestMethod]
        [DataRow("724YlnEzfIBXRWSmT1ur6W")]
        public async Task ListAlbumViewModel_Artist_Test(string id)
        {
            // Arrange
            var model = new ListAlbumViewModel(_client, AlbumType.Artist, id);
            // Act
            await ((ISupportIncrementalLoading)model.Collection).LoadMoreItemsAsync(count);
            // Assert
            Assert.IsTrue(model.Collection.Count > 0);
        }

        /// <summary>
        /// List Album View Model (User Saved)
        /// </summary>
        [TestMethod]
        public async Task ListAlbumViewModel_UserSaved_Test()
        {
            // Arrange
            var model = new ListAlbumViewModel(_client, AlbumType.UserSaved);
            // Act
            await ((ISupportIncrementalLoading)model.Collection).LoadMoreItemsAsync(count);
            // Assert
            Assert.IsTrue(model.Collection.Count > 0);
        }
        #endregion List Album View Model

        #region List Artist View Model
        /// <summary>
        /// List Artist View Model (Favourite)
        /// </summary>
        /// <param name="id">Artist Id</param>
        [TestMethod]
        [DataRow("724YlnEzfIBXRWSmT1ur6W")]
        public async Task ListArtistViewModel_Favourite_Test(string id)
        {
            // Arrange
            _client.Favourites.ArtistIds = new List<string> { id };
            var model = new ListArtistViewModel(_client, ArtistType.Favourite);
            // Act
            var result = await ((ISupportIncrementalLoading)model.Collection).LoadMoreItemsAsync(count);
            // Assert
            Assert.IsTrue(model.Collection.Any(a => a.Id == id));
        }

        /// <summary>
        /// List Artist View Model (Multiple)
        /// </summary>
        /// <param name="id">Artist Id</param>
        [TestMethod]
        [DataRow("724YlnEzfIBXRWSmT1ur6W")]
        public async Task ListArtistViewModel_Multiple_Test(string id)
        {
            // Arrange
            var ids = new List<string> { id };
            var model = new ListArtistViewModel(_client, ArtistType.Multiple, multipleArtistIds: ids);
            // Act
            await ((ISupportIncrementalLoading)model.Collection).LoadMoreItemsAsync(count);
            // Assert
            Assert.IsTrue(model.Collection.Any(a => a.Id == id));
        }

        /// <summary>
        /// List Artist View Model (Search)
        /// </summary>
        /// <param name="search">Artist Name</param>
        [TestMethod]
        [DataRow("Mike Oldfield")]
        public async Task ListArtistViewModel_Search_Test(string search)
        {
            // Arrange
            var model = new ListArtistViewModel(_client, ArtistType.Search, value: search);
            // Act
            await ((ISupportIncrementalLoading)model.Collection).LoadMoreItemsAsync(count);
            // Assert
            Assert.IsTrue(model.Collection.Any(a => a.Name == search));
        }

        /// <summary>
        /// List Artist View Model (Related)
        /// </summary>
        /// <param name="id">Artist Id</param>
        [TestMethod]
        [DataRow("724YlnEzfIBXRWSmT1ur6W")]
        public async Task ListArtistViewModel_Related_Test(string id)
        {
            // Arrange
            var model = new ListArtistViewModel(_client, ArtistType.Related, id);
            // Act
            await ((ISupportIncrementalLoading)model.Collection).LoadMoreItemsAsync(count);
            // Assert
            Assert.IsTrue(model.Collection.Count > 0);
        }

        /// <summary>
        /// List Artist View Model (User Followed)
        /// </summary>
        [TestMethod]
        public async Task ListArtistViewModel_UserFollowed_Test()
        {
            // Arrange
            var model = new ListArtistViewModel(_client, ArtistType.UserFollowed);
            // Act
            await ((ISupportIncrementalLoading)model.Collection).LoadMoreItemsAsync(count);
            // Assert
            Assert.IsTrue(model.Collection.Count > 0);
        }

        /// <summary>
        /// List Artist View Model (User Top)
        /// </summary>
        [TestMethod]
        public async Task ListArtistViewModel_UserTop_Test()
        {
            // Arrange
            var model = new ListArtistViewModel(_client, ArtistType.UserTop);
            // Act
            await ((ISupportIncrementalLoading)model.Collection).LoadMoreItemsAsync(count);
            // Assert
            Assert.IsTrue(model.Collection.Count > 0);
        }
        #endregion List Album View Model

        #region List Category View Model
        /// <summary>
        /// List Category View Model
        /// </summary>
        [TestMethod]
        public async Task ListCategoryViewModel_Test()
        {
            // Arrange
            var model = new ListCategoryViewModel(_client);
            // Act
            await ((ISupportIncrementalLoading)model.Collection).LoadMoreItemsAsync(count);
            // Assert
            Assert.IsTrue(model.Collection.Count > 0);
        }
        #endregion List Category View Model

        #region List Device View Model
        /// <summary>
        /// List Device View Model
        /// </summary>
        [TestMethod]
        public async Task ListDeviceViewModel_Test()
        {
            // Arrange
            var model = new ListDeviceViewModel(_client);
            // Act
            await ((ISupportIncrementalLoading)model.Collection).LoadMoreItemsAsync(count);
            // Assert
            Assert.IsTrue(model.Collection.Count > 0);
        }
        #endregion List Device View Model

        #region List Episode View Model
        /// <summary>
        /// List Episode View Model (Favourite)
        /// </summary>
        /// <param name="id">Episode Id</param>
        [TestMethod]
        [DataRow("5ENWPU6zOwnCerrvS8MbS7")]
        public async Task ListEpisodeViewModel_Favourite_Test(string id)
        {
            // Arrange
            _client.Favourites.EpisodeIds = new List<string> { id };
            var model = new ListEpisodeViewModel(_client, EpisodeType.Favourite);
            // Act
            var result = await ((ISupportIncrementalLoading)model.Collection).LoadMoreItemsAsync(count);
            // Assert
            Assert.IsTrue(model.Collection.Any(a => a.Id == id));
        }

        /// <summary>
        /// List Episode View Model (Multiple)
        /// </summary>
        /// <param name="id">Episode Id</param>
        [TestMethod]
        [DataRow("5ENWPU6zOwnCerrvS8MbS7")]
        public async Task ListEpisodeViewModel_Multiple_Test(string id)
        {
            // Arrange
            var ids = new List<string> { id };
            var model = new ListEpisodeViewModel(_client, EpisodeType.Multiple, multipleEpisodeIds: ids);
            // Act
            await ((ISupportIncrementalLoading)model.Collection).LoadMoreItemsAsync(count);
            // Assert
            Assert.IsTrue(model.Collection.Any(a => a.Id == id));
        }

        /// <summary>
        /// List Episode View Model (Search)
        /// </summary>
        /// <param name="search">Episode Name</param>
        [TestMethod]
        [DataRow("Hamilton")]
        public async Task ListEpisodeViewModel_Search_Test(string search)
        {
            // Arrange
            var model = new ListEpisodeViewModel(_client, EpisodeType.Search, value: search);
            // Act
            await ((ISupportIncrementalLoading)model.Collection).LoadMoreItemsAsync(count);
            // Assert
            Assert.IsTrue(model.Collection.Count > 0);
        }

        /// <summary>
        /// List Episode View Model (Related)
        /// </summary>
        /// <param name="id">Show Id</param>
        [TestMethod]
        [DataRow("5tz9eGgXtNHmq3WVD3EwYx")]
        public async Task ListEpisodeViewModel_Show_Test(string id)
        {
            // Arrange
            var model = new ListEpisodeViewModel(_client, EpisodeType.Show, id);
            // Act
            await ((ISupportIncrementalLoading)model.Collection).LoadMoreItemsAsync(count);
            // Assert
            Assert.IsTrue(model.Collection.Count > 0);
        }
        #endregion List Episode View Model

        #region List Playlist Item View Model
        /// <summary>
        /// List Playlist Item View Model 
        /// </summary>
        /// <param name="id">Playlist Id</param>
        [TestMethod]
        [DataRow("37i9dQZF1DZ06evO2ZGJkQ")]
        public async Task ListPlaylistItemViewModel_Test(string id)
        {
            // Arrange
            var model = new ListPlaylistItemViewModel(_client, id);
            // Act
            await ((ISupportIncrementalLoading)model.Collection).LoadMoreItemsAsync(count);
            // Assert
            Assert.IsTrue(model.Collection.Count > 0);
        }
        #endregion List Playlist Item View Model

        #region List Playlist View Model
        /// <summary>
        /// List Playlist View Model (Search)
        /// </summary>
        /// <param name="search">Playlist Name</param>
        [TestMethod]
        [DataRow("This Is Mike Oldfield")]
        public async Task ListPlaylistViewModel_Search_Test(string search)
        {
            // Arrange
            var model = new ListPlaylistViewModel(_client, PlaylistType.Search, search);
            // Act
            var result = await ((ISupportIncrementalLoading)model.Collection).LoadMoreItemsAsync(count);
            // Assert
            Assert.IsTrue(model.Collection.Any(a => a.Name == search));
        }

        /// <summary>
        /// List Playlist View Model (Featured)
        /// </summary>
        [TestMethod]
        public async Task ListPlaylistViewModel_Featured_Test()
        {
            // Arrange
            var model = new ListPlaylistViewModel(_client, PlaylistType.Featured);
            // Act
            await ((ISupportIncrementalLoading)model.Collection).LoadMoreItemsAsync(count);
            // Assert
            Assert.IsTrue(model.Collection.Count > 0);
        }

        /// <summary>
        /// List Playlist View Model (Category)
        /// </summary>
        /// <param name="category">Category Name</param>
        [TestMethod]
        [DataRow("rock")]
        public async Task ListPlaylistViewModel_Category_Test(string category)
        {
            // Arrange
            var model = new ListPlaylistViewModel(_client, PlaylistType.Category, value: category);
            // Act
            await ((ISupportIncrementalLoading)model.Collection).LoadMoreItemsAsync(count);
            // Assert
            Assert.IsTrue(model.Collection.Count > 0);
        }

        /// <summary>
        /// List Playlist View Model (User)
        /// </summary>
        /// <param name="id">User Id</param>
        [TestMethod]
        [DataRow("spotify")]
        public async Task ListPlaylistViewModel_User_Test(string userId)
        {
            // Arrange
            var model = new ListPlaylistViewModel(_client, PlaylistType.User, userId);
            // Act
            await ((ISupportIncrementalLoading)model.Collection).LoadMoreItemsAsync(count);
            // Assert
            Assert.IsTrue(model.Collection.Count > 0);
        }

        /// <summary>
        /// List Playlist View Model (Current User)
        /// </summary>
        [TestMethod]
        public async Task ListPlaylistViewModel_CurrentUser_Test()
        {
            // Arrange
            var model = new ListPlaylistViewModel(_client, PlaylistType.CurrentUser);
            // Act
            await ((ISupportIncrementalLoading)model.Collection).LoadMoreItemsAsync(count);
            // Assert
            Assert.IsTrue(model.Collection.Count > 0);
        }

        /// <summary>
        /// List Playlist View Model (Current User Addable)
        /// </summary>
        [TestMethod]
        public async Task ListPlaylistViewModel_CurrentUserAddable_Test()
        {
            // Arrange
            var model = new ListPlaylistViewModel(_client, PlaylistType.CurrentUserAddable);
            // Act
            await ((ISupportIncrementalLoading)model.Collection).LoadMoreItemsAsync(count);
            // Assert
            Assert.IsTrue(model.Collection.Count > 0);
        }
        #endregion List Playlist View Model

        #region List Device View Model
        /// <summary>
        /// List Recommendation Genre View Model
        /// </summary>
        [TestMethod]
        public async Task ListRecommendationGenre_Test()
        {
            // Arrange
            var model = new ListRecommendationGenreViewModel(_client);
            // Act
            await ((ISupportIncrementalLoading)model.Collection).LoadMoreItemsAsync(count);
            // Assert
            Assert.IsTrue(model.Collection.Count > 0);
        }
        #endregion List Device View Model

        #region List Show View Model
        /// <summary>
        /// List Show View Model (Favourite)
        /// </summary>
        /// <param name="id">Show Id</param>
        [TestMethod]
        [DataRow("5tz9eGgXtNHmq3WVD3EwYx")]
        public async Task ListShowViewModel_Favourite_Test(string id)
        {
            // Arrange
            _client.Favourites.ShowIds = new List<string> { id };
            var model = new ListShowViewModel(_client, ShowType.Favourite);
            // Act
            var result = await ((ISupportIncrementalLoading)model.Collection).LoadMoreItemsAsync(count);
            // Assert
            Assert.IsTrue(model.Collection.Any(a => a.Id == id));
        }

        /// <summary>
        /// List Show View Model (Multiple)
        /// </summary>
        /// <param name="id">Show Id</param>
        [TestMethod]
        [DataRow("5tz9eGgXtNHmq3WVD3EwYx")]
        public async Task ListShowViewModel_Multiple_Test(string id)
        {
            // Arrange
            var ids = new List<string> { id };
            var model = new ListShowViewModel(_client, ShowType.Multiple, multipleShowIds: ids);
            // Act
            await ((ISupportIncrementalLoading)model.Collection).LoadMoreItemsAsync(count);
            // Assert
            Assert.IsTrue(model.Collection.Any(a => a.Id == id));
        }

        /// <summary>
        /// List Show View Model (Search)
        /// </summary>
        /// <param name="search">Show Name</param>
        [TestMethod]
        [DataRow("Dot Net Rocks")]
        public async Task ListShowViewModel_Search_Test(string search)
        {
            // Arrange
            var model = new ListShowViewModel(_client, ShowType.Search, value: search);
            // Act
            await ((ISupportIncrementalLoading)model.Collection).LoadMoreItemsAsync(count);
            // Assert
            Assert.IsTrue(model.Collection.Count > 0);
        }

        /// <summary>
        /// List Show View Model (User Saved)
        /// </summary>
        public async Task ListShowViewModel_UserSaved_Test()
        {
            // Arrange
            var model = new ListShowViewModel(_client, ShowType.UserSaved);
            // Act
            await ((ISupportIncrementalLoading)model.Collection).LoadMoreItemsAsync(count);
            // Assert
            Assert.IsTrue(model.Collection.Count > 0);
        }
        #endregion List Show View Model

        #region List Track View Model
        /// <summary>
        /// List Track View Model (Favourite)
        /// </summary>
        /// <param name="id">Track Id</param>
        [TestMethod]
        [DataRow("5plveMW66pe7YdXLbf060h")]
        public async Task ListTrackViewModel_Favourite_Test(string id)
        {
            // Arrange
            _client.Favourites.TrackIds = new List<string> { id };
            var model = new ListTrackViewModel(_client, TrackType.Favourite);
            // Act
            var result = await ((ISupportIncrementalLoading)model.Collection).LoadMoreItemsAsync(count);
            // Assert
            Assert.IsTrue(model.Collection.Any(a => a.Id == id));
        }

        /// <summary>
        /// List Track View Model (Multiple)
        /// </summary>
        /// <param name="id">Track Id</param>
        [TestMethod]
        [DataRow("5plveMW66pe7YdXLbf060h")]
        public async Task ListTrackViewModel_Multiple_Test(string id)
        {
            // Arrange
            var ids = new List<string> { id };
            var model = new ListTrackViewModel(_client, TrackType.Multiple, multipleTrackIds: ids);
            // Act
            await ((ISupportIncrementalLoading)model.Collection).LoadMoreItemsAsync(count);
            // Assert
            Assert.IsTrue(model.Collection.Any(a => a.Id == id));
        }

        /// <summary>
        /// List Track View Model (Search)
        /// </summary>
        /// <param name="search">Track Name</param>
        [TestMethod]
        [DataRow("Tubular Bells")]
        public async Task ListTrackViewModel_Search_Test(string search)
        {
            // Arrange
            var model = new ListTrackViewModel(_client, TrackType.Search, value: search);
            // Act
            await ((ISupportIncrementalLoading)model.Collection).LoadMoreItemsAsync(count);
            // Assert
            Assert.IsTrue(model.Collection.Any(a => a.Name == search));
        }

        /// <summary>
        /// List Album View Model (Recommended)
        /// </summary>
        [DataRow("rock")]
        [TestMethod]
        public async Task ListTrackViewModel_Recommended_Test(string genre)
        {
            // Arrange
            var model = new ListTrackViewModel(_client, TrackType.Recommended, 
                recommendation : new RecommendationRequest()
                {
                    SeedGenre = genre
                });
            // Act
            await ((ISupportIncrementalLoading)model.Collection).LoadMoreItemsAsync(count);
            // Assert
            Assert.IsTrue(model.Collection.Count > 0);
        }

        /// <summary>
        /// List Track View Model (Album)
        /// </summary>
        /// <param name="id">Album Id</param>
        [TestMethod]
        [DataRow("2C5HYffMBumERQlNfceyrO")]
        public async Task ListTrackViewModel_Album_Test(string id)
        {
            // Arrange
            var model = new ListTrackViewModel(_client, TrackType.Album, id);
            // Act
            await ((ISupportIncrementalLoading)model.Collection).LoadMoreItemsAsync(count);
            // Assert
            Assert.IsTrue(model.Collection.Count > 0);
        }

        /// <summary>
        /// List Track View Model (Artist)
        /// </summary>
        /// <param name="id">Artist Id</param>
        [TestMethod]
        [DataRow("724YlnEzfIBXRWSmT1ur6W")]
        public async Task ListTrackViewModel_Artist_Test(string id)
        {
            // Arrange
            var model = new ListTrackViewModel(_client, TrackType.Artist, id);
            // Act
            await ((ISupportIncrementalLoading)model.Collection).LoadMoreItemsAsync(count);
            // Assert
            Assert.IsTrue(model.Collection.Count > 0);
        }

        /// <summary>
        /// List Track View Model (User Recently Played)
        /// </summary>
        [TestMethod]
        public async Task ListTrackViewModel_UserRecentlyPlayed_Test()
        {
            // Arrange
            var model = new ListTrackViewModel(_client, TrackType.UserRecentlyPlayed);
            // Act
            await ((ISupportIncrementalLoading)model.Collection).LoadMoreItemsAsync(count);
            // Assert
            Assert.IsTrue(model.Collection.Count > 0);
        }

        /// <summary>
        /// List Track View Model (User Saved)
        /// </summary>
        [TestMethod]
        public async Task ListTrackViewModel_UserSaved_Test()
        {
            // Arrange
            var model = new ListTrackViewModel(_client, TrackType.UserSaved);
            // Act
            await ((ISupportIncrementalLoading)model.Collection).LoadMoreItemsAsync(count);
            // Assert
            Assert.IsTrue(model.Collection.Count > 0);
        }

        /// <summary>
        /// List Track View Model (User Top)
        /// </summary>
        [TestMethod]
        public async Task ListTrackViewModel_UserTop_Test()
        {
            // Arrange
            var model = new ListTrackViewModel(_client, TrackType.UserTop);
            // Act
            await ((ISupportIncrementalLoading)model.Collection).LoadMoreItemsAsync(count);
            // Assert
            Assert.IsTrue(model.Collection.Count > 0);
        }
        #endregion List Track View Model

    }
}