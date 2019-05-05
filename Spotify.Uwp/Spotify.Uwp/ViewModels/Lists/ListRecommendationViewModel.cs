using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// List Recommendation View Model
    /// </summary>
    public class ListRecommendationViewModel : ObservableCollection<RecommendationViewModel>, 
        IDisposable
    {
        #region Private Members
        private ISpotifySdkClient _client = null;
        #endregion Private Members

        #region Constructor
        /// <summary>Constructor</summary>
        /// <param name="client">Spotify SDK Client</param>
        public ListRecommendationViewModel(ISpotifySdkClient client) => 
            _client = client;
        #endregion Constructor

        #region Public Methods
        /// <summary>
        /// Set
        /// </summary>
        /// <returns>ListRecommendation ViewModel</returns>
        public async Task<ListRecommendationViewModel> Set()
        {
            var response = await _client.ListRecommendationGenresAsync();
            response.ForEach(f => Add(f));
            return this;
        }

        /// <summary>Dispose</summary>
        public void Dispose() => 
            _client = null;
        #endregion Public Methods
    }
}
