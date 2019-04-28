using System.Collections.ObjectModel;

namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// List Audio Feature View Model
    /// </summary>
    public class ListAudioFeatureViewModel : ObservableCollection<AudioFeatureViewModel>
    {
        #region Private Members
        private ISpotifySdkClient _client = null;
        #endregion Private Members

        #region Private Methods
        /// <summary>
        /// Init
        /// </summary>
        private async void Init(
            string id)
        {
            var response = await _client.ListAudioFeatureAsync(id);
            response.ForEach(f => Add(f));
        }
        #endregion Private Methods

        #region Constructor
        /// <summary>Constructor</summary>
        /// <param name="client">Music Client</param>
        /// <param name="id">Id</param>
        public ListAudioFeatureViewModel(
            ISpotifySdkClient client,
            string id)
        {
            _client = client;
            Init(id);
        }
        #endregion Constructor

        #region Public Methods
        /// <summary>Dispose</summary>
        public void Dispose() =>
            _client = null;
        #endregion Public Methods
    }
}
