using Spotify.NetStandard.Sdk;
using System.Collections.Generic;

namespace Spotify.Uwp
{
    /// <summary>
    /// List Album View Model
    /// </summary>
    public class ListAlbumViewModel : BaseListViewModel<AlbumsRequest, AlbumResponse>
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">(Required) Spotify Sdk Client</param>
        /// <param name="albumsRequest">(Required) Album Request</param>
        public ListAlbumViewModel(
            ISpotifySdkClient client,
            AlbumsRequest albumsRequest) :
            base(client, albumsRequest) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">(Required) Spotify Sdk Client</param>
        /// <param name="albumType">(Required) Album Type</param>
        /// <param name="value">(Required) Only for AlbumType.Search - Album Search Term and AlbumType.Artist - Artist Id</param>
        /// <param name="multipleAlbumIds">(Required) Only for AlbumType.Multiple - Multiple Spotify Album Ids</param>
        /// <param name="searchIsExternal">(Optional) Only for AlbumType.Search, If true the response will include any relevant audio content that is hosted externally</param>
        /// <param name="includeGroup">(Optional) For AlbumType.Artist filters the response. If not supplied, all album types will be returned</param>
        public ListAlbumViewModel(
            ISpotifySdkClient client,
            AlbumType albumType,
            string value = null,
            List<string> multipleAlbumIds = null,
            bool? searchIsExternal = null,
            IncludeGroupRequest includeGroup = null)
            : base(client, new AlbumsRequest()
            {
                AlbumType = albumType,
                Value = value,
                MultipleAlbumIds = multipleAlbumIds,
                SearchIsExternal = searchIsExternal,
                IncludeGroup = includeGroup
            }) { }
        #endregion Constructor
    }
}
