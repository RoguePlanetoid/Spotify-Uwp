using Spotify.NetStandard.Client.Interfaces;
using Spotify.Uwp.ViewModels;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Spotify.Uwp
{
    /// <summary>
    /// Spotify SDK Client
    /// </summary>
    public interface ISpotifySdkClient
    {
        #region Public Properties
        /// <summary>
        /// Spotify Client
        /// </summary>
        ISpotifyClient SpotifyClient { get; }

        /// <summary>
        /// ISO 3166-1 alpha-2 country code e.g. GB
        /// </summary>
        string Country { get; set; }

        /// <summary>
        /// ISO 639-1 language code and an ISO 3166-1 alpha-2 country code, joined by an underscore e.g. en_GB
        /// </summary>
        string Locale { get; set; }

        /// <summary>
        /// Number of items to return per request
        /// </summary>
        int? Limit { get; set; }

        /// <summary>
        /// Token View Model
        /// </summary>
        TokenViewModel Token { get; set; }

        /// <summary>
        /// List Favourite ViewModel 
        /// </summary>
        ListFavouriteViewModel Favourites { get; set; }
        #endregion Public Properties

        #region Public Methods
        /// <summary>
        /// Set
        /// </summary>
        /// <param name="cultureInfo">Culture Info</param>
        /// <returns>ISpotifySdkClient</returns>
        ISpotifySdkClient Set(CultureInfo cultureInfo);

        /// <summary>
        /// Set
        /// </summary>
        /// <param name="country">ISO 3166-1 alpha-2 country code e.g. GB</param>
        /// <param name="locale">ISO 639-1 language code and an ISO 3166-1 alpha-2 country code, joined by an underscore e.g. en_GB</param>
        /// <returns></returns>
        ISpotifySdkClient Set(
            string country = null,
            string locale = null);
        #endregion Public Methods

        #region Get Methods
        /// <summary>
        /// Get Category
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <param name="page">Page</param>
        /// <returns>Category ViewModel</returns>
        Task<CategoryViewModel> GetCategoryAsync(
            string id);

        /// <summary>
        /// Get Artist
        /// </summary>
        /// <param name="id">Artist Spotify Id</param>
        /// <returns>Artist ViewModel</returns>
        Task<ArtistViewModel> GetArtistAsync(
            string id);

        /// <summary>
        /// Get Album
        /// </summary>
        /// <param name="id">Album Spotify Id</param>
        /// <returns>Album View Model</returns>
        Task<AlbumViewModel> GetAlbumAsync(
            string id);

        /// <summary>
        /// Get Playlist
        /// </summary>
        /// <param name="id">Playlist Spotify Id</param>
        /// <returns>Playlist ViewModel</returns>
        Task<PlaylistViewModel> GetPlaylistAsync(
            string id);

        /// <summary>
        /// Get Track
        /// </summary>
        /// <param name="id">Track Spotify Id</param>
        /// <returns>Track ViewModel</returns>
        Task<TrackViewModel> GetTrackAsync(
            string id);

        /// <summary>
        /// Get Audio Analysis
        /// </summary>
        /// <param name="id">Track Spotify Id</param>
        /// <returns>AudioAnalysis ViewModel</returns>
        Task<AudioAnalysisViewModel> GetAudioAnalysisAsync(
            string id);
        #endregion Get Methods

        #region List Methods
        /// <summary>
        /// List Category
        /// </summary>
        /// <returns>Navigation ViewModel of Category ViewModel</returns>
        Task<NavigationViewModel<CategoryViewModel>>
            ListCategoriesAsync();

        /// <summary>
        /// List Categories
        /// </summary>
        /// <param name="navigation">Navigation ViewModel of Category ViewModel</param>
        /// <returns>Navigation ViewModel of Category ViewModel</returns>
        Task<NavigationViewModel<CategoryViewModel>>
            ListCategoriesAsync(NavigationViewModel<CategoryViewModel> navigation);

        /// <summary>
        /// List Artists
        /// </summary>
        /// <param name="type">Artist Type</param>
        /// <param name="id">Artist Spotify Id</param>
        /// <returns>Navigation ViewModel of Artist ViewModel</returns>
        Task<NavigationViewModel<ArtistViewModel>>
            ListArtistsAsync(
            ArtistType type,
            string id = null);

        /// <summary>
        /// List Artists
        /// </summary>
        /// <param name="navigation">Navigation ViewModel of Artist ViewModel</param>
        /// <returns>Navigation ViewModel of Artist ViewModel</returns>
        Task<NavigationViewModel<ArtistViewModel>> ListArtistsAsync(
            NavigationViewModel<ArtistViewModel> navigation);

        /// <summary>
        /// List Albums
        /// </summary>
        /// <param name="type">Album Type</param>
        /// <param name="id">Album Spotify Id</param>
        /// <returns>Navigation ViewModel of Album ViewModel</returns>
        Task<NavigationViewModel<AlbumViewModel>>
            ListAlbumsAsync(
            AlbumType type,
            string id = null);

        /// <summary>
        /// List Albums
        /// </summary>
        /// <param name="navigation">Navigation ViewModel of Album ViewModel</param>
        /// <returns>Navigation ViewModel of Album ViewModel</returns>
        Task<NavigationViewModel<AlbumViewModel>> ListAlbumsAsync(
            NavigationViewModel<AlbumViewModel> navigation);

        /// <summary>
        /// List Playlists
        /// </summary>
        /// <param name="type">Playlist Type</param>
        /// <param name="id">Playlist Spotify Id</param>
        /// <returns>Navigation ViewModel of Playlist ViewModel</returns>
        Task<NavigationViewModel<PlaylistViewModel>>
            ListPlaylistsAsync(
            PlaylistType type,
            string id = null);

        /// <summary>
        /// List Playlists
        /// </summary>
        /// <param name="navigation">Navigation ViewModel of Playlist ViewModel</param>
        /// <returns>Navigation ViewModel of Playlist ViewModel</returns>
        Task<NavigationViewModel<PlaylistViewModel>> ListPlaylistsAsync(
            NavigationViewModel<PlaylistViewModel> navigation);

        /// <summary>
        /// List Recommendation Genres
        /// </summary>
        /// <returns>List of Recommendation ViewModel</returns>
        Task<List<RecommendationViewModel>> ListRecommendationGenresAsync();

        /// <summary>
        /// List Tracks
        /// </summary>
        /// <param name="type">Track Type</param>
        /// <param name="id">Track Spotify Id</param>
        /// <returns>Navigation ViewModel of Track ViewModel</returns>
        Task<NavigationViewModel<TrackViewModel>>
            ListTracksAsync(
            TrackType type,
            string id = null);

        /// <summary>
        /// List Tracks
        /// </summary>
        /// <param name="navigation">Navigation ViewModel of Track ViewModel</param>
        /// <returns>Navigation ViewModel of Track ViewModel</returns>
        Task<NavigationViewModel<TrackViewModel>> ListTracksAsync(
            NavigationViewModel<TrackViewModel> navigation);

        /// <summary>
        /// List Audio Features
        /// </summary>
        /// <param name="id">Track Spotify Id</param>
        /// <returns>List of AudioFeatureViewModel</returns>
        Task<List<AudioFeatureViewModel>> ListAudioFeatureAsync(
            string id);

        /// <summary>
        /// List Audio Features
        /// </summary>
        /// <param name="ids">List of Track Spotify Id</param>
        /// <returns>List of List of AudioFeature ViewModel</returns>
        Task<List<List<AudioFeatureViewModel>>> ListAudioFeaturesAsync(
            List<string> ids);
        #endregion List Methods   
    }
}