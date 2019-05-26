using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spotify.Uwp.ViewModels;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Spotify.Uwp.Test
{
    [TestClass]
    public class SpotifySdkClientAuthTests
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
            // Spotify Client Token
            var accessToken = new TokenViewModel()
            {
                Token = config["token"],
                Refresh = config["refresh"],
                Expiration = DateTime.Parse(config["expires"]),
                TokenType = (TokenType)Enum.Parse(typeof(TokenType), config["type"])
            };
            _client.Token = accessToken;
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

        #region List Methods
        [TestMethod]
        public async Task Test_ListAlbums_Saved()
        {
            var items = await _client.ListAlbumsAsync(AlbumType.Saved);
            Assert.IsNotNull(items?.Items);
            Assert.IsTrue(items?.Items.Count > 0);
        }

        [TestMethod]
        public async Task Test_ListTracks_Saved()
        {
            var items = await _client.ListTracksAsync(TrackType.Saved);
            Assert.IsNotNull(items?.Items);
            Assert.IsTrue(items?.Items.Count > 0);
        }
        #endregion List Methods
    }
}
