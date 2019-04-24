using Spotify.NetStandard.Client.Interfaces;
using Spotify.Uwp.ViewModels;
using System.Collections.Generic;
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
        /// Locale Code
        /// </summary>
        string Locale { get; set; }

        /// <summary>
        /// Country Code
        /// </summary>
        string Country { get; set; }

        /// <summary>
        /// Limit Value
        /// </summary>
        int? Limit { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        TokenViewModel Token { get; set; }

        /// <summary>
        /// Favourites
        /// </summary>
        ListFavouriteViewModel Favourites { get; set; }
        #endregion Public Properties

        #region Get Methods
        /// <summary>
        /// Get Category
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <param name="page">Page</param>
        /// <returns>CategoryViewModel</returns>
        Task<CategoryViewModel> GetCategory(
            string id);

        /// <summary>
        /// Get Artist
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ArtistViewModel> GetArtist(
            string id);

        /// <summary>
        /// Get Album
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AlbumViewModel> GetAlbum(
            string id);

        /// <summary>
        /// Get Playlist
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<PlaylistViewModel> GetPlaylist(
            string id);

        /// <summary>
        /// Get Track
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TrackViewModel> GetTrack(
            string id);
        #endregion Get Methods

        #region List Methods
        /// <summary>
        /// List Category
        /// </summary>
        /// <param name="limit">Limit</param>
        /// <returns>Page</returns>
        Task<NavigationViewModel<CategoryViewModel>>
            ListCategories();

        /// <summary>
        /// List Categories
        /// </summary>
        /// <param name="page">Page</param>
        /// <param name="country"></param>
        /// <returns>Page</returns>
        Task<NavigationViewModel<CategoryViewModel>>
            ListCategories(NavigationViewModel<CategoryViewModel> navigation);

        /// <summary>
        /// List Artists
        /// </summary>
        /// <param name="page">Page</param>
        /// <returns>Page</returns>
        Task<NavigationViewModel<ArtistViewModel>>
            ListArtists(
            ArtistType type,
            string id = null);

        /// <summary>
        /// List Artists
        /// </summary>
        /// <param name="navigation">Navigation</param>
        /// <returns>Page</returns>
        Task<NavigationViewModel<ArtistViewModel>> ListArtists(
            NavigationViewModel<ArtistViewModel> navigation);

        /// <summary>
        /// List Albums
        /// </summary>
        /// <param name="page">Page</param>
        /// <returns>Page</returns>
        Task<NavigationViewModel<AlbumViewModel>>
            ListAlbums(
            AlbumType type,
            string id = null);

        /// <summary>
        /// List Albums
        /// </summary>
        /// <param name="navigation">Navigation</param>
        /// <returns>Page</returns>
        Task<NavigationViewModel<AlbumViewModel>> ListAlbums(
            NavigationViewModel<AlbumViewModel> navigation);

        /// <summary>
        /// List Playlists
        /// </summary>
        /// <param name="page">Page</param>
        /// <returns>Page</returns>
        Task<NavigationViewModel<PlaylistViewModel>>
            ListPlaylists(
            PlaylistType type,
            string id = null);

        /// <summary>
        /// List Playlists
        /// </summary>
        /// <param name="paging">Paging</param>
        /// <returns>Page</returns>
        Task<NavigationViewModel<PlaylistViewModel>> ListPlaylists(
            NavigationViewModel<PlaylistViewModel> navigation);

        /// <summary>
        /// List Recommendation Genres
        /// </summary>
        /// <returns></returns>
        Task<List<RecommendationViewModel>> ListRecommendationGenres();

        /// <summary>
        /// List Tracks
        /// </summary>
        /// <param name="page">Page</param>
        /// <returns>Page</returns>
        Task<NavigationViewModel<TrackViewModel>>
            ListTracks(
            TrackType type,
            string id = null);

        /// <summary>
        /// List Tracks
        /// </summary>
        /// <param name="paging">Paging</param>
        /// <returns>Page</returns>
        Task<NavigationViewModel<TrackViewModel>> ListTracks(
            NavigationViewModel<TrackViewModel> navigation);

        /// <summary>
        /// List Audio Features
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        Task<List<AudioFeatureViewModel>> ListAudioFeatures(
            string id);
        #endregion List Methods   
    }
}