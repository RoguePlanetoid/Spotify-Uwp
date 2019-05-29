using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// List Track ViewModel
    /// </summary>
    public class ListTrackViewModel : BaseListViewModel<TrackViewModel>, IDisposable
    {
        #region Private Members
        private string _id;
        private TrackType _type;
        private ISpotifySdkClient _client;
        private NavigationViewModel<TrackViewModel> _results = null;
        #endregion Private Members

        #region Constructor
        /// <summary>Constructor</summary>
        /// <param name="client">Music Client</param>
        public ListTrackViewModel(
            ISpotifySdkClient client,
            TrackType type,
            string id = null)
        {
            _client = client;
            _type = type;
            _id = id;
        }
        #endregion Constructor

        #region Public Methods
        /// <summary>
        /// Load Data
        /// </summary>
        /// <returns>IEnumerable of Track View Model</returns>
        protected override async Task<IEnumerable<TrackViewModel>> LoadItemsAsync()
        {
            _results = _results == null ?
            await _client.ListTracksAsync(_type, _id) :
            await _client.ListTracksAsync(_results);
            return _results?.Items;
        }

        /// <summary>Dispose</summary>
        public void Dispose() =>
            _client = null;
        #endregion Public Methods
    }
}