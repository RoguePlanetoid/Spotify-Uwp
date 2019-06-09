using Spotify.NetStandard.Client.Interfaces;
using Spotify.Uwp.Exceptions;
using Spotify.Uwp.ViewModels;
using System;
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
        /// Login Redirect Uri
        /// </summary>
        Uri LoginRedirectUri { get; set; }

        /// <summary>
        /// Login State
        /// </summary>
        string LoginState { get; set; }

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
        /// Time Frame for User Top Artists and Tracks. Long Term: calculated from several years of data and including all new data as it becomes available, Medium Term: (Default) approximately last 6 months, Short Term: approximately last 4 weeks
        /// </summary>
        UserTopTimeFrame UserTopTimeFrame { get; set; }

        /// <summary>
        /// Is User Logged In
        /// </summary>
        bool IsUserLoggedIn { get; set; }

        /// <summary>
        /// Token View Model
        /// </summary>
        TokenViewModel Token { get; set; }

        /// <summary>
        /// List Favourite View Model 
        /// </summary>
        ListFavouriteViewModel Favourites { get; set; }
        #endregion Public Properties

        #region Public Events
        /// <summary>
        /// Token Required Event
        /// </summary>
        event EventHandler<TokenRequiredArgs> TokenRequiredEvent;
        #endregion Public Events

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

        #region Authentication Methods
        /// <summary>
        /// Get Login Uri
        /// </summary>
        /// <param name="type">(Required) LoginType.AuthorisationCode or LoginType.ImplicitGrant</param>
        /// <param name="scope">(Optional) Authorisation Scopes</param>
        /// <param name="showDialog">(Optional) Whether or not to force the user to approve the app again if they’ve already done so.</param>
        /// <exception cref="ArgumentNullException"></exception>
        Uri GetLoginUri(
            LoginType type,
            ScopeViewModel scope = null,
            bool showDialog = false);

        /// <summary>
        /// Get Login Token
        /// </summary>
        /// <param name="type">Login Type</param>
        /// <param name="responseUri">(Required for LoginType.AuthorisationCode or LoginType.ImplicitGrant) Response Uri</param>
        /// <returns>AccessToken on Success, Null if Not</returns>
        /// <exception cref="AuthValueException">AuthValueException</exception>
        /// <exception cref="AuthStateException">AuthStateException</exception>
        Task<TokenViewModel> GetLoginTokenAsync(
            LoginType type,
            Uri responseUri = null);
        #endregion Authentication Methods

        #region Get Methods
        /// <summary>
        /// Get Category
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <returns>Category View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        Task<CategoryViewModel> GetCategoryAsync(
            string id);

        /// <summary>
        /// Get Artist
        /// </summary>
        /// <param name="id">Artist Spotify Id</param>
        /// <returns>Artist View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        Task<ArtistViewModel> GetArtistAsync(
            string id);

        /// <summary>
        /// Get Album
        /// </summary>
        /// <param name="id">Album Spotify Id</param>
        /// <returns>Album View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        Task<AlbumViewModel> GetAlbumAsync(
            string id);

        /// <summary>
        /// Get Playlist
        /// </summary>
        /// <param name="id">Playlist Spotify Id</param>
        /// <returns>Playlist View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        Task<PlaylistViewModel> GetPlaylistAsync(
            string id);

        /// <summary>
        /// Get Track
        /// </summary>
        /// <param name="id">Track Spotify Id</param>
        /// <returns>Track View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        Task<TrackViewModel> GetTrackAsync(
            string id);

        /// <summary>
        /// Get Audio Analysis
        /// </summary>
        /// <param name="id">Track Spotify Id</param>
        /// <returns>AudioAnalysis View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        Task<AudioAnalysisViewModel> GetAudioAnalysisAsync(
            string id);

        /// <summary>
        /// Get User
        /// </summary>
        /// <param name="id">User Spotify Id</param>
        /// <returns>Public User View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        Task<UserViewModel> GetUserAsync(
            string id);

        /// <summary>
        /// Get Current User
        /// <para>Scopes: UserReadPrivate, UserReadEmail, UserReadBirthDate, UserReadPrivate</para>
        /// </summary>
        /// <returns>Current User View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        Task<CurrentUserViewModel> GetCurrentUserAsync();
        #endregion Get Methods

        #region List Methods
        /// <summary>
        /// List Category
        /// </summary>
        /// <returns>Navigation View Model of Category View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        Task<NavigationViewModel<CategoryViewModel>> ListCategoriesAsync();

        /// <summary>
        /// List Categories
        /// </summary>
        /// <param name="navigation">Navigation View Model of Category View Model</param>
        /// <returns>Navigation View Model of Category View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        Task<NavigationViewModel<CategoryViewModel>> ListCategoriesAsync(
            NavigationViewModel<CategoryViewModel> navigation);

        /// <summary>
        /// List Artists
        /// </summary>
        /// <param name="type">Artist Type</param>
        /// <param name="id">Artist Spotify Id</param>
        /// <returns>Navigation View Model of Artist View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        Task<NavigationViewModel<ArtistViewModel>> ListArtistsAsync(
            ArtistType type,
            string id = null);

        /// <summary>
        /// List Artists
        /// </summary>
        /// <param name="navigation">Navigation View Model of Artist View Model</param>
        /// <returns>Navigation View Model of Artist View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        Task<NavigationViewModel<ArtistViewModel>> ListArtistsAsync(
            NavigationViewModel<ArtistViewModel> navigation);

        /// <summary>
        /// List Albums
        /// </summary>
        /// <param name="type">Album Type</param>
        /// <param name="id">Album Spotify Id</param>
        /// <returns>Navigation View Model of Album View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        Task<NavigationViewModel<AlbumViewModel>>
            ListAlbumsAsync(
            AlbumType type,
            string id = null);

        /// <summary>
        /// List Albums
        /// </summary>
        /// <param name="navigation">Navigation View Model of Album View Model</param>
        /// <returns>Navigation View Model of Album View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        Task<NavigationViewModel<AlbumViewModel>> ListAlbumsAsync(
            NavigationViewModel<AlbumViewModel> navigation);

        /// <summary>
        /// List Playlists
        /// </summary>
        /// <param name="type">Playlist Type</param>
        /// <param name="id">Playlist Spotify Id</param>
        /// <returns>Navigation View Model of Playlist View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        Task<NavigationViewModel<PlaylistViewModel>>
            ListPlaylistsAsync(
            PlaylistType type,
            string id = null);

        /// <summary>
        /// List Playlists
        /// </summary>
        /// <param name="navigation">Navigation View Model of Playlist View Model</param>
        /// <returns>Navigation View Model of Playlist View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        Task<NavigationViewModel<PlaylistViewModel>> ListPlaylistsAsync(
            NavigationViewModel<PlaylistViewModel> navigation);

        /// <summary>
        /// List Recommendation Genres
        /// </summary>
        /// <returns>List of Recommendation View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        Task<List<RecommendationViewModel>> ListRecommendationGenresAsync();

        /// <summary>
        /// List Tracks
        /// </summary>
        /// <param name="type">Track Type</param>
        /// <param name="id">Track Spotify Id</param>
        /// <returns>Navigation ViewModel of Track View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        Task<NavigationViewModel<TrackViewModel>> ListTracksAsync(
            TrackType type,
            string id = null);

        /// <summary>
        /// List Tracks
        /// </summary>
        /// <param name="navigation">Navigation View Model of Track View Model</param>
        /// <returns>Navigation ViewModel of Track View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        Task<NavigationViewModel<TrackViewModel>> ListTracksAsync(
            NavigationViewModel<TrackViewModel> navigation);

        /// <summary>
        /// List Audio Features
        /// </summary>
        /// <param name="id">Track Spotify Id</param>
        /// <returns>List of Audio Feature View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        Task<List<AudioFeatureViewModel>> ListAudioFeatureAsync(
            string id);

        /// <summary>
        /// List Audio Features
        /// </summary>
        /// <param name="ids">List of Track Spotify Id</param>
        /// <returns>List of List of Audio Feature View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        Task<List<List<AudioFeatureViewModel>>> ListAudioFeaturesAsync(
            List<string> ids);
        #endregion List Methods   

        #region Follow Methods
        /// <summary>
        /// Is Following Artists or Users and Check if Users Follow a Playlist
        /// <para>Scopes: FollowRead, PlaylistReadPrivate</para>
        /// </summary>
        /// <param name="ids">(Required for FollowType.Artist or FollowType.User) List of the Artist or the User Spotify IDs to check</param>
        /// <param name="type">(Required) Either Artist, User or Playlist</param>
        /// <param name="playlistId">(Required for FollowType.Playlist) The Spotify ID of the playlist</param>
        /// <returns>List of True or False values</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        /// <exception cref="ArgumentNullException"></exception>
        Task<List<bool>> IsFollowing(
            List<string> ids,
            FollowType type,
            string playlistId = null);

        /// <summary>
        /// Is Following
        /// <para>Scopes: FollowRead, PlaylistReadPrivate</para>
        /// </summary>
        /// <param name="id">(Required) Artist, User or Playlist Spotify ID to check</param>
        /// <param name="type">(Required) Either Artist, User or Playlist</param>
        /// <returns>True if Is, False if Not</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        /// <exception cref="ArgumentNullException"></exception>
        Task<bool> IsFollowing(
            string id,
            FollowType type);

        /// <summary>
        /// Follow
        /// <para>Scopes: FollowModify</para>
        /// </summary>
        /// <param name="ids">(Required for FollowType.Artist or FollowType.User) Artist or the User Spotify IDs to Follow</param>
        /// <param name="type">(Required) Either Artist, User or Playlist</param>
        /// <param name="playlistId">(Required for FollowType.Playlist) The Spotify ID of the playlist</param>
        /// <returns>True if Successful, False if Not Successful</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        /// <exception cref="ArgumentNullException"></exception>
        Task<bool> Follow(
            List<string> ids,
            FollowType type,
            string playlistId = null);

        /// <summary>
        /// Follow
        /// <para>Scopes: FollowModify</para>
        /// </summary>
        /// <param name="id">(Required) Artist, User or Playlist Spotify ID to Follow</param>
        /// <param name="type">(Required) Either Artist, User or Playlist</param>
        /// <returns>True if Is, False if Not</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        /// <exception cref="ArgumentNullException"></exception>
        Task<bool> Follow(
            string id,
            FollowType type);

        /// <summary>
        /// Unfollow
        /// <para>Scopes: FollowModify, PlaylistModifyPublic, PlaylistModifyPrivate</para>
        /// </summary>
        /// <param name="ids">(Required for FollowType.Artist or FollowType.User) Artist or the User Spotify IDs to Follow</param>
        /// <param name="type">(Required) Either Artist, User or Playlist</param>
        /// <param name="playlistId">(Required for FollowType.Playlist) The Spotify ID of the playlist</param>
        /// <returns>True if Successful, False if Not Successful</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        /// <exception cref="ArgumentNullException"></exception>
        Task<bool> Unfollow(
            List<string> ids,
            FollowType type,
            string playlistId = null);

        /// <summary>
        /// Unfollow
        /// <para>Scopes: FollowModify, PlaylistModifyPublic, PlaylistModifyPrivate</para>
        /// </summary>
        /// <param name="id">(Required) Artist or the User Spotify ID to Unfollow</param>
        /// <param name="type">(Required) Either Artist, User or Playlist</param>
        /// <returns>True if Is, False if Not</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        /// <exception cref="ArgumentNullException"></exception>
        Task<bool> Unfollow(
            string id,
            FollowType type);
        #endregion Follow Methods
    }
}