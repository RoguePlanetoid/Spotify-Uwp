using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spotify.Uwp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Spotify.Uwp.Test
{
    [TestClass]
    public class SpotifySdkClientTest
    {
        private readonly Uri redirect_url = new Uri("https://www.example.org/spotify");
        private const string state = "spotify.state";

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
                config["client_id"], config["client_secret"]).Set("GB");
            Assert.IsNotNull(_client);
        }

        #region Authentication Methods
        [TestMethod]
        public void Test_Get_AuthorisationCodeFlowUri()
        {
            var uri = _client.GetAuthorisationCodeFlowUri(redirect_url, state, ScopeViewModel.AllPermissions);
            Assert.IsNotNull(uri);
        }

        [TestMethod]
        public void Test_Get_ImplicitGrantFlowUri()
        {
            var uri = _client.GetImplicitGrantFlowUri(redirect_url, state, ScopeViewModel.AllPermissions);
            Assert.IsNotNull(uri);
        }
        #endregion Authentication Methods

        #region Get Methods
        [TestMethod]
        public async Task Test_Get_Category()
        {
            CategoryViewModel item = await _client.GetCategoryAsync("pop");
            Assert.IsTrue(item?.Id == "pop");
        }

        [TestMethod]
        public async Task Test_Get_Artist()
        {
            ArtistViewModel item = await _client.GetArtistAsync("43ZHCT0cAZBISjO8DG9PnE");
            Assert.IsTrue(item?.Id == "43ZHCT0cAZBISjO8DG9PnE");
        }

        [TestMethod]
        public async Task Test_GetAlbum()
        {
            AlbumViewModel item = await _client.GetAlbumAsync("1ZGxGu4fMROqmZsFSoepeE");
            Assert.IsTrue(item?.Id == "1ZGxGu4fMROqmZsFSoepeE");
        }

        [TestMethod]
        public async Task Test_GetPlaylist()
        {
            PlaylistViewModel item = await _client.GetPlaylistAsync("37i9dQZF1DXatMjChPKgBk");
            Assert.IsTrue(item?.Id == "37i9dQZF1DXatMjChPKgBk");
        }

        [TestMethod]
        public async Task Test_GetTrack()
        {
            TrackViewModel item = await _client.GetTrackAsync("1cTZMwcBJT0Ka3UJPXOeeN");
            Assert.IsTrue(item?.Id == "1cTZMwcBJT0Ka3UJPXOeeN");
        }

        [TestMethod]
        public async Task Test_GetAudioAnalysis()
        {
            AudioAnalysisViewModel item = await _client.GetAudioAnalysisAsync("1cTZMwcBJT0Ka3UJPXOeeN");
            Assert.IsNotNull(item);
        }
        #endregion Get Methods

        #region List Methods
        [TestMethod]
        public async Task Test_ListCategories()
        {
            NavigationViewModel<CategoryViewModel> items = await _client.ListCategoriesAsync();
            Assert.IsNotNull(items?.Items);
            Assert.IsTrue(items?.Items.Count > 0);
        }

        [TestMethod]
        public async Task Test_ListArtists_Related()
        {
            NavigationViewModel<ArtistViewModel> items = await _client.ListArtistsAsync(
                ArtistType.Related, "43ZHCT0cAZBISjO8DG9PnE");
            Assert.IsNotNull(items?.Items);
            Assert.IsTrue(items?.Items.Count > 0);
        }

        [TestMethod]
        public async Task Test_ListArtists_Search()
        {
            NavigationViewModel<ArtistViewModel> items = await _client.ListArtistsAsync(
                ArtistType.Search, "Mike Oldfield");
            Assert.IsNotNull(items?.Items);
            Assert.IsTrue(items?.Items.Count > 0);
        }

        [TestMethod]
        public async Task Test_ListAlbums_Artist()
        {
            NavigationViewModel<AlbumViewModel> items = await _client.ListAlbumsAsync(
                AlbumType.Artist, "43ZHCT0cAZBISjO8DG9PnE");
            Assert.IsNotNull(items?.Items);
            Assert.IsTrue(items?.Items.Count > 0);
        }

        [TestMethod]
        public async Task Test_ListAlbums_NewReleases()
        {
            NavigationViewModel<AlbumViewModel> items = await _client.ListAlbumsAsync(
                AlbumType.NewReleases);
            Assert.IsNotNull(items?.Items);
            Assert.IsTrue(items?.Items.Count > 0);
        }

        [TestMethod]
        public async Task Test_ListAlbums_Search()
        {
            NavigationViewModel<AlbumViewModel> items = await _client.ListAlbumsAsync(
                AlbumType.Search, "Tubular Bells");
            Assert.IsNotNull(items?.Items);
            Assert.IsTrue(items?.Items.Count > 0);
        }

        [TestMethod]
        public async Task Test_ListPlaylists_CategoriesPlaylists()
        {
            NavigationViewModel<PlaylistViewModel> items = await _client.ListPlaylistsAsync(
                PlaylistType.CategoriesPlaylists, "pop");
            Assert.IsNotNull(items?.Items);
            Assert.IsTrue(items?.Items.Count > 0);
        }

        [TestMethod]
        public async Task Test_ListPlaylists_Featured()
        {
            NavigationViewModel<PlaylistViewModel> items = await _client.ListPlaylistsAsync(
                PlaylistType.Featured);
            Assert.IsNotNull(items?.Items);
            Assert.IsTrue(items?.Items.Count > 0);
        }

        [TestMethod]
        public async Task Test_ListPlaylists_Search()
        {
            NavigationViewModel<PlaylistViewModel> items = await _client.ListPlaylistsAsync(
                PlaylistType.Search, "Chill");
            Assert.IsNotNull(items?.Items);
            Assert.IsTrue(items?.Items.Count > 0);
        }

        [TestMethod]
        public async Task Test_ListRecommendationGenres()
        {
            List<RecommendationViewModel> items = await _client.ListRecommendationGenresAsync();
            Assert.IsNotNull(items);
            Assert.IsTrue(items.Count > 0);
        }

        [TestMethod]
        public async Task Test_ListTracks_Album()
        {
            NavigationViewModel<TrackViewModel> items = await _client.ListTracksAsync(
                TrackType.Album, "1ZGxGu4fMROqmZsFSoepeE");
            Assert.IsNotNull(items?.Items);
            Assert.IsTrue(items?.Items.Count > 0);
        }

        [TestMethod]
        public async Task Test_ListTracks_Artist()
        {
            _client.Country = "GB";
            NavigationViewModel<TrackViewModel> items = await _client.ListTracksAsync(
                TrackType.Artist, "43ZHCT0cAZBISjO8DG9PnE");
            Assert.IsNotNull(items?.Items);
            Assert.IsTrue(items?.Items.Count > 0);
        }

        [TestMethod]
        public async Task Test_ListTracks_Playlist()
        {
            NavigationViewModel<TrackViewModel> items = await _client.ListTracksAsync(
                TrackType.Playlist, "37i9dQZF1DXatMjChPKgBk");
            Assert.IsNotNull(items?.Items);
            Assert.IsTrue(items?.Items.Count > 0);
        }

        [TestMethod]
        public async Task Test_ListTracks_Recommended()
        {
            NavigationViewModel<TrackViewModel> items = await _client.ListTracksAsync(
                TrackType.Recommended, "rock");
            Assert.IsNotNull(items?.Items);
            Assert.IsTrue(items?.Items.Count > 0);
        }

        [TestMethod]
        public async Task Test_ListTracks_Search()
        {
            NavigationViewModel<TrackViewModel> items = await _client.ListTracksAsync(
                TrackType.Search, "Hello");
            Assert.IsNotNull(items?.Items);
            Assert.IsTrue(items?.Items.Count > 0);
        }

        [TestMethod]
        public async Task Test_ListAudioFeatures()
        {
            List<AudioFeatureViewModel> items = await _client.ListAudioFeatureAsync("1cTZMwcBJT0Ka3UJPXOeeN");
            Assert.IsNotNull(items);
            Assert.IsTrue(items.Count > 0);
        }

        [TestMethod]
        public async Task Test_ListAudioFeatures_List()
        {
            List<List<AudioFeatureViewModel>> items = await _client.ListAudioFeaturesAsync(
                new List<string> {
                "3n3Ppam7vgaVa1iaRUc9Lp",
                "3twNvmDtFQtAd5gMKedhLD"
                });
            Assert.IsNotNull(items);
            Assert.IsTrue(items.Count > 0);
        }
        #endregion List Methods
    }
}
