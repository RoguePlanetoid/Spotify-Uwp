using System;
using System.Collections.ObjectModel;

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

        #region Private Methods
        /// <summary>
        /// Init
        /// </summary>
        private async void Init()
        {
            var response = await _client.ListRecommendationGenres();
            response.ForEach(f => Add(f));
        }
        #endregion Private Methods

        #region Constructor
        /// <summary>Constructor</summary>
        /// <param name="client">Music Client</param>
        public ListRecommendationViewModel(
            ISpotifySdkClient client)
        {
            _client = client;
            Init();
        }
        #endregion Constructor

        #region Public Methods
        /// <summary>Dispose</summary>
        public void Dispose() => 
            _client = null;
        #endregion Public Methods
    }
}
