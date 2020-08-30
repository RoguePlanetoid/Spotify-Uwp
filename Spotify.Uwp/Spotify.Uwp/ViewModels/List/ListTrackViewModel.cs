using Spotify.NetStandard.Sdk;
using System.Collections.Generic;

namespace Spotify.Uwp
{
    /// <summary>
    /// List Track View Model
    /// </summary>
    public class ListTrackViewModel : BaseListViewModel<TracksRequest, TrackResponse>
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">(Required) Spotify Sdk Client</param>
        /// <param name="tracksRequest">(Required) Tracks Request</param>
        public ListTrackViewModel(
            ISpotifySdkClient client,
            TracksRequest tracksRequest)
            : base(client, tracksRequest) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">(Required) Spotify Sdk Client</param>
        /// <param name="trackType">(Required) Track Type</param>
        /// <param name="value">(Required) Only for TrackType.Search - Track Search Term, TrackType.Album - Spotify Album Id and TrackType.Artist - Spotify Artist Id</param>
        /// <param name="multipleTrackIds">(Required) Only for TrackType.Multiple - Multiple Spotify Track Ids</param>
        /// <param name="searchIsExternal">(Optional) Only for TrackType.Search, If true the response will include any relevant audio content that is hosted externally</param>
        /// <param name="recommendation">(Optional) Only for TrackType.Recommended - Recommendation Request</param>
        public ListTrackViewModel(
            ISpotifySdkClient client,
            TrackType trackType,
            string value = null,
            List<string> multipleTrackIds = null,
            bool? searchIsExternal = null,
            RecommendationRequest recommendation = null)
            : base(client, new TracksRequest()
                {
                    TrackType = trackType,
                    Value = value,
                    MultipleTrackIds = multipleTrackIds,
                    SearchIsExternal = searchIsExternal,
                    Recommendation = recommendation
                }) { }
        #endregion Constructor
    }
}
