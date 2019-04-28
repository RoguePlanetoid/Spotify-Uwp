namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// Playlist View Model
    /// </summary>
    public class PlaylistViewModel : AssetViewModel
    {
        /// <summary>
        /// true if the owner allows other users to modify the playlist.
        /// </summary>
        public bool Collaborative { get; set; }

        /// <summary>
        /// The playlist description. Only returned for modified, verified playlists, otherwise null.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Information about the followers of the playlist.
        /// </summary>
        public int Followers { get; set; }

        /// <summary>
        /// The user who owns the playlist
        /// </summary>
        public PublicUserViewModel Owner { get; set; }

        /// <summary>
        /// The playlist’s public/private status: true the playlist is public, false the playlist is private, null the playlist status is not relevant
        /// </summary>
        public bool? Public { get; set; }

        /// <summary>
        /// The version identifier for the current playlist.
        /// </summary>
        public string SnapshotId { get; set; }

        /// <summary>
        /// Information about the tracks of the playlist.
        /// </summary>
        public NavigationViewModel<TrackViewModel> Tracks { get; set; }
    }
}
