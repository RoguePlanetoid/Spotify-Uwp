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
    internal class SpotifySdkClient : ISpotifySdkClient
    {
        #region Constructor
        /// <summary>
        /// Spotify SDK Client
        /// </summary>
        /// <param name="clientId">Spotify Client Id</param>
        /// <param name="clientSecret">Spotify Client Secret</param>
        /// <param name="cultureInfo">Culture Info</param>
        public SpotifySdkClient(
            string clientId,
            string clientSecret) =>
            SpotifyClient = SpotifyClientFactory.CreateSpotifyClient(
                clientId, clientSecret);
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
        /// Token View Model
        /// </summary>
        public TokenViewModel Token
        {
            get => Mapping.MapToken(SpotifyClient.GetToken());
            set => SpotifyClient.SetToken(Mapping.MapToken(value));
        }

        /// <summary>
        /// List Favourite ViewModel 
        /// </summary>
        public ListFavouriteViewModel Favourites { get; set; } = new ListFavouriteViewModel();
        #endregion Public Properties

        #region Authentication Methods
        /// <summary>
        /// Get Authorisation Code Flow Uri
        /// </summary>
        /// <param name="redirectUri">Redirect Uri</param>
        /// <param name="state">State for Request / Response Validation</param>
        /// <param name="scope">Authorisation Scopes</param>
        /// <param name="showDialog">(Optional) Whether or not to force the user to approve the app again if they’ve already done so.</param>
        /// <returns>Uri</returns>
        public Uri GetAuthorisationCodeFlowUri(
            Uri redirectUri,
            string state,
            ScopeViewModel scope,
            bool showDialog = false) =>
            SpotifyClient.AuthUser(redirectUri, state, Mapping.MapScope(scope), showDialog);

        /// <summary>
        /// Get Authorisation Code Flow Token
        /// </summary>
        /// <param name="responseUri">Response Uri</param>
        /// <param name="redirectUri">Redirect Uri</param>
        /// <param name="state">State for Request Validation</param>
        /// <returns>AccessToken on Success, Null if Not</returns>
        /// <exception cref="AuthCodeValueException">AuthCodeValueException</exception>
        /// <exception cref="AuthCodeStateException">AuthCodeStateException</exception>
        public async Task<TokenViewModel> GetAuthorisationCodeFlowTokenAsync(
            Uri responseUri,
            Uri redirectUri,
            string state) =>
                Mapping.MapToken(await SpotifyClient.AuthUserAsync(
                responseUri, redirectUri, state));

        /// <summary>
        /// Get Client Credentials Flow Token
        /// </summary>
        /// <returns>AccessToken on Success, Null if Not</returns>
        public async Task<TokenViewModel> GetClientCredentialsFlowTokenAsync() =>
            Mapping.MapToken(await SpotifyClient.AuthAsync());

        /// <summary>
        /// Get Implicit Grant Flow Uri
        /// </summary>
        /// <param name="redirectUri">Redirect Uri</param>
        /// <param name="state">State for Request / Response Validation</param>
        /// <param name="scope">Authorisation Scopes</param>
        /// <param name="showDialog">(Optional) Whether or not to force the user to approve the app again if they’ve already done so.</param>
        /// <returns>Uri</returns>
        public Uri GetImplicitGrantFlowUri(
            Uri redirectUri,
            string state,
            ScopeViewModel scope,
            bool showDialog = false) =>
            SpotifyClient.AuthUserImplicit(redirectUri, state, Mapping.MapScope(scope), showDialog);

        /// <summary>
        /// Get Implicit Grant Flow Token
        /// </summary>
        /// <param name="responseUri">Response Uri</param>
        /// <param name="redirectUri">Redirect Uri</param>
        /// <param name="state">State for Request / Response Validation</param>
        /// <returns>AccessToken on Success, Null if Not</returns>
        /// <exception cref="AuthTokenValueException">AuthCodeValueException</exception>
        /// <exception cref="AuthTokenStateException">AuthCodeStateException</exception>
        public TokenViewModel GetImplicitGrantFlowToken(
            Uri responseUri,
            Uri redirectUri,
            string state) =>
            Mapping.MapToken(SpotifyClient.AuthUserImplicit(responseUri, redirectUri, state));
        #endregion Authentication Methods

        #region Get Methods
        /// <summary>
        /// Get Category
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <param name="page">Page</param>
        /// <returns>Category ViewModel</returns>
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
                throw new TokenRequiredException(TokenType.Access);
            }
            return result;
        }

        /// <summary>
        /// Get Artist
        /// </summary>
        /// <param name="id">Artist Spotify Id</param>
        /// <returns>Artist ViewModel</returns>
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
                throw new TokenRequiredException(TokenType.Access);
            }
            return result;
        }

        /// <summary>
        /// Get Album
        /// </summary>
        /// <param name="id">Album Spotify Id</param>
        /// <returns>Album View Model</returns>
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
                throw new TokenRequiredException(TokenType.Access);
            }
            return result;
        }

        /// <summary>
        /// Get Playlist
        /// </summary>
        /// <param name="id">Playlist Spotify Id</param>
        /// <returns>Playlist ViewModel</returns>
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
                throw new TokenRequiredException(TokenType.Access);
            }
            return result;
        }

        /// <summary>
        /// Get Track
        /// </summary>
        /// <param name="id">Track Spotify Id</param>
        /// <returns>Track ViewModel</returns>
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
                throw new TokenRequiredException(TokenType.Access);
            }
            return result;
        }

        /// <summary>
        /// Get Audio Analysis
        /// </summary>
        /// <param name="id">Track Spotify Id</param>
        /// <returns>AudioAnalysis ViewModel</returns>
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
                throw new TokenRequiredException(TokenType.Access);
            }
            return result;
        }
        #endregion Get Methods

        #region List Methods
        /// <summary>
        /// List Category
        /// </summary>
        /// <returns>Navigation ViewModel of Category ViewModel</returns>
        public async Task<NavigationViewModel<CategoryViewModel>>
            ListCategoriesAsync()
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
                throw new TokenRequiredException(TokenType.Access);
            }
            return result;
        }

        /// <summary>
        /// List Categories
        /// </summary>
        /// <param name="navigation">Navigation ViewModel of Category ViewModel</param>
        /// <returns>Navigation ViewModel of Category ViewModel</returns>
        public async Task<NavigationViewModel<CategoryViewModel>>
            ListCategoriesAsync(NavigationViewModel<CategoryViewModel> navigation)
        {
            NavigationViewModel<CategoryViewModel> result = null;
            try
            {
                var paging = Mapping.MapNavigationPaging<Category, CategoryViewModel>(navigation);
                var response = await SpotifyClient.NavigateAsync(
                    paging, NavigateType.Next);
                result = Mapping.MapPagingCategory(response?.Categories);
                result = Mapping.MapError(response, result);
            }
            catch (AuthAccessTokenRequiredException)
            {
                throw new TokenRequiredException(TokenType.Access);
            }
            return result;
        }

        /// <summary>
        /// List Artists
        /// </summary>
        /// <param name="type">Artist Type</param>
        /// <param name="id">Artist Spotify Id</param>
        /// <returns>Navigation ViewModel of Artist ViewModel</returns>
        public async Task<NavigationViewModel<ArtistViewModel>>
            ListArtistsAsync(
            ArtistType type,
            string id = null)
        {
            NavigationViewModel<ArtistViewModel> result = null;
            try
            {
                var page = (Limit != null) ? new Page() { Limit = Limit.Value } : null;
                switch (type)
                {
                    case ArtistType.Favourites:
                        var favourite = await SpotifyClient.LookupAsync(
                            Favourites?.ArtistIds, LookupType.Artists);
                        result = Mapping.MapArtistList(favourite?.Artists);
                        result = Mapping.MapError(favourite, result);
                        break;
                    case ArtistType.Search:
                        var search = new SearchType() { Artist = true };
                        var searchArtist = await SpotifyClient.SearchAsync(
                        query: id, searchType: search,
                        country: Country, page: page);
                        result = Mapping.MapPagingArtist(searchArtist?.Artists);
                        result = Mapping.MapError(searchArtist, result);
                        break;
                    case ArtistType.Related:
                        var related = await SpotifyClient.LookupArtistRelatedArtistsAsync(
                            itemId: id);
                        result = Mapping.MapArtistList(related?.Artists);
                        result = Mapping.MapError(related, result);
                        break;
                }
            }
            catch (AuthAccessTokenRequiredException)
            {
                throw new TokenRequiredException(TokenType.Access);
            }
            return result;
        }

        /// <summary>
        /// List Artists
        /// </summary>
        /// <param name="navigation">Navigation ViewModel of Artist ViewModel</param>
        /// <returns>Navigation ViewModel of Artist ViewModel</returns>
        public async Task<NavigationViewModel<ArtistViewModel>> ListArtistsAsync(
            NavigationViewModel<ArtistViewModel> navigation)
        {
            NavigationViewModel<ArtistViewModel> result = null;
            try
            {
                var paging = Mapping.MapNavigationPaging<Artist, ArtistViewModel>(navigation);
                var response = await SpotifyClient.NavigateAsync(
                    paging, NavigateType.Next);
                result = Mapping.MapPagingArtist(response?.Artists);
                result = Mapping.MapError(response, result);
            }
            catch (AuthAccessTokenRequiredException)
            {
                throw new TokenRequiredException(TokenType.Access);
            }
            return result;
        }

        /// <summary>
        /// List Albums
        /// </summary>
        /// <param name="type">Album Type</param>
        /// <param name="id">Album Spotify Id</param>
        /// <returns>Navigation ViewModel of Album ViewModel</returns>
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
                        result = Mapping.MapAlbumList(favourite?.Albums);
                        result = Mapping.MapError(favourite, result);
                        break;
                    case AlbumType.Search:
                        var search = new SearchType() { Album = true };
                        var searchAlbum = await SpotifyClient.SearchAsync(
                        query: id, searchType: search,
                        country: Country, page: page);
                        result = Mapping.MapPagingAlbum(searchAlbum?.Albums);
                        result = Mapping.MapError(searchAlbum, result);
                        break;
                    case AlbumType.NewReleases:
                        var releases = await SpotifyClient.LookupNewReleasesAsync(
                        country: Country, page: page);
                        result = Mapping.MapPagingAlbum(releases?.Albums);
                        result = Mapping.MapError(releases, result);
                        break;
                    case AlbumType.Artist:
                        var albums = await SpotifyClient.LookupAsync<Paging<Album>>(
                        id, lookupType: LookupType.ArtistAlbums,
                        market: Country, page: page);
                        result = Mapping.MapPagingAlbum(albums);
                        result = Mapping.MapError(albums, result);
                        break;
                    case AlbumType.Saved:
                        var saved = await SpotifyClient.AuthLookupUserSavedAlbumsAsync(
                            market: Country, cursor: cursor);
                        result = Mapping.MapCursorAlbum(saved);
                        result = Mapping.MapError(saved, result);
                        break;
                }
            }
            catch (AuthAccessTokenRequiredException)
            {
                throw new TokenRequiredException(TokenType.Access);
            }
            catch (AuthUserTokenRequiredException)
            {
                throw new TokenRequiredException(TokenType.User);
            }
            return result;
        }

        /// <summary>
        /// List Albums
        /// </summary>
        /// <param name="navigation">Navigation ViewModel of Album ViewModel</param>
        /// <returns>Navigation ViewModel of Album ViewModel</returns>
        public async Task<NavigationViewModel<AlbumViewModel>> ListAlbumsAsync(
            NavigationViewModel<AlbumViewModel> navigation)
        {
            NavigationViewModel<AlbumViewModel> result = null;
            try
            {

                var paging = Mapping.MapNavigationPaging<Album, AlbumViewModel>(navigation);
                var response = await SpotifyClient.NavigateAsync(
                    paging, NavigateType.Next);
                result = Mapping.MapPagingAlbum(response?.Albums);
                result = Mapping.MapError(response, result);
            }
            catch (AuthAccessTokenRequiredException)
            {
                throw new TokenRequiredException(TokenType.Access);
            }
            return result;
        }

        /// <summary>
        /// List Playlists
        /// </summary>
        /// <param name="type">Playlist Type</param>
        /// <param name="id">Playlist Spotify Id</param>
        /// <returns>Navigation ViewModel of Playlist ViewModel</returns>
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
                switch (type)
                {
                    case PlaylistType.Search:
                        var search = new SearchType() { Playlist = true };
                        response = await SpotifyClient.SearchAsync(
                        query: id, searchType: search,
                        country: Country, page: page);
                        break;
                    case PlaylistType.Featured:
                        response = await SpotifyClient.LookupFeaturedPlaylistsAsync(
                        country: Country, locale: Locale, page: page);
                        break;
                    case PlaylistType.CategoriesPlaylists:
                        response = await SpotifyClient.LookupAsync<ContentResponse>(
                            itemId: id, lookupType: LookupType.CategoriesPlaylists,
                            market: Country, page: page);
                        break;
                }
                result = Mapping.MapPagingPlaylist(response?.Playlists);
                result = Mapping.MapError(response, result);
            }
            catch (AuthAccessTokenRequiredException)
            {
                throw new TokenRequiredException(TokenType.Access);
            }
            return result;
        }

        /// <summary>
        /// List Playlists
        /// </summary>
        /// <param name="navigation">Navigation ViewModel of Playlist ViewModel</param>
        /// <returns>Navigation ViewModel of Playlist ViewModel</returns>
        public async Task<NavigationViewModel<PlaylistViewModel>> ListPlaylistsAsync(
            NavigationViewModel<PlaylistViewModel> navigation)
        {
            NavigationViewModel<PlaylistViewModel> result = null;
            try
            {
                var paging = Mapping.MapNavigationPaging<Playlist, PlaylistViewModel>(navigation);
                var response = await SpotifyClient.NavigateAsync(
                    paging, NavigateType.Next);
                result = Mapping.MapPagingPlaylist(response?.Playlists);
                result = Mapping.MapError(response, result);
            }
            catch (AuthAccessTokenRequiredException)
            {
                throw new TokenRequiredException(TokenType.Access);
            }
            return result;
        }

        /// <summary>
        /// List Recommendation Genres
        /// </summary>
        /// <returns>List of Recommendation ViewModel</returns>
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
                throw new TokenRequiredException(TokenType.Access);
            }
            return result;
        }

        /// <summary>
        /// List Tracks
        /// </summary>
        /// <param name="type">Track Type</param>
        /// <param name="id">Track Spotify Id</param>
        /// <returns>Navigation ViewModel of Track ViewModel</returns>
        public async Task<NavigationViewModel<TrackViewModel>>
            ListTracksAsync(
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
                    case TrackType.Saved:
                        var saved = await SpotifyClient.AuthLookupUserSavedTracksAsync(
                            market: Country, cursor: cursor);
                        result = Mapping.MapCursorTrack(saved, type);
                        result = Mapping.MapError(saved, result);
                        break;
                }
            }
            catch (AuthUserTokenRequiredException)
            {
                throw new TokenRequiredException(TokenType.User);
            }
            catch (AuthAccessTokenRequiredException)
            {
                throw new TokenRequiredException(TokenType.Access);
            }
            return result;
        }

        /// <summary>
        /// List Tracks
        /// </summary>
        /// <param name="navigation">Navigation ViewModel of Track ViewModel</param>
        /// <returns>Navigation ViewModel of Track ViewModel</returns>
        public async Task<NavigationViewModel<TrackViewModel>> ListTracksAsync(
            NavigationViewModel<TrackViewModel> navigation)
        {
            NavigationViewModel<TrackViewModel> result = null;
            try
            {
                var type = (TrackType)navigation.Type;
                if (type == TrackType.Saved)
                {
                    var cursor = Mapping.MapNavigationCursor<SavedTrack, TrackViewModel>(navigation);
                    var response = await SpotifyClient.AuthNavigateAsync(
                        cursor, NavigateType.Next);
                    result = Mapping.MapCursorTrack(response, type);
                    result = Mapping.MapError(response, result);
                }
                else
                {
                    var paging = Mapping.MapNavigationPaging<Track, TrackViewModel>(navigation);
                    var response = await SpotifyClient.NavigateAsync(
                        paging, NavigateType.Next);
                    result = Mapping.MapPagingTrack(response?.Tracks, type);
                    result = Mapping.MapError(response, result);
                }
            }
            catch (AuthAccessTokenRequiredException)
            {
                throw new TokenRequiredException(TokenType.Access);
            }
            return result;
        }

        /// <summary>
        /// List Audio Features
        /// </summary>
        /// <param name="id">Track Spotify Id</param>
        /// <returns>List of AudioFeatureViewModel</returns>
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
                throw new TokenRequiredException(TokenType.Access);
            }
            return results;
        }

        /// <summary>
        /// List Audio Features
        /// </summary>
        /// <param name="ids">List of Track Spotify Id</param>
        /// <returns>List of List of AudioFeature ViewModel</returns>
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
                throw new TokenRequiredException(TokenType.Access);
            }
            return results;
        }
        #endregion List Methods   

        #region Follow Methods
        /// <summary>
        /// Is Following Artists or Users and Check if Users Follow a Playlist
        /// <para>Scopes: FollowRead, PlaylistReadPrivate</para>
        /// </summary>
        /// <param name="itemIds">(Required) List of the Artist or the User Spotify IDs to check</param>
        /// <param name="followType">(Required) Either Artist or User</param>
        /// <param name="playlistId">(Required for FollowType.Playlist) The Spotify ID of the playlist</param>
        /// <returns>List of True or False values</returns>
        /// <exception cref="TokenRequiredException"></exception>
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
                        return await SpotifyClient.AuthLookupFollowingStateAsync(
                            ids, NetStandard.Enums.FollowType.Artist);
                    case FollowType.User:
                        return await SpotifyClient.AuthLookupFollowingStateAsync(
                        ids, NetStandard.Enums.FollowType.User);
                    case FollowType.Playlist:
                        if (playlistId == null) throw new ArgumentNullException(nameof(playlistId));
                        return await SpotifyClient.AuthLookupUserFollowingPlaylistAsync(ids,
                            playlistId);
                }
                return null;
            }
            catch (AuthUserTokenRequiredException)
            {
                throw new TokenRequiredException(TokenType.User);
            }
        }

        /// <summary>
        /// Is Following
        /// </summary>
        /// <param name="itemIds">(Required) Artist or the User Spotify IDs to check</param>
        /// <param name="followType">(Required) Either Artist or User</param>
        /// <param name="playlistId">(Required for FollowType.Playlist) The Spotify ID of the playlist</param>
        /// <returns>True if Is, False if Not</returns>
        public async Task<bool> IsFollowing(
            string id,
            FollowType type, 
            string playlistId = null) => 
            (await IsFollowing(new List<string> { id }, type, playlistId)).FirstOrDefault();
        #endregion Follow Methods
    }
}