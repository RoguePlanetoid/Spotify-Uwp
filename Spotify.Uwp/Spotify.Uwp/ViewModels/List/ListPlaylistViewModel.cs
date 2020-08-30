using Spotify.NetStandard.Sdk;

namespace Spotify.Uwp
{
    /// <summary>
    /// List Playlist View Model
    /// </summary>
    public class ListPlaylistViewModel : BaseListViewModel<PlaylistsRequest, PlaylistResponse>
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">(Required) Spotify Sdk Client</param>
        /// <param name="playlistsRequest">(Required) Playlists Request</param>
        public ListPlaylistViewModel(
            ISpotifySdkClient client,
            PlaylistsRequest playlistsRequest)
            : base(client, playlistsRequest) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">(Required) Spotify Sdk Client</param>
        /// <param name="playlistType">(Required) Playlist Type</param>
        /// <param name="value">(Required) For PlaylistType.Search - Playlist Search Term, PlaylistType.CategoriesPlaylists - Category Id, and PlaylistType.User - User Id</param>
        /// <param name="searchIsExternal">(Optional) Only for PlaylistType.Search, If true the response will include any relevant audio content that is hosted externally</param>
        public ListPlaylistViewModel(
            ISpotifySdkClient client,
            PlaylistType playlistType,
            string value = null,
            bool? searchIsExternal = null)
            : base(client, new PlaylistsRequest()
            {
                PlaylistType = playlistType,
                Value = value,
                SearchIsExternal = searchIsExternal
            }) { }
        #endregion Constructor
    }
}
