using Spotify.NetStandard.Sdk;

namespace Spotify.Uwp
{
    /// <summary>
    /// List Playlist Item View Model
    /// </summary>
    public class ListPlaylistItemViewModel : BaseListViewModel<PlaylistItemsRequest, PlaylistItemResponse>
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">(Required) Spotify Sdk Client</param>
        /// <param name="playlistItemsRequest">(Required) Playlist Items Request</param>
        public ListPlaylistItemViewModel(
            ISpotifySdkClient client,
            PlaylistItemsRequest playlistItemsRequest)
            : base(client, playlistItemsRequest) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">(Required) Spotify Sdk Client</param>
        /// <param name="playlistId">(Required) Spotify Playlist Id</param>
        /// <param name="fields">(Optional) Filters for the query: a comma-separated list of the fields to return. If omitted, all fields are returned</param>
        public ListPlaylistItemViewModel(
            ISpotifySdkClient client,
            string playlistId = null,
            string fields = null)
            : base(client, new PlaylistItemsRequest()
                {
                    PlaylistId = playlistId,
                    Fields = fields
                }) { }
        #endregion Constructor
    }
}
