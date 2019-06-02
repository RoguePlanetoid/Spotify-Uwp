using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// List Recommendation View Model
    /// </summary>
    public class ListRecommendationViewModel : BaseListViewModel<RecommendationViewModel>
    {
        #region Constructor
        /// <summary>Constructor</summary>
        /// <param name="client">Music Client</param>
        /// <param name="id">Id</param>
        public ListRecommendationViewModel(
            ISpotifySdkClient client)
            : base(client) { }
        #endregion Constructor

        #region Public Methods
        /// <summary>
        /// Load Data
        /// </summary>
        /// <returns>IEnumerable of Album View Model</returns>
        protected override async Task<IEnumerable<RecommendationViewModel>> LoadItemsAsync() =>
            await Client.ListRecommendationGenresAsync();
        #endregion Public Methods
    }
}
