namespace Spotify.Uwp
{
    #region Public Enums
    public enum TokenType : byte
    {
        Access,
        User
    }

    public enum PlaylistType
    {
        Search,
        Featured,
        CategoriesPlaylists
    }

    public enum TrackType
    {
        Favourites,
        Search,
        Recommended,
        Playlist,
        Album,
        Artist,
        Saved
    }

    public enum AlbumType
    {
        Favourites,
        Search,
        NewReleases,
        Artist,
        Saved
    }

    public enum ArtistType
    {
        Favourites,
        Search,
        Related,
        Followed
    }

    public enum FavouriteType
    {
        Album,
        Artist,
        Track
    }

    public enum FollowType
    {
        Artist,
        User,
        Playlist
    }
    #endregion Public Enums
}
