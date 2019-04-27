using Spotify.NetStandard.Client.Authentication;
using Spotify.NetStandard.Responses;
using Spotify.Uwp.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Spotify.Uwp.Internal
{
    internal static class Mapping
    {
        #region Public Methods
        public static TokenViewModel MapToken(AccessToken source)
        {
            if (source == null) return null;
            var result = new TokenViewModel()
            {
                Expiration = source.Expiration,
                Refresh = source.Refresh,
                Scopes = source.Scopes,
                Token = source.Token,
                TokenType = (TokenType)source.TokenType
            };
            return result;
        }

        public static AccessToken MapToken(TokenViewModel source)
        {
            if (source == null) return null;
            var result = new AccessToken()
            {
                Expiration = source.Expiration,
                Refresh = source.Refresh,
                Scopes = source.Scopes,
                Token = source.Token,
                TokenType = (NetStandard.Client.Authentication.Enums.TokenType)source.TokenType
            };
            return result;
        }

        public static ImageViewModel MapImage(Image source)
        {
            if (source == null) return null;
            var result = new ImageViewModel()
            {
                Url = source.Url,
                Height = source.Height,
                Width = source.Width
            };
            return result;
        }

        public static CopyrightViewModel MapCopyright(Copyright source)
        {
            if (source == null) return null;
            var result = new CopyrightViewModel()
            {
                Text = source.Text,
                Type = source.Type
            };
            return result;
        }

        public static string MapExternalUrl(ExternalUrl source)
        {
            if (source == null) return null;
            return source?.Spotify;
        }

        public static ExternalIdViewModel MapExternalId(ExternalId source)
        {
            if (source == null) return null;
            var result = new ExternalIdViewModel()
            {
                Ean = source.Ean,
                Isrc = source.Isrc,
                Upc = source.Upc
            };
            return result;
        }

        public static PublicUserViewModel MapPublicUser(PublicUser source)
        {
            if (source == null) return null;
            var result = new PublicUserViewModel()
            {
                DisplayName = source.DisplayName,
                ExternalUrl = MapExternalUrl(source.ExternalUrls),
                Followers = source?.Followers?.Total ?? 0,
                Href = source.Href,
                Id = source.Id,
                Images = source?.Images?.ConvertAll(MapImage),
                Product = source.Product,
                Type = source.Type,
                Uri = source.Uri
            };
            return result;
        }

        public static LinkedTrackViewModel MapLinkedTrack(LinkedTrack source)
        {
            if (source == null) return null;
            var result = new LinkedTrackViewModel()
            {
                ExternalUrl = MapExternalUrl(source.ExternalUrls),
                Href = source.Href,
                Id = source.Id,
                Name = source.Name,
                Type = source.Type,
                Uri = source.Uri
            };
            return result;
        }

        public static CategoryViewModel MapCategory(Category source)
        {
            if (source == null) return null;
            var result = new CategoryViewModel()
            {
                ExternalUrl = MapExternalUrl(source.ExternalUrls),
                Href = source.Href,
                Id = source.Id,
                Images = source?.Images?.ConvertAll(MapImage),
                Name = source.Name,
                Type = source.Type,
                Uri = source.Uri
            };
            return result;
        }

        public static ArtistViewModel MapArtist(Artist source)
        {
            if (source == null) return null;
            var result = new ArtistViewModel()
            {
                ExternalUrl = MapExternalUrl(source.ExternalUrls),
                Followers = source.Followers,
                Genres = source.Genres,
                Href = source.Href,
                Id = source.Id,
                Images = source?.Images?.ConvertAll(MapImage),
                Name = source.Name,
                Popularity = source.Popularity,
                Type = source.Type,
                Uri = source.Uri
            };
            return result;
        }

        public static AlbumViewModel MapAlbum(Album source)
        {
            if (source == null) return null;
            var result = new AlbumViewModel()
            {
                AlbumGroup = source.AlbumGroup,
                AlbumType = source.AlbumType,
                Artists = MapArtistList(source?.Artists),
                AvailableMarkets = source.AvailableMarkets,
                Copyrights = source?.Copyrights?.ConvertAll(MapCopyright),
                ExternalId = MapExternalId(source.ExternalId),
                ExternalUrl = MapExternalUrl(source.ExternalUrls),
                Genres = source.Genres,
                Href = source.Href,
                Id = source.Id,
                Images = source?.Images?.ConvertAll(MapImage),
                Label = source.Label,
                Name = source.Name,
                Popularity = source.Popularity,
                ReleaseDate = source.ReleaseDate,
                ReleaseDatePrecision = source.ReleaseDatePrecision,
                TotalTracks = source.TotalTracks,
                Tracks = MapPagingTrack(source.Tracks),
                Type = source.Type,
                Uri = source.Uri,
            };
            return result;
        }

        public static AlbumViewModel MapAlbum(SimplifiedAlbum source)
        {
            if (source == null) return null;
            var result = new AlbumViewModel()
            {
                AlbumGroup = source.AlbumGroup,
                AlbumType = source.AlbumType,
                AvailableMarkets = source.AvailableMarkets,
                ExternalUrl = MapExternalUrl(source?.ExternalUrls),
                Href = source.Href,
                Id = source.Id,
                Images = source?.Images?.ConvertAll(MapImage),
                Name = source.Name,
                TotalTracks = source.TotalTracks,
                Type = source.Type,
                Uri = source.Uri,
            };
            return result;
        }

        public static PlaylistViewModel MapPlaylist(Playlist source)
        {
            if (source == null) return null;
            var result = new PlaylistViewModel()
            {
                Collaborative = source.Collaborative,
                Description = source.Description,
                ExternalUrl = MapExternalUrl(source.ExternalUrls),
                Followers = source?.Followers?.Total ?? 0,
                Href = source.Href,
                Id = source.Id,
                Images = source?.Images?.ConvertAll(MapImage),
                Name = source.Name,
                Owner = MapPublicUser(source.Owner),
                Public = source.Public,
                SnapshotId = source.SnapshotId,
                Tracks = MapPagingTrack(source.Tracks),
                Type = source.Type,
                Uri = source.Uri
            };
            return result;
        }

        public static NavigationViewModel<ArtistViewModel> MapPagingArtist(Paging<Artist> source)
        {
            if (source == null) return null;
            var result = new NavigationViewModel<ArtistViewModel>()
            {
                Limit = source.Limit,
                Next = source.Next,
                Offset = source.Offset,
                Back = source.Previous,
                Total = source.Total,
                Items = source.Items.ConvertAll(MapArtist),
            };
            return result;
        }

        public static NavigationViewModel<AlbumViewModel> MapPagingAlbum(Paging<Album> source)
        {
            if (source == null) return null;
            var result = new NavigationViewModel<AlbumViewModel>()
            {
                Limit = source.Limit,
                Next = source.Next,
                Offset = source.Offset,
                Back = source.Previous,
                Total = source.Total,
                Items = source.Items.ConvertAll(MapAlbum),
            };
            return result;
        }

        public static NavigationViewModel<PlaylistViewModel> MapPagingPlaylist(Paging<Playlist> source)
        {
            if (source == null) return null;
            var result = new NavigationViewModel<PlaylistViewModel>()
            {
                Limit = source.Limit,
                Next = source.Next,
                Offset = source.Offset,
                Back = source.Previous,
                Total = source.Total,
                Items = source.Items.ConvertAll(MapPlaylist),
            };
            return result;
        }

        public static TrackViewModel MapTrack(Track source)
        {
            if (source == null) return null;
            var result = new TrackViewModel()
            {
                Album = MapAlbum(source.Album),
                Artists = MapArtistList(source.Artists),
                AvailableMarkets = source.AvailableMarkets,
                DiscNumber = source.DiscNumber,
                Duration = source.Duration,
                ExternalId = MapExternalId(source.ExternalId),
                ExternalUrl = MapExternalUrl(source.ExternalUrls),
                Href = source.Href,
                Id = source.Id,
                IsExplicit = source.IsExplicit,
                IsPlayable = source.IsPlayable,
                LinkedFrom = MapLinkedTrack(source.LinkedFrom),
                Name = source.Name,
                Popularity = source.Popularity,
                Preview = source.Preview,
                Restrictions = source?.Restrictions?.Select(s => s.Reason)?.ToList(),
                TrackNumber = source.TrackNumber,
                Type = source.Type,
                Uri = source.Uri
            };
            return result;
        }

        public static TrackViewModel MapTrack(SimplifiedTrack source)
        {
            if (source == null) return null;
            var result = new TrackViewModel()
            {
                AvailableMarkets = source.AvailableMarkets,
                DiscNumber = source.DiscNumber,
                Duration = source.Duration,
                ExternalUrl = MapExternalUrl(source.ExternalUrls),
                Href = source.Href,
                Id = source.Id,
                IsExplicit = source.IsExplicit,
                IsPlayable = source.IsPlayable,
                LinkedFrom = MapLinkedTrack(source.LinkedFrom),
                Name = source.Name,
                Preview = source.Preview,
                TrackNumber = source.TrackNumber,
                Type = source.Type,
                Uri = source.Uri
            };
            return result;
        }

        public static TrackViewModel MapTrack(PlaylistTrack source)
        {
            if (source == null) return null;
            return MapTrack(source.Track);
        }

        public static RecommendationViewModel MapRecommendationGenre(string source)
        {
            if (source == null) return null;
            var result = new RecommendationViewModel()
            {
                Id = source,
                Name = source
            };
            return result;
        }

        public static NavigationViewModel<CategoryViewModel> MapPagingCategory(Paging<Category> source)
        {
            if (source == null) return null;
            var result = new NavigationViewModel<CategoryViewModel>()
            {
                Total = source.Total,
                Next = source.Next,
                Back = source.Previous,
                Limit = source.Limit,
                Offset = source.Offset,
                Items = source.Items.ConvertAll(MapCategory)
            };
            return result;
        }

        public static NavigationViewModel<TrackViewModel> MapPagingTrack(Paging<SimplifiedTrack> source)
        {
            if (source == null) return null;
            var result = new NavigationViewModel<TrackViewModel>()
            {
                Total = source.Total,
                Next = source.Next,
                Back = source.Previous,
                Limit = source.Limit,
                Offset = source.Offset,
                Items = source.Items.ConvertAll(MapTrack)
            };
            return result;
        }

        public static NavigationViewModel<TrackViewModel> MapPagingTrack(Paging<Track> source)
        {
            if (source == null) return null;
            var result = new NavigationViewModel<TrackViewModel>()
            {
                Total = source.Total,
                Next = source.Next,
                Back = source.Previous,
                Limit = source.Limit,
                Offset = source.Offset,
                Items = source.Items.ConvertAll(MapTrack)
            };
            return result;
        }

        public static NavigationViewModel<TrackViewModel> MapPagingTrack(Paging<PlaylistTrack> source)
        {
            if (source == null) return null;
            var result = new NavigationViewModel<TrackViewModel>()
            {
                Total = source.Total,
                Next = source.Next,
                Back = source.Previous,
                Limit = source.Limit,
                Offset = source.Offset,
                Items = source.Items.ConvertAll(MapTrack)
            };
            return result;
        }

        public static NavigationViewModel<AlbumViewModel> MapPagingTrack(Paging<Album> source)
        {
            if (source == null) return null;
            var result = new NavigationViewModel<AlbumViewModel>()
            {
                Total = source.Total,
                Next = source.Next,
                Back = source.Previous,
                Limit = source.Limit,
                Offset = source.Offset,
                Items = source.Items.ConvertAll(MapAlbum)
            };
            return result;
        }

        public static Paging<TModel> MapNavigation<TModel, TViewModel>(NavigationViewModel<TViewModel> source)
        {
            if (source == null) return null;
            var result = new Paging<TModel>()
            {
                Total = source.Total,
                Next = source.Next,
                Previous = source.Back,
                Limit = source.Limit,
                Offset = source.Offset
            };
            return result;
        }

        public static NavigationViewModel<TrackViewModel> MapTrackList(List<SimplifiedTrack> source)
        {
            if (source == null) return null;
            var result = new NavigationViewModel<TrackViewModel>()
            {
                Total = source.Count,
                Items = source.ConvertAll(MapTrack)
            };
            return result;
        }

        public static NavigationViewModel<TrackViewModel> MapTrackList(List<Track> source)
        {
            if (source == null) return null;
            var result = new NavigationViewModel<TrackViewModel>()
            {
                Total = source.Count,
                Items = source.ConvertAll(MapTrack)
            };
            return result;
        }

        public static NavigationViewModel<ArtistViewModel> MapArtistList(List<Artist> source)
        {
            if (source == null) return null;
            var result = new NavigationViewModel<ArtistViewModel>()
            {
                Total = source.Count,
                Items = source.ConvertAll(MapArtist)
            };
            return result;
        }

        public static NavigationViewModel<AlbumViewModel> MapAlbumList(List<Album> source)
        {
            if (source == null) return null;
            var result = new NavigationViewModel<AlbumViewModel>()
            {
                Total = source.Count,
                Items = source.ConvertAll(MapAlbum)
            };
            return result;
        }

        public static string MapKey(int? source)
        {
            if (source == null) return null;
            var result = string.Empty;
            switch (source.Value)
            {
                case 0:
                    result = "C";
                    break;
                case 1:
                    result = "C♯";
                    break;
                case 2:
                    result = "D";
                    break;
                case 3:
                    result = "D♯";
                    break;
                case 4:
                    result = "E";
                    break;
                case 5:
                    result = "F";
                    break;
                case 6:
                    result = "F♯";
                    break;
                case 7:
                    result = "G";
                    break;
                case 8:
                    result = "G♯";
                    break;
                case 9:
                    result = "A";
                    break;
                case 10:
                    result = "A♯";
                    break;
                case 11:
                    result = "B";
                    break;
                default:
                    break;
            }
            return result;
        }

        public static List<AudioFeatureViewModel> MapAudioFeatureList(AudioFeatures source)
        {
            AudioFeatureViewModel MapAudioFeature(string name, float? value) =>
            new AudioFeatureViewModel()
            {
                Name = name,
                Value = (int)((value ?? 0) * 100)
            };
            if (source == null) return null;
            var results = new List<AudioFeatureViewModel>()
            {
                MapAudioFeature("Acoustic", source.Acousticness),
                MapAudioFeature("Danceable", source.Danceability),
                MapAudioFeature("Energy", source.Energy),
                MapAudioFeature("Instrumental", source.Instrumentalness),
                MapAudioFeature("Live", source.Liveness),
                MapAudioFeature("Speech", source.Speechiness),
                MapAudioFeature("Positivity", source.Valence),
                MapAudioFeature($"Loudness:{source.Loudness}db", 1.0f),
                MapAudioFeature($"Key:{MapKey(source.Key)}", 1.0f),
                MapAudioFeature($"Mode:{((source.Mode ?? 0) == 0 ? "MINOR": "MAJOR")}", 1.0f),
                MapAudioFeature($"Tempo:{source.Tempo}", 1.0f),
                MapAudioFeature($"Meter:{source.TimeSignature}", 1.0f)
            };
            return results;
        }
        #endregion Public Methods
    }
}
