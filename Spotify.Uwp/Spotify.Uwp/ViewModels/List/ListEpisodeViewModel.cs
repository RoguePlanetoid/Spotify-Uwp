using Spotify.NetStandard.Sdk;
using System.Collections.Generic;

namespace Spotify.Uwp
{
    /// <summary>
    /// List Episode View Model
    /// </summary>
    public class ListEpisodeViewModel : BaseListViewModel<EpisodesRequest, EpisodeResponse>
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">(Required) Spotify Sdk Client</param>
        /// <param name="episodesRequest">(Required) Episodes Request</param>
        public ListEpisodeViewModel(
            ISpotifySdkClient client,
            EpisodesRequest episodesRequest)
            : base(client, episodesRequest) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">(Required) Spotify Sdk Client</param>
        /// <param name="episodeType">(Required) Episode Type</param>
        /// <param name="value">(Required) Only for EpisodeType.Search - Episode Search Term or EpisodeType.Show - Show Id</param>
        /// <param name="multipleEpisodeIds">(Required) Only for EpisodeType.Multiple - Multiple Spotify Episode Ids</param>
        /// <param name="searchIsExternal">(Optional) Only for EpisodeType.Search, If true the response will include any relevant audio content that is hosted externally</param>
        public ListEpisodeViewModel(
            ISpotifySdkClient client,
            EpisodeType episodeType,
            string value = null,
            List<string> multipleEpisodeIds = null,
            bool? searchIsExternal = null)
            : base(client, new EpisodesRequest()
            {
                EpisodeType = episodeType,
                Value = value,
                MultipleEpisodeIds = multipleEpisodeIds,
                SearchIsExternal = searchIsExternal
            }) { }
        #endregion Constructor
    }
}
