using Spotify.NetStandard.Sdk;
using System.Collections.Generic;

namespace Spotify.Uwp
{
    /// <summary>
    /// List Artist View Model
    /// </summary>
    public class ListArtistViewModel : BaseListViewModel<ArtistsRequest, ArtistResponse>
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">(Required) Spotify Sdk Client</param>
        /// <param name="artistsRequest">(Required) Artist Request</param>
        public ListArtistViewModel(
            ISpotifySdkClient client,
            ArtistsRequest artistsRequest)
            : base(client, artistsRequest) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">(Required) Spotify Sdk Client</param>
        /// <param name="artistType">(Required) Artist Type</param>
        /// <param name="value">(Required) Only for ArtistType.Search - Artist Search Term and ArtistType.Related - Artist Id</param>
        /// <param name="multipleArtistIds">(Required) Only for ArtistType.Multiple - Multiple Artist Spotify Ids</param>
        /// <param name="searchIsExternal">(Optional) Only for ArtistType.Search, If true the response will include any relevant audio content that is hosted externally</param>
        /// <param name="userTopTimeRangeType">(Optional) Only for ArtistType.UserTop, Long Term: calculated from several years of data and including all new data as it becomes available, Medium Term: (Default) approximately last 6 months, Short Term: approximately last 4 weeks</param>
        public ListArtistViewModel(
            ISpotifySdkClient client,
            ArtistType artistType,
            string value = null,
            List<string> multipleArtistIds = null,
            bool? searchIsExternal = null,
            UserTopTimeRangeType? userTopTimeRangeType = null)
            : base(client, new ArtistsRequest()
            {
                ArtistType = artistType,
                Value = value,
                MultipleArtistIds = multipleArtistIds,
                SearchIsExternal = searchIsExternal,
                UserTopTimeRangeType = userTopTimeRangeType
            }) { }
        #endregion Constructor
    }
}
