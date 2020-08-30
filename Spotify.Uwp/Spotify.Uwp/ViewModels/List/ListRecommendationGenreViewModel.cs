using Spotify.NetStandard.Sdk;

namespace Spotify.Uwp
{
    /// <summary>
    /// List Recommenation Genre View Model
    /// </summary>
    public class ListRecommendationGenreViewModel : BaseListViewModel<RecommendationGenreResponse, RecommendationGenreResponse>
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">(Required) Spotify Sdk Client</param>
        public ListRecommendationGenreViewModel(
            ISpotifySdkClient client)
            : base(client, null) { }
        #endregion Constructor
    }
}
