using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spotify.Uwp.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Spotify.Uwp.Test
{
    [TestClass]
    public class SpotifySdkClientTest
    {
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
            // Spotify Client Factory
            _client = SpotifySdkClientFactory.CreateSpotifySdkClient(
                config["client_id"], config["client_secret"]);
            Assert.IsNotNull(_client);
        }

        #region Get Methods
        [TestMethod]
        public async Task Test_Get_Category()
        {
            CategoryViewModel item = await _client.GetCategory("decades");
            Assert.IsTrue(item?.Id == "decades");
        }

        [TestMethod]
        public async Task Test_Get_Artist()
        {
            ArtistViewModel item = await _client.GetArtist("43ZHCT0cAZBISjO8DG9PnE");
            Assert.IsTrue(item?.Id == "43ZHCT0cAZBISjO8DG9PnE");
        }

        [TestMethod]
        public async Task Test_GetAlbum()
        {
            AlbumViewModel item = await _client.GetAlbum("1ZGxGu4fMROqmZsFSoepeE");
            Assert.IsTrue(item?.Id == "1ZGxGu4fMROqmZsFSoepeE");
        }

        [TestMethod]
        public async Task Test_GetPlaylist()
        {
            PlaylistViewModel item = await _client.GetPlaylist("37i9dQZF1DXatMjChPKgBk");
            Assert.IsTrue(item?.Id == "37i9dQZF1DXatMjChPKgBk");
        }

        [TestMethod]
        public async Task Test_GetTrack()
        {
            TrackViewModel item = await _client.GetTrack("1cTZMwcBJT0Ka3UJPXOeeN");
            Assert.IsTrue(item?.Id == "1cTZMwcBJT0Ka3UJPXOeeN");
        }
        #endregion Get Methods

        #region List Methods
        [TestMethod]
        public async Task Test_ListCategories()
        {
            NavigationViewModel<CategoryViewModel> items = await _client.ListCategories();
            Assert.IsNotNull(items?.Items);
            Assert.IsTrue(items?.Items.Count > 0);
        }

        [TestMethod]
        public async Task Test_ListArtists_Related()
        {
            NavigationViewModel<ArtistViewModel> items = await _client.ListArtists(
                ArtistType.Related, "43ZHCT0cAZBISjO8DG9PnE");
            Assert.IsNotNull(items?.Items);
            Assert.IsTrue(items?.Items.Count > 0);
        }

        [TestMethod]
        public async Task Test_ListArtists_Search()
        {
            NavigationViewModel<ArtistViewModel> items = await _client.ListArtists(
                ArtistType.Search, "Mike Oldfield");
            Assert.IsNotNull(items?.Items);
            Assert.IsTrue(items?.Items.Count > 0);
        }

        [TestMethod]
        public async Task Test_ListAlbums_Artist()
        {
            NavigationViewModel<AlbumViewModel> items = await _client.ListAlbums(
                AlbumType.Artist, "43ZHCT0cAZBISjO8DG9PnE");
            Assert.IsNotNull(items?.Items);
            Assert.IsTrue(items?.Items.Count > 0);
        }

        [TestMethod]
        public async Task Test_ListAlbums_NewReleases()
        {
            NavigationViewModel<AlbumViewModel> items = await _client.ListAlbums(
                AlbumType.NewReleases);
            Assert.IsNotNull(items?.Items);
            Assert.IsTrue(items?.Items.Count > 0);
        }

        [TestMethod]
        public async Task Test_ListAlbums_Search()
        {
            NavigationViewModel<AlbumViewModel> items = await _client.ListAlbums(
                AlbumType.Search, "Tubular Bells");
            Assert.IsNotNull(items?.Items);
            Assert.IsTrue(items?.Items.Count > 0);
        }

        [TestMethod]
        public async Task Test_ListPlaylists_CategoriesPlaylists()
        {
            NavigationViewModel<PlaylistViewModel> items = await _client.ListPlaylists(
                PlaylistType.CategoriesPlaylists, "decades");
            Assert.IsNotNull(items?.Items);
            Assert.IsTrue(items?.Items.Count > 0);
        }

        [TestMethod]
        public async Task Test_ListPlaylists_Featured()
        {
            NavigationViewModel<PlaylistViewModel> items = await _client.ListPlaylists(
                PlaylistType.Featured);
            Assert.IsNotNull(items?.Items);
            Assert.IsTrue(items?.Items.Count > 0);
        }

        [TestMethod]
        public async Task Test_ListPlaylists_Search()
        {
            NavigationViewModel<PlaylistViewModel> items = await _client.ListPlaylists(
                PlaylistType.Search, "Chill");
            Assert.IsNotNull(items?.Items);
            Assert.IsTrue(items?.Items.Count > 0);
        }

        [TestMethod]
        public async Task Test_ListRecommendationGenres()
        {
            List<RecommendationViewModel> items = await _client.ListRecommendationGenres();
            Assert.IsNotNull(items);
            Assert.IsTrue(items.Count > 0);
        }

        [TestMethod]
        public async Task Test_ListTracks_Album()
        {
            NavigationViewModel<TrackViewModel> items = await _client.ListTracks(
                TrackType.Album, "1ZGxGu4fMROqmZsFSoepeE");
            Assert.IsNotNull(items?.Items);
            Assert.IsTrue(items?.Items.Count > 0);
        }

        [TestMethod]
        public async Task Test_ListTracks_Artist()
        {
            _client.Country = "GB";
            NavigationViewModel<TrackViewModel> items = await _client.ListTracks(
                TrackType.Artist, "43ZHCT0cAZBISjO8DG9PnE");
            Assert.IsNotNull(items?.Items);
            Assert.IsTrue(items?.Items.Count > 0);
        }

        [TestMethod]
        public async Task Test_ListTracks_Playlist()
        {
            NavigationViewModel<TrackViewModel> items = await _client.ListTracks(
                TrackType.Playlist, "37i9dQZF1DXatMjChPKgBk");
            Assert.IsNotNull(items?.Items);
            Assert.IsTrue(items?.Items.Count > 0);
        }

        [TestMethod]
        public async Task Test_ListTracks_Recommended()
        {
            NavigationViewModel<TrackViewModel> items = await _client.ListTracks(
                TrackType.Recommended, "rock");
            Assert.IsNotNull(items?.Items);
            Assert.IsTrue(items?.Items.Count > 0);
        }

        [TestMethod]
        public async Task Test_ListTracks_Search()
        {
            NavigationViewModel<TrackViewModel> items = await _client.ListTracks(
                TrackType.Search, "Hello");
            Assert.IsNotNull(items?.Items);
            Assert.IsTrue(items?.Items.Count > 0);
        }

        [TestMethod]
        public async Task Test_ListAudioFeatures()
        {
            List<AudioFeatureViewModel> items = await _client.ListAudioFeatures("1cTZMwcBJT0Ka3UJPXOeeN");
            Assert.IsNotNull(items);
            Assert.IsTrue(items.Count > 0);
        }
        #endregion List Methods
    }
}
