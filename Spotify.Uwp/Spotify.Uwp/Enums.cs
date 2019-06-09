namespace Spotify.Uwp
{
    #region Public Enums
    /// <summary>
    /// Token Type
    /// </summary>
    public enum TokenType
    {
        /// <summary>
        /// Access Token
        /// </summary>
        Access,
        /// <summary>
        /// User Token
        /// </summary>
        User
    }

    /// <summary>
    /// Login Type
    /// </summary>
    public enum LoginType
    {
        /// <summary>
        /// Authorisation Code Flow
        /// </summary>
        AuthorisationCode,
        /// <summary>
        /// Client Credentials Flow
        /// </summary>
        ClientCredentials,
        /// <summary>
        /// Implicit Grant Flow
        /// </summary>
        ImplicitGrant
    }

    /// <summary>
    /// Playlist Type
    /// </summary>
    public enum PlaylistType
    {
        /// <summary>
        /// Search for Playlist
        /// </summary>
        Search,
        /// <summary>
        /// Featured Playlists
        /// </summary>
        Featured,
        /// <summary>
        /// Category Playlists
        /// </summary>
        CategoriesPlaylists,
        /// <summary>
        /// User's Playlists
        /// </summary>
        User
    }

    /// <summary>
    /// Track Type
    /// </summary>
    public enum TrackType
    {
        /// <summary>
        /// Favourite Tracks
        /// </summary>
        Favourites,
        /// <summary>
        /// Search for Track
        /// </summary>
        Search,
        /// <summary>
        /// Recommended Track by Genre
        /// </summary>
        Recommended,
        /// <summary>
        /// Playlist Tracks
        /// </summary>
        Playlist,
        /// <summary>
        /// Album Tracks
        /// </summary>
        Album,
        /// <summary>
        /// Artist Top Tracks
        /// </summary>
        Artist,
        /// <summary>
        /// User's Recently Played Tracks
        /// </summary>
        UserRecentlyPlayed,
        /// <summary>
        /// User's Saved Tracks
        /// </summary>
        UserSaved,
        /// <summary>
        /// User's Top Tracks
        /// </summary>
        UserTop
    }

    /// <summary>
    /// Album Type
    /// </summary>
    public enum AlbumType
    {
        /// <summary>
        /// Favourite Albums
        /// </summary>
        Favourites,
        /// <summary>
        /// Search for Album
        /// </summary>
        Search,
        /// <summary>
        /// New Releases
        /// </summary>
        NewReleases,
        /// <summary>
        /// Artist Albums
        /// </summary>
        Artist,
        /// <summary>
        /// User's Saved Albums
        /// </summary>
        UserSaved
    }

    /// <summary>
    /// Artist Type
    /// </summary>
    public enum ArtistType
    {
        /// <summary>
        /// Favourite Artists
        /// </summary>
        Favourites,
        /// <summary>
        /// Search for Artist
        /// </summary>
        Search,
        /// <summary>
        /// Related Artists
        /// </summary>
        Related,
        /// <summary>
        /// User's Followed Artists
        /// </summary>
        UserFollowed,
        /// <summary>
        /// User's Top Artists
        /// </summary>
        UserTop
    }

    /// <summary>
    /// Favourite Type
    /// </summary>
    public enum FavouriteType
    {
        /// <summary>
        /// Favourite Albums
        /// </summary>
        Album,
        /// <summary>
        /// Favourite Artists
        /// </summary>
        Artist,
        /// <summary>
        /// Favourite Tracks
        /// </summary>
        Track
    }

    /// <summary>
    /// Follow Type
    /// </summary>
    public enum FollowType
    {
        /// <summary>
        /// Artist
        /// </summary>
        Artist,
        /// <summary>
        /// User
        /// </summary>
        User,
        /// <summary>
        /// Playlist
        /// </summary>
        Playlist
    }

    /// <summary>
    /// User Top Time Frame
    /// </summary>
    public enum UserTopTimeFrame
    {
        /// <summary>
        /// Calculated from several years of data and including all new data as it becomes available
        /// </summary>
        LongTerm,
        /// <summary>
        /// Approximately last 6 months
        /// </summary>
        MediumTerm,
        /// <summary>
        /// Approximately last 4 weeks
        /// </summary>
        ShortTerm
    }
    #endregion Public Enums
}
