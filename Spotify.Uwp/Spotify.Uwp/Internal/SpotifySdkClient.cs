using Spotify.NetStandard.Client;
using Spotify.NetStandard.Client.Exceptions;
using Spotify.NetStandard.Client.Interfaces;
using Spotify.NetStandard.Enums;
using Spotify.NetStandard.Requests;
using Spotify.NetStandard.Responses;
using Spotify.Uwp.Exceptions;
using Spotify.Uwp.ViewModels;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Spotify.Uwp.Internal
{
    /// <summary>
    /// Spotify SDK Client
    /// </summary>
    internal class SpotifySdkClient : ISpotifySdkClient
    {
        #region Private Methods
        /// <summary>
        /// Init
        /// </summary>
        private void Init(CultureInfo culture)
        {
            if (culture != null)
            {
                var region = new RegionInfo(culture.LCID);
                if (Country == null)
                    Country = region.TwoLetterISORegionName;
                if (Locale == null)
                    Locale = $"{culture.TwoLetterISOLanguageName.ToLower()}_{region.TwoLetterISORegionName.ToUpper()}";
            }
        }
        #endregion Private Methods

        #region Constructor
        /// <summary>
        /// Spotify SDK
        /// </summary>
        /// <param name="clientId">Spotify Client Id</param>
        /// <param name="clientSecret">Spotify Client Secret</param>
        /// <param name="cultureInfo">Culture Info</param>
        public SpotifySdkClient(
            string clientId,
            string clientSecret,
            CultureInfo cultureInfo = null)
        {
            Favourites = new ListFavouriteViewModel();
            Init(cultureInfo);
            SpotifyClient = SpotifyClientFactory.CreateSpotifyClient(
                clientId, clientSecret);
        }
        #endregion Constructor

        #region Public Properties
        /// <summary>
        /// Spotify Client
        /// </summary>
        public ISpotifyClient SpotifyClient { get; }

        /// <summary>
        /// Locale Code
        /// </summary>
        public string Locale { get; set; } = null;

        /// <summary>
        /// Country Code
        /// </summary>
        public string Country { get; set; } = null;

        /// <summary>
        /// Limit Value
        /// </summary>
        public int? Limit { get; set; } = null;

        public TokenViewModel Token
        {
            get => Mapping.MapToken(SpotifyClient.GetToken());
            set => SpotifyClient.SetToken(Mapping.MapToken(value));
        }

        public ListFavouriteViewModel Favourites { get; set; }
        #endregion Public Properties

        #region Get Methods
        /// <summary>
        /// Get Category
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <param name="page">Page</param>
        /// <returns>CategoryViewModel</returns>
        public async Task<CategoryViewModel> GetCategory(
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
                Token = Mapping.MapToken(SpotifyClient.GetToken());
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
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ArtistViewModel> GetArtist(
            string id)
        {
            ArtistViewModel result = null;
            try
            {
                var response = await SpotifyClient.LookupAsync<Artist>(
                id, LookupType.Artists);
                result = Mapping.MapArtist(response);
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
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AlbumViewModel> GetAlbum(
            string id)
        {
            AlbumViewModel result = null;
            try
            {
                var response = await SpotifyClient.LookupAsync<Album>(
                id, LookupType.Albums);
                result = Mapping.MapAlbum(response);
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
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<PlaylistViewModel> GetPlaylist(
            string id)
        {
            PlaylistViewModel result = null;
            try
            {
                var response = await SpotifyClient.LookupAsync<Playlist>(
                id, LookupType.Playlist);
                result = Mapping.MapPlaylist(response);
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
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TrackViewModel> GetTrack(
            string id)
        {
            TrackViewModel result = null;
            try
            {
                var response = await SpotifyClient.LookupAsync<Track>(
                id, LookupType.Tracks);
                result = Mapping.MapTrack(response);
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
        /// <param name="limit">Limit</param>
        /// <returns>Page</returns>
        public async Task<NavigationViewModel<CategoryViewModel>>
            ListCategories()
        {
            NavigationViewModel<CategoryViewModel> results = null;
            try
            {
                var page = (Limit != null) ? new Page() { Limit = Limit.Value } : null;
                var response = await SpotifyClient.LookupAllCategoriesAsync(
                    country: Country, locale: Locale, page: page);
                results = Mapping.MapPagingCategory(response?.Categories);
            }
            catch (AuthAccessTokenRequiredException)
            {
                throw new TokenRequiredException(TokenType.Access);
            }
            return results;
        }

        /// <summary>
        /// List Categories
        /// </summary>
        /// <param name="page">Page</param>
        /// <param name="country"></param>
        /// <returns>Page</returns>
        public async Task<NavigationViewModel<CategoryViewModel>>
            ListCategories(NavigationViewModel<CategoryViewModel> navigation)
        {
            NavigationViewModel<CategoryViewModel> result = null;
            try
            {
                var paging = Mapping.MapNavigation<Category, CategoryViewModel>(navigation);
                var response = await SpotifyClient.NavigateAsync(
                    paging, NavigateType.Next);
                result = Mapping.MapPagingCategory(response?.Categories);
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
        /// <param name="page">Page</param>
        /// <returns>Page</returns>
        public async Task<NavigationViewModel<ArtistViewModel>>
            ListArtists(
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
                        break;
                    case ArtistType.Search:
                        var search = new SearchType() { Artist = true };
                        var response = await SpotifyClient.SearchAsync(
                        query: id, searchType: search,
                        country: Country, page: page);
                        result = Mapping.MapPagingArtist(response?.Artists);
                        break;
                    case ArtistType.Related:
                        var related = await SpotifyClient.LookupArtistRelatedArtistsAsync(
                            itemId: id);
                        result = Mapping.MapArtistList(related?.Artists);
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
        /// <param name="navigation">Navigation</param>
        /// <returns>Page</returns>
        public async Task<NavigationViewModel<ArtistViewModel>> ListArtists(
            NavigationViewModel<ArtistViewModel> navigation)
        {
            NavigationViewModel<ArtistViewModel> result = null;
            try
            {
                var paging = Mapping.MapNavigation<Artist, ArtistViewModel>(navigation);
                var response = await SpotifyClient.NavigateAsync(
                    paging, NavigateType.Next);
                result = Mapping.MapPagingArtist(response?.Artists);
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
        /// <param name="page">Page</param>
        /// <returns>Page</returns>
        public async Task<NavigationViewModel<AlbumViewModel>>
            ListAlbums(
            AlbumType type,
            string id = null)
        {
            NavigationViewModel<AlbumViewModel> result = null;
            try
            {
                var page = (Limit != null) ? new Page() { Limit = Limit.Value } : null;
                switch (type)
                {
                    case AlbumType.Favourites:
                        var favourite = await SpotifyClient.LookupAsync(
                            Favourites?.AlbumIds, LookupType.Albums);
                        result = Mapping.MapAlbumList(favourite?.Albums);
                        break;
                    case AlbumType.Search:
                        var search = new SearchType() { Album = true };
                        var response = await SpotifyClient.SearchAsync(
                        query: id, searchType: search,
                        country: Country, page: page);
                        result = Mapping.MapPagingAlbum(response?.Albums);
                        break;
                    case AlbumType.NewReleases:
                        var releases = await SpotifyClient.LookupNewReleasesAsync(
                        country: Country, page: page);
                        result = Mapping.MapPagingAlbum(releases?.Albums);
                        break;
                    case AlbumType.Artist:
                        var albums = await SpotifyClient.LookupAsync<Paging<Album>>(
                        id, lookupType: LookupType.ArtistAlbums,
                        market: Country, page: page);
                        result = Mapping.MapPagingAlbum(albums);
                        break;
                }
                Token = Mapping.MapToken(SpotifyClient.GetToken());
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
        /// <param name="navigation">Navigation</param>
        /// <returns>Page</returns>
        public async Task<NavigationViewModel<AlbumViewModel>> ListAlbums(
            NavigationViewModel<AlbumViewModel> navigation)
        {
            NavigationViewModel<AlbumViewModel> result = null;
            try
            {
                var paging = Mapping.MapNavigation<Album, AlbumViewModel>(navigation);
                var response = await SpotifyClient.NavigateAsync(
                    paging, NavigateType.Next);
                result = Mapping.MapPagingAlbum(response?.Albums);
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
        /// <param name="page">Page</param>
        /// <returns>Page</returns>
        public async Task<NavigationViewModel<PlaylistViewModel>>
            ListPlaylists(
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
        /// <param name="paging">Paging</param>
        /// <returns>Page</returns>
        public async Task<NavigationViewModel<PlaylistViewModel>> ListPlaylists(
            NavigationViewModel<PlaylistViewModel> navigation)
        {
            NavigationViewModel<PlaylistViewModel> result = null;
            try
            {
                var paging = Mapping.MapNavigation<Playlist, PlaylistViewModel>(navigation);
                var response = await SpotifyClient.NavigateAsync(
                    paging, NavigateType.Next);
                result = Mapping.MapPagingPlaylist(response?.Playlists);
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
        /// <returns></returns>
        public async Task<List<RecommendationViewModel>> ListRecommendationGenres()
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
        /// <param name="page">Page</param>
        /// <returns>Page</returns>
        public async Task<NavigationViewModel<TrackViewModel>>
            ListTracks(
            TrackType type,
            string id = null)
        {
            NavigationViewModel<TrackViewModel> result = null;
            try
            {
                var page = (Limit != null) ? new Page() { Limit = Limit.Value } : null;
                switch (type)
                {
                    case TrackType.Favourites:
                        var favourite = await SpotifyClient.LookupAsync(
                            Favourites?.TrackIds, LookupType.Tracks);
                        result = Mapping.MapTrackList(favourite?.Tracks);
                        break;
                    case TrackType.Search:
                        var searchType = new SearchType() { Track = true };
                        var searchTracks = await SpotifyClient.SearchAsync(
                            query: id, searchType: searchType, Country, page: page);
                        result = Mapping.MapPagingTrack(searchTracks?.Tracks);
                        break;
                    case TrackType.Recommended:
                        var recommendedTracks = await SpotifyClient.LookupRecommendationsAsync(
                            seedGenres: new List<string> { id },
                            market: Country);
                        result = Mapping.MapTrackList(recommendedTracks?.Tracks);
                        break;
                    case TrackType.Playlist:
                        var playlistTracks = await SpotifyClient.LookupAsync<Paging<PlaylistTrack>>(
                            itemId: id, lookupType: LookupType.PlaylistTracks,
                            market: Country, page: page);
                        result = Mapping.MapPagingTrack(playlistTracks);
                        break;
                    case TrackType.Album:
                        var albumTracks = await SpotifyClient.LookupAsync<Paging<Track>>(
                        itemId: id, lookupType: LookupType.AlbumTracks,
                        market: Country, page: page);
                        result = Mapping.MapPagingTrack(albumTracks);
                        break;
                    case TrackType.Artist:
                        var artistTracks = await SpotifyClient.LookupArtistTopTracksAsync(
                            itemId: id, market: Country);
                        result = Mapping.MapTrackList(artistTracks?.Tracks);
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
        /// List Tracks
        /// </summary>
        /// <param name="paging">Paging</param>
        /// <returns>Page</returns>
        public async Task<NavigationViewModel<TrackViewModel>> ListTracks(
            NavigationViewModel<TrackViewModel> navigation)
        {
            NavigationViewModel<TrackViewModel> result = null;
            try
            {
                var paging = Mapping.MapNavigation<Track, TrackViewModel>(navigation);
                var response = await SpotifyClient.NavigateAsync(
                    paging, NavigateType.Next);
                result = Mapping.MapPagingTrack(response?.Tracks);
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
        /// <param name="id">Id</param>
        /// <returns></returns>
        public async Task<List<AudioFeatureViewModel>> ListAudioFeatures(
            string id)
        {
            List<AudioFeatureViewModel> results = null;
            try
            {
                var response = await SpotifyClient.LookupAsync<AudioFeatures>(
                    id, LookupType.AudioFeatures);
                results = Mapping.MapAudioFeatureList(response);
            }
            catch (AuthAccessTokenRequiredException)
            {
                throw new TokenRequiredException(TokenType.Access);
            }
            return results;
        }
        #endregion List Methods   
    }
}