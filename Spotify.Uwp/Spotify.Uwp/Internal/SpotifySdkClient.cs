using Spotify.NetStandard.Client;
using Spotify.NetStandard.Client.Exceptions;
using Spotify.NetStandard.Client.Interfaces;
using Spotify.NetStandard.Enums;
using Spotify.NetStandard.Requests;
using Spotify.NetStandard.Responses;
using Spotify.Uwp.Exceptions;
using Spotify.Uwp.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Spotify.Uwp.Internal
{
    /// <summary>
    /// Spotify SDK Client
    /// </summary>
    internal class SpotifySdkClient : BaseNotifyPropertyChanged, ISpotifySdkClient
    {
        #region Private Members
        private bool _isUserLoggedIn;
        #endregion Private Members

        #region Protected Methods
        /// <summary>
        /// On Token Required 
        /// </summary>
        /// <param name="tokenType">Token Type</param>
        /// <exception cref="TokenRequiredException">If No Event Subscribers Event will be Raised</exception>
        protected virtual void OnTokenRequired(TokenType tokenType)
        {
            var handler = TokenRequiredEvent;
            if (handler != null)
            {
                handler(this, new TokenRequiredArgs(tokenType));
            }
            else
            {
                throw new TokenRequiredException(tokenType);
            }
        }
        #endregion Protected Methods

        #region Constructor
        /// <summary>
        /// Spotify SDK Client
        /// </summary>
        /// <param name="clientId">(Required) Spotify Client Id</param>
        /// <param name="clientSecret">Spotify Client Secret</param>
        /// <param name="loginRedirectUri">Login Redirect Uri</param>
        /// <param name="loginState">Login State</param>
        public SpotifySdkClient(
            string clientId,
            string clientSecret = null,
            Uri loginRedirectUri = null,
            string loginState = null)
        {
            LoginRedirectUri = loginRedirectUri;
            LoginState = loginState;
            SpotifyClient = SpotifyClientFactory.CreateSpotifyClient(clientId, clientSecret);
        }
        #endregion Constructor

        #region Public Methods
        /// <summary>
        /// Set
        /// </summary>
        /// <param name="cultureInfo">Culture Info</param>
        /// <returns>ISpotifySdkClient</returns>
        public ISpotifySdkClient Set(CultureInfo cultureInfo)
        {
            if (cultureInfo != null)
            {
                var region = new RegionInfo(cultureInfo.LCID);
                if (Country == null)
                    Country = region.TwoLetterISORegionName;
                if (Locale == null)
                    Locale = $"{cultureInfo.TwoLetterISOLanguageName.ToLower()}_{region.TwoLetterISORegionName.ToUpper()}";
            }
            return this;
        }

        /// <summary>
        /// Set
        /// </summary>
        /// <param name="country">ISO 3166-1 alpha-2 country code e.g. GB</param>
        /// <param name="locale">ISO 639-1 language code and an ISO 3166-1 alpha-2 country code, joined by an underscore e.g. en_GB</param>
        /// <returns></returns>
        public ISpotifySdkClient Set(
            string country = null,
            string locale = null)
        {
            Country = country;
            Locale = locale;
            return this;
        }
        #endregion Public Methods

        #region Public Properties
        /// <summary>
        /// Spotify Client
        /// </summary>
        public ISpotifyClient SpotifyClient { get; }

        /// <summary>
        /// Login Redirect Uri
        /// </summary>
        public Uri LoginRedirectUri { get; set; }

        /// <summary>
        /// Login State
        /// </summary>
        public string LoginState { get; set; }

        /// <summary>
        /// ISO 3166-1 alpha-2 country code e.g. GB
        /// </summary>
        public string Country { get; set; } = null;

        /// <summary>
        /// ISO 639-1 language code and an ISO 3166-1 alpha-2 country code, joined by an underscore e.g. en_GB
        /// </summary>
        public string Locale { get; set; } = null;

        /// <summary>
        /// Number of items to return per request
        /// </summary>
        public int? Limit { get; set; } = null;

        /// <summary>
        /// Time Frame for User Top Artists and Tracks. Long Term: calculated from several years of data and including all new data as it becomes available, Medium Term: (Default) approximately last 6 months, Short Term: approximately last 4 weeks
        /// </summary>
        public UserTopTimeFrame UserTopTimeFrame { get; set; } = UserTopTimeFrame.ShortTerm;

        /// <summary>
        /// Is User Logged In
        /// </summary>
        public bool IsUserLoggedIn
        {
            get => _isUserLoggedIn;
            set { _isUserLoggedIn = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// Token View Model
        /// </summary>
        public TokenViewModel Token
        {
            get => Mapping.MapToken(SpotifyClient.GetToken());
            set => SpotifyClient.SetToken(Mapping.MapToken(value));
        }

        /// <summary>
        /// List Favourite View Model 
        /// </summary>
        public ListFavouriteViewModel Favourites { get; set; } = new ListFavouriteViewModel();
        #endregion Public Properties

        #region Public Events
        /// <summary>
        /// Token Required Event
        /// </summary>
        public event EventHandler<TokenRequiredArgs> TokenRequiredEvent;
        #endregion Public Events

        #region Authentication Methods
        /// <summary>
        /// Get Login Uri
        /// </summary>
        /// <param name="type">(Required) LoginType.AuthorisationCode or LoginType.ImplicitGrant</param>
        /// <param name="scope">(Optional) Authorisation Scopes</param>
        /// <param name="showDialog">(Optional) Whether or not to force the user to approve the app again if they’ve already done so.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Uri GetLoginUri(
            LoginType type,
            ScopeViewModel scope = null,
            bool showDialog = false)
        {
            if (LoginRedirectUri == null)
                throw new ArgumentNullException(nameof(LoginRedirectUri));
            switch (type)
            {
                case LoginType.AuthorisationCode:
                    return SpotifyClient.AuthUser(LoginRedirectUri, LoginState, Mapping.MapScope(scope), showDialog);
                case LoginType.ClientCredentials:
                    return null; // Client Credentials Unsupported
                case LoginType.ImplicitGrant:
                    return SpotifyClient.AuthUserImplicit(LoginRedirectUri, LoginState, Mapping.MapScope(scope), showDialog);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Get Login Token
        /// </summary>
        /// <param name="type">Login Type</param>
        /// <param name="responseUri">(Required for LoginType.AuthorisationCode or LoginType.ImplicitGrant) Response Uri</param>
        /// <returns>AccessToken on Success, Null if Not</returns>
        /// <exception cref="AuthValueException">AuthValueException</exception>
        /// <exception cref="AuthStateException">AuthStateException</exception>
        public async Task<TokenViewModel> GetLoginTokenAsync(
            LoginType type,
            Uri responseUri = null)
        {
            TokenViewModel model = null;
            try
            {
                switch (type)
                {
                    case LoginType.AuthorisationCode:
                        if (responseUri == null)
                            throw new ArgumentNullException(nameof(responseUri));
                        if (LoginRedirectUri == null)
                            throw new ArgumentNullException(nameof(LoginRedirectUri));
                        model = Mapping.MapToken(await SpotifyClient.AuthUserAsync(
                            responseUri, LoginRedirectUri, LoginState));
                        break;
                    case LoginType.ClientCredentials:
                        model = Mapping.MapToken(await SpotifyClient.AuthAsync());
                        break;
                    case LoginType.ImplicitGrant:
                        if (responseUri == null)
                            throw new ArgumentNullException(nameof(responseUri));
                        if (LoginRedirectUri == null)
                            throw new ArgumentNullException(nameof(LoginRedirectUri));
                        model = Mapping.MapToken(SpotifyClient.AuthUserImplicit(
                            responseUri, LoginRedirectUri, LoginState));
                        break;
                }
                IsUserLoggedIn = model.IsLoggedIn;
            }
            catch (AuthCodeValueException ex)
            {
                throw new AuthValueException(ex.Message, ex);
            }
            catch (AuthCodeStateException ex)
            {
                throw new AuthStateException(ex.Message, ex);
            }
            catch (AuthTokenValueException ex)
            {
                throw new AuthValueException(ex.Message, ex);
            }
            catch (AuthTokenStateException ex)
            {
                throw new AuthStateException(ex.Message, ex);
            }
            return model;
        }
        #endregion Authentication Methods

        #region Get Methods
        /// <summary>
        /// Get Category
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <returns>Category View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        public async Task<CategoryViewModel> GetCategoryAsync(
            string id)
        {
            CategoryViewModel result = null;
            try
            {
                var response = await SpotifyClient.LookupCategoryAsync(
                    categoryId: id,
                    country: Country,
                    locale: Locale);
                result = Mapping.MapCategory(response);
                result = Mapping.MapError(response, result);
            }
            catch (AuthAccessTokenRequiredException)
            {
                OnTokenRequired(TokenType.Access);
            }
            return result;
        }

        /// <summary>
        /// Get Artist
        /// </summary>
        /// <param name="id">Artist Spotify Id</param>
        /// <returns>Artist View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        public async Task<ArtistViewModel> GetArtistAsync(
            string id)
        {
            ArtistViewModel result = null;
            try
            {
                var response = await SpotifyClient.LookupAsync<Artist>(
                id, LookupType.Artists);
                result = Mapping.MapArtist(response);
                result = Mapping.MapError(response, result);
            }
            catch (AuthAccessTokenRequiredException)
            {
                OnTokenRequired(TokenType.Access);
            }
            return result;
        }

        /// <summary>
        /// Get Album
        /// </summary>
        /// <param name="id">Album Spotify Id</param>
        /// <returns>Album View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        public async Task<AlbumViewModel> GetAlbumAsync(
            string id)
        {
            AlbumViewModel result = null;
            try
            {
                var response = await SpotifyClient.LookupAsync<Album>(
                id, LookupType.Albums);
                result = Mapping.MapAlbum(response);
                result = Mapping.MapError(response, result);
            }
            catch (AuthAccessTokenRequiredException)
            {
                OnTokenRequired(TokenType.Access);
            }
            return result;
        }

        /// <summary>
        /// Get Playlist
        /// </summary>
        /// <param name="id">Playlist Spotify Id</param>
        /// <returns>Playlist View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        public async Task<PlaylistViewModel> GetPlaylistAsync(
            string id)
        {
            PlaylistViewModel result = null;
            try
            {
                var response = await SpotifyClient.LookupAsync<Playlist>(
                id, LookupType.Playlist);
                result = Mapping.MapPlaylist(response);
                result = Mapping.MapError(response, result);
            }
            catch (AuthAccessTokenRequiredException)
            {
                OnTokenRequired(TokenType.Access);
            }
            return result;
        }

        /// <summary>
        /// Get Track
        /// </summary>
        /// <param name="id">Track Spotify Id</param>
        /// <returns>Track View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        public async Task<TrackViewModel> GetTrackAsync(
            string id)
        {
            TrackViewModel result = null;
            try
            {
                var response = await SpotifyClient.LookupAsync<Track>(
                id, LookupType.Tracks);
                result = Mapping.MapTrack(response);
                result = Mapping.MapError(response, result);
            }
            catch (AuthAccessTokenRequiredException)
            {
                OnTokenRequired(TokenType.Access);
            }
            return result;
        }

        /// <summary>
        /// Get Audio Analysis
        /// </summary>
        /// <param name="id">Track Spotify Id</param>
        /// <returns>AudioAnalysis View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        public async Task<AudioAnalysisViewModel> GetAudioAnalysisAsync(
            string id)
        {
            AudioAnalysisViewModel result = null;
            try
            {
                var response = await SpotifyClient.LookupAsync<AudioAnalysis>(
                id, LookupType.AudioAnalysis);
                result = Mapping.MapAudioAnalysis(response);
                result = Mapping.MapError(response, result);
            }
            catch (AuthAccessTokenRequiredException)
            {
                OnTokenRequired(TokenType.Access);
            }
            return result;
        }

        /// <summary>
        /// Get User
        /// </summary>
        /// <param name="id">User Spotify Id</param>
        /// <returns>Public User View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        public async Task<UserViewModel> GetUserAsync(
            string id)
        {
            UserViewModel result = null;
            try
            {
                var response = await SpotifyClient.AuthLookupUserProfileAsync(id);
                result = Mapping.MapUser(response);
                result = Mapping.MapError(response, result);
            }
            catch (AuthUserTokenRequiredException)
            {
                OnTokenRequired(TokenType.User);
            }
            return result;
        }

        /// <summary>
        /// Get Current User
        /// <para>Scopes: UserReadPrivate, UserReadEmail, UserReadBirthDate, UserReadPrivate</para>
        /// </summary>
        /// <returns>Current User View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        public async Task<CurrentUserViewModel> GetCurrentUserAsync()
        {
            CurrentUserViewModel result = null;
            try
            {
                var response = await SpotifyClient.AuthLookupUserProfileAsync();
                result = Mapping.MapCurrentUser(response);
                result = Mapping.MapError(response, result);
            }
            catch (AuthUserTokenRequiredException)
            {
                OnTokenRequired(TokenType.User);
            }
            return result;
        }
        #endregion Get Methods

        #region List Methods
        /// <summary>
        /// List Category
        /// </summary>
        /// <returns>Navigation ViewModel of Category View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        public async Task<NavigationViewModel<CategoryViewModel>> ListCategoriesAsync()
        {
            NavigationViewModel<CategoryViewModel> result = null;
            try
            {
                var page = (Limit != null) ? new Page() { Limit = Limit.Value } : null;
                var response = await SpotifyClient.LookupAllCategoriesAsync(
                    country: Country, locale: Locale, page: page);
                result = Mapping.MapPagingCategory(response?.Categories);
                result = Mapping.MapError(response, result);
            }
            catch (AuthAccessTokenRequiredException)
            {
                OnTokenRequired(TokenType.Access);
            }
            return result;
        }

        /// <summary>
        /// List Categories
        /// </summary>
        /// <param name="navigation">Navigation View Model of Category View Model</param>
        /// <returns>Navigation ViewModel of Category View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        public async Task<NavigationViewModel<CategoryViewModel>> ListCategoriesAsync(
            NavigationViewModel<CategoryViewModel> navigation)
        {
            NavigationViewModel<CategoryViewModel> result = null;
            try
            {
                var paging = Mapping.MapNavigationPaging<Category, CategoryViewModel>(navigation);
                if (paging.Next != null)
                {
                    var response = await SpotifyClient.NavigateAsync(
                    paging, NavigateType.Next);
                    result = Mapping.MapPagingCategory(response?.Categories);
                    result = Mapping.MapError(response, result);
                }
            }
            catch (AuthAccessTokenRequiredException)
            {
                OnTokenRequired(TokenType.Access);
            }
            return result;
        }

        /// <summary>
        /// List Artists
        /// </summary>
        /// <param name="type">Artist Type</param>
        /// <param name="id">Artist Spotify Id</param>
        /// <returns>Navigation ViewModel of Artist View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        public async Task<NavigationViewModel<ArtistViewModel>> ListArtistsAsync(
            ArtistType type,
            string id = null)
        {
            NavigationViewModel<ArtistViewModel> result = null;
            try
            {
                var page = (Limit != null) ? new Page() { Limit = Limit.Value } : null;
                var cursor = (Limit != null) ? new Cursor() { Limit = Limit.Value } : null;
                switch (type)
                {
                    case ArtistType.Favourites:
                        var favourite = await SpotifyClient.LookupAsync(
                            Favourites?.ArtistIds, LookupType.Artists);
                        result = Mapping.MapArtistList(favourite?.Artists);
                        result = Mapping.MapError(favourite, result);
                        break;
                    case ArtistType.Search:
                        var searchType = new SearchType() { Artist = true };
                        var search = await SpotifyClient.SearchAsync(
                        query: id, searchType: searchType,
                        country: Country, page: page);
                        result = Mapping.MapPagingArtist(search?.Artists, type);
                        result = Mapping.MapError(search, result);
                        break;
                    case ArtistType.Related:
                        var related = await SpotifyClient.LookupArtistRelatedArtistsAsync(
                            itemId: id);
                        result = Mapping.MapArtistList(related?.Artists);
                        result = Mapping.MapError(related, result);
                        break;
                    case ArtistType.UserFollowed:
                        var followed = await SpotifyClient.AuthLookupFollowedArtistsAsync(
                            cursor: cursor);
                        result = Mapping.MapCursorArtist(followed, type);
                        result = Mapping.MapError(followed, result);
                        break;
                    case ArtistType.UserTop:
                        var top = await SpotifyClient.AuthLookupUserTopArtistsAsync(
                             cursor: cursor);
                        result = Mapping.MapCursorArtist(top, type);
                        result = Mapping.MapError(top, result);
                        break;
                }
            }
            catch (AuthAccessTokenRequiredException)
            {
                OnTokenRequired(TokenType.Access);
            }
            catch (AuthUserTokenRequiredException)
            {
                OnTokenRequired(TokenType.User);
            }
            return result;
        }

        /// <summary>
        /// List Artists
        /// </summary>
        /// <param name="navigation">Navigation View Model of Artist View Model</param>
        /// <returns>Navigation View Model of Artist View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        public async Task<NavigationViewModel<ArtistViewModel>> ListArtistsAsync(
            NavigationViewModel<ArtistViewModel> navigation)
        {
            NavigationViewModel<ArtistViewModel> result = null;
            try
            {
                if (navigation.Type != null)
                {
                    var type = (ArtistType)navigation.Type;
                    if (type == ArtistType.UserFollowed || type == ArtistType.UserTop)
                    {
                        var cursor = Mapping.MapNavigationCursor<Artist, ArtistViewModel>(navigation);
                        if (cursor.Next != null)
                        {
                            var response = await SpotifyClient.AuthNavigateAsync(
                                cursor, NavigateType.Next);
                            result = Mapping.MapCursorArtist(response, type);
                            result = Mapping.MapError(response, result);
                        }
                    }
                    else
                    {
                        var paging = Mapping.MapNavigationPaging<Artist, ArtistViewModel>(navigation);
                        if (paging.Next != null)
                        {
                            var response = await SpotifyClient.NavigateAsync(
                            paging, NavigateType.Next);
                            result = Mapping.MapPagingArtist(response?.Artists, type);
                            result = Mapping.MapError(response, result);
                        }
                    }
                }
            }
            catch (AuthAccessTokenRequiredException)
            {
                OnTokenRequired(TokenType.Access);
            }
            catch (AuthUserTokenRequiredException)
            {
                OnTokenRequired(TokenType.User);
            }
            return result;
        }

        /// <summary>
        /// List Albums
        /// </summary>
        /// <param name="type">Album Type</param>
        /// <param name="id">Album Spotify Id</param>
        /// <returns>Navigation View Model of Album View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        public async Task<NavigationViewModel<AlbumViewModel>>
            ListAlbumsAsync(
            AlbumType type,
            string id = null)
        {
            NavigationViewModel<AlbumViewModel> result = null;
            try
            {
                var page = (Limit != null) ? new Page() { Limit = Limit.Value } : null;
                var cursor = (Limit != null) ? new Cursor() { Limit = Limit.Value } : null;
                switch (type)
                {
                    case AlbumType.Favourites:
                        var favourite = await SpotifyClient.LookupAsync(
                            Favourites?.AlbumIds, LookupType.Albums);
                        result = Mapping.MapAlbumList(favourite?.Albums, type);
                        result = Mapping.MapError(favourite, result);
                        break;
                    case AlbumType.Search:
                        var search = new SearchType() { Album = true };
                        var searchAlbum = await SpotifyClient.SearchAsync(
                        query: id, searchType: search,
                        country: Country, page: page);
                        result = Mapping.MapPagingAlbum(searchAlbum?.Albums, type);
                        result = Mapping.MapError(searchAlbum, result);
                        break;
                    case AlbumType.NewReleases:
                        var releases = await SpotifyClient.LookupNewReleasesAsync(
                        country: Country, page: page);
                        result = Mapping.MapPagingAlbum(releases?.Albums, type);
                        result = Mapping.MapError(releases, result);
                        break;
                    case AlbumType.Artist:
                        var albums = await SpotifyClient.LookupAsync<Paging<Album>>(
                        id, lookupType: LookupType.ArtistAlbums,
                        market: Country, page: page);
                        result = Mapping.MapPagingAlbum(albums, type);
                        result = Mapping.MapError(albums, result);
                        break;
                    case AlbumType.UserSaved:
                        var saved = await SpotifyClient.AuthLookupUserSavedAlbumsAsync(
                            market: Country, cursor: cursor);
                        result = Mapping.MapCursorAlbum(saved, type);
                        result = Mapping.MapError(saved, result);
                        break;
                }
            }
            catch (AuthAccessTokenRequiredException)
            {
                OnTokenRequired(TokenType.Access);
            }
            catch (AuthUserTokenRequiredException)
            {
                OnTokenRequired(TokenType.User);
            }
            return result;
        }

        /// <summary>
        /// List Albums
        /// </summary>
        /// <param name="navigation">Navigation View Model of Album View Model</param>
        /// <returns>Navigation View Model of Album View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        public async Task<NavigationViewModel<AlbumViewModel>> ListAlbumsAsync(
            NavigationViewModel<AlbumViewModel> navigation)
        {
            NavigationViewModel<AlbumViewModel> result = null;
            try
            {
                if (navigation.Type != null)
                {
                    var type = (AlbumType)navigation.Type;
                    if (type == AlbumType.UserSaved)
                    {
                        var cursor = Mapping.MapNavigationCursor<SavedAlbum, AlbumViewModel>(navigation);
                        if (cursor.Next != null)
                        {
                            var response = await SpotifyClient.AuthNavigateAsync(
                                cursor, NavigateType.Next);
                            result = Mapping.MapCursorAlbum(response, type);
                            result = Mapping.MapError(response, result);
                        }
                    }
                    else
                    {
                        var paging = Mapping.MapNavigationPaging<Album, AlbumViewModel>(navigation);
                        if (paging.Next != null)
                        {
                            var response = await SpotifyClient.NavigateAsync(
                            paging, NavigateType.Next);
                            result = Mapping.MapPagingAlbum(response?.Albums, type);
                            result = Mapping.MapError(response, result);
                        }
                    }
                }
            }
            catch (AuthAccessTokenRequiredException)
            {
                OnTokenRequired(TokenType.Access);
            }
            catch (AuthUserTokenRequiredException)
            {
                OnTokenRequired(TokenType.User);
            }
            return result;
        }

        /// <summary>
        /// List Playlists
        /// </summary>
        /// <param name="type">Playlist Type</param>
        /// <param name="id">Playlist Spotify Id</param>
        /// <returns>Navigation View Model of Playlist View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        public async Task<NavigationViewModel<PlaylistViewModel>>
            ListPlaylistsAsync(
            PlaylistType type,
            string id = null)
        {
            NavigationViewModel<PlaylistViewModel> result = null;
            try
            {
                ContentResponse response = null;
                var page = (Limit != null) ? new Page() { Limit = Limit.Value } : null;
                var cursor = (Limit != null) ? new Cursor() { Limit = Limit.Value } : null;
                switch (type)
                {
                    case PlaylistType.Search:
                        var search = new SearchType() { Playlist = true };
                        response = await SpotifyClient.SearchAsync(
                        query: id, searchType: search,
                        country: Country, page: page);
                        result = Mapping.MapPagingPlaylist(response?.Playlists, type);
                        result = Mapping.MapError(response, result);
                        break;
                    case PlaylistType.Featured:
                        response = await SpotifyClient.LookupFeaturedPlaylistsAsync(
                        country: Country, locale: Locale, page: page);
                        result = Mapping.MapPagingPlaylist(response?.Playlists, type);
                        result = Mapping.MapError(response, result);
                        break;
                    case PlaylistType.CategoriesPlaylists:
                        response = await SpotifyClient.LookupAsync<ContentResponse>(
                            itemId: id, lookupType: LookupType.CategoriesPlaylists,
                            market: Country, page: page);
                        result = Mapping.MapPagingPlaylist(response?.Playlists, type);
                        result = Mapping.MapError(response, result);
                        break;
                    case PlaylistType.User:
                        var user = await SpotifyClient.AuthLookupUserPlaylistsAsync(
                            userId: id, cursor: cursor);
                        result = Mapping.MapCursorPlaylist(user, type);
                        result = Mapping.MapError(user, result);
                        break;
                }
            }
            catch (AuthAccessTokenRequiredException)
            {
                OnTokenRequired(TokenType.Access);
            }
            catch (AuthUserTokenRequiredException)
            {
                OnTokenRequired(TokenType.User);
            }
            return result;
        }

        /// <summary>
        /// List Playlists
        /// </summary>
        /// <param name="navigation">Navigation View Model of Playlist View Model</param>
        /// <returns>Navigation View Model of Playlist View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        public async Task<NavigationViewModel<PlaylistViewModel>> ListPlaylistsAsync(
            NavigationViewModel<PlaylistViewModel> navigation)
        {
            NavigationViewModel<PlaylistViewModel> result = null;
            try
            {
                if (navigation.Type != null)
                {
                    var type = (PlaylistType)navigation.Type;
                    if (type == PlaylistType.User)
                    {
                        var cursor = Mapping.MapNavigationCursor<Playlist, PlaylistViewModel>(navigation);
                        if (cursor.Next != null)
                        {
                            var response = await SpotifyClient.AuthNavigateAsync(
                                cursor, NavigateType.Next);
                            result = Mapping.MapCursorPlaylist(response, type);
                            result = Mapping.MapError(response, result);
                        }
                    }
                    else
                    {
                        var paging = Mapping.MapNavigationPaging<Playlist, PlaylistViewModel>(navigation);
                        if (paging.Next != null)
                        {
                            var response = await SpotifyClient.NavigateAsync(
                            paging, NavigateType.Next);
                            result = Mapping.MapPagingPlaylist(response?.Playlists, type);
                            result = Mapping.MapError(response, result);
                        }
                    }
                }
            }
            catch (AuthAccessTokenRequiredException)
            {
                OnTokenRequired(TokenType.Access);
            }
            catch (AuthUserTokenRequiredException)
            {
                OnTokenRequired(TokenType.User);
            }
            return result;
        }

        /// <summary>
        /// List Recommendation Genres
        /// </summary>
        /// <returns>List of Recommendation View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        public async Task<List<RecommendationViewModel>> ListRecommendationGenresAsync()
        {
            List<RecommendationViewModel> result = null;
            try
            {
                var response = await SpotifyClient.LookupRecommendationGenres();
                result = response.Genres.ConvertAll(Mapping.MapRecommendationGenre);
            }
            catch (AuthAccessTokenRequiredException)
            {
                OnTokenRequired(TokenType.Access);
            }
            return result;
        }

        /// <summary>
        /// List Tracks
        /// </summary>
        /// <param name="type">Track Type</param>
        /// <param name="id">Track Spotify Id</param>
        /// <returns>Navigation View Model of Track View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        public async Task<NavigationViewModel<TrackViewModel>> ListTracksAsync(
            TrackType type,
            string id = null)
        {
            NavigationViewModel<TrackViewModel> result = null;
            try
            {
                var page = (Limit != null) ? new Page() { Limit = Limit.Value } : null;
                var cursor = (Limit != null) ? new Cursor() { Limit = Limit.Value } : null;
                switch (type)
                {
                    case TrackType.Favourites:
                        var favourite = await SpotifyClient.LookupAsync(
                            Favourites?.TrackIds, LookupType.Tracks);
                        result = Mapping.MapTrackList(favourite?.Tracks, type);
                        result = Mapping.MapError(favourite, result);
                        break;
                    case TrackType.Search:
                        var searchType = new SearchType() { Track = true };
                        var searchTracks = await SpotifyClient.SearchAsync(
                            query: id, searchType: searchType, Country, page: page);
                        result = Mapping.MapPagingTrack(searchTracks?.Tracks, type);
                        result = Mapping.MapError(searchTracks, result);
                        break;
                    case TrackType.Recommended:
                        var recommendedTracks = await SpotifyClient.LookupRecommendationsAsync(
                            seedGenres: new List<string> { id },
                            market: Country);
                        result = Mapping.MapTrackList(recommendedTracks?.Tracks, type);
                        result = Mapping.MapError(recommendedTracks, result);
                        break;
                    case TrackType.Playlist:
                        var playlistTracks = await SpotifyClient.LookupAsync<Paging<PlaylistTrack>>(
                            itemId: id, lookupType: LookupType.PlaylistTracks,
                            market: Country, page: page);
                        result = Mapping.MapPagingTrack(playlistTracks, type);
                        result = Mapping.MapError(playlistTracks, result);
                        break;
                    case TrackType.Album:
                        var albumTracks = await SpotifyClient.LookupAsync<Paging<Track>>(
                        itemId: id, lookupType: LookupType.AlbumTracks,
                        market: Country, page: page);
                        result = Mapping.MapPagingTrack(albumTracks, type);
                        result = Mapping.MapError(albumTracks, result);
                        break;
                    case TrackType.Artist:
                        var artistTracks = await SpotifyClient.LookupArtistTopTracksAsync(
                            itemId: id, market: Country);
                        result = Mapping.MapTrackList(artistTracks?.Tracks, type);
                        result = Mapping.MapError(artistTracks, result);
                        break;
                    case TrackType.UserRecentlyPlayed:
                        var played = await SpotifyClient.AuthLookupUserRecentlyPlayedTracksAsync(
                            cursor: cursor);
                        result = Mapping.MapCursorTrack(played, type);
                        result = Mapping.MapError(played, result);
                        break;
                    case TrackType.UserSaved:
                        var saved = await SpotifyClient.AuthLookupUserSavedTracksAsync(
                            market: Country, cursor: cursor);
                        result = Mapping.MapCursorTrack(saved, type);
                        result = Mapping.MapError(saved, result);
                        break;
                    case TrackType.UserTop:
                        var top = await SpotifyClient.AuthLookupUserTopTracksAsync(
                             cursor: cursor);
                        result = Mapping.MapCursorTrack(top, type);
                        result = Mapping.MapError(top, result);
                        break;
                }
            }
            catch (AuthAccessTokenRequiredException)
            {
                OnTokenRequired(TokenType.Access);
            }
            catch (AuthUserTokenRequiredException)
            {
                OnTokenRequired(TokenType.User);
            }
            return result;
        }

        /// <summary>
        /// List Tracks
        /// </summary>
        /// <param name="navigation">Navigation View Model of Track View Model</param>
        /// <returns>Navigation View Model of Track View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        public async Task<NavigationViewModel<TrackViewModel>> ListTracksAsync(
            NavigationViewModel<TrackViewModel> navigation)
        {
            NavigationViewModel<TrackViewModel> result = null;
            try
            {
                if (navigation.Type != null)
                {
                    var type = (TrackType)navigation.Type;
                    if (type == TrackType.UserRecentlyPlayed)
                    {
                        var cursor = Mapping.MapNavigationCursor<PlayHistory, TrackViewModel>(navigation);
                        if (cursor.Next != null)
                        {
                            var response = await SpotifyClient.AuthNavigateAsync(
                                cursor, NavigateType.Next);
                            result = Mapping.MapCursorTrack(response, type);
                            result = Mapping.MapError(response, result);
                        }
                    }
                    else if (type == TrackType.UserSaved)
                    {
                        var cursor = Mapping.MapNavigationCursor<SavedTrack, TrackViewModel>(navigation);
                        if (cursor.Next != null)
                        {
                            var response = await SpotifyClient.AuthNavigateAsync(
                                cursor, NavigateType.Next);
                            result = Mapping.MapCursorTrack(response, type);
                            result = Mapping.MapError(response, result);
                        }
                    }
                    else if (type == TrackType.UserTop)
                    {
                        var cursor = Mapping.MapNavigationCursor<Track, TrackViewModel>(navigation);
                        if (cursor.Next != null)
                        {
                            var response = await SpotifyClient.AuthNavigateAsync(
                                cursor, NavigateType.Next);
                            result = Mapping.MapCursorTrack(response, type);
                            result = Mapping.MapError(response, result);
                        }
                    }
                    else
                    {
                        var paging = Mapping.MapNavigationPaging<Track, TrackViewModel>(navigation);
                        if (paging.Next != null)
                        {
                            var response = await SpotifyClient.NavigateAsync(
                                paging, NavigateType.Next);
                            result = Mapping.MapPagingTrack(response?.Tracks, type);
                            result = Mapping.MapError(response, result);
                        }
                    }
                }
            }
            catch (AuthAccessTokenRequiredException)
            {
                OnTokenRequired(TokenType.Access);
            }
            catch (AuthUserTokenRequiredException)
            {
                OnTokenRequired(TokenType.User);
            }
            return result;
        }

        /// <summary>
        /// List Audio Features
        /// </summary>
        /// <param name="id">Track Spotify Id</param>
        /// <returns>List of Audio Feature View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        public async Task<List<AudioFeatureViewModel>> ListAudioFeatureAsync(
            string id)
        {
            List<AudioFeatureViewModel> results = null;
            try
            {
                var response = await SpotifyClient.LookupAsync<AudioFeatures>(
                    id, LookupType.AudioFeatures);
                results = Mapping.MapAudioFeature(response);
            }
            catch (AuthAccessTokenRequiredException)
            {
               OnTokenRequired(TokenType.Access);
            }
            return results;
        }

        /// <summary>
        /// List Audio Features
        /// </summary>
        /// <param name="ids">List of Track Spotify Id</param>
        /// <returns>List of List of Audio Feature View Model</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        public async Task<List<List<AudioFeatureViewModel>>> ListAudioFeaturesAsync(
            List<string> ids)
        {
            List<List<AudioFeatureViewModel>> results = null;
            try
            {
                var response = await SpotifyClient.LookupAsync(
                    ids, LookupType.AudioFeatures);
                results = Mapping.MapAudioFeature(response.AudioFeatures);
            }
            catch (AuthAccessTokenRequiredException)
            {
                OnTokenRequired(TokenType.Access);
            }
            return results;
        }
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
        public async Task<List<bool>> IsFollowing(
            List<string> ids,
            FollowType type,
            string playlistId = null)
        {
            try
            {
                switch (type)
                {
                    case FollowType.Artist:
                        if(ids == null) throw new ArgumentNullException(nameof(ids));
                        return await SpotifyClient.AuthLookupFollowingStateAsync(
                            ids, NetStandard.Enums.FollowType.Artist);
                    case FollowType.User:
                        if (ids == null) throw new ArgumentNullException(nameof(ids));
                        return await SpotifyClient.AuthLookupFollowingStateAsync(
                        ids, NetStandard.Enums.FollowType.User);
                    case FollowType.Playlist:
                        if (playlistId == null) throw new ArgumentNullException(nameof(playlistId));
                        return await SpotifyClient.AuthLookupUserFollowingPlaylistAsync(ids,
                            playlistId);
                }
            }
            catch (AuthUserTokenRequiredException)
            {
                OnTokenRequired(TokenType.User);
            }
            return null;
        }

        /// <summary>
        /// Is Following
        /// <para>Scopes: FollowRead, PlaylistReadPrivate</para>
        /// </summary>
        /// <param name="id">(Required) Artist, User or Playlist Spotify ID to check</param>
        /// <param name="type">(Required) Either Artist, User or Playlist</param>
        /// <returns>True if Is, False if Not</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<bool> IsFollowing(
            string id,
            FollowType type) => 
            (await IsFollowing(new List<string> { id }, type, id)).FirstOrDefault();

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
        public async Task<bool> Follow(
            List<string> ids,
            FollowType type,
            string playlistId = null)
        {
            try
            {
                switch (type)
                {
                    case FollowType.Artist:
                        if (ids == null) throw new ArgumentNullException(nameof(ids));
                        return (await SpotifyClient.AuthFollowAsync(
                            ids, NetStandard.Enums.FollowType.Artist)).Success;
                    case FollowType.User:
                        if (ids == null) throw new ArgumentNullException(nameof(ids));
                        return (await SpotifyClient.AuthFollowAsync(
                            ids, NetStandard.Enums.FollowType.User)).Success;
                    case FollowType.Playlist:
                        if (playlistId == null) throw new ArgumentNullException(nameof(playlistId));
                        return (await SpotifyClient.AuthFollowPlaylistAsync(playlistId)).Success;
                }
            }
            catch (AuthUserTokenRequiredException)
            {
                OnTokenRequired(TokenType.User);
            }
            return false;
        }

        /// <summary>
        /// Follow
        /// <para>Scopes: FollowModify</para>
        /// </summary>
        /// <param name="id">(Required) Artist, User or Playlist Spotify ID to Follow</param>
        /// <param name="type">(Required) Either Artist, User or Playlist</param>
        /// <returns>True if Is, False if Not</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<bool> Follow(
            string id,
            FollowType type) =>
            (await Follow(new List<string> { id }, type, id));

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
        public async Task<bool> Unfollow(
            List<string> ids,
            FollowType type,
            string playlistId = null)
        {
            try
            {
                switch (type)
                {
                    case FollowType.Artist:
                        if (ids == null) throw new ArgumentNullException(nameof(ids));
                        return (await SpotifyClient.AuthUnfollowAsync(
                            ids, NetStandard.Enums.FollowType.Artist)).Success;
                    case FollowType.User:
                        if (ids == null) throw new ArgumentNullException(nameof(ids));
                        return (await SpotifyClient.AuthUnfollowAsync(
                            ids, NetStandard.Enums.FollowType.User)).Success;
                    case FollowType.Playlist:
                        if (playlistId == null) throw new ArgumentNullException(nameof(playlistId));
                        return (await SpotifyClient.AuthUnfollowPlaylistAsync(playlistId)).Success;
                }
            }
            catch (AuthUserTokenRequiredException)
            {
                OnTokenRequired(TokenType.User);
            }
            return false;
        }

        /// <summary>
        /// Unfollow
        /// <para>Scopes: FollowModify, PlaylistModifyPublic, PlaylistModifyPrivate</para>
        /// </summary>
        /// <param name="id">(Required) Artist or the User Spotify ID to Unfollow</param>
        /// <param name="type">(Required) Either Artist, User or Playlist</param>
        /// <returns>True if Is, False if Not</returns>
        /// <exception cref="TokenRequiredException">Token Required and TokenRequiredEvent not Subscribed to</exception>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<bool> Unfollow(
            string id,
            FollowType type) =>
            (await Unfollow(new List<string> { id }, type, id));
        #endregion Follow Methods
    }
}