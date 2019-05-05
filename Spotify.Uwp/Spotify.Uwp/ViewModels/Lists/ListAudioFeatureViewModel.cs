using System.Collections.ObjectModel;
using System.Threading.Tasks;

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

        #region Constructor
        /// <summary>Constructor</summary>
        /// <param name="client">Spotify SDK Client</param>
        public ListAudioFeatureViewModel(ISpotifySdkClient client) => 
            _client = client;
        #endregion Constructor

        #region Public Methods
        /// <summary>
        /// Set
        /// </summary>
        /// <param name="id">Value</param>
        /// <returns>ListAudioFeature ViewModel</returns>
        public async Task<ListAudioFeatureViewModel> Set(
            string id)
        {
            var response = await _client.ListAudioFeatureAsync(id);
            response.ForEach(f => Add(f));
            return this;
        }

        /// <summary>Dispose</summary>
        public void Dispose() =>
            _client = null;
        #endregion Public Methods
    }
}
