using Spotify.NetStandard.Client.Authentication;
using Spotify.NetStandard.Requests;
using Spotify.NetStandard.Responses;
using Spotify.Uwp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spotify.Uwp.Internal
{
    /// <summary>
    /// Mapping
    /// </summary>
    internal static class Mapping
    {
        #region Public Methods
        public static TViewModel MapError<TResponse, TViewModel>(
            TResponse source, TViewModel target) 
            where TResponse : BaseResponse 
            where TViewModel : ErrorViewModel
        {
            if (target == null) target = (TViewModel)Activator.CreateInstance(typeof(TViewModel));
            if (source != null)
            {
                target.Message = source?.Error?.Message;
                target.StatusCode = source?.Error?.StatusCode ?? 0;
            }
            return target;
        }

        public static TViewModel MapError<TResponse, TViewModel>(
        Paging<TResponse> source, TViewModel target)
        where TResponse : BaseResponse
        where TViewModel : ErrorViewModel
        {
            if (target == null) target = (TViewModel)Activator.CreateInstance(typeof(TViewModel));
            if (source != null)
            {
                target.Message = source?.Error?.Message;
                target.StatusCode = source?.Error?.StatusCode ?? 0;
            }
            return target;
        }

        public static TViewModel MapError<TResponse, TViewModel>(
        CursorPaging<TResponse> source, TViewModel target)
        where TResponse : BaseResponse
        where TViewModel : ErrorViewModel
        {
            if (target == null) target = (TViewModel)Activator.CreateInstance(typeof(TViewModel));
            if (source != null)
            {
                target.Message = source?.Error?.Message;
                target.StatusCode = source?.Error?.StatusCode ?? 0;
            }
            return target;
        }

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

        public static ScopeViewModel MapScope(Scope source)
        {
            if (source == null) return null;
            var result = new ScopeViewModel()
            {
                ConnectModifyPlaybackState = source.ConnectModifyPlaybackState,
                ConnectReadCurrentlyPlaying = source.ConnectReadCurrentlyPlaying,
                ConnectReadPlaybackState = source.ConnectReadPlaybackState,
                FollowModify = source.FollowModify,
                FollowRead = source.FollowRead,
                LibraryModify = source.LibraryModify,
                LibraryRead = source.LibraryRead,
                ListeningRecentlyPlayed = source.ListeningRecentlyPlayed,
                ListeningTopRead = source.ListeningTopRead,
                PlaybackAppRemoteControl = source.PlaybackAppRemoteControl,
                PlaybackStreaming = source.PlaybackStreaming,
                PlaylistModifyPrivate = source.PlaylistModifyPrivate,
                PlaylistModifyPublic = source.PlaylistModifyPublic,
                PlaylistReadCollaborative = source.PlaylistReadCollaborative,
                PlaylistReadPrivate = source.PlaylistReadPrivate,
                UserGeneratedContentImageUpload = source.UserGeneratedContentImageUpload,
                UserReadBirthDate = source.UserReadBirthDate,
                UserReadPrivate = source.UserReadPrivate,
                UserReadEmail = source.UserReadEmail
            };
            return result;
        }

        public static Scope MapScope(ScopeViewModel source)
        {
            if (source == null) return null;
            var result = new Scope()
            {
                ConnectModifyPlaybackState = source.ConnectModifyPlaybackState,
                ConnectReadCurrentlyPlaying = source.ConnectReadCurrentlyPlaying,
                ConnectReadPlaybackState = source.ConnectReadPlaybackState,
                FollowModify = source.FollowModify,
                FollowRead = source.FollowRead,
                LibraryModify = source.LibraryModify,
                LibraryRead = source.LibraryRead,
                ListeningRecentlyPlayed = source.ListeningRecentlyPlayed,
                ListeningTopRead = source.ListeningTopRead,
                PlaybackAppRemoteControl = source.PlaybackAppRemoteControl,
                PlaybackStreaming = source.PlaybackStreaming,
                PlaylistModifyPrivate = source.PlaylistModifyPrivate,
                PlaylistModifyPublic = source.PlaylistModifyPublic,
                PlaylistReadCollaborative = source.PlaylistReadCollaborative,
                PlaylistReadPrivate = source.PlaylistReadPrivate,
                UserGeneratedContentImageUpload = source.UserGeneratedContentImageUpload,
                UserReadBirthDate = source.UserReadBirthDate,
                UserReadPrivate = source.UserReadPrivate,
                UserReadEmail = source.UserReadEmail
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

        public static ArtistViewModel MapArtist(SimplifiedArtist source)
        {
            if (source == null) return null;
            var result = new ArtistViewModel()
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

        public static NavigationViewModel<ArtistViewModel> MapArtistList(List<SimplifiedArtist> source)
        {
            if (source == null) return null;
            var result = new NavigationViewModel<ArtistViewModel>()
            {
                Total = source?.Count ?? 0,
                Items = source?.ConvertAll(MapArtist)
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

        public static AlbumViewModel MapAlbum(SavedAlbum source)
        {
            if (source == null) return null;
            return MapAlbum(source.Album);
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
                Tracks = MapPagingTrack(source.Tracks, TrackType.Playlist),
                Type = source.Type,
                Uri = source.Uri
            };
            return result;
        }

        public static NavigationViewModel<ArtistViewModel> MapPagingArtist(
            Paging<Artist> source, ArtistType type)
        {
            if (source == null) return null;
            var result = new NavigationViewModel<ArtistViewModel>()
            {
                Type = type,
                Limit = source.Limit,
                Next = source.Next,
                Offset = source.Offset,
                Back = source.Previous,
                Total = source.Total,
                Items = source.Items.ConvertAll(MapArtist)
            };
            return result;
        }

        public static NavigationViewModel<ArtistViewModel> MapCursorArtist(
            CursorPaging<Artist> source, ArtistType type)
        {
            if (source == null) return null;
            var result = new NavigationViewModel<ArtistViewModel>()
            {
                Type = type,
                Limit = source.Limit ?? 0,
                Next = source.Next,
                Back = source.Before,
                Total = source.Total,
                Items = source.Items.ConvertAll(MapArtist)
            };
            return result;
        }

        public static NavigationViewModel<AlbumViewModel> MapPagingAlbum(
            Paging<Album> source, AlbumType type)
        {
            if (source == null) return null;
            var result = new NavigationViewModel<AlbumViewModel>()
            {
                Type = type,
                Limit = source.Limit,
                Next = source.Next,
                Offset = source.Offset,
                Back = source.Previous,
                Total = source.Total,
                Items = source.Items.ConvertAll(MapAlbum)
            };
            return result;
        }

        public static NavigationViewModel<AlbumViewModel> MapCursorAlbum(
            CursorPaging<SavedAlbum> source, AlbumType type)
        {
            if (source == null) return null;
            var result = new NavigationViewModel<AlbumViewModel>()
            {
                Type = type,
                Limit = source.Limit ?? 0,
                Next = source.Next,
                Back = source.Before,
                Total = source.Total,
                Items = source.Items.ConvertAll(MapAlbum)
            };
            return result;
        }

        public static NavigationViewModel<PlaylistViewModel> MapPagingPlaylist(
            Paging<Playlist> source, PlaylistType type)
        {
            if (source == null) return null;
            var result = new NavigationViewModel<PlaylistViewModel>()
            {
                Type = type,
                Limit = source.Limit,
                Next = source.Next,
                Offset = source.Offset,
                Back = source.Previous,
                Total = source.Total,
                Items = source.Items.ConvertAll(MapPlaylist),
                StatusCode = source?.Error?.StatusCode ?? 0,
                Message = source?.Error?.Message
            };
            return result;
        }

        public static NavigationViewModel<PlaylistViewModel> MapCursorPlaylist(
        CursorPaging<Playlist> source, PlaylistType type)
        {
            if (source == null) return null;
            var result = new NavigationViewModel<PlaylistViewModel>()
            {
                Type = type,
                Limit = source.Limit ?? 0,
                Next = source.Next,
                Back = source.Before,
                Total = source.Total,
                Items = source.Items.ConvertAll(MapPlaylist)
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
                Artists = MapArtistList(source?.Artists),        
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

        public static TrackViewModel MapTrack(SavedTrack source)
        {
            if (source == null) return null;
            return MapTrack(source.Track);
        }

        public static TrackViewModel MapTrack(PlayHistory source)
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

        public static TimeIntervalViewModel MapTimeInterval(TimeInterval source)
        {
            if (source == null) return null;
            var result = new TimeIntervalViewModel()
            {
                Confidence = source.Confidence,
                Duration = source.Duration,
                Start = source.Start
            };
            return result;
        }

        public static SegmentViewModel MapSegment(Segment source)
        {
            if (source == null) return null;
            var result = new SegmentViewModel()
            {
                Confidence = source.Confidence,
                Duration = source.Duration,
                LoudnessEnd = source.Duration,
                LoudnessMax = source.LoudnessMax,
                LoudnessMaxTime = source.LoudnessMaxTime,
                LoudnessStart = source.LoudnessStart,
                Pitches = source.Pitches,
                Start = source.Start,
                Timbre = source.Timbre
            };
            return result;
        }

        public static SectionViewModel MapSection(Section source)
        {
            if (source == null) return null;
            var result = new SectionViewModel()
            {
                Confidence = source.Confidence,
                Duration = source.Duration,
                Key = source.Key,
                KeyConfidence = source.KeyConfidence,
                Loudness = source.Loudness,
                Mode = source.Mode,
                ModeConfidence = source.ModeConfidence,
                Start = source.Start,
                Tempo = source.Tempo,
                TempoConfidence = source.TempoConfidence,
                TimeSignature = source.TimeSignature,
                TimeSignatureConfidence = source.TimeSignatureConfidence
            };
            return result;
        }

        public static AudioAnalysisViewModel MapAudioAnalysis(AudioAnalysis source)
        {
            if (source == null) return null;
            var result = new AudioAnalysisViewModel()
            {
                Bars = source?.Bars?.ConvertAll(MapTimeInterval),
                Beats = source?.Beats?.ConvertAll(MapTimeInterval),
                Sections = source?.Sections?.ConvertAll(MapSection),
                Segments = source?.Segments?.ConvertAll(MapSegment),
                Tatums = source?.Tatums?.ConvertAll(MapTimeInterval)
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

        public static NavigationViewModel<TrackViewModel> MapPagingTrack(Paging<Track> source, TrackType type)
        {
            if (source == null) return null;
            var result = new NavigationViewModel<TrackViewModel>()
            {
                Type = type,
                Total = source.Total,
                Next = source.Next,
                Back = source.Previous,
                Limit = source.Limit,
                Offset = source.Offset,
                Items = source.Items.ConvertAll(MapTrack)
            };
            return result;
        }

        public static NavigationViewModel<TrackViewModel> MapCursorTrack(
            CursorPaging<SavedTrack> source, TrackType type)
        {
            if (source == null) return null;
            var result = new NavigationViewModel<TrackViewModel>()
            {
                Type = type,
                Limit = source.Limit ?? 0,
                Next = source.Next,
                Back = source.Before,
                Total = source.Total,
                Items = source.Items.ConvertAll(MapTrack)
            };
            return result;
        }

        public static NavigationViewModel<TrackViewModel> MapCursorTrack(
            CursorPaging<PlayHistory> source, TrackType type)
        {
            if (source == null) return null;
            var result = new NavigationViewModel<TrackViewModel>()
            {
                Type = type,
                Limit = source.Limit ?? 0,
                Next = source.Next,
                Back = source.Before,
                Total = source.Total,
                Items = source.Items.ConvertAll(MapTrack)
            };
            return result;
        }

        public static NavigationViewModel<TrackViewModel> MapPagingTrack(Paging<PlaylistTrack> source, TrackType type)
        {
            if (source == null) return null;
            var result = new NavigationViewModel<TrackViewModel>()
            {
                Type = type,
                Total = source.Total,
                Next = source.Next,
                Back = source.Previous,
                Limit = source.Limit,
                Offset = source.Offset,
                Items = source.Items.ConvertAll(MapTrack),
                StatusCode = source?.Error?.StatusCode ?? 0,
                Message = source?.Error?.Message
            };
            return result;
        }

        public static Paging<TModel> MapNavigationPaging<TModel, TViewModel>(NavigationViewModel<TViewModel> source)
        {
            if (source == null) return null;
            var result = new Paging<TModel>()
            {
                Total = source.Total,
                Next = source.Next,
                Previous = source.Back,
                Limit = source.Limit,
                Offset = source.Offset,
                Error = new ErrorResponse()
                {
                    Message = source.Message,
                    StatusCode = source.StatusCode
                }
            };
            return result;
        }

        public static CursorPaging<TModel> MapNavigationCursor<TModel, TViewModel>(NavigationViewModel<TViewModel> source)
        {
            if (source == null) return null;
            var result = new CursorPaging<TModel>()
            {
                Total = source.Total,
                Next = source.Next,
                Before = source.Back,
                Limit = source.Limit,          
                Error = new ErrorResponse()
                {
                    Message = source.Message,
                    StatusCode = source.StatusCode
                }
            };
            return result;
        }

        public static NavigationViewModel<TrackViewModel> MapTrackList(List<SimplifiedTrack> source, TrackType type)
        {
            if (source == null) return null;
            var result = new NavigationViewModel<TrackViewModel>()
            {
                Type = type,
                Total = source?.Count ?? 0,
                Items = source?.ConvertAll(MapTrack)
            };
            return result;
        }

        public static NavigationViewModel<TrackViewModel> MapTrackList(List<Track> source, TrackType type)
        {
            if (source == null) return null;
            var result = new NavigationViewModel<TrackViewModel>()
            {
                Type = type,
                Total = source?.Count ?? 0,
                Items = source?.ConvertAll(MapTrack)
            };
            return result;
        }

        public static NavigationViewModel<ArtistViewModel> MapArtistList(List<Artist> source)
        {
            if (source == null) return null;
            var result = new NavigationViewModel<ArtistViewModel>()
            {
                Total = source?.Count ?? 0,
                Items = source?.ConvertAll(MapArtist)
            };
            return result;
        }

        public static NavigationViewModel<AlbumViewModel> MapAlbumList(List<Album> source, AlbumType type)
        {
            if (source == null) return null;
            var result = new NavigationViewModel<AlbumViewModel>()
            {
                Type = type,
                Total = source?.Count ?? 0,
                Items = source?.ConvertAll(MapAlbum)
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

        public static List<AudioFeatureViewModel> MapAudioFeature(AudioFeatures source)
        {
            if (source == null) return null;
            AudioFeatureViewModel MapAudioFeature(string name, float? value) =>
            new AudioFeatureViewModel()
            {
                Id = source.Id,
                Href = source.TrackHref,
                ExternalUrl = source.AnalysisUrl,
                Uri = source.Uri,
                Type = source.Type,                
                Name = name,
                Value = (int)((value ?? 0) * 100),
                Message = source?.Error?.Message,
                StatusCode = source?.Error?.StatusCode ?? 0
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

        public static List<List<AudioFeatureViewModel>> MapAudioFeature(List<AudioFeatures> source)
        {
            if (source == null) return null;
            return source.ConvertAll(MapAudioFeature);
        }
        #endregion Public Methods
    }
}
